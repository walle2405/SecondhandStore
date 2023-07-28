using System.Net.Mail;

namespace SecondhandStore.Extension
{
    public class EmailContent
    {
        public string Dear { get; set; }
        public string BodyContent { get; set; }
        public string Dashline = "-----------------------------------------------";
        public string Contact = "FPT OSE SERVICE\nHotline:0886647866\nEmail:fptoseservice@gmail.com\nAddress:Block E2a-7, D1 Street Saigon Hi-tech Park, Long Thanh My Ward, District 9, Ho Chi Minh City, Vietnam";
        public string ToString() { 
            return Dear +"\n" + BodyContent + "\n" + Dashline+ "\n"+ Contact;
        }
    }
}
