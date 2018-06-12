using System;
using System.Net.Sockets;
using System.Net;
using System.Text;
using System.Windows.Forms;

namespace UDPClient
{
    public partial class Client : Form
    {
        // Создаем сокет
        Socket socket;
        IPEndPoint ipe;
        EndPoint Ippoint;
        // хранит имя пользователя
        string nameUser;
        byte[] okk = new byte[256];
        string ok;

        public Client(string name_user)
        {
            InitializeComponent();
            // присваиваем переменной имя пользователя
            this.nameUser = name_user;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            // закрываем сокет
            socket.Close();
            // закрытие приложения
            Application.Exit();
        }

        private void Client_Load(object sender, EventArgs e)
        {
            // Инициализируем сокет
            socket = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp);
            // настраиваем параметр соединения
            ipe = new IPEndPoint(IPAddress.Broadcast, 5400);
            button_New.Visible = false;
            label1.Text = "Введите сообщение!";
        }

        private void DataTransferBtn_Click(object sender, EventArgs e)
        {
                label1.Text = "Сообщение оправляется!";
                // добавляем в начало сообщение имя пользователя
                string str = nameUser + ":\n" + richTextBox1.Text;
                // разрешение широковещательного адреса
                socket.SetSocketOption(SocketOptionLevel.Socket, SocketOptionName.Broadcast, true);
                // формируем байтовый массив для передачи
                byte[] paket = Encoding.Default.GetBytes(str);
                // отправка сообщения
                socket.SendTo(paket, ipe);
                richTextBox1.Visible = false;
                button_New.Visible = false;

            //System.Diagnostics.Stopwatch myStopwatch = new System.Diagnostics.Stopwatch();
            //myStopwatch.Start(); //запуск
            while (ok == null)
                {
                    // прием данных от удаленного сокета
                    Ippoint = (EndPoint)ipe;
                    socket.ReceiveFrom(okk, ref Ippoint);
                    // формирование строки сообщения из полученного массива байт
                    ok = Encoding.Default.GetString(okk);
                }
            //myStopwatch.Stop(); //остановить
                    button_New.Visible = true;
                    label1.Text = "Сообщение получено сервером!";
                    //label1.Text = myStopwatch.Elapsed.Seconds.ToString();
                    ok = null;
        }

        private void button_New_Click(object sender, EventArgs e)
        {
            richTextBox1.Visible = true;
            richTextBox1.Text = null;
            label1.Text = "Введите сообщение!";
        }
    }
}
