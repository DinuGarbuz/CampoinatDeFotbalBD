using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Npgsql;

namespace CampoinatDeFotbal
{
    public partial class TeamW : Form
    {
        private string connString = String.Format("Server={0};Port={1};User Id={2};Password={3};Database={4};", "localhost", 5432,
        "postgres", "1111", "CampionatDeFotbal");

        private NpgsqlConnection conn;
        private string sql;
        private NpgsqlCommand cmd;
        private DataTable dt;
        private int rowIndex = -1;
        public TeamW()
        {
            InitializeComponent();
            conn = new NpgsqlConnection(connString);
            Select();
        }


        private void Select()
        {
            try
            {
                conn.Open();
                sql = @"select * from team_select()";
                cmd = new NpgsqlCommand(sql, conn);
                dt = new DataTable();
                dt.Load(cmd.ExecuteReader());
                conn.Close();
                dgvData.DataSource = null;
                dgvData.DataSource = dt.DefaultView;
            }
            catch (Exception ex)
            {
                conn.Close();
                MessageBox.Show("Error: " + ex.Message);
            }
        }


        private void dgvData_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                rowIndex = e.RowIndex+1;
                   //textFirstname.Text = dgvData.Rows[e.RowIndex].Cells["id"].Value.ToString();
                //    txtMidname.Text = dgvData.Rows[e.RowIndex].Cells["_midname"].Value.ToString();
                //    txtLastname.Text = dgvData.Rows[e.RowIndex].Cells["_lastname"].Value.ToString();
                //}
            }
            conn.Open();
            rowIndex = int.Parse(textFirstname.Text = dgvData.Rows[e.RowIndex].Cells["id"].Value.ToString());
            sql = @"select * from player_select("+rowIndex+")";
            cmd = new NpgsqlCommand(sql, conn);
            dt = new DataTable();
            dt.Load(cmd.ExecuteReader());
            conn.Close();
            dgv2.DataSource = null;
            dgv2.DataSource = dt.DefaultView;

        }

        private void btnInsert_Click(object sender, EventArgs e)
        {
            rowIndex = -1;
            textFirstname = null;
            textFirstname.Select();
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            if (rowIndex < 0)
            {
                MessageBox.Show("Please choose student to update");
                return;
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (rowIndex < 0)
            {
                MessageBox.Show("Please choose student to delete");
                return;
            }
            try
            {
                conn.Open();
                sql = @"select * from st_delete(:+_id)";
                cmd = new NpgsqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("_id", int.Parse(dgvData.Rows[rowIndex].Cells["_id"].Value.ToString()));
                if ((int)cmd.ExecuteScalar() == 1)
                {
                    MessageBox.Show("Delete student succesfully");
                    rowIndex = -1;
                    Select();
                }
                conn.Close();
            }
            catch (Exception ex)
            {
                conn.Close();
                MessageBox.Show("Deleted fail. Error: " + ex.Message);
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            int result = 0;
            if (rowIndex < 0)
            {
                try
                {
                    conn.Open();
                    sql = @"select * from st_insert(:_firstname, :_midname, :_lastname)";
                    cmd = new NpgsqlCommand(sql, conn);
                    cmd.Parameters.AddWithValue("_firstname", textFirstname.Text);
                    cmd.Parameters.AddWithValue("_midname", txtMidname.Text);
                    cmd.Parameters.AddWithValue("_lastname", txtLastname.Text);
                    result = (int)cmd.ExecuteScalar();
                    conn.Close();
                    if (result == 1)
                    {
                        MessageBox.Show("Insert successfully");
                        Select();
                    }
                    else
                    {
                        MessageBox.Show("Insert fail");
                    }
                }
                catch (Exception ex)
                {
                    conn.Close();
                    MessageBox.Show("Insert fail. Error: " + ex.Message);
                }
            }
            else
            {
                try
                {
                    conn.Open();
                    sql = @"select *from st_update(:_id, :_firstname, :_midname, :_lastname";
                    cmd = new NpgsqlCommand(sql, conn);
                    cmd.Parameters.AddWithValue("_id", int.Parse(dgvData.Rows[rowIndex].Cells["_id"].Value.ToString()));
                    cmd.Parameters.AddWithValue("_firstname", textFirstname.Text);
                    cmd.Parameters.AddWithValue("_midname", txtMidname.Text);
                    cmd.Parameters.AddWithValue("_lastname", txtLastname.Text);
                    result = (int)cmd.ExecuteScalar();
                    conn.Close();
                    if (result == 1)
                    {
                        MessageBox.Show("Update Successfully");
                        Select();
                    }
                    else
                    {
                        MessageBox.Show("Update fail");
                    }
                }
                catch (Exception ex)
                {
                    conn.Close();
                    MessageBox.Show("Update fail. Error: " + ex.Message);
                }
            }
            result = 0;
        }

        private void dgv3_CellClick(object sender, DataGridViewCellEventArgs e)
        {
          
            rowIndex = int.Parse(textFirstname.Text = dgvData.Rows[e.RowIndex].Cells["id"].Value.ToString());
            conn.Open();
            sql = @"select * from player_stats_select(" + rowIndex + ")";
            cmd = new NpgsqlCommand(sql, conn);
            dt = new DataTable();
            dt.Load(cmd.ExecuteReader());
            conn.Close();
            dgv3.DataSource = null;
            dgv3.DataSource = dt.DefaultView;
        }

        private void dgv2_CellClick(object sender, DataGridViewCellEventArgs e)
        {
        
          
            rowIndex = int.Parse(dgv2.Rows[e.RowIndex].Cells["id"].Value.ToString());
            conn.Open();
            sql = @"select * from player_stats_select(" + rowIndex + ")";
            cmd = new NpgsqlCommand(sql, conn);
            dt = new DataTable();
            dt.Load(cmd.ExecuteReader());
            conn.Close();
            dgv3.DataSource = null;
            dgv3.DataSource = dt.DefaultView;
        }

        private void btn_back_Click(object sender, EventArgs e)
        {
            this.Hide();
            Menu form1 = new Menu();
            form1.ShowDialog();
            this.Close();
        }
    }
}
