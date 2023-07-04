namespace SecondhandStore.EntityViewModel
{
    public class ExchangeOrderEntityViewModel
    {
        public int OrderId { get; set; }
        public int? PostId { get; set; }
        public string ProductName { get; set; }
        public double Price { get; set; }
        public int BuyerId { get; set; }
        public string BuyerName { get; set; }
        public string BuyerPhoneNumber { get; set; }
        public string BuyerEmail { get; set; }
        public DateTime OrderDate { get; set; }
        public string OrderStatusName { get; set; }

    }
}
