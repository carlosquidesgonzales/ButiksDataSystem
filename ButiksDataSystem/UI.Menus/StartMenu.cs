using ButiksDataSystem.DataLayer;
using ButiksDataSystem.Enteties;
using System;

namespace ButiksDataSystem.Menus
{
    public class StartMenu
    {
        private CustomerMenu userMenu = new CustomerMenu();
        private AdminMenu adminMenu = new AdminMenu();
        public void ShowStartMenu()
        {
            Console.WriteLine("MAIN MENU");
            bool openMenu = true;
            while (openMenu)
            {
                Console.WriteLine("1: User Menu");
                Console.WriteLine("2: Admin Menu");
                Console.WriteLine("0: Exit");
                int number;
                int.TryParse(Console.ReadLine(), out number);
                switch (number)
                {
                    case 0:
                        Environment.Exit(0);
                        openMenu = false;
                        break;
                    case 1:
                        userMenu.ShowUserMenu();
                        openMenu = false;
                        break;
                    case 2:
                        Console.Clear();
                        //Loggin();
                        adminMenu.ShowAdminMenu();
                        openMenu = false;
                        break;
                    default:
                        Console.WriteLine("Invalid menu. Choose between 0 and 2");
                        break;
                }
            }
        }
        private void Loggin()
        {
            while (true)
            {
                const string uName = "admin";
                const string pword = "admin";
                Console.WriteLine("User name:");
                string userName = Console.ReadLine();
                Console.WriteLine("Password:");
                string password = Console.ReadLine();
                if (uName == userName && pword == password)
                {
                    userMenu.ShowUserMenu();
                    break;
                }
                else
                {
                    Console.WriteLine("Incorrect user name or password. Try again!");
                }
            }
        }
    }
}
