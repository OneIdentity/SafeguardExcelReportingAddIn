using System;
using System.Data;
using System.Threading.Tasks;
using OneIdentity.SafeguardDotNet;

namespace Safeguard.Common.Reports
{
    internal class AssetManagementReport : ISafeguardReportImpl
    {
        public string Name => "";
        public string Description => "";
        public Task<DataTable> Execute(ISafeguardConnection connection)
        {
            throw new NotImplementedException();
            
        }
    }
}
