namespace E_Medicine_Application.Models
{
    public class Medicines
    {
        public int ID { get; set; }
        public string name { get; set; }
        public string Manufacturer { get; set; }
        public decimal UnitPrice { get; set; }
        public decimal Discount { get; set; }
        public int Quantity { get; set; }
        public DateTime ExpDate { get; set; }
        public string ImgUrl { get; set; }
        public int Status { get; set; }


    }
}
