using AutoMapper;
using MassTransit;
using MassTransit.Transports;
using Microsoft.AspNetCore.Mvc.Formatters;
using Scholarship.Service.Backup.Models;
using Scholarship.Shared.Commons.Exceptions;
using Scholarship.Shared.Messages.HistoryMessages;
using Scholarship.Shared.Messages.LoansMessages;
using Scholarship.Shared.Messages.UsersMessages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace Scholarship.Service.Backup.Infrastructure
{
    internal class BackupService : IBackupService
    {
        private readonly IPublishEndpoint publishEndpoint = default!;
        private readonly IScopedClientFactory clientFactory = default!;
        private readonly IMapper mapper = default!;
        protected XmlSerializer XmlSerializer { get; set; } = new XmlSerializer(typeof(BackupXmlModel));
        public BackupService(IScopedClientFactory clientFactory, IPublishEndpoint publishEndpoint,
            IMapper mapper) : base()
        {
            this.publishEndpoint = publishEndpoint;
            this.mapper = mapper;
            this.clientFactory = clientFactory;
        }
        public async Task<byte[]> GetDbFromBytes()
        {
            var usersClient = this.clientFactory.CreateRequestClient<GetAllUsersRequest>();
            var loansClient = this.clientFactory.CreateRequestClient<GetAllLoansRequest>();
            var historyClient = this.clientFactory.CreateRequestClient<GetAllHistoryRequest>();

            var usersList = await usersClient.GetResponse<GetAllUsersResponse>(new GetAllUsersRequest());
            var loansList = await loansClient.GetResponse<GetAllLoansResponse>(new GetAllLoansRequest());
            var historyList = await historyClient.GetResponse<GetAllHistoryResponse>(new GetAllHistoryRequest());
            using (var memoryStream = new MemoryStream())
            {
                this.XmlSerializer.Serialize(memoryStream, new BackupXmlModel()
                {
                    Loans = this.mapper.Map<List<LoansBackupModel>>(loansList.Message.Loans),
                    Users = this.mapper.Map<List<UsersBackupModel>>(usersList.Message.Users),
                    History = this.mapper.Map<List<HistoryBackupModel>>(historyList.Message.Loans)
                });
                return memoryStream.ToArray();
            }
        }
        public async Task LoadDbFromBytes(byte[] data)
        {
            using (var memoryStream = new MemoryStream(data))
            {
                var backupData = this.XmlSerializer.Deserialize(memoryStream) as BackupXmlModel;
                if (backupData == null) throw new ProcessException("Unable to process data");

                await this.publishEndpoint.Publish(new RewriteUsersRequest()
                {
                    Users = this.mapper.Map<List<RewriteUsersRequest.UsersItemMessage>>(backupData.Users)
                });
                await this.publishEndpoint.Publish(new RewriteLoansRequest()
                {
                    Loans = this.mapper.Map<List<RewriteLoansRequest.LoansItemMessage>>(backupData.Loans)
                });
                await this.publishEndpoint.Publish(new RewriteHistoryRequest()
                {
                    ClosedLoans = this.mapper.Map<List<RewriteHistoryRequest.LoansItemMessage>>(backupData.History)
                });
            }
        }
    }
}
