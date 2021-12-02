using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.WebSockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace BatteryDaemon
{
    class webSocketClient
    {

        string _strwsip = "";
        public webSocketClient()
        {

        }

        public webSocketClient(string wsip)
        {
            _strwsip = wsip;
        }


        public void task_webSocketClient()
        {

            try
            {
                Task t = Start();

                t.Wait();
            }catch (Exception ex)
            {
                Console.WriteLine(ex.Message);

            }

        }

    // 1.  WebServer 접속한다.
    // 2. 연결되었을때, String 으로만 문자 전송한다.
    private async Task Start()
    {

        using (ClientWebSocket ws = new ClientWebSocket())
        {
            string strUri = "ws://echo.websocket.org";
            if ("" != _strwsip)
            {
                strUri = _strwsip;
            }
            Uri serverUri = new Uri(strUri);

            await ws.ConnectAsync(serverUri, CancellationToken.None);
            while (WebSocketState.Open == ws.State)
            {
                Console.WriteLine("Input message ('exit' to exit) :'");
                string msg = Console.ReadLine();
                if (msg == "exit")
                {
                    break;

                }

                ArraySegment<byte> bytesToSend = new ArraySegment<byte>(Encoding.UTF8.GetBytes(msg));
                await ws.SendAsync(bytesToSend, WebSocketMessageType.Text, true, CancellationToken.None);
                ArraySegment<byte> bytestRecived = new ArraySegment<byte>(new byte[1024]);

                WebSocketReceiveResult result = await ws.ReceiveAsync(bytestRecived, CancellationToken.None);

                Console.WriteLine(Encoding.UTF8.GetString(bytestRecived.Array, 0, result.Count));
            }
        }

    }


    } //class

}
