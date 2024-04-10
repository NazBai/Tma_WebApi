using TMA_Warehouse.Data;
using TMA_Warehouse.Interfaces;
using TMA_Warehouse.Models;

namespace TMA_Warehouse.Repository
{
    public class RequestRepository : IRequestRepository
    {
        private readonly DataContext dataContext;
        public RequestRepository(DataContext dataContext)
        {
            this.dataContext = dataContext;
        }
        public ICollection<Request> GetRequests()
        {
            return dataContext.Request.OrderBy(p => p.Request_ID).ToList();
        }

        public Request GetRequest(int requestId)
        {
            return dataContext.Request.Where(i => i.Request_ID == requestId).FirstOrDefault();
        }

        public bool CreateRequest(Request request)
        {
            dataContext.Add(request);
            return Save();
        }

        public bool UpdateRequest(Request request)
        {
            dataContext.Update(request);
            return Save();
        }

        public bool DeleteRequest(Request request)
        {
            dataContext.RemoveRange(dataContext.RequestRow.Where(r => r.Request_ID == request.Request_ID).ToList());
            dataContext.Remove(request);
            return Save();
        }

        public bool Save()
        {
            var saved = dataContext.SaveChanges();
            return saved > 0 ? true : false;
        }

        public bool IsExist(int id)
        {
            return dataContext.Request.Any(i => i.Request_ID == id);
        }
    }
}
