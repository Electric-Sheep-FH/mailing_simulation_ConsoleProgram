using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace mail_TP
{
    public class Mail
    {
        public User Sender { get; set; }
        public User Receveir { get; set; }
        public DateTime Date { get; set; }
        public string Subject { get; set; }
        public string Message { get; set; }
        public bool IsRead { get; set; }
        public int Number;

        public Mail(User sender, User receveir, DateTime date, string subject, string message, int number)
        {
            Sender = sender;
            Receveir = receveir;
            Date = date;
            Subject = subject;
            Message = message;
            IsRead = false;
            Number = number;
            

            DataBase.Mails.Add(this);
        }

    }
}
