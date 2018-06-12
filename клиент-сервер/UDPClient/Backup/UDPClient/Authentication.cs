using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Net;
using System.Net.Sockets;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace UDPClient
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Client client = new Client(this.textBox1.Text);
            client.Show();
            this.Hide();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            Random rnd = new Random();
            IPEndPoint ip = new IPEndPoint(IPAddress.Broadcast, rnd.Next(0, 9999));
        }
    }
}
