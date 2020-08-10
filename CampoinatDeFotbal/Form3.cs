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
    public partial class Match : Form
    {

        private string connString = String.Format("Server={0};Port={1};User Id={2};Password={3};Database={4};", "localhost", 5432,
       "postgres", "1111", "CampionatDeFotbal");

        private NpgsqlConnection conn;
        private string sql;
        private NpgsqlCommand cmd;
        private DataTable dt;
        private int rowIndex = -1;

        public Match()
        {
            InitializeComponent();
            conn = new NpgsqlConnection(connString);
            Select();
        }

        private void btn_back_Click(object sender, EventArgs e)
        {
            this.Hide();
            Menu form1 = new Menu();
            form1.ShowDialog();
            this.Close();
        }
        private void Select()
        {
            try
            {
                conn.Open();
                sql = @"select * from game_select()";
                cmd = new NpgsqlCommand(sql, conn);
                dt = new DataTable();
                dt.Load(cmd.ExecuteReader());
                conn.Close();
                dgv1.DataSource = null;
                dgv1.DataSource = dt.DefaultView;
            }
            catch (Exception ex)
            {
                conn.Close();
                MessageBox.Show("Error: " + ex.Message);
            }
        }
    }

   
}

