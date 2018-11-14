using System;
using System.IO;

namespace fcp
{
    public class FileManager
    {
        public string Reader(FileInfo arquivo)
        {
            using (StreamReader sr = new StreamReader(arquivo.FullName))
            {
                return sr.ReadToEnd();
            }
        }
        public void Writer(string message, string fileData)
        {
            using (StreamWriter wr = new StreamWriter(fileData))
            {
                wr.Write(message);
                wr.Close();
            }
        }
    }
}