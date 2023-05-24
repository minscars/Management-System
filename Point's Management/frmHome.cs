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
    public partial class frmHome : Form
    {
        public string MaGV;

        public frmHome(string maGV)
        {

            InitializeComponent();
            this.MaGV = maGV;
        }

        private void label5_Click(object sender, EventArgs e)
        {
            
        }

        private void frmHome_Load(object sender, EventArgs e)
        {
            DataTable dt = new DataTable();
            DataTable d = new DataTable();
            try
            {   
                cmbSubject.Enabled= true;
                
                SqlCommand cmd = new SqlCommand("select DISTINCT mh.MaMon, mh.TenMon from GiangDay gd JOIN MonHoc mh ON gd.MaMon = mh.MaMon WHERE gd.MaCB = @MaCB", clsDatabase.connect);
                cmd.Parameters.AddWithValue("@MaCB", MaGV);
                cmd.ExecuteNonQuery();               

                SqlDataAdapter sda = new SqlDataAdapter(cmd);
                sda.Fill(dt);
                dt.Dispose();
                cmbSubject.DataSource= dt;
                cmbSubject.DisplayMember = "TenMon";
                cmbSubject.ValueMember= "MaMon";

                cmbSubject.Text = "";

                SqlCommand cd = new SqlCommand("select TenCB from CanBo where MaCB = @MaCB", clsDatabase.connect);
                cd.Parameters.AddWithValue("@MaCB", MaGV);
                lbName.Text = "Hello " + cd.ExecuteScalar().ToString() + " !";

            }

            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void label6_Click(object sender, EventArgs e)
        {
            //DataTable dt = new DataTable();
            //string selected = cmbSubject.SelectedValue.ToString();
            //SqlCommand cmd = new SqlCommand("select DISTINCT MaLop " +
            //    "from GiangDay WHERE MaMon = @MaMon and MaCB = @MaCB", clsDatabase.connect);
            //cmd.Parameters.AddWithValue("@MaMon", selected);
            //cmd.Parameters.AddWithValue("@MaCB", MaGV);
            //cmd.ExecuteNonQuery();
            //SqlDataAdapter sda = new SqlDataAdapter(cmd);
            //sda.Fill(dt);
            //dt.Dispose();
            //cmbClass.DataSource = dt;
            //cmbClass.DisplayMember = "MaLop";
            //cmbClass.ValueMember = "MaLop";

            //cmbClass.Text = "";
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
        }

        private void dataGridView_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                int r = this.dtgvDSL.CurrentCell.RowIndex;
                string tenSV = dtgvDSL.Rows[r].Cells[1].Value.ToString();
                string diemSV = dtgvDSL.Rows[r].Cells[2].Value.ToString();

                txtShowName.Text = tenSV;
                txtShowPoint.Focus();
            }
            catch
            {
                dtgvDSL.DataSource = null;
            }
        }
                
        private void cmbClass_SelectedIndexChanged(object sender, EventArgs e)
        {
            cmbClass.Text = "";
            string malop = cmbClass.SelectedValue.ToString();
            string mamon = cmbSubject.SelectedValue.ToString();
            DataTable dt = new System.Data.DataTable();
            SqlCommand cmd = new SqlCommand("select sv.mssv, sv.HoTen, ct.Diem " +
                                            "from DSSinhVien sv join ChiTietDiem ct " +
                                            "on sv.MSSV = ct.MSSV " +
                                            "where MaLop = @MaLOp and MaMon = @MaMon\r\n", clsDatabase.connect);
            cmd.Parameters.AddWithValue("@MaLop", malop);
            cmd.Parameters.AddWithValue("@MaMon", mamon);
            cmd.ExecuteNonQuery();
            SqlDataAdapter sda = new SqlDataAdapter(cmd);
            sda.Fill(dt);
            dt.Dispose();
            dtgvDSL.DataSource = dt;           
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            if(txtShowName.Text == "")
            {
                MessageBox.Show("Choose a student!", "Notification", MessageBoxButtons.OK, MessageBoxIcon.Information);
            } else if (txtShowPoint.Text=="")
            {
                //Thông báo chưa nhập điểm
                MessageBox.Show("Enter the point!", "Notification", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtShowPoint.Focus();

            }
            else
            {
                int r = this.dtgvDSL.CurrentCell.RowIndex;
                string maSV = dtgvDSL.Rows[r].Cells[0].Value.ToString();
                string diemSV = txtShowPoint.Text;
                string mamon = cmbSubject.SelectedValue.ToString();
                SqlCommand cmd = new SqlCommand("update ChiTietDiem set Diem = @Diem where mssv = @MSSV and MaMon = @MaMon", clsDatabase.connect);

                cmd.Parameters.AddWithValue("@Diem", diemSV);
                cmd.Parameters.AddWithValue("@MSSV", maSV);
                cmd.Parameters.AddWithValue("@MaMon", mamon);
                cmd.ExecuteNonQuery();
                
                //thông báo cập nhật điểm thành công
                MessageBox.Show("Update successfully!", "Notification", MessageBoxButtons.OK, MessageBoxIcon.Information);
                
                //Load lại data get view
                cmbClass_SelectedIndexChanged(sender, e);
                txtShowName.Clear();
                txtShowPoint.Clear();

            }

        }

        private void btnQuit_Click(object sender, EventArgs e)
        {
            DialogResult traloi;
            traloi = MessageBox.Show("Do you want to close the window?", "Answer", MessageBoxButtons.OKCancel, MessageBoxIcon.Question);
            if (traloi == DialogResult.OK)
            Application.Exit();
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            DataTable dt = new DataTable();
            string selected = cmbSubject.SelectedValue.ToString();
            SqlCommand cmd = new SqlCommand("select DISTINCT MaLop " +
                "from GiangDay WHERE MaMon = @MaMon and MaCB = @MaCB", clsDatabase.connect);
            cmd.Parameters.AddWithValue("@MaMon", selected);
            cmd.Parameters.AddWithValue("@MaCB", MaGV);
            cmd.ExecuteNonQuery();
            SqlDataAdapter sda = new SqlDataAdapter(cmd);
            sda.Fill(dt);
            dt.Dispose();
            cmbClass.DataSource = dt;
            cmbClass.DisplayMember = "MaLop";
            cmbClass.ValueMember = "MaLop";
            dtgvDSL.DataSource = null;
        }
    }
}
