namespace TMA_Warehouse.Models
{
    public class Request
    {

        public int Request_ID { get; set; }
        public string Employee_name { get; set; }
        public string Comment { get; set; }
        public string Status { get; set; }
        public ICollection<RequestRow> RequestRows { get; set; }

    }
    
    
}
