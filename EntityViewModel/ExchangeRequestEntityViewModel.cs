namespace SecondhandStore.EntityViewModel
{
    public class ExchangeRequestEntityViewModel
    {
        public int OrderId { get; set; }
        public int? PostId { get; set; }
        public string ProductName { get; set; }
        public double Price { get; set; }
        public int SellerId { get; set; }
        public string SellerName { get; set; }
        public string SellerPhoneNumber { get; set; }
        public string SellerEmail { get; set; }
        public string SellerAddress { get; set; }
        public DateTime OrderDate { get; set; }
        public string OrderStatusName { get; set; }
        
    }
}
