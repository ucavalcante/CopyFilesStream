using System;
using System.Collections.Generic;
using System.IO;
using System.Threading;

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
                        return;
                    case "-i":
                        IP = args[i + 1];
                        i++;
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
            Thread thread = new Thread(new ThreadStart(ParalelalRunner));
            thread.Start();

            Machine.Sender(port, ArquivosList, IP);
        }
        private static void ParalelalRunner()
        {
            Machine.Receiver(port);
        }
    }
}
