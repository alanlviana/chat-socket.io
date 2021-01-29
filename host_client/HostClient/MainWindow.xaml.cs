using Quobject.EngineIoClientDotNet.Client;
using SocketIOClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace HostClient
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        private SocketIO Socket;
        public MainWindow()
        {
            StartSocketIO();
            InitializeComponent();
        }

        private async Task StartSocketIO()
        {
            const string uri = "http://localhost:3000/";
            Socket = new SocketIO(uri, new SocketIOOptions
            {
                Query = new Dictionary<string, string>
                {
                    {"token", "v3" }
                },
                EIO = 4
            });

            Socket.OnConnected += Socket_OnConnected;
            Socket.OnPing += Socket_OnPing;
            Socket.OnPong += Socket_OnPong;
            Socket.OnDisconnected += Socket_OnDisconnected;
            Socket.OnReconnecting += Socket_OnReconnecting;
            await Socket.ConnectAsync();

            Socket.On("Host_Initialize", response =>
            {
                var id = response.GetValue().ToObject<ResponseHandle>().id;
                Initialize();
                Socket.EmitAsync("Host_Result", new { 
                    id,
                    response =  "Iniciado"
                });
            });

            Socket.On("Host_Log", response =>
            {
                var id = response.GetValue().ToObject<ResponseHandle>().id;
                WriteLog();
                Socket.EmitAsync("Host_Result", new
                {
                    id,
                    response = "Logged"
                });
            });
        }

        private void Initialize()
        {
            //
            Thread.Sleep(3000);
        }

        private void WriteLog()
        {
            //
            Thread.Sleep(3000);
        }

        private void Socket_OnPing(object sender, EventArgs e)
        {
            Console.WriteLine("Socket_OnPing");
        }

        private void Socket_OnReconnecting(object sender, int e)
        {
            Console.WriteLine("Socket_OnReconnecting");

        }

        private void Socket_OnPong(object sender, TimeSpan e)
        {
            Console.WriteLine("Socket_OnPong");

        }

        private void Socket_OnDisconnected(object sender, string e)
        {
            Console.WriteLine("Socket_OnDisconnected");
        }

        private void Socket_OnConnected(object sender, EventArgs e)
        {
            Console.WriteLine("Socket_OnConnected");

        }
    }
}
