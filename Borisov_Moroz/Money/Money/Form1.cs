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
    public partial class Form1 : Form
    {
        
        public Form1()
        {
            InitializeComponent();
        }

        void login(string name)
        {
            

            using (StreamReader m = File.OpenText(name + "\\" + name + ".plus"))   // доходы
            {
                string s;
                for (int x = 0; ; x++)
                {
                    if ((s = m.ReadLine()) == null || s.Length == 0)
                        break;

                    List<String> st = new List<String>();

                        st.Add(s);

                    for (int y = 0; y < 3; y++)
                    {
                        s = m.ReadLine();
                            st.Add(s);
                    }

                    st.Add(name);

                    GlobalNames.plus.Add(st);

                }
                m.Close();
            }

            using (StreamReader m = File.OpenText(name + "\\" + name + ".minus"))   // расходы
            {
                string s;
                for (int x = 0; ; x++)
                {
                    if ((s = m.ReadLine()) == null || s.Length == 0)
                        break;

                    List<String> st = new List<String>();

                        st.Add(s);


                    for (int y = 0; y < 3; y++)
                    {

                        s = m.ReadLine();

                            st.Add(s);
                    }

                    st.Add(name);

                    GlobalNames.minus.Add(st);

                }
                m.Close();
            }

            using (StreamReader m = File.OpenText(name + "\\" + name + ".dream"))   // мечта
            {
                string s;
                for (int x = 0; ; x++)
                {
                    if ((s = m.ReadLine()) == null || s.Length == 0)
                        break;

                    List<String> st = new List<String>();

                        st.Add(s);

                    for (int y = 0; y < 5; y++)
                    {
                        s = m.ReadLine();
                            st.Add(s);
                    }

                    st.Add(name);

                    GlobalNames.dream.Add(st);

                }
                m.Close();
            }

            using (StreamReader m = File.OpenText(name + "\\" + name + ".one"))   //единовременные затраты
            {
                string s;
                for (int x = 0; ; x++)
                {
                    if ((s = m.ReadLine()) == null || s.Length == 0)
                        break;

                    List<String> st = new List<String>();

                    st.Add(s);

                    for (int y = 0; y < 2; y++)
                    {
                        s = m.ReadLine();
                        st.Add(s);
                    }

                    GlobalNames.one.Add(st);

                }
                m.Close();
            }
        }

        void AllClear()
        {
            GlobalNames.minus.Clear();
            GlobalNames.dream.Clear();
            GlobalNames.plus.Clear();
            GlobalNames.one.Clear();

        }

        private void button1_Click(object sender, EventArgs e)
        {
            string n = textBox1.Text;
            string pass = textBox2.Text;

            GlobalNames.clearD = GlobalNames.clearDream = GlobalNames.clearR = GlobalNames.clearOne = false;

            bool f = true;

            if (n.Length == 0)
            {
                MessageBox.Show("Неправильное имя пользователя и / или пароль!");
            }
            else
            {
                for (int i = 0; i < GlobalNames.Users.Count; i++)
                {
                    if (string.Compare(GlobalNames.Users[i].name, n, true) == 0)
                    {
                        if (GlobalNames.Users[i].isPasswird == 1 && GlobalNames.Users[i].password == pass)
                        {
                            f = false;

                            GlobalNames.nomerUsera = i;
                            GlobalNames.name = n;

                            if (GlobalNames.Users[i].isAdmin == 1)
                            {
                                GlobalNames.admin = true;
                                for (int j = 0; j < GlobalNames.Users.Count; j++)
                                    login(GlobalNames.Users[j].name);
                            }
                            else
                            {
                                GlobalNames.admin = false;
                                login(textBox1.Text);
                            }

                            Form Form4 = new Form4();
                            Form4.ShowDialog();

                            AllClear();
                            
                            break;
                        }
                        else
                        {
                            if (GlobalNames.Users[i].isPasswird == 0)
                            {
                                f = false;

                                GlobalNames.nomerUsera = i;
                                GlobalNames.name = n;

                                if (GlobalNames.Users[i].isAdmin == 1)
                                {
                                    GlobalNames.admin = true;
                                    for (int j = 0; j < GlobalNames.Users.Count; j++)
                                        login(GlobalNames.Users[j].name);
                                }
                                else
                                {
                                    GlobalNames.admin = false;
                                    login(textBox1.Text);
                                }

                                Form Form4 = new Form4();
                                Form4.ShowDialog();

                                AllClear();

                                break;
                            }
                            else
                            {
                                MessageBox.Show("Неправильное имя пользователя и / или пароль!");
                                f = false;
                                break;
                            }
                        }

                    }
                }
                if (f)
                {
                    MessageBox.Show("Неправильное имя пользователя и / или пароль!");
                }
            }
            
        }

        private void linkLabel2_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Form Form2 = new Form2();
            Form2.ShowDialog();
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Form Form3 = new Form3();
            Form3.ShowDialog();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
          /*  
            using (StreamWriter r = File.CreateText("users.cfg"))
            {
                r.WriteLine("5");
                r.Close();
            }*/
            

            
            using(StreamReader r = File.OpenText("users.cfg"))
            {
                
                string name;

                while ((name = r.ReadLine()) != null && name.Length != 0)
                {
                    User u = new User();
                    u.name = name;
                    u.isPasswird = Convert.ToInt32(r.ReadLine());
                    u.isAdmin = Convert.ToInt32(r.ReadLine());
                    u.password = r.ReadLine();
                    u.question = r.ReadLine();
                    u.key = r.ReadLine();

                    listBox1.Items.Add(u.name);
                    listBox1.Items.Add(u.isPasswird);
                    listBox1.Items.Add(u.isAdmin);
                    listBox1.Items.Add(u.password);
                    listBox1.Items.Add(u.question);
                    listBox1.Items.Add(u.key);
                    GlobalNames.Users.Add(u);
                }
                listBox1.Items.Add(GlobalNames.Users.Count);
                

                r.Close();
            }
        }

    }
}
