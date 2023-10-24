using YourPizzaShop.Pages.Checkout;

namespace YourPizzaShop.Models


{
    public class MailData
    {
        //Receiver
        public List<string> To { get; } 
        //public List<string> Bcc { get; }
        //public List<string> Cc { get; }

        // Sender
        public string? From { get; set; }
        public string? DisplayName { get; set; } =  "the PizzaHouse";
        //public string? ReplyTo { get; }
        //public string? ReplyToName { get; }

        //Content
        public string Subject { get; }
        public string? Body { get; }

        public MailData(List<string> to, string subject, string? body=null, string? from = null, string? displayName=null)
        {
            //Reciever
            To = to;
            //Bcc = bcc ?? new List<string>();  
            //Cc = cc ?? new List<string>();

            //Sender
            From = from;
            DisplayName = displayName;
           
            //Content
            Subject = subject;
            Body = body;
        }


    }
}
