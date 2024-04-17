namespace mail_TP
{
    internal class Program
    {
        static void Main(string[] args)
        {
            User user1 = new User("premierMail@youhou.fr","123","Homer");
            User fastTest = new User("abc", "abc", "Fab");

            Mail mail1 = new Mail(user1, fastTest, DateTime.Now.AddDays(-7), "Premier Mail !!!!", "Ceci est le premier mail, c'est un grand jour.", 1);
            Mail mail2 = new Mail(user1, fastTest, DateTime.Now.AddDays(-4), "Second Mail !!!!", "Et le deuxième !", 2);
            Mail mail3 = new Mail(user1, fastTest, DateTime.Now.AddHours(-4), "Jamais 203", "Voilà, voilà...", 3);
            Mail mail4 = new Mail(user1, fastTest, DateTime.Now, "Je m'ennuie.", "Et toi ? Réponds moi :(", 4);
            Mail mail5 = new Mail(user1, fastTest, DateTime.Now.AddDays(103), "Back to the futur", "Le futur c'est pourrie Marty, laisse la Delorean où elle est.", 5);

            while(true)
            {
                bool logged = true;
                bool access = false;
                string email = "";
                while (!access)
                {
                    Console.Clear();
                    Console.WriteLine("Welcome to your mailbox services ! To log in your account, please complete the following fields.\n");
                    Console.Write("Mail : ");
                    email = Console.ReadLine();
                    Console.WriteLine();
                    Console.Write("Password : ");
                    string password = Console.ReadLine();

                    if (DataBase.AskCredentials(email,password))
                    {
                        Console.WriteLine("\nEmail and password match. Access granted. Mailing service loading, please wait.");
                        access = true;
                    } else
                    {
                        Console.WriteLine("\nEmail and password doesn't match. Try again.");
                    }
                    Thread.Sleep(1500);
                    Console.WriteLine();
                }

                while (logged)
                {
                    string choice = DataBase.HomePage(email);
                    int chooseMailNo;
                    string chooseBackToMenu = "";
                    bool checkingMail = true;

                    switch (choice)
                    {
                        case "1":
                            while(checkingMail)
                            {
                                chooseMailNo = DataBase.MailListDeleteOrRead(email);
                                if (chooseMailNo == 0)
                                    break;
                                chooseBackToMenu = DataBase.DisplayChoosenMail(chooseMailNo);
                                if (chooseBackToMenu == "h")
                                {
                                    checkingMail = false;
                                }
                            }
                            break;
                        case "2":
                            DataBase.WriteMail(email);
                            break;
                        case "3":
                            logged = false;
                            break;
                        default:
                            break;
                    }

                }
                Console.Clear();
                Console.WriteLine("Wait, we are unlogging you...");
                Thread.Sleep(1000);

            }


        }

    }
}
