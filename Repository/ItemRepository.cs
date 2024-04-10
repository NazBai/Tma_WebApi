using TMA_Warehouse.Data;
using TMA_Warehouse.Interfaces;
using TMA_Warehouse.Models;

namespace TMA_Warehouse.Repository
{
    public class ItemRepository : IItemRepository
    {
        private readonly DataContext dataContext;
        public ItemRepository(DataContext dataContext) 
        {
            this.dataContext = dataContext;
        }

        public Item GetItem(int id)
        {
            return dataContext.Item.Where(i => i.Item_ID == id).FirstOrDefault();
        }

        public ICollection<Item> GetItems()
        {
            return dataContext.Item.OrderBy(p => p.Item_ID).ToList();
        }

        public int GetQantity(int id)
        {
            return dataContext.Item.Where(i => i.Item_ID == id).FirstOrDefault().Quantity;     }

        public bool IsExist(int id)
        {
            return dataContext.Item.Any(i => i.Item_ID == id);
        }

        public bool CreateItem(Item item)
        {
            dataContext.Add(item);
            return Save();
        }

        public bool DeleteItem(Item item)
        {
            dataContext.Remove(item);
            return Save();
        }

        public bool Save()
        {
            var saved = dataContext.SaveChanges();
            return saved > 0 ? true : false;
        }

        public bool UpdateItem(Item item)
        {
            dataContext.Update(item);
            return Save();
        }
    }
}
