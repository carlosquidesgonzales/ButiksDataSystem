using ButiksDataSystem.Exeptions;
using System;
using System.IO;

namespace ButiksDataSystem.Extension_Methods
{
    public static class Extension
    {
        //
        public static void AppendToFile(this string item, string path)
        {
            try
            {
                using (StreamWriter sw = File.AppendText(path))  //Append new receipt in the same file
                {
                    sw.Write(item);
                }
            }
            catch (Exception ex)
            {
                throw new EntityException("Could not save item", ex);
            }
        }
    }
}
