using AutoMapper;
using Scholarship.Shared.Messages.HistoryMessages;
using Scholarship.Shared.Messages.LoansMessages;
using Scholarship.Shared.Messages.UsersMessages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace Scholarship.Service.Backup.Models
{
    [XmlRoot("SystemDbBackup")]
    [XmlInclude(typeof(UsersBackupModel)), XmlInclude(typeof(LoansBackupModel))]
    [XmlInclude(typeof(HistoryBackupModel))]
    public class BackupXmlModel : object
    {
        [XmlArray("UsersList")]
        [XmlArrayItem("UserItem")]
        public List<UsersBackupModel> Users { get; set; } = new();

        [XmlArray("LoansList")]
        [XmlArrayItem("LoanItem")]
        public List<LoansBackupModel> Loans { get; set; } = new();

        [XmlArray("HistoryList")]
        [XmlArrayItem("HistoryItem")]
        public List<HistoryBackupModel> History { get; set; } = new();
    }
    public class UsersBackupModel : object
    {
        public Guid Uuid { get; set; } = Guid.Empty;
        public string Email { get; set; } = string.Empty;
        public string Name { get; set; } = string.Empty;

        public string Password { get; set; } = string.Empty;
        public string RoleName { get; set; } = string.Empty;
    }
    public class UsersBackupModelProfile : Profile
    {
        public UsersBackupModelProfile() : base()
        {
            this.CreateMap<GetAllUsersResponse.UsersItemMessage, UsersBackupModel>();
            this.CreateMap<UsersBackupModel, RewriteUsersRequest.UsersItemMessage>();
        }
    }
    public class LoansBackupModel : object
    {
        public Guid Uuid { get; set; } = Guid.Empty;
        public Guid ClientUuid { get; set; } = Guid.Empty;
        public double MoneyAmount { get; set; } = default!;

        [XmlIgnore]
        public DateOnly OpenTime { get; set; } = default!;

        [XmlElement(ElementName = "OpenTime")]
        public string OpenTimeSurrogate
        {
            get { return OpenTime.ToString("yyyy-MM-dd"); }
            set { OpenTime = DateOnly.Parse(value); }
        }
        [XmlIgnore]
        public DateOnly BeforeTime { get; set; } = default!;

        [XmlElement(ElementName = "BeforeTime")]
        public string BeforeTimeSurrogate
        {
            get { return BeforeTime.ToString("yyyy-MM-dd"); }
            set { BeforeTime = DateOnly.Parse(value); }
        }
        public string CreditorSurname { get; set; } = string.Empty;
        public string CreditorName { get; set; } = string.Empty;
        public string CreditorPatronymic { get; set; } = string.Empty;
    }
    public class LoansBackupModelProfile : Profile
    {
        public LoansBackupModelProfile() : base()
        {
            this.CreateMap<GetAllLoansResponse.LoansMessageItem, LoansBackupModel>();
            this.CreateMap<LoansBackupModel, RewriteLoansRequest.LoansItemMessage>();
        }
    }
    public class HistoryBackupModel : object
    {
        public Guid Uuid { get; set; } = Guid.Empty;
        public Guid ClientUuid { get; set; } = Guid.Empty;
        public double MoneyAmount { get; set; } = default!;

        [XmlIgnore]
        public DateOnly OpenTime { get; set; } = default!;

        [XmlElement(ElementName = "OpenTime")]
        public string OpenTimeSurrogate
        {
            get { return OpenTime.ToString("yyyy-MM-dd"); }
            set { OpenTime = DateOnly.Parse(value); }
        }
        [XmlIgnore]
        public DateOnly BeforeTime { get; set; } = default!;

        [XmlElement(ElementName = "BeforeTime")]
        public string BeforeTimeSurrogate
        {
            get { return BeforeTime.ToString("yyyy-MM-dd"); }
            set { BeforeTime = DateOnly.Parse(value); }
        }
        [XmlIgnore]
        public DateOnly ClosedTime { get; set; } = default!;

        [XmlElement(ElementName = "ClosedTime")]
        public string ClosedTimeSurrogate
        {
            get { return ClosedTime.ToString("yyyy-MM-dd"); }
            set { ClosedTime = DateOnly.Parse(value); }
        }
        public string CreditorSurname { get; set; } = string.Empty;
        public string CreditorName { get; set; } = string.Empty;
        public string CreditorPatronymic { get; set; } = string.Empty;
    }
    public class HistoryBackupModelProfile : Profile
    {
        public HistoryBackupModelProfile() : base()
        {
            this.CreateMap<GetAllHistoryResponse.LoansMessageItem, HistoryBackupModel>();
            this.CreateMap<HistoryBackupModel, RewriteHistoryRequest.LoansItemMessage>();
        }
    }
}
