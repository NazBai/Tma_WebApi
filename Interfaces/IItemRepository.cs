using TMA_Warehouse.Models;

namespace TMA_Warehouse.Interfaces
{
    public interface IItemRepository
    {
        ICollection<Item> GetItems();
        Item GetItem(int id);
        int GetQantity(int id);
        bool IsExist(int id);
        bool CreateItem(Item item);
        bool UpdateItem(Item item);
        bool DeleteItem(Item item);

        bool Save();

    }
}
