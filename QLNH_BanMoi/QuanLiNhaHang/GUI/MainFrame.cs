using QuanLiNhaHang.DAL;
using QuanLiNhaHang.ENTITY;
using SuperWebSocket;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Reflection;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace QuanLiNhaHang.GUI
{
    public partial class MainFrame : Form
    {
        public delegate void doIt();
        public static WebSocketServer wsServer;
        Account acc = new Account();
        public MainFrame(int PhanQuyen, string username, string password)
        {
            InitializeComponent();
            LoadTable(2);

            Thread thread = new Thread(new ThreadStart(socket));
            thread.Name = "alo";
            
            thread.IsBackground = true;
            thread.Start();
            this.acc.UserName = username;
            this.acc.PassWord = password;
            this.acc.Type = PhanQuyen;

           
        }



        public void socket()
        {
                     
             int i = 0;
            string ipad = "192.168.43.180";
            IPAddress ip = IPAddress.Parse(ipad);
             TcpListener server = new TcpListener(ip, 8535);
             TcpClient client = default(TcpClient);

            try {server.Start();
                while (true)
                {
                    client = server.AcceptTcpClient();

                    byte[] rece = new byte[100];
                    NetworkStream stream = client.GetStream();

                    stream.Read(rece, 0, rece.Length);
                    string msg = Encoding.ASCII.GetString(rece, 0, rece.Length);

                    if (msg != null)
                    {

                        flpTable.Invoke((MethodInvoker)delegate {
                            // Running on the UI thread
                            LoadTable(2);
                        });



                    }

                }
            }
            catch
            {

            }
            
                

            
        }

        public void LoadTable(int tt)
        {
            

            TableDAL tbDAL = new TableDAL();
            List<Table> lsTable = tbDAL.getListTable(tt);
            while (flpTable.Controls.Count > 0) flpTable.Controls.RemoveAt(0);
            foreach (Table table in lsTable)
            {
                Button bt = new Button() { Width = 105, Height = 105 };
                bt.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;   // tu zoom image
                bt.Text = table.TableName;
                bt.TextAlign = System.Drawing.ContentAlignment.BottomCenter;


                if (table.Status == 0)
                {
                    bt.BackgroundImage = Image.FromFile(Application.StartupPath + "\\IMAGE\\table0.png");
                }
                else if (table.Status == 1)
                {
                    bt.BackgroundImage = Image.FromFile(Application.StartupPath + "\\IMAGE\\table2.png");
                }
                else
                {
                    bt.BackgroundImage = Image.FromFile(Application.StartupPath + "\\IMAGE\\table1.png");
                }

                bt.Tag = table;
                bt.Click += btTable_Click;

                flpTable.Controls.Add(bt);
            }
        }

        private void btTable_Click(object sender, EventArgs e)
        {
            Table tb = (sender as Button).Tag as Table;
            if (tb.Status == 0||tb.Status==1)
            {
                BilDAL billDAL = new BilDAL();
                string dt = DateTime.Today.ToString().Trim();
                DateTime d = Convert.ToDateTime(dt);
                billDAL.insertBill(tb.Id, d);
            }

            this.Hide();
            FrmOrder frOrder = new FrmOrder(tb.Id, this.acc.Type, this.acc.UserName, this.acc.PassWord);

            frOrder.ShowDialog();
        }

        private void cbChonTrangThai_SelectedIndexChanged(object sender, EventArgs e)
        {
            int tt;
            if (cbChonTrangThai.Text == "All") tt = 2;
            else if (cbChonTrangThai.Text == "Bàn Trống") tt = 0;
            else tt = 1;
            LoadTable(tt);
        }

       
        private void adminToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (this.acc.Type == 1)
            {
                FrmAdmin frmAdmin = new FrmAdmin();
                frmAdmin.ShowDialog();
            }
            else
            {
                MessageBox.Show("Bạn không có quyền truy cập", "Lỗi");
            }
        }

        private void đổiMậtKhẩuToolStripMenuItem_Click_1(object sender, EventArgs e)
        {
            FrmDoiMatKhau FrmDoiPass = new FrmDoiMatKhau(acc.UserName, acc.PassWord);
            FrmDoiPass.ShowDialog();
        }

        private void đăngXuấtToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Hide();
            FrmLogin frmLogin = new FrmLogin(); ;
            frmLogin.ShowDialog();
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void flpTable_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}
