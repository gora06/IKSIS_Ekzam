using System;
using System.ComponentModel;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Windows.Forms;

namespace UDPServer
{
    public partial class Form1 : Form
    {
        Socket socket;
        EndPoint Rempoint;
        IPEndPoint ipe;
        byte[] buffer = new byte[256];
        string str;

        public Form1()
        {
            InitializeComponent();
        }
        // кнопка закрытия формы
        private void button1_Click(object sender, EventArgs e)
        {
            socket.Close();
            Application.Exit();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            // инициализируем сокет
            socket = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp);
            // устаавливаем параметры сокета
            // время приема ограничиваем 10 сек.
            socket.SetSocketOption(SocketOptionLevel.Socket, SocketOptionName.ReceiveTimeout, 10000);
            // создаем параметр подключения
            ipe = new IPEndPoint(IPAddress.Any, 5400);
            // связываем сокет с параметром подключения
            socket.Bind(ipe);
            // создаем структуру удаленного сокета
            IPAddress ipRem = IPAddress.Any;
            IPEndPoint Rem = new IPEndPoint(ipRem, 0);
            Rempoint = (EndPoint)Rem;
            label1.Text = "Ожидаем данные";
        }
        // кнопка приема данных
        private void button2_Click(object sender, EventArgs e)
        {
            // прием данных от удаленного сокета
            socket.ReceiveFrom(buffer, ref Rempoint);
            // формирование строки сообщения из полученного массива байт
            str = Encoding.Default.GetString(buffer);
            label1.Text = "Прием данных закончен!";
            // отображение сообщения в текстовом поле
            richTextBox1.Text += "\n" + str;
        }
    }
}
