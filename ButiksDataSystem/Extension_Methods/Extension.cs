using ButiksDataSystem.Exeptions;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace ButiksDataSystem.Extension_Methods
{
    public static class Extension
    {
        public static void AppendToFile(this string item, string path)
        {
            try
            {
                using (StreamWriter sw = File.AppendText(path))  //Append new receipt in the same file
                {
                    sw.WriteLine(item);
                }
            }
            catch (Exception ex)
            {
                throw new EntityException("Could not save item", ex);
            }
        }
    }
}
