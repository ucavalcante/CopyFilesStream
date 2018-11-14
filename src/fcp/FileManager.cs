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
        public void Writer(string msg)
        {
            using (StreamWriter wr = new StreamWriter(""))
            {
                wr.Write(msg);
                wr.Close();
            }
        }
    }
}