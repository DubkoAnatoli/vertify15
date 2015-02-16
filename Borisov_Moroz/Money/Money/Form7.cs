using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Money
{
    public partial class Form7 : Form
    {
        public Form7()
        {
            InitializeComponent();
        }

        private void textBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar != 8 && (e.KeyChar < 48 || e.KeyChar > 57) && e.KeyChar != 44)
                e.Handled = true;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (textBox1.Text.Length != 0)
            {
                GlobalNames.kurs = Convert.ToDouble(textBox1.Text);
                this.Close();
            }
            else
            {
                GlobalNames.kurs = 1.0;
                this.Close();
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            GlobalNames.kurs = 1.0;
            this.Close();
        }
    }
}
