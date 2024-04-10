using System.ComponentModel.DataAnnotations;

namespace TMA_Warehouse.Models
{
    public class Item
    {
        public int Item_ID { get; set; }
        public string Name { get; set; }
        public string Item_Group { get; set; }
        public string Unit_of_measurement { get; set; }
        public int Quantity { get; set; }
        public decimal Price_without_VAT { get; set; }
        public string Status { get; set; }
        public string Storage_location { get; set; }
        public string Contact_person { get; set; }
    }
}
