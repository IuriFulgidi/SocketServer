using System;
using System.Net;
using System.Net.Sockets;
using System.Text;
namespace SocketServer
{
    class Program
    {
        static void Main(string[] args)
        {
            //EndPoint: coppia ip-porta

            //Il socket vuole tre parametri:
            //1) protocolllo a livello network(Ipv4)
            //2) socket type (stream)
            //3) protocollo di trasporto
            Socket listenerSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);

            //Configuraxoine del serversocket
            //serve un programma che sta in ascolto su un endpoint
            IPAddress ipaddr = IPAddress.Any;
            IPEndPoint ipep = new IPEndPoint(ipaddr, 23000);

            //collegamento tra listersocket e endpoint
            //Bind: si imposta il server ad ascoltare qualunque ip su porta 23000
            listenerSocket.Bind(ipep);

            //mettere il server in ascolto
            listenerSocket.Listen(5);
            Console.WriteLine("Server in ascolto...");

            //istruzione bloccante
            //un client si connette
            Socket client = listenerSocket.Accept();
            Console.WriteLine("CLient Connesso\n Client info: " + client.RemoteEndPoint.ToString());

            //ricevere un messaggio da parte del client
            byte[] buff = new byte[128];
            int numberByteReceived = 0;

            //ricezione del messaggio
            numberByteReceived = client.Receive(buff);

            //traduzione dei byte in ASCII
            string mesRicevuto = Encoding.ASCII.GetString(buff, 0, numberByteReceived);
            Console.WriteLine("il cliet dice: " + mesRicevuto);

            //risposta al client
            string mesInviare = "Benvenuto, mi hai scirtto " + mesRicevuto;

            //pulisco il buffer
            Array.Clear(buff, 0, buff.Length);
            numberByteReceived = 0;

            //creazione del messaggio
            string risposta = "Benvenuto" + client.RemoteEndPoint.ToString() + "! al to servizio\n" + "Il tuo ultimo messaggio è stato: " + mesRicevuto;

            //Traduzione
            buff = Encoding.ASCII.GetBytes(risposta);

            //invio del messaggio al client
            client.Send(buff);
            Console.WriteLine("Termina");
            Console.ReadLine();

        }
    }
}
