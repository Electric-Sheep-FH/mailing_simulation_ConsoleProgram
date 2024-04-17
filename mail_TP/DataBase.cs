using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Quic;
using System.Reflection;
using System.Runtime.Intrinsics.X86;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace mail_TP
{
    public class DataBase
    {
        public static List<User> Users = new List<User>();
        public static List<Mail> Mails = new List<Mail>();
        public static bool AskCredentials(string email, string password)
        {
            foreach (User user in Users)
            {
                if (user.Email == email && user.Password == password)
                {
                    return true;
                }
            }
            return false;
        }
        public static string HomePage(string email)
        {
            Console.Clear();
            string choice = "";
            foreach(User user in Users)
            {
                if (user.Email == email)
                {
                    Console.WriteLine($"Welcome to your mailing services {user.Nickname} ! What would you do ?");
                    Console.WriteLine();
                    Console.WriteLine("1 - Check inbox mail");
                    Console.WriteLine("2 - Send mail");
                    Console.WriteLine("3 - Unlog");
                    Console.WriteLine("\n---------------------------------------------\n");
                    Console.Write("Please write your choice to continue (1, 2, or 3) : ");
                    choice = Console.ReadLine();
                }
            }
            return choice;
        }
        public static int DisplayMailList(string email)
        {
            int countMails = 0;
            foreach (Mail mail in Mails)
            {
                if (mail.Receveir.Email == email)
                {
                    countMails++;
                    Console.Write($"{mail.Number} - Subject : {mail.Subject} - Send date : {mail.Date} - ");
                    if (mail.IsRead)
                    {
                        Console.Write("READ\n");
                    }
                    else
                    {
                        Console.Write("NOT-READ\n");

                    }
                }
            }
            return countMails;
        }
        public static int MailListDeleteOrRead(string email)
        {
            Console.Clear();

            string chooseMailNo = "";
            bool delete = true;
            int countMails = DisplayMailList(email);

            Console.WriteLine();
            if (countMails == 0)
            {
                Console.WriteLine("Sadly you have no mail to read actually ! Please wait while we redirecting you to the home page...");
                Thread.Sleep(1500);
                return countMails;
            }
            while(delete)
            {
                Console.WriteLine("---------------------------------------------\n");
                Console.WriteLine("What would you do ? Press \"d\" to delete a mail, or the number of the mail to open and read it : ");
                chooseMailNo = Console.ReadLine();
                if (chooseMailNo == "d")
                {
                    Console.Write("\nPlease enter the number of the mail to delete : ");
                    string mailToDelete = Console.ReadLine();
                    int.TryParse(mailToDelete, out int parsedMailNumber);
                    DeleteChoosenMail(parsedMailNumber);
                    Console.Clear();
                    DisplayMailList(email);
                } else
                {
                    delete = false;
                }
            }
            int.TryParse(chooseMailNo, out int parsedMailChoice);
            return parsedMailChoice;
        }
        public static void DeleteChoosenMail(int mailNumber)
        {
            Mail mailToDelete = Mails.Where(i => i.Number == mailNumber).FirstOrDefault();
            Mails.Remove(mailToDelete);
            Console.WriteLine("\n---------------------------------------------\n");
            Console.WriteLine("Mail deleted with succes ! Back to the list, please wait...");
            Thread.Sleep(1500);
        }
        public static string DisplayChoosenMail(int emailNo)
        {
            Console.Clear();
            foreach(Mail mail in Mails)
            {
                if (emailNo == mail.Number)
                {
                    Console.WriteLine($"From : {mail.Sender.Email}");
                    Console.WriteLine($"Object : {mail.Subject}\n");
                    Console.WriteLine($"Message :\n{mail.Message}\n");
                    Console.WriteLine($"Sent the : {mail.Date}");
                    mail.IsRead = true;
                }
            }
            Console.WriteLine();
            Console.WriteLine("---------------------------------------------");
            Console.WriteLine();
            Console.Write("Press 'h' to back home, or 'p' for your inbox mail list : ");
            string choice = Console.ReadLine();
            return choice;

        }
        public static Mail WriteMail(string email)
        {
            User myAccount = new User("", "", "");
            User receiverAccount = new User("", "", "");
            bool modifyMail = true;
            string objectMail = "";
            string message = "";

            while (modifyMail)
            {
                Console.Clear();
                Console.Write("Send email to : ");
                string receiverMail = Console.ReadLine();
                Console.Write("Mail object : ");
                objectMail = Console.ReadLine();
                Console.Write("Message : ");
                message = Console.ReadLine();

                foreach (User user in Users)
                {
                    if(user.Email == email)
                    {
                        myAccount = user;

                    }
                    if(user.Email == receiverMail)
                    {
                        receiverAccount = user;
                    }
                }
                Console.WriteLine("\n---------------------------------------------\n");
                Console.Write($"Is the mail information are correct ? If you want to change it press \"m\", if you want to go back home without send it press \"x\" else to send it, press any other key : ");
                string rewriteMail = Console.ReadLine();
                if (rewriteMail == "x") 
                {
                    return null;
                } else if (rewriteMail != "m") {
                    modifyMail = false;
                }
            }
            return new Mail(myAccount, receiverAccount, DateTime.Now, objectMail, message, Mails.Count()+1);
        }
    }
}
