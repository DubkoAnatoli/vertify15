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
    public partial class Form5 : Form
    {
        public Form5()
        {
            InitializeComponent();
        }

        private void tabControl1_DrawItem(object sender, DrawItemEventArgs e)
        {
            Graphics g;
            string sText;
            int iX;
            float iY;

            SizeF sizeText;
            TabControl ctlTab;

            ctlTab = (TabControl)sender;

            g = e.Graphics;

            sText = ctlTab.TabPages[e.Index].Text;
            sizeText = g.MeasureString(sText, ctlTab.Font);
            iX = e.Bounds.Left + 6;
            iY = e.Bounds.Top + (e.Bounds.Height - sizeText.Height) / 2;
            g.DrawString(sText, ctlTab.Font, Brushes.Black, iX, iY);
        }

        bool userChange;

        private void button1_Click(object sender, EventArgs e)
        {
            GlobalNames.clearD = GlobalNames.clearDream = GlobalNames.clearR = GlobalNames.clearOne = false;
            this.Close();
        }

        Font f;

        private void Form5_Load(object sender, EventArgs e)
        {
            this.Font = GlobalNames.f;
            
            this.radioButton1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            
            f = this.radioButton1.Font;
            
            userChange = false;

            if (GlobalNames.Users[GlobalNames.nomerUsera].isPasswird == 0)
            {
                checkBox1.Checked = false;
                textBox1.Enabled = false;
            }
            else
                checkBox1.Checked = true;

            panel1.Enabled = checkBox1.Checked;

             checkBox7.Checked = GlobalNames.rashodi[0];
             checkBox8.Checked = GlobalNames.rashodi[1];
             checkBox9.Checked = GlobalNames.rashodi[2];
             checkBox10.Checked = GlobalNames.rashodi[3];
             checkBox11.Checked = GlobalNames.rashodi[4];

             checkBox12.Checked = GlobalNames.dohodi[3];
             checkBox13.Checked = GlobalNames.dohodi[2];
             checkBox14.Checked = GlobalNames.dohodi[1];
             checkBox15.Checked = GlobalNames.dohodi[0];


        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            userChange = true;
            panel1.Enabled = checkBox1.Checked;
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            userChange = true;
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {
            userChange = true;
        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {
            userChange = true;
        }

        private void textBox4_TextChanged(object sender, EventArgs e)
        {
            userChange = true;
        }

        private void textBox5_TextChanged(object sender, EventArgs e)
        {
            userChange = true;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            GlobalNames.clearD = checkBox2.Checked;
            GlobalNames.clearR = checkBox3.Checked;
            GlobalNames.clearDream = checkBox4.Checked;
            GlobalNames.clearOne = checkBox5.Checked;

            bool write = false;

            bool error = false;

            if (userChange)
            {
                if (GlobalNames.Users[GlobalNames.nomerUsera].isPasswird == 1)//был пароль
                {
                    if (checkBox1.Checked)      //использовать пароль
                    {
                        if (textBox1.Text.Length != 0)   //был введён старый пароль
                        {
                            if (textBox2.Text.Length != 0 && textBox3.Text.Length != 0) //новый пароль не пуст
                            {
                                if (textBox2.Text == textBox3.Text)  // новые пароли совпадают
                                {
                                    if (textBox1.Text == GlobalNames.Users[GlobalNames.nomerUsera].password)    // если старый пароль совпадает
                                    {
                                        GlobalNames.Users[GlobalNames.nomerUsera].password = textBox2.Text;     // ставим новый пароль

                                        write = true;
                                    }
                                    else
                                    {
                                        MessageBox.Show("Старые пароли не совпадают!");
                                        error = true;
                                    }
                                }
                                else
                                {
                                    MessageBox.Show("Новые пароли не совпадают!");
                                    error = true;
                                }
                            }
                            else
                            {
                                MessageBox.Show("Необходимо ввести новый пароль!");
                                error = true;
                            }
                        }

                        if (textBox4.Text.Length != 0)  // был введён новый секретный вопрос
                        {
                            if (textBox5.Text.Length != 0)  //был введён новый ответ
                            {
                                GlobalNames.Users[GlobalNames.nomerUsera].question = textBox4.Text;
                                GlobalNames.Users[GlobalNames.nomerUsera].key = textBox5.Text;

                                write = true;
                            }
                            else
                            {
                                MessageBox.Show("Необходимо ввести ответ на новый секретный вопрос!");
                                error = true;
                            }
                        }
                        else
                            if (textBox5.Text.Length != 0)  //если введён ответ без вопроса
                            {
                                MessageBox.Show("Необходимо ввести новый секретный вопрос на данный ответ!");
                                error = true;
                            }

                    }
                    else
                    {
                        GlobalNames.Users[GlobalNames.nomerUsera].isPasswird = 0;
                        GlobalNames.Users[GlobalNames.nomerUsera].password = null;
                        GlobalNames.Users[GlobalNames.nomerUsera].question = null;
                        GlobalNames.Users[GlobalNames.nomerUsera].key = null;

                        write = true;
                    }
                }
                else            // пaроля не было
                {
                    if (checkBox1.Checked)
                    {
                        if (textBox2.Text.Length != 0 && textBox3.Text.Length != 0)     //если пароли не пустые
                        {
                            if (textBox2.Text == textBox3.Text)  //если пароли одинаковые
                            {
                                if (textBox4.Text.Length != 0)  // был введён секретный вопрос
                                {
                                    if (textBox5.Text.Length != 0)  //был введён ответ
                                    {
                                        GlobalNames.Users[GlobalNames.nomerUsera].isPasswird = 1;
                                        GlobalNames.Users[GlobalNames.nomerUsera].password = textBox2.Text;
                                        GlobalNames.Users[GlobalNames.nomerUsera].question = textBox4.Text;
                                        GlobalNames.Users[GlobalNames.nomerUsera].key = textBox5.Text;

                                        write = true;
                                    }
                                    else
                                    {
                                        MessageBox.Show("Необходимо ввести ответ на секретный вопрос!");
                                        error = true;
                                    }
                                }
                                else
                                {
                                    MessageBox.Show("Необходимо ввести секретный вопрос!");
                                    error = true;
                                }
                            }
                            else
                            {
                                MessageBox.Show("Пароли не совпадают!");
                                error = true;
                            }
                        }
                        else
                        {
                            MessageBox.Show("Необходимо ввести пароль!");
                            error = true;
                        }
                    }
                }
                
            }

            if (write)
            {
                // переписываем файл users.cfg
                using (StreamWriter r = File.CreateText("users.cfg"))
                {
                    for (int i = 0; i < GlobalNames.Users.Count; i++)
                    {
                        r.WriteLine(GlobalNames.Users[i].name);
                        r.WriteLine(GlobalNames.Users[i].isPasswird);
                        r.WriteLine(GlobalNames.Users[i].isAdmin);
                        r.WriteLine(GlobalNames.Users[i].password);
                        r.WriteLine(GlobalNames.Users[i].question);
                        r.WriteLine(GlobalNames.Users[i].key);
                    }
                    r.Close();
                }
                //userChange = false;
            }
            GlobalNames.rashodi[0] = checkBox7.Checked;
            GlobalNames.rashodi[1] = checkBox8.Checked;
            GlobalNames.rashodi[2] = checkBox9.Checked;
            GlobalNames.rashodi[3] = checkBox10.Checked;
            GlobalNames.rashodi[4] = checkBox11.Checked;

            GlobalNames.dohodi[3] = checkBox12.Checked;
            GlobalNames.dohodi[2] = checkBox13.Checked;
            GlobalNames.dohodi[1] = checkBox14.Checked;
            GlobalNames.dohodi[0] = checkBox15.Checked;

            if (!error)
            {
                this.Close();
                GlobalNames.f = f;
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
        }

        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {
            f = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
        }

        private void radioButton2_CheckedChanged(object sender, EventArgs e)
        {
            f = new System.Drawing.Font("Microsoft Sans Serif", 9.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
        }

        private void textBox6_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar != 8 && (e.KeyChar < 48 || e.KeyChar > 57) && e.KeyChar != 44)
                e.Handled = true;
            /*if (!Char.IsDigit(e.KeyChar) && e.KeyChar != 44)
                e.Handled = true; */
        }

    }
}
