using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Socket_Servidor
{
    class Program
    {
        static void Main(string[] args)
        {

            const int PORTA = 9009;
            Servidor servidor;
            Console.Title = "TCPServer";
            
            servidor = new Servidor(PORTA);

            Console.WriteLine(">> Conexão estabelecida com cliente");

            while (true)
            {
                try
                {
                    servidor.Run();

                    Console.Clear();
                    Console.WriteLine(">> Cliente Conectado ao servidor \n");
                    Console.WriteLine(">> Dados do Cliente: " + servidor.mensagemCliente + "\n");
                    Console.WriteLine(">> Resposta do Servidor: " + servidor.respostaServidor);
                }
                catch (Exception)
                {
                    Console.Clear();
                    Console.WriteLine("\n\nException: >> Conexão perdida ou encerrada. \n");
                    Console.ReadLine();
                }
            }
        }
    }
}
