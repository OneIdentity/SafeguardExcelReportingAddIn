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


    class SafeguardTestImpl : ISafeguard
    {
        public async Task Authenticate()
        {
            await Authenticate("10.5.32.97");
        }

        public async Task Authenticate(string safeguardHost)
        {
            await Task.Delay(TimeSpan.FromSeconds(2));
        }

        public async Task Logout()
        {
            await Task.Delay(TimeSpan.FromSeconds(1));
        }

        public Task<IEnumerable<ISafeguardReport>> GetReports()
        {
            var reports = new List<ISafeguardReport>
            {
                new SafeguardReport
                {
                    Name = "Yesterday's Authorizations",
                    Description = "All Access Requests that were authorized yesterday."
                },
                new SafeguardReport
                {
                    Name = "User Entititlements",
                    Description = "All entitlements for each safeguard user"
                },
                new SafeguardReport
                {
                    Name = "Group Memberships",
                    Description = "All groups and their membership lists"
                }
            };
            return Task.FromResult<IEnumerable<ISafeguardReport>>(reports);
        }
    }

    class ExcelTestImpl : IExcel
    {

    }
}
