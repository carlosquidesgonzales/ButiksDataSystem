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
        static void Main(string[] args)
        {
            StartMenu startMenu = new StartMenu();
            startMenu.ShowStartMenu();
        } 
    }
}
