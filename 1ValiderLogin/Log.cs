using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace _1ValiderLogin
{
    public class Log
    {
        public void EcrireLog(string fichier, string ligne, bool append)
        {
            try
            {
                using (StreamWriter sw = new StreamWriter(fichier, append))
                {
                    sw.WriteLine(ligne);
                    sw.Flush();
                    sw.Close();
                    Console.WriteLine(ligne);
                }
            }
            catch (Exception ex)
            {
                throw (ex);
            }
        }
    }
}
