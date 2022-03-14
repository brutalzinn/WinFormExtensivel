using NetCoreServer;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace WinFormExtensivel
{
    public class TcpChatSession : TcpSession
    {

        public TcpChatSession(TcpServer server) : base(server) { }

        private static Dictionary<string, object> Parse(byte[] buffer, long offset, long size)
        {
            string jsonStr = Encoding.UTF8.GetString(buffer, (int)offset, (int)size);
            return JsonSerializer.Deserialize<Dictionary<string, object>>(jsonStr) ?? new Dictionary<string, object>();
        }
        protected override void OnConnected()
        {
            Debug.WriteLine($"Chat TCP session with Id {Id} connected!");
            // _form.ConectarPlugin(Id.ToString(), Message);
            //RegistrarPlugin(Message);
        }

        protected override void OnDisconnected()
        {
            Debug.WriteLine($"Chat TCP session with Id {Id} disconnected!");
        }

        protected override void OnReceived(byte[] buffer, long offset, long size)
        {
            // Message = Encoding.UTF8.GetString(buffer, (int)offset, (int)size);
            var teste = Parse(buffer, (int)offset, (int)size);

        }


        protected override void OnError(SocketError error)
        {
            Console.WriteLine($"Chat TCP session caught an error with code {error}");
        }
    }
}
