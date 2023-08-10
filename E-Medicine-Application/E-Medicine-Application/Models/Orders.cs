using System.ComponentModel;

namespace E_Medicine_Application.Models
{
    public class Orders
    {
        public int ID { get; set; }
        public int UserId { get; set; }
        public string OrderNO { get; set; }
        public decimal OrderTotal { get; set; }
        public string OrderStatus { get; set; }


    }
}
