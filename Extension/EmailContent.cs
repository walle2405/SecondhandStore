namespace SecondhandStore.Extension
{
    public class EmailContent
    {
        public string Dear { get; set; }
        public string BodyContent { get; set; }
        public string Dashline = "--------------------------------------------";
        public string Contact = "FPT OSE SERVICE\nHotline:0886647866\nEmail:fptoseservice@gmail.com";
        public string ToString() { 
            return Dear +"\n" + BodyContent + "\n" + Dashline+ "\n"+ Contact;
        }
    }
}
