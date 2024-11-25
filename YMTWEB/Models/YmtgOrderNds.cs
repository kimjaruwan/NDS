using System.ComponentModel.DataAnnotations.Schema;
namespace QuoNDS.Models

{
    [Table("YMTG_Order_NDS")]
    public class YmtgOrderNds
    {
        //YMTG_Order_NDS
        public int Id { get; set; }
        public string QuotationNumber { get; set; }
        public string CustomerName { get; set; }
        public DateTime OrderDate { get; set; }
        public string OrderStatus { get; set; }
        public DateTime ShipDate { get; set; }
        public int TotalQty { get; set; }
        public decimal TotalPrice { get; set; }
        public string CustomerEmail { get; set; }
        public string CustomerAddress { get; set; }
        public string CustomerAddressTax { get; set; }
        public string CustomerPhone { get; set; }
        public string Remark { get; set; }
        public string CreateBy { get; set; }
        public DateTime CreateDate { get; set; }
    }
}
