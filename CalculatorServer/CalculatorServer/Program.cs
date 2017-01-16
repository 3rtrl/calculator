using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Fleck;
using Calculator;
using Newtonsoft.Json;

namespace CalculatorServer
{
    class Program
    {
  
        static void Main(string[] args)
        {
            FleckLog.Level = LogLevel.Info;
            var allsockets = new List<IWebSocketConnection>();
            var server = new WebSocketServer("ws://127.0.0.1:8181");
            server.Start(socket =>
            {
                socket.OnOpen = () =>
                {   //See socket.ConnectionInfo.* for additional informations
                    Console.WriteLine(String.Empty);
                    Console.WriteLine("GUID: " + socket.ConnectionInfo.Id);
                    Console.WriteLine("IP: " + socket.ConnectionInfo.ClientIpAddress);
                    Console.WriteLine("Port: " + socket.ConnectionInfo.ClientPort);
                    Console.WriteLine("=============================================");
                    allsockets.Add(socket);
                    socket.Send("server bağlantı sağlandı");
                };

                socket.OnClose = () =>
                {
                    Console.WriteLine(String.Empty);
                    Console.WriteLine("GUID: " + socket.ConnectionInfo.Id);
                    Console.WriteLine("IP: " + socket.ConnectionInfo.ClientIpAddress);
                    Console.WriteLine("Port: " + socket.ConnectionInfo.ClientPort);
                    allsockets.Remove(socket);
                };

                socket.OnMessage = (message) =>
                {
                    //TODO: Json.Net Deserialize
                    Console.WriteLine("[JSON MESSAGE] " + message);
                    SaveMessageModel save = JsonConvert.DeserializeObject<SaveMessageModel>(message);
                    double sayi1, sayi2, sonuc;
                    sonuc = 0;
                    sayi1 = Convert.ToDouble(save.sayi1);
                    sayi2 = Convert.ToDouble(save.sayi2);
                    switch (save.operation)
                    {
                        case "Topla":
                            sonuc = sayi1 + sayi2;
                            break;
                        case "Cikar":
                            sonuc = sayi1 - sayi2;
                            break;
                        case "Carp":
                            sonuc = sayi1 * sayi2;
                            break;
                        case "Bol":
                            sonuc = sayi1 / sayi2;
                            break;
                    }
                    SaveResultModel result = new SaveResultModel();
                    result.sonuc = sonuc;
                    allsockets.ToList().ForEach(s => s.Send(JsonConvert.SerializeObject(result)));
                };
            });

            var input = Console.ReadLine();
            while (input != "exit")
            {
                foreach (var socket in allsockets.ToList())
                {
                    socket.Send(input);
                }

                input = Console.ReadLine();
            }
        }


    }
}
