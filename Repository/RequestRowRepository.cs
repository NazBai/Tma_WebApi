using TMA_Warehouse.Data;
using TMA_Warehouse.Interfaces;
using TMA_Warehouse.Models;

namespace TMA_Warehouse.Repository
{
    public class RequestRowRepository : IRequestRowRepository
    {
        private readonly DataContext dataContext;
        
        public RequestRowRepository(DataContext dataContext)
        {
            this.dataContext = dataContext;
        }
        public ICollection<RequestRow> GetRequestRows()
        {
            return dataContext.RequestRow.OrderBy(p => p.Request_Row_ID).ToList();
        }

        public ICollection<RequestRow> GetRequestRowsByRequest(int requestId)
        {
            return dataContext.RequestRow.Where(r => r.Request_ID == requestId).ToList();
        }

        public RequestRow GetRequestRow(int requestRowId)
        {
            return dataContext.RequestRow.Where(r => r.Request_Row_ID == requestRowId).FirstOrDefault();
        }

        public bool CreateRequestRow(RequestRow requestRow)
        {
            dataContext.Add(requestRow);
            return Save();
        }

        public bool UpdateRequestRow(RequestRow requestRow)
        {
            dataContext.Update(requestRow);
            return Save();
        }

        public bool DeleteRequestRow(RequestRow requestRow)
        {
            dataContext.Remove(requestRow);
            return Save();
        }

        public bool DeleteRequestRows(List<RequestRow> requestRows)
        {
            dataContext.RemoveRange(requestRows);
            return Save();
        }

        public bool IsExist(int id)
        {
            return dataContext.RequestRow.Any(i => i.Request_Row_ID == id);
        }

        public bool Save()
        {
            var saved = dataContext.SaveChanges();
            return saved > 0 ? true : false;
        }
    }
}
