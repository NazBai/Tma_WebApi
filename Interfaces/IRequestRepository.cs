using TMA_Warehouse.Models;

namespace TMA_Warehouse.Interfaces
{
    public interface IRequestRepository
    {
        ICollection<Request> GetRequests();
        Request GetRequest(int requestId);
        bool CreateRequest(Request request);
        bool UpdateRequest(Request request);
        bool DeleteRequest(Request request);
        bool Save();
        bool IsExist(int id);
    }
}
