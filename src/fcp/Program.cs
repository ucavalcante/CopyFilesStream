using System;
using System.Collections.Generic;
using System.IO;

namespace fcp
{
    class Program
    {
        private static int port = 1983;
        private static string IP = "";
        static void Main(string[] args)
        {
            CommandDriver(args);
        }

        private static void CommandDriver(string[] args)
        {
            List<FileInfo> ArquivosList = new List<FileInfo>();
            for (int i = 0; i < args.Length; i++)
            {
                var arq = new FileInfo(args[i]);
                if (arq.Exists)
                {
                    ArquivosList.Add(arq);
                }

                switch (args[i])
                {
                    case "-t":
                        TestMode();
                        break;
                    case "-i":
                        IP = args[i + 1];
                        break;
                    case "-p":
                        port = Convert.ToInt32(args[i + 1]);
                        i++;
                        break;
                    default:
                        Console.WriteLine($"-h|--help para ajuda");
                        break;
                }
            }
            if (ArquivosList.Count > 0)
            {
                Machine.Sender(port, ArquivosList, IP);
            }
            else
            {
                Machine.Receiver(port);
            }
        }

        private static void TestMode()
        {
            Console.WriteLine("Instancia de Testes.");
            var x = new FileInfo(@"C:\Users\ulc\Source\myRepos\CopyFilesStream\src\fcp\bin\Debug\Resource\teste.json");
            List<FileInfo> ArquivosList = new List<FileInfo>();
            ArquivosList.Add(x);
            Machine.Receiver(port);
            Machine.Sender(port, ArquivosList, "");
        }
    }
}
