namespace SecondhandStore.EntityRequest
{
    public class AccountUpdateRequest
    {
        public string Password { get; set; }
        public string Fullname { get; set; }
        public string Email { get; set; }
        public string Address { get; set; }
        public string PhoneNo { get; set; }
        public bool IsActive { get; set; }
    }
}
