using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CampoinatDeFotbal
{
    public partial class Menu : Form
    {
        public Menu()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Hide();
            TeamW form1 = new TeamW();
            form1.ShowDialog();
            this.Close();
        }

        private void btn_calendar_Click(object sender, EventArgs e)
        {
            this.Hide();
            Match form1 = new Match();
            form1.ShowDialog();
            this.Close();
        }
    }
}
