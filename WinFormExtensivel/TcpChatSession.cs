using NetCoreServer;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WinFormExtensivel
{
    public class TcpChatSession : TcpSession
    {

        public TcpChatSession(TcpServer server) : base(server) { }



        protected override void OnConnected()
        {
          //  Server.Emit("teste", "testando minha mensagem com emit event.");
            Debug.WriteLine($"Chat TCP session with Id {Id} connected!");
            // _form.ConectarPlugin(Id.ToString(), Message);
            //RegistrarPlugin(Message);
            //Server.On("register", (object data) =>
            //{
            //    Debug.WriteLine(data);

            //});



            }

   

        protected override void OnDisconnected()
        {
            Debug.WriteLine($"Chat TCP session with Id {Id} disconnected!");
        }

        protected override void OnReceived(byte[] buffer, long offset, long size)
        {
            // string jsonStr = Encoding.UTF8.GetString(buffer, (int)offset, (int)size);
           // Server.HandleMessage(buffer, (int)offset, (int)size);
            //Server.On("teste", (object data) =>
            //{
            //    Debug.WriteLine(data);

            //});
            //Server.HandleMessage(jsonStr);
            //var teste = JsonSerializer.Deserialize<MessageChannel>(jsonStr);
            //Globals._GlobalEventHandle.OnThresholdReached(teste);
        }


        protected override void OnError(SocketError error)
        {
            Console.WriteLine($"Chat TCP session caught an error with code {error}");
        }
    }
}
