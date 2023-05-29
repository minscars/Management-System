using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Point_s_Management
{
    public partial class frmLogin : Form
    {
        public frmLogin()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            DialogResult traloi;
            traloi = MessageBox.Show("Do you want to close the window?", "Answer",
            MessageBoxButtons.OKCancel, MessageBoxIcon.Question);
            if (traloi == DialogResult.OK)
                Application.Exit();
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            
            try
            {
                //string MaCB = txtUsername.Text;
                clsDatabase.OpenConnection();
                SqlCommand cmd = new SqlCommand("Select MatKhau from CanBo where MaCB = @MaCB", clsDatabase.connect);
                cmd.Parameters.AddWithValue("@MaCB", txtUsername.Text);
                string pass = Convert.ToString(cmd.ExecuteScalar());

                if (txtUsername.Text == "" || txtPassword.Text == "")
                {
                    MessageBox.Show("You've not entered username or password yet! Please enter your username or password!", "Notification",
                        MessageBoxButtons.OK, MessageBoxIcon.Information);
                } else if (this.txtPassword.Text == pass)
                {
                    MessageBox.Show("Login successfully!", "Notification", MessageBoxButtons.OK,
                        MessageBoxIcon.Information);

                    //Gọi hàm xây dựng của lớp frmHome có tham số truyền vào là mã giáo viên
                    frmHome main = new frmHome(txtUsername.Text);
                    this.Hide();
                    main.ShowDialog();
                    this.Show();
                } else
                {
                    MessageBox.Show("Username or password is wrong !", "Notification",
                        MessageBoxButtons.OK, MessageBoxIcon.Information);
                    this.txtUsername.Focus();
                }


                //if ((this.txtUsername.Text == MaCB) && (this.txtPassword.Text == pass))
                //{
                //    MessageBox.Show("Login successfully!", "Notification", MessageBoxButtons.OK,
                //        MessageBoxIcon.Information);
                //    frmHome main = new frmHome();
                //    this.Hide();
                //    main.ShowDialog();
                //    this.Show();
                //}

                //else
                //{
                //    MessageBox.Show("Sai tên đăng nhập hoặc mật khẩu!", "Thông báo",
                //        MessageBoxButtons.OK, MessageBoxIcon.Information);
                //    this.txtUsername.Focus();
                //}

                clsDatabase.CloseConnection();
            }

            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {

        }
    }
}
