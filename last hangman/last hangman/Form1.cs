using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace last_hangman
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (comboBox1.Text == "LEVEL 1")
            {
                level_1 a = new level_1();
                a.Show();
                this.Hide();
            }
            else if (comboBox1.Text == "LEVEL 2")
            {
                level_2 a = new level_2();
                a.Show();
                this.Hide();
            }
            else if (comboBox1.Text == "LEVEL 3")
            {
                level_3 a = new level_3();
                a.Show();
                this.Hide();
            }
            else
            {
                level_1 a = new level_1();
                a.Show();
                this.Hide();
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
