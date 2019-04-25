using System.Collections.Generic;
using System.Threading.Tasks;

namespace Safeguard.Common
{
    public interface IExcel
    {

    }

    public interface ISafeguardReport
    {
        string Name { get; }
        string Description { get; }

        Task Execute(IExcel excel);
    }

    public interface ISafeguard
    {
        Task Authenticate();
        Task Logout();

        Task<IEnumerable<ISafeguardReport>> GetReports();
    }
}