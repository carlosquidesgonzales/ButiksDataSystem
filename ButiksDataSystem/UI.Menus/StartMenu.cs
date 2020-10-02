using ButiksDataSystem.DataLayer;
using ButiksDataSystem.Enteties;
using System;
using System.Collections.Generic;
using System.Text;

namespace ButiksDataSystem.Menus
{
    public class StartMenu
    {
        private CustomerMenu userMenu = new CustomerMenu(new Data<Product>());
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
                        adminMenu.ShowAdminMenu();
                         openMenu = false;
                        break;                   
                    default:
                        Console.WriteLine("Invalid menu. Choose between 0 and 2");
                        break;
                }
            }
        }
    }
}
