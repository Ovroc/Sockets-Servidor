using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace Socket_Servidor
{
    class Servidor
    {

        //tamanho máximo da mensagem recebida do cliente
        private const int TAMANHO_BUFFER = 1000000;
        //Socket do servidor
        private TcpListener servidor;
        //Socket do cliente
        private TcpClient cliente;
        //mensagem que o cliente manda para o servidor
        public string mensagemCliente;
        //mensagem que o servidor manda ao cliente
        public string respostaServidor;

        public Servidor(int porta)
        {
            this.servidor = new TcpListener(IPAddress.Any, porta);
            this.cliente = default(TcpClient);

            this.servidor.Start();

            Console.WriteLine(">> Servidor iniciado, pronto para receber requisições..\n");

            this.cliente = servidor.AcceptTcpClient();

            this.respostaServidor = "";
        }

        public void Run()
        {
            NetworkStream netStream = cliente.GetStream();

            byte[] recebido = new byte[TAMANHO_BUFFER];
            //recebe a mensagem do cliente
            netStream.Read(recebido, 0, (int)cliente.ReceiveBufferSize);

            //converte bytes em string
            mensagemCliente = Encoding.ASCII.GetString(recebido);

            /* reduz a string deixando de fora os caracteres 
             * adicionados durante o processo de conversão bytes->string
             */
            mensagemCliente = mensagemCliente.Substring(0, mensagemCliente.IndexOf("$"));

            /* define a resposta do servidor
             * manda para o cliente a mensagem recebida
             * convertida em letras maiusculas
             */

            if (mensagemCliente == "1564398")
            {
                this.respostaServidor = "Verdadeiro";
            }
            else
            {
                this.respostaServidor = "Falso";
            }

            Byte[] enviado = Encoding.ASCII.GetBytes(respostaServidor);

            //envia a resposta em bytes ao cliente
            netStream.Write(enviado, 0, enviado.Length);
            netStream.Flush();
        }
    }
}
