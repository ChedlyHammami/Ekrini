using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.Runtime.InteropServices.ObjectiveC;

namespace Ekrini
{
    public partial class Tenants : Form
    {
        public Tenants()
        {
            InitializeComponent();
            showTenants();
        }

        SqlConnection con = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\chedl\OneDrive\Documents\HouseRental.mdf;Integrated Security=True;Connect Timeout=30");
        private void showTenants()
        {
            con.Open();
            string Query = "select * from TenantTbl";
            SqlDataAdapter sda = new SqlDataAdapter(Query, con);
            SqlCommandBuilder Builer = new SqlCommandBuilder(sda);
            var ds = new DataSet();
            sda.Fill(ds);
            TenantsDGV.DataSource = ds.Tables[0];
            con.Close();
        }
     
        private void ResetData()
        {
            phoneTb.Text = "";
            GenCb.SelectedIndex = -1;
            TnameTb.Text = "";
        }
        private void Savebtn_Click(object sender, EventArgs e)
        {
            if(TnameTb.Text == "" || GenCb.SelectedIndex == -1 || phoneTb.Text == "")
            {
                MessageBox.Show("Missing Information !");
            }
            else
            {
                try
                {
                    con.Open();
                    SqlCommand cmd = new SqlCommand("insert into TenantTbl(TenName, TenPhone, TenGen) values(@TN,@TP,@TG)",con);
                    cmd.Parameters.AddWithValue("@TN", TnameTb.Text);
                    cmd.Parameters.AddWithValue("@TP", phoneTb.Text);
                    cmd.Parameters.AddWithValue("@TG", GenCb.SelectedItem.ToString());
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Tenant Added !");
                    con.Close();
                    ResetData();
                    showTenants();
                }
                catch (Exception Ex)
                {
                    MessageBox.Show(Ex.Message);
                }

            }
        }
        int key = 0;
        private void TenantsDGV_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            TnameTb.Text = TenantsDGV.SelectedRows[0].Cells[1].Value.ToString();
            phoneTb.Text = TenantsDGV.SelectedRows[0].Cells[2].Value.ToString();
            GenCb.Text = TenantsDGV.SelectedRows[0].Cells[3].Value.ToString();
            if (TnameTb.Text == "")
            {
                key = 0;
            }
            else
            {
                key = Convert.ToInt32(TenantsDGV.SelectedRows[0].Cells[0].Value.ToString());
            }

        }
        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void pictureBox8_Click(object sender, EventArgs e)
        {

        }

        private void guna2GradientButton2_Click(object sender, EventArgs e)
        {
            if(key == 0)
            {
                MessageBox.Show("Select A Tenant");
            }
            else
            {
                try
                {
                    con.Open();
                    SqlCommand cmd = new SqlCommand("delete from TenantTbl where TenId=@Tkey", con);
                    cmd.Parameters.AddWithValue("@Tkey", key);
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Tenant Deleted !");
                    con.Close();
                    ResetData();
                    showTenants();
                }
                catch(Exception Ex)
                {
                    MessageBox.Show(Ex.Message);
                }
            }
        }

        private void guna2GradientButton1_Click(object sender, EventArgs e)
        {
            if (TnameTb.Text == "" || GenCb.SelectedIndex == -1 || phoneTb.Text == "")
            {
                MessageBox.Show("Missing Information !");
            }
            else
            {
                try
                {
                    con.Open();
                    SqlCommand cmd = new SqlCommand("update TenantTbl set TenName=@TN, TenPhone=@TP, TenGen=@TG where TenId=@Tkey", con);
                    cmd.Parameters.AddWithValue("@TN", TnameTb.Text);
                    cmd.Parameters.AddWithValue("@TP", phoneTb.Text);
                    cmd.Parameters.AddWithValue("@TG", GenCb.SelectedItem.ToString());
                    cmd.Parameters.AddWithValue("@Tkey", key);
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Tenant Updated !");
                    con.Close();
                    ResetData();
                    showTenants();
                }
                catch (Exception Ex)
                {
                    MessageBox.Show(Ex.Message);
                }

            }
        }
    }
}
