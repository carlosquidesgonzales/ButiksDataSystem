using ButiksDataSystem.DataLayer;
using ButiksDataSystem.Enteties;
using ButiksDataSystem.Enums;
using ButiksDataSystem.Menus;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ButiksDataSystem
{
    class Program
    {
        static StartMenu startMenu = new StartMenu();
        static IData bl = new Data();
        static AdminMenu aMenu = new AdminMenu();
        static void Main(string[] args)
        {

            //var items = bl.GetProducts();
            startMenu.ShowStartMenu();
            //var user = new CustomerMenu();
            //user.ShowUserMenu();
            //aMenu.SearchReceipt();

        } 
    }
}
