using test_task.Models;
using System.Collections.Generic;
using System.Threading.Tasks;


namespace test_task.Interfaces
{
    public interface IHelpdeskRequestService
    {
        public interface IHelpdeskRequestService
        {
            Task<HelpdeskRequest> AddRequestAsync(HelpdeskRequest request);
            Task<IEnumerable<HelpdeskRequest>> GetActiveRequestsAsync();
            Task MarkRequestAsResolvedAsync(int requestId);
        }
    }
}
