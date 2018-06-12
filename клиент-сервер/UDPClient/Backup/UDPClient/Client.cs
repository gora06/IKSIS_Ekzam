using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Net.Sockets;
using System.Net;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace UDPClient
{
    public partial class Client : Form
    {
        // Создаем сокет
        Socket socket;
        IPEndPoint ipe;
        // хранит имя пользователя
        string nameUser;

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
        }

        private void DataTransferBtn_Click(object sender, EventArgs e)
        {
            // добавляем в начало сообщение имя пользователя
            string str = nameUser + ":\n"+ richTextBox1.Text;
            // разрешение широковещательного адреса
            socket.SetSocketOption(SocketOptionLevel.Socket, SocketOptionName.Broadcast, true);
            // формируем байтовый массив для передачи
            byte[] paket = Encoding.Default.GetBytes(str);
            // проверка на размер сообщения
            if (paket.Length > 256)
            {
                MessageBox.Show("Сообщение превышает допустимый размер!\nПопробуйте уменьшить сообщение", "Предупреждение", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
            else
            {
                // отправка сообщения
                socket.SendTo(paket, ipe);
                label1.Text = "Передача данных закончена!";
            }
        }
    }
}
