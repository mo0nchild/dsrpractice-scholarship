using MassTransit;
using Scholarship.Shared.Commons.TransitModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Scholarship.Service.Backup.Infrastructure
{
    public class BackupService : IBackupService
    {
        private readonly IRequestClient<GetAllLoansRequest> getLoansClient = default!;
        public BackupService(IRequestClient<GetAllLoansRequest> getLoansClient) : base()
        {
            this.getLoansClient = getLoansClient;
        }
        public Task<byte[]> GetDbFromBytes()
        {
            throw new NotImplementedException();
        }
        public Task LoadDbFromBytes(byte[] data)
        {
            throw new NotImplementedException();
        }
    }
}
