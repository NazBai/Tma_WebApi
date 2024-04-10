using TMA_Warehouse.Models;

namespace TMA_Warehouse.Interfaces
{
    public interface IRequestRowRepository
    {
        ICollection<RequestRow> GetRequestRows();
        ICollection<RequestRow> GetRequestRowsByRequest(int requestId);
        RequestRow GetRequestRow(int requestRowId);
        bool CreateRequestRow(RequestRow requestRow);
        bool UpdateRequestRow(RequestRow requestRow);
        bool DeleteRequestRow(RequestRow requestRow);
        bool DeleteRequestRows(List<RequestRow> requestRows);
        bool IsExist(int id);
        bool Save();
    }
}
