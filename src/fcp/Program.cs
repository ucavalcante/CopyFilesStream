using System;
using System.Collections.Generic;
using System.IO;

namespace fcp
{
    class Program
    {
        private static int port = 1983;
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
                
            }
            else
            {
                
            }
        }

    }
}
