﻿using QuanLiNhaHang.DAL;
using QuanLiNhaHang.ENTITY;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Printing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace QuanLiNhaHang.GUI
{
    public partial class FrmOrder : Form
    {
       
        Table table = new Table();
        int total = 0;
        int type;
        int idBill = 0;
        Account acc = new Account();
        public FrmOrder(int idtable, int PhanQuyen,string usename, string password)
        {

            InitializeComponent();
      
            TableDAL tbDAL = new TableDAL();
            this.table = tbDAL.getTableById(idtable);

            this.acc.UserName = usename;
            this.acc.PassWord = password;
            this.acc.Type = PhanQuyen;
            //
            // tbSoLuong.Text = this.acc.Type.ToString();

            this.idBill = this.table.Status;
            lbTableName.Text = this.table.TableName;
            loadTable();
            loadCategory();
            loadBill(this.table.Status);
        }

        //hiển thị danh sách bàn trống 
        public void loadTable()
        {
            TableDAL tableDAL = new TableDAL();
            cbTable.DataSource = tableDAL.getListTableEmpty();
            cbTable.DisplayMember = "TableName";
            cbTable.ValueMember = "id";

        }

        // hiển thị danh sách Category

        public void loadCategory()
        {
            MenuCategoryDAL cateDAL = new MenuCategoryDAL();
            cbMenuCategory.DataSource = cateDAL.getListCategory();
            cbMenuCategory.DisplayMember = "Name";
            cbMenuCategory.ValueMember = "Id";
            try
            {
                int idCategory = Convert.ToInt32(cbMenuCategory.SelectedValue.ToString());
                loadMenu(idCategory);
            }
            catch
            {

            }
        }

        // hien thi menu theo category

        public void loadMenu(int idCategory)
        {
            MenuDAL menuDAL = new MenuDAL();
            cbMenu.DataSource = menuDAL.getListMenuByIdCategory(idCategory);
            cbMenu.DisplayMember = "Name";
            cbMenu.ValueMember = "Id";
        }

        // hiển thị hóa đơn 
        public void loadBill(int id)
        {
            dgrBillInfo.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            BillInfoDAL billInfoDAL = new BillInfoDAL();
            DataTable data= billInfoDAL.getBillInfoByIdBill(id);
            dgrBillInfo.DataSource = data;

            // hien thi tổng tiền hiện tại

            this.total = 0;
            foreach( DataRow row in data.Rows){
                this.total = total + Convert.ToInt32(row.ItemArray[5].ToString());
            }
            lbTotal.Text = total.ToString();
            // cap nhat lai tong tin hao don
            BilDAL billDAL = new BilDAL();
            billDAL.updateTotalBill(id, total);
        }


        // action button ChuyenBan
        private void btChuyenBan_Click(object sender, EventArgs e)
        {
            int idNewTable = Convert.ToInt32(cbTable.SelectedValue.ToString());
            TableDAL tableDAL = new TableDAL();
            tableDAL.ChuyenBan(this.table.Id, idNewTable);
            
           
            this.table = tableDAL.getTableById(idNewTable);
            lbTableName.Text = this.table.TableName;
        }


        // chon 1 mon trong ordel
        private void cbMenuCategory_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                int idCategory = Convert.ToInt32(cbMenuCategory.SelectedValue.ToString());

                loadMenu(idCategory);
            }
            catch
            {

            }
        }
        // check textbox so 
        public bool checkNumber(string str)
        {
            try
            {
                int d = Convert.ToInt32(str);
                if (d > 0)
                {
                    return true;
                }
                else
                {
                    MessageBox.Show("Bạn đã nhập sai", "Lỗi");
                }
                
            }
            catch
            {
                MessageBox.Show("Bạn đã nhập sai", "Lỗi");
            }

            return false;
        }

        // action button them mon
        private void btThemMon_Click(object sender, EventArgs e)
        {
            int soLuong;
            int idMenu = Convert.ToInt32(cbMenu.SelectedValue.ToString());
            if (checkNumber(tbSoLuong.Text))
            {
                soLuong= Convert.ToInt32(tbSoLuong.Text);
                int idMenuSelect = Convert.ToInt32(cbMenu.SelectedValue.ToString());
                BillInfoDAL billInfoDAL = new BillInfoDAL();
                billInfoDAL.insertBillInfo(table.Status, idMenuSelect, soLuong);
                loadBill(this.table.Status);
            }
        }


        // select item in ordel
        private void dgrBillInfo_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            int numberRow = e.RowIndex;
            if (dgrBillInfo.Rows[numberRow].Cells[0].Value.ToString() != "")
            {
                int idMenu = Convert.ToInt32(dgrBillInfo.Rows[numberRow].Cells[0].Value.ToString());
                cbMenu.SelectedValue = idMenu;
            }
            if (dgrBillInfo.Rows[numberRow].Cells[2].Value.ToString() != "")
            {
                int idCategory = Convert.ToInt32(dgrBillInfo.Rows[numberRow].Cells[2].Value.ToString());
                cbMenuCategory.SelectedValue = idCategory;
            }
            tbSoLuong.Text= dgrBillInfo.Rows[numberRow].Cells[3].Value.ToString();
        }

        private void tbKhach_TextChanged(object sender, EventArgs e)
        {
            if (checkNumber(tbKhach.Text))
            {
                int money = Convert.ToInt32(tbKhach.Text);
                lbSoDu.Text = (money - this.total).ToString();
            }
        }

        private void btThanhToan_Click(object sender, EventArgs e)
        {

            if (Convert.ToInt32(lbSoDu.Text) >= 0)
            {
                MessageBox.Show(table.TableName + " đã thanh toán số tiền là  : " + total + "." + " Tiền thừa là: " + Convert.ToInt32(lbSoDu.Text), "Thông báo");
            }
            else
            {
                MessageBox.Show(table.TableName + " chưa thanh toán" + " . " + " Còn thiếu" + " " +((-1) * Convert.ToInt32(lbSoDu.Text)));
            }
            // gọi proc thanh toan 

            TableDAL tableDAL = new TableDAL();
            tableDAL.ThanhToan(this.table.Id, this.table.Status);
        }


        // quay trở lại mainframe
        private void btReturn_Click(object sender, EventArgs e)
        {
            int phanQuyen = this.acc.Type;
            this.Hide();
            MainFrame frmMain = new MainFrame(phanQuyen, this.acc.UserName, this.acc.PassWord);
            frmMain.ShowDialog();
        }

       

        private void trangADMINToolStripMenuItem_Click(object sender, EventArgs e)
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

        private void btEdit_Click(object sender, EventArgs e)
        {
            int soLuong;
            int idMenu = Convert.ToInt32(cbMenu.SelectedValue.ToString());
            if (checkNumber(tbSoLuong.Text))
            {
                soLuong = Convert.ToInt32(tbSoLuong.Text);
                int idMenuSelect = Convert.ToInt32(cbMenu.SelectedValue.ToString());
                BillInfoDAL billInfoDAL = new BillInfoDAL();
                billInfoDAL.editBillInfo(table.Status, idMenuSelect, soLuong);
                loadBill(this.table.Status);
            }

        }

        private void btDelete_Click(object sender, EventArgs e)
        {

            if (MessageBox.Show("Bạn có chắc muốn xóa món?", "Thông báo", MessageBoxButtons.OKCancel) == System.Windows.Forms.DialogResult.OK)
            {
                int idMenuSelect = Convert.ToInt32(cbMenu.SelectedValue.ToString());
                BillInfoDAL billInfoDAL = new BillInfoDAL();
                billInfoDAL.DeleteBillInfo(table.Status, idMenuSelect);
                loadBill(this.table.Status);

            }
            else
            {
                MessageBox.Show(" Có lỗi khi xóa món", "Thông báo");
            }

           
        }

        private void btInHoaDon_Click(object sender, EventArgs e)
        {
            try
            {
                PrintDialog _PrintDialog = new PrintDialog();
                PrintDocument _PrintDocument = new PrintDocument();

                _PrintDialog.Document = _PrintDocument; 

                _PrintDocument.PrintPage += new System.Drawing.Printing.PrintPageEventHandler(_CreateReceipt); 
                
                DialogResult result = _PrintDialog.ShowDialog();

                if (result == DialogResult.OK)
                {
                    _PrintDocument.Print();
                }
            }
            catch (Exception)
            {

            }
        }
        private void _CreateReceipt(object sender, System.Drawing.Printing.PrintPageEventArgs e)
        {
            Graphics graphic = e.Graphics;
            Font font = new Font("Courier New", 12);
            float FontHeight = font.GetHeight();
            int startX = 10;
            int startY = 10;
            int offset = 40;


            graphic.DrawString("       NHÀ HÀNG ẨM THỰC ADNB", new Font("Courier New", 18), new SolidBrush(Color.Black), startX, startY);
            offset = offset + (int)FontHeight + 5;
            graphic.DrawString("         HOÁ ĐƠN BÁN HÀNG", new Font("Courier New", 18), new SolidBrush(Color.Black), startX, startY + offset);

            offset = offset + (int)FontHeight + 20;


            string top = "Tên Sản Phẩm".PadRight(24) + "Số lượng".PadRight(10) + "Đơn giá".PadRight(10) + "Thành Tiền";
            graphic.DrawString(top, font, new SolidBrush(Color.Black), startX, startY + offset);
            offset = offset + (int)FontHeight;
            graphic.DrawString("--------------------------------------------------------------", font, new SolidBrush(Color.Black), startX, startY + offset);
            offset = offset + (int)FontHeight + 5;

            BillInfoDAL billInfoDAL = new BillInfoDAL();
            DataTable data = billInfoDAL.getBillInfoByIdBill(idBill);
            
            

            for (int i = 0; i < data.Rows.Count; i++)
            {
                graphic.DrawString(data.Rows[i].ItemArray[1].ToString(), font, new SolidBrush(Color.Black), startX, startY + offset);
                graphic.DrawString(data.Rows[i].ItemArray[3].ToString(), font, new SolidBrush(Color.Black), startX + 250, startY + offset);
                graphic.DrawString(data.Rows[i].ItemArray[4].ToString(), font, new SolidBrush(Color.Black), startX + 350, startY + offset);
                graphic.DrawString(data.Rows[i].ItemArray[5].ToString(), font, new SolidBrush(Color.Black), startX + 450, startY + offset);


                offset = offset + (int)FontHeight + 5; //xuong 1 dong
            }



            offset = offset + 10;

            graphic.DrawString("--------------------------------------------------------------", font, new SolidBrush(Color.Black), startX, startY + offset);

            offset = offset + 10;

            graphic.DrawString("TỔNG TIỀN TRẢ ", new Font("Courier New", 12, FontStyle.Bold), new SolidBrush(Color.Black), startX, startY + offset);
            graphic.DrawString(this.total.ToString(), new Font("Courier New", 12, FontStyle.Bold), new SolidBrush(Color.Black), startX + 450, startY + offset);

            try
            {
                AccountDAL accDAL = new AccountDAL();
                Employee emp = accDAL.getEmployeeByAccount(this.acc.UserName);

                offset = offset + (int)FontHeight + 10;
                graphic.DrawString("Nhân viên : "+ emp.Name, font, new SolidBrush(Color.Black), startX, startY + offset);

            }
            catch
            {

            }
           

            offset = offset + (int)FontHeight + 15;              
            graphic.DrawString("              CẢM ƠN BẠN ĐÃ GHÉ THĂM!,", font, new SolidBrush(Color.Black), startX, startY + offset);
            offset = offset + (int)FontHeight + 5;              
            graphic.DrawString("             HI VỌNG BẠN SẼ GHÉ THĂM LẠI!", font, new SolidBrush(Color.Black), startX, startY + offset);
        }
    }

}
