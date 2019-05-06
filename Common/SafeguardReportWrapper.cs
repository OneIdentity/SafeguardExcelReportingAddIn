using System;
using System.Data;
using System.Threading.Tasks;
using OneIdentity.SafeguardDotNet;

namespace Safeguard.Common
{
    interface ISafeguardReportImpl
    {
        string Name { get; }
        string Description { get; }
        Task <DataTable> Execute(ISafeguardConnection connection);
    }

    internal class SafeguardReportWrapper : ISafeguardReport
    {
        private readonly ISafeguardReportImpl _impl;

        public SafeguardReportWrapper(ISafeguardReportImpl impl)
        {
            _impl = impl;
        }

        public string Name { get; set; } //=> _impl.Name;
    
        public string Description { get; set; } // => _impl.Description;

        public Task Execute(IExcel excel)
        {
            throw new NotImplementedException();
            // TODO: execute impl and format data table into new worksheet using IExcel
        }
    }
}
