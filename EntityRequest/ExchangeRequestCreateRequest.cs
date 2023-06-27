namespace SecondhandStore.EntityRequest
{
    public class ExchangeRequestCreateRequest
    {
        public string SellerId { get; set; }
        public DateTime OrderDate { get; set; }
        public int PostId { get; set; }
        public string SellerPhoneNumber { get; set; }
        public string SellerEmail { get; set; }
    }
}
