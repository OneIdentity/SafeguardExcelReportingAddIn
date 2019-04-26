using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Safeguard.Common;

namespace Safeguard.Test.Ui
{
    class SafeguardReport : ISafeguardReport
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public async Task Execute(IExcel excel)
        {
            await Task.Delay(TimeSpan.FromSeconds(5));
        }
    }
    

    class ExcelTestImpl : IExcel
    {

    }
}
