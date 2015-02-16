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
    public partial class Form3 : Form
    {
        public Form3()
        {
            InitializeComponent();
        }

        int index;

        private void button1_Click(object sender, EventArgs e)
        {
            if (index != -1)
            {
                string a = GlobalNames.Users[index].key;
                if (string.Compare(textBox2.Text,a,true) == 0)
                    MessageBox.Show("Ваш пароль: " + GlobalNames.Users[index].password);
                else
                    MessageBox.Show("Ответ неверный!");
            }

        }

        private void textBox1_Leave(object sender, EventArgs e)
        {
            string n = textBox1.Text;
            bool f = true;

            for (int i = 0; i < GlobalNames.Users.Count; i++)
            {
                if (string.Compare(GlobalNames.Users[i].name,n,true) == 0)
                    if (GlobalNames.Users[i].isPasswird == 1)
                    {
                        label3.Text = GlobalNames.Users[i].question + " ?";
                        f = false;
                        index = i;
                    }
                    else
                    {
                        label3.Text = "Аккаунт имеет свободный доступ";
                        f = false;
                    }
            }
            if (f)
            {
                MessageBox.Show("Пользователь " + n + " не найден!"); 
            }
        }

        private void Form3_Load(object sender, EventArgs e)
        {
            index = -1;
        }
    }
}
