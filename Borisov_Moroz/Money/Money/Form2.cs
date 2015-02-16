using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;

namespace Money
{
    public partial class Form2 : Form
    {
        public Form2()
        {
            InitializeComponent();
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            panel1.Enabled = checkBox1.Checked;
        }

        private void Form2_Load(object sender, EventArgs e)
        {
            toolTip1.SetToolTip(radioButton1, "Данный тип пользователя может просматривать личный бютжет других пользоватей");
            toolTip1.SetToolTip(radioButton2, "Данный тип пользователя не может просматривать личный бютжет других пользоватей");
        }

        private void button1_Click(object sender, EventArgs e)
        {
            bool f = true;

            if (textBox1.Text.Length == 0)
            {
                MessageBox.Show("Введите имя пользователя!");
            }
            else
            {

                for (int i = 0; i < GlobalNames.Users.Count; i++)
                {
                    if (string.Compare(GlobalNames.Users[i].name, textBox1.Text, true) == 0)
                    {
                        f = false;
                    }
                }
                if (f)
                {
                        User u = new User();

                        u.name = textBox1.Text;
                        if (radioButton1.Checked)
                            u.isAdmin = 1;
                        else
                            u.isAdmin = 0;

                        if (checkBox1.Checked == true)
                        {
                            if (textBox2.Text == textBox3.Text && textBox2.Text.Length != 0)
                            {
                                if (textBox4.Text.Length != 0 && textBox5.Text.Length != 0)
                                {
                                    u.isPasswird = 1;
                                    u.password = textBox2.Text;
                                    u.question = textBox4.Text;
                                    u.key = textBox5.Text;
                                    GlobalNames.Users.Add(u);

                                    using (StreamWriter r = File.AppendText("users.cfg"))
                                    {
                                        r.WriteLine(u.name);
                                        r.WriteLine(u.isPasswird);
                                        r.WriteLine(u.isAdmin);
                                        r.WriteLine(u.password);
                                        r.WriteLine(u.question);
                                        r.WriteLine(u.key);

                                        System.IO.Directory.CreateDirectory(textBox1.Text);

                                        using (StreamWriter m = File.CreateText( textBox1.Text + "\\" + textBox1.Text + ".plus"))   // доходы
                                        {
                                            m.Close();
                                        }
                                        using (StreamWriter m = File.CreateText(textBox1.Text + "\\" + textBox1.Text + ".minus"))   //расходы
                                        {
                                            m.Close();
                                        }
                                        using (StreamWriter m = File.CreateText(textBox1.Text + "\\" + textBox1.Text + ".dream"))   //мечта
                                        {
                                            m.Close();
                                        }
                                        using (StreamWriter m = File.CreateText(textBox1.Text + "\\" + textBox1.Text + ".one"))     //единовременные затраты
                                        {
                                            m.Close();
                                        }

                                        r.Close();
                                    }
                                    MessageBox.Show("Пользователь " + textBox1.Text + " успешно зарегистрирован!");
                                    this.Close();
                                }
                                else
                                {
                                    MessageBox.Show("Некорректно заданы секретный вопрос и / или ответ на секретный вопрос!");
                                }
                            }
                            else
                                MessageBox.Show("Пароли не совпадают!!!");
                        }
                        else
                        {
                            u.isPasswird = 0;
                            u.password = null;
                            u.question = null;
                            u.key = null;
                            GlobalNames.Users.Add(u);

                            using (StreamWriter r = File.AppendText("users.cfg"))
                            {
                                r.WriteLine(u.name);
                                r.WriteLine(u.isPasswird);
                                r.WriteLine(u.isAdmin);
                                r.WriteLine(u.password);
                                r.WriteLine(u.question);
                                r.WriteLine(u.key);

                                System.IO.Directory.CreateDirectory(textBox1.Text);

                                using (StreamWriter m = File.CreateText(textBox1.Text + "\\" + textBox1.Text + ".plus"))   // доходы
                                {
                                    m.Close();
                                }
                                using (StreamWriter m = File.CreateText(textBox1.Text + "\\" + textBox1.Text + ".minus"))   //расходы
                                {
                                    m.Close();
                                }
                                using (StreamWriter m = File.CreateText(textBox1.Text + "\\" + textBox1.Text + ".dream"))   //мечта
                                {
                                    m.Close();
                                }
                                using (StreamWriter m = File.CreateText(textBox1.Text + "\\" + textBox1.Text + ".one"))     //единовременные затраты
                                {
                                    m.Close();
                                }

                                r.Close();
                            }
                            MessageBox.Show("Пользователь " + textBox1.Text + " успешно зарегистрирован!");
                            this.Close();
                        }

                        
                }
                else
                    MessageBox.Show("Пользователь " + textBox1.Text + " уже зарегистрирован!");
            }
        }

    }
}
