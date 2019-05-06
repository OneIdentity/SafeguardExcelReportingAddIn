using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using OneIdentity.SafeguardDotNet;
using OneIdentity.SafeguardDotNet.GuiLogin;

namespace Safeguard.Common
{
    public class SafeguardHandle : ISafeguardHandle
    {
        private ISafeguardConnection _safeguardConnection;
        public Task Authenticate(string appliance)
        {
            if (_safeguardConnection != null)
            {
                _safeguardConnection.LogOut();
                _safeguardConnection.Dispose();
            }
            _safeguardConnection = LoginWindow.Connect(appliance);
            return Task.CompletedTask;
        }

        public Task Logout()
        {
            return Task.Run(() =>
            {
                if (_safeguardConnection != null)
                {
                    _safeguardConnection.LogOut();
                    _safeguardConnection.Dispose();
                    _safeguardConnection = null;
                }
            });
        }

        public Task<IEnumerable<ISafeguardReport>> GetReports()
        {
            var reports = new List<ISafeguardReport>
            {
                new SafeguardReportWrapper(null)
                {
                    Name = "Yesterday's Authorizations",
                    Description = "All Access Requests that were authorized yesterday."
                },
                new SafeguardReportWrapper(null)
                {
                    Name = "User Entititlements",
                    Description = "All entitlements for each safeguard user"
                },
                new SafeguardReportWrapper(null)
                {
                    Name = "Group Memberships",
                    Description = "All groups and their membership lists"
                }
            };
            return Task.FromResult<IEnumerable<ISafeguardReport>>(reports);
        }
    }
}
