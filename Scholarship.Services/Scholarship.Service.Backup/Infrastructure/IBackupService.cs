using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Scholarship.Service.Backup.Infrastructure
{
    public interface IBackupService
    {
        public Task<byte[]> GetDbFromBytes();
        public Task LoadDbFromBytes(byte[] data);
    }
}
