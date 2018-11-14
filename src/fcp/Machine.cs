using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Text;
using Ncel;


namespace fcp
{
    public class Machine
    {
        public static void Sender(int port, List<FileInfo> arquivosList, string ip)
        {
            var tcpClient = new TcpClient(ip, port);
            foreach (var arquivo in arquivosList)
            {
                
                try
                {
                    NetworkStream networkStream = tcpClient.GetStream();
                    
                    string serverResponse = "Teste";
                    Byte[] sendBytes = Encoding.ASCII.GetBytes(serverResponse);
                    networkStream.Write(sendBytes, 0, sendBytes.Length);
                    networkStream.Flush();
                    
                    networkStream.Close();
                    Console.WriteLine($"Recebido:{serverResponse}");
                    Log.Information($"Recebido:{serverResponse}");

                }
                catch (System.Exception)
                {

                    throw;
                }
            }

        }
        public static void Receiver(int port)
        {
            IPAddress address = IPAddress.Parse("0.0.0.0");
            
            var server = new TcpListener( address, port);
            while (true)
            {
                try
                {
                    server.Start();
                    byte[] bytes = new byte[1024];
                    string data;

                    while (true)
                    {
                        Console.WriteLine("Aguardando Conexão.");
                        Log.Information("Aguardando Conexão.");
                        var client = server.AcceptTcpClient();
                        Console.WriteLine("Conexão estabelecida.");
                        Log.Information("Conexão estabelecida.");

                        NetworkStream stream = client.GetStream();

                        int i;

                        i = stream.Read(bytes, 0, bytes.Length);
                        while (i != 0)
                        {
                            data = Encoding.ASCII.GetString(bytes, 0, i);
                            Console.WriteLine($"Recebido:{data}");
                            Log.Information($"Recebido:{data}");

                            data = data.ToUpper();

                            byte[] msg = Encoding.ASCII.GetBytes(data);

                            stream.Write(msg, 0, msg.Length);
                            Console.WriteLine($"Enviado:{data}");

                             i = stream.Read(bytes, 0, bytes.Length);

                        }
                        client.Close();
                    }
                }
                catch (SocketException ex)
                {
                    Console.WriteLine(ex.Message);
                    Log.Information(ex.Message);
                }
                catch (System.Exception ex)
                {
                    Console.WriteLine($"Exceção não esperada:{ex.Message}");
                    Log.Information($"Exceção não esperada:{ex.Message}");
                }
            }
        }
    }
}