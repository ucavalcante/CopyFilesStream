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
                NetworkStream networkStream = tcpClient.GetStream();
                try
                {
                    FileManager fileReader = new FileManager();
                    string sendingMessage = fileReader.Reader(arquivo);
                    Byte[] sendBytes = Encoding.ASCII.GetBytes(sendingMessage);
                    networkStream.Write(sendBytes, 0, sendBytes.Length);
                    networkStream.Flush();
                }
                catch (System.Exception ex)
                {
                    Log.Information(ex.Message);
                }
                finally
                {
                    networkStream.Close();
                }
            }

        }
        public static void Receiver(int port)
        {
            IPAddress address = IPAddress.Parse("0.0.0.0");

            var server = new TcpListener(address, port);
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
                        try
                        {
                            NetworkStream stream = client.GetStream();

                            int i;

                            i = stream.Read(bytes, 0, bytes.Length);
                            while (i != 0)
                            {
                                data = Encoding.ASCII.GetString(bytes, 0, i);
                                Console.WriteLine($"Recebido:{data}");
                                Log.Information($"Recebido:{data}");
                                FileManager fileManager = new FileManager();
                                fileManager.Writer(data);
                                i = stream.Read(bytes, 0, bytes.Length);
                            }
                        }
                        catch (System.Exception ex)
                        {
                            Log.Information(ex.Message);
                        }
                        finally
                        {
                            client.Close();
                        }
                    }
                }
                catch (SocketException ex)
                {
                    Log.Information(ex.Message);
                }
                catch (System.Exception ex)
                {
                    Log.Information($"Exceção não esperada:{ex.Message}");
                }
            }
        }
    }
}