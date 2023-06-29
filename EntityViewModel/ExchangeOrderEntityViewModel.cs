namespace SecondhandStore.EntityViewModel
{
    public class ExchangeOrderEntityViewModel
    {
        public int OrderId { get; set; }
        public int? PostId { get; set; }
        public int SellerId { get; set; }
        public int BuyerId { get; set; }
        public DateTime OrderDate { get; set; }
        public bool OrderStatus { get; set; }

    }
}
