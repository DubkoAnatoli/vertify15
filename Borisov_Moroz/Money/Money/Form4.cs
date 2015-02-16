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
    public partial class Form4 : Form
    {
        public Form4()
        {
            InitializeComponent();
        }

        void haveSpendNeed()
        {
            double have = 0;        //есть всего денег
            double spend = 0;       //потрачено
            double need = 0;        //Неоплаченные счета 
            double save = 0;        //отложено 

            int i;

            for (i = 0; i < dataGridView1.Rows.Count - 1; i++)
            {
                if (Convert.ToString(dataGridView1.Rows[i].Cells[0].Value) != "Дата оплаты")
                    spend += Convert.ToDouble(dataGridView1.Rows[i].Cells[3].Value);
                else
                    need += Convert.ToDouble(dataGridView1.Rows[i].Cells[3].Value);
            }

            for (i = 0; i < dataGridView3.Rows.Count - 1; i++)
            {
                have += Convert.ToDouble(dataGridView3.Rows[i].Cells[3].Value);
            }

            for (i = 0; i < dataGridView2.Rows.Count-1; i++ )
                save += Convert.ToDouble(dataGridView2.Rows[i].Cells[4].Value);

                if (have - spend - save > 0)
                    toolStripStatusLabel2.ForeColor = System.Drawing.Color.Green;
                else
                    toolStripStatusLabel2.ForeColor = System.Drawing.Color.Red;
            this.toolStripStatusLabel2.Text = Convert.ToString(have - spend - save);
            this.toolStripStatusLabel4.Text = Convert.ToString(need);
        }

        void saveUser(string name)
        {
            using (StreamWriter m = File.CreateText(name + "\\" + name + ".minus"))   // расходы
            {

                for (int i = 0; i < dataGridView1.Rows.Count - 1; i++)
                {
                    for (int j = 0; j < 4; j++)
                    {
                        if (Convert.ToString(dataGridView1.Rows[i].Cells[j].Value).Length == 0)
                            m.WriteLine("###");
                        else
                            m.WriteLine(dataGridView1.Rows[i].Cells[j].Value);
                    }
                }

                m.Close();
            }

            using (StreamWriter m = File.CreateText(name + "\\" + name + ".plus"))   //доходы
            {
                for (int i = 0; i < dataGridView3.Rows.Count - 1; i++)
                {
                    for (int j = 0; j < 4; j++)
                    {
                        if (Convert.ToString(dataGridView3.Rows[i].Cells[j].Value).Length == 0)
                            m.WriteLine("###");
                        else
                            m.WriteLine(dataGridView3.Rows[i].Cells[j].Value);
                    }
                }
                m.Close();
            }

            using (StreamWriter m = File.CreateText(name + "\\" + name + ".dream"))   //мечта
            {
                for (int i = 0; i < dataGridView2.Rows.Count - 1; i++)
                {
                    for (int j = 0; j < 6; j++)
                    {
                        if (Convert.ToString(dataGridView2.Rows[i].Cells[j].Value).Length == 0)
                            m.WriteLine("###");
                        else
                            m.WriteLine(dataGridView2.Rows[i].Cells[j].Value);
                    }
                }
                m.Close();
            }
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

        private void настройкаПрограммыToolStripMenuItem_Click(object sender, EventArgs e)
        {
            GlobalNames.clearD = GlobalNames.clearDream = GlobalNames.clearR = GlobalNames.clearOne = false;

            Form Form5 = new Form5();
            Form5.ShowDialog();

            if (GlobalNames.clearD) //доходы
            {
                dataGridView3.Rows.Clear();
            }

            if (GlobalNames.clearR) //расходы
            {
                dataGridView1.Rows.Clear();
            }

            if (GlobalNames.clearDream) //мечты
            {
                dataGridView2.Rows.Clear();
            }

            if (GlobalNames.clearOne)   //разовые траты
            {
                GlobalNames.one.Clear();
            }

            this.Font = GlobalNames.f;

            button4.Visible = GlobalNames.rashodi[0];

            checkBox1.Visible = textBox5.Visible = textBox6.Visible = label1.Visible = GlobalNames.rashodi[1];

            checkBox2.Visible = textBox1.Visible = GlobalNames.rashodi[2];

            checkBox3.Visible = comboBox1.Visible = GlobalNames.rashodi[3];

            checkBox4.Visible = textBox2.Visible = textBox7.Visible = label6.Visible = GlobalNames.rashodi[4];



            checkBox13.Visible = textBox8.Visible = textBox9.Visible = label5.Visible = GlobalNames.dohodi[0];

            checkBox14.Visible = textBox4.Visible = GlobalNames.dohodi[1];

            checkBox15.Visible = comboBox2.Visible = GlobalNames.dohodi[2];

            checkBox16.Visible = textBox3.Visible = textBox10.Visible = label7.Visible = GlobalNames.dohodi[3];

            GlobalNames.clearD = GlobalNames.clearDream = GlobalNames.clearR = GlobalNames.clearOne = false;


        }

        private void checkBox6_CheckedChanged(object sender, EventArgs e)   //все категории
        {
            panel2.Enabled = !checkBox6.Checked;
            panel3.Enabled = !checkBox6.Checked;
            checkBox22.Checked = checkBox7.Checked = checkBox8.Checked = checkBox9.Checked = checkBox10.Checked = checkBox11.Checked = checkBox12.Checked = checkBox17.Checked = checkBox18.Checked = checkBox19.Checked = checkBox6.Checked;
                
        }

        private void Form4_Load(object sender, EventArgs e)
        {

            GlobalNames.kurs = 1.0;
            int i;
            for ( i = 0; i < 5; i++)
            {
                GlobalNames.dohodi[i] = GlobalNames.rashodi[i] = true;
            }

                this.Text += GlobalNames.name;

            

            double have = 0;        //есть всего денег
            double spend = 0;       //потрачено
            double need = 0;        //Неоплаченные счета 

            


            for (i = 0; i < GlobalNames.minus.Count ; i++)
            {
                dataGridView1.Rows.Add();
                for (int j = 0;j < 5; j++)
                {
                    dataGridView1.Rows[i].Cells[j].Value = GlobalNames.minus[i][j];
                    if (j == 3 && GlobalNames.minus[i][0] != "Дата оплаты")
                    {
                        spend += Convert.ToDouble(GlobalNames.minus[i][j]);
                    }
                    if (j == 3 && GlobalNames.minus[i][0] == "Дата оплаты")
                    {
                        need += Convert.ToDouble(GlobalNames.minus[i][j]);
                    }

                }
            }


            for (i = 0; i < GlobalNames.plus.Count; i++)
            {
                dataGridView3.Rows.Add();
                for (int j = 0; j < 5; j++)
                {
                    dataGridView3.Rows[i].Cells[j].Value = GlobalNames.plus[i][j];
                    if (j == 3)
                    {
                        have += Convert.ToDouble(GlobalNames.plus[i][j]);
                    }
                }
            }

            for (i = 0; i < GlobalNames.dream.Count; i++)
            {
                dataGridView2.Rows.Add();
                for (int j = 0; j < 7; j++)
                {
                    dataGridView2.Rows[i].Cells[j].Value = GlobalNames.dream[i][j];
                }
            }

            if (have - spend > 0)
                toolStripStatusLabel2.ForeColor = System.Drawing.Color.Green;
            else
                toolStripStatusLabel2.ForeColor = System.Drawing.Color.Red;

            this.toolStripStatusLabel2.Text = Convert.ToString(have - spend);
            this.toolStripStatusLabel4.Text = Convert.ToString(need);


            if (GlobalNames.Users[GlobalNames.nomerUsera].isAdmin == 1)
            {
                dataGridView1.ReadOnly = true;
                dataGridView1.AllowUserToAddRows = false;
                dataGridView1.AllowUserToDeleteRows = false;
                dataGridView1.Columns[4].Visible = true;

                dataGridView3.ReadOnly = true;
                dataGridView3.AllowUserToAddRows = false;
                dataGridView3.AllowUserToDeleteRows = false;
                dataGridView3.Columns[4].Visible = true;

                dataGridView2.ReadOnly = true;
                dataGridView2.AllowUserToAddRows = false;
                dataGridView2.AllowUserToDeleteRows = false;
                dataGridView2.Columns[6].Visible = true;

                this.Text += " (Только чтение)";
                сохранитьToolStripMenuItem.Enabled = false;
                сохранитьИВыйтиToolStripMenuItem.Enabled = false;
                button4.Enabled = false;
                
            }
            else
            {
                checkBox21.Visible = checkBox20.Visible = textBox13.Visible = textBox14.Visible = false;
            }

        }

        private void сохранитьToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string name = GlobalNames.name;

            saveUser(name);
            
        }

        void searchText()
        {
            for (int i = 0; i < dataGridView1.Rows.Count - 1; i++)
                if (Convert.ToString(dataGridView1.Rows[i].Cells[1].Value).ToLower().IndexOf(textBox1.Text.ToLower()) != -1)
                {
                   // dataGridView1.Rows[i].Visible = true;
                }
                else
                    dataGridView1.Rows[i].Visible = false;
        }

        void searchName()
        {
            for (int i = 0; i < dataGridView1.Rows.Count - 1; i++)
                if (Convert.ToString(dataGridView1.Rows[i].Cells[4].Value).ToLower().IndexOf(textBox13.Text.ToLower()) != -1)
                {
                    // dataGridView1.Rows[i].Visible = true;
                }
                else
                    dataGridView1.Rows[i].Visible = false;
        }

        void searchPrise()
        {
            double ot;
            double doo;

            if (textBox2.Text.Length == 0)
                ot = 0;
            else
                ot = Convert.ToDouble(textBox2.Text);

            if (textBox7.Text.Length == 0)
                doo = 9999999;
            else
                doo = Convert.ToDouble(textBox7.Text);

            for (int i = 0; i < dataGridView1.Rows.Count - 1; i++)
                if (ot <= Convert.ToDouble(dataGridView1.Rows[i].Cells[3].Value) && doo >= Convert.ToDouble(dataGridView1.Rows[i].Cells[3].Value))
                {
                    //dataGridView1.Rows[i].Visible = true;
                }
                else
                    dataGridView1.Rows[i].Visible = false;
        }

        void searchCombo()
        {
            for (int i = 0; i < dataGridView1.Rows.Count - 1; i++)
                if (Convert.ToString(dataGridView1.Rows[i].Cells[2].Value).ToLower().IndexOf(comboBox1.Text.ToLower()) != -1)
                {
                //    dataGridView1.Rows[i].Visible = true;
                }
                else
                    dataGridView1.Rows[i].Visible = false;
        }

        void searchDate()
        {
            string ot = textBox5.Text;
            string doo = textBox6.Text;

            if (ot.Length == 0)
                ot = "0";

            if (doo.Length == 0)
                doo = "9999999";

            for (int i = 0; i < dataGridView1.Rows.Count - 1; i++)
                if (string.Compare(ot, Convert.ToString(dataGridView1.Rows[i].Cells[0].Value), true) < 0 && string.Compare(doo, Convert.ToString(dataGridView1.Rows[i].Cells[0].Value), true) > 0)
                {
              //      dataGridView1.Rows[i].Visible = true;
                }
                else
                    dataGridView1.Rows[i].Visible = false;
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            textBox5.Enabled = textBox6.Enabled = checkBox1.Checked;
        }

        private void checkBox2_CheckedChanged(object sender, EventArgs e)
        {
            textBox1.Enabled = checkBox2.Checked;
            if (checkBox2.Checked == false)
            {
                textBox1.Text = "";
            }
        }

        private void checkBox3_CheckedChanged(object sender, EventArgs e)
        {
            comboBox1.Enabled = checkBox3.Checked;
        }

        private void checkBox4_CheckedChanged(object sender, EventArgs e)
        {
            textBox2.Enabled = textBox7.Enabled = checkBox4.Checked;
            textBox7.Text = textBox2.Text = "";

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
           /* if (!checkBox1.Checked && !checkBox3.Checked && !checkBox4.Checked)
            {
                for (int i = 0; i < dataGridView1.Rows.Count - 1; i++)
                    dataGridView1.Rows[i].Visible = false;
                searchText();
            }
            else
                for (int i = 0; i < dataGridView1.Rows.Count - 1; i++)
                    dataGridView1.Rows[i].Visible = true;*/
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            /*if (!checkBox1.Checked && !checkBox2.Checked && !checkBox4.Checked)
            {
                for (int i = 0; i < dataGridView1.Rows.Count - 1; i++)
                    dataGridView1.Rows[i].Visible = false;
                searchCombo();
            }
            else
                for (int i = 0; i < dataGridView1.Rows.Count - 1; i++)
                    dataGridView1.Rows[i].Visible = true;*/
        }

        private void button5_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < dataGridView1.Rows.Count - 1; i++)
                dataGridView1.Rows[i].Visible = true;
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {
            /*if (!checkBox1.Checked && !checkBox2.Checked && !checkBox3.Checked)
            {
                for (int i = 0; i < dataGridView1.Rows.Count - 1; i++)
                    dataGridView1.Rows[i].Visible = false;
                searchPrise();
            }
            else
                for (int i = 0; i < dataGridView1.Rows.Count - 1; i++)
                    dataGridView1.Rows[i].Visible = true;*/
        }

        private void textBox7_TextChanged(object sender, EventArgs e)
        {
          /*  if (!checkBox1.Checked && !checkBox2.Checked && !checkBox3.Checked)
            {
                for (int i = 0; i < dataGridView1.Rows.Count - 1; i++)
                    dataGridView1.Rows[i].Visible = false;
                searchPrise();
            }
            else
                for (int i = 0; i < dataGridView1.Rows.Count - 1; i++)
                    dataGridView1.Rows[i].Visible = true;*/
        }

        void all()
        {
            if (checkBox1.Checked == true)
                searchDate();
            if (checkBox2.Checked == true)
                searchText();
            if (checkBox3.Checked == true)
                searchCombo();
            if (checkBox4.Checked == true)
                searchPrise();
            if(checkBox20.Checked == true)
                searchName();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < dataGridView1.Rows.Count - 1; i++)
                dataGridView1.Rows[i].Visible = true;
            all();

        }

        private void textBox5_TextChanged(object sender, EventArgs e)
        {
           /* if (!checkBox2.Checked && !checkBox3.Checked && !checkBox4.Checked)
            {
                for (int i = 0; i < dataGridView1.Rows.Count - 1; i++)
                    dataGridView1.Rows[i].Visible = false;
                searchDate();
            }
            else
                for (int i = 0; i < dataGridView1.Rows.Count - 1; i++)
                    dataGridView1.Rows[i].Visible = true;*/
        }

        private void textBox6_TextChanged(object sender, EventArgs e)
        {
           /* if (!checkBox2.Checked && !checkBox3.Checked && !checkBox4.Checked)
            {
                for (int i = 0; i < dataGridView1.Rows.Count - 1; i++)
                    dataGridView1.Rows[i].Visible = false;
                searchDate();
            }
            else
                for (int i = 0; i < dataGridView1.Rows.Count - 1; i++)
                    dataGridView1.Rows[i].Visible = true;*/

        }

        void _searchDate()
        {
            string ot = textBox8.Text;
            string doo = textBox9.Text;

            if (ot.Length == 0)
                ot = "0";

            if (doo.Length == 0)
                doo = "9999999";

            for (int i = 0; i < dataGridView3.Rows.Count - 1; i++)
                if (string.Compare(ot, Convert.ToString(dataGridView3.Rows[i].Cells[0].Value), true) < 0 && string.Compare(doo, Convert.ToString(dataGridView3.Rows[i].Cells[0].Value), true) > 0)
                {
                    //      dataGridView1.Rows[i].Visible = true;
                }
                else
                    dataGridView3.Rows[i].Visible = false;
        }

        void _searchText()
        {
            for (int i = 0; i < dataGridView3.Rows.Count - 1; i++)
                if (Convert.ToString(dataGridView3.Rows[i].Cells[1].Value).ToLower().IndexOf(textBox4.Text.ToLower()) != -1)
                {
                    // dataGridView1.Rows[i].Visible = true;
                }
                else
                    dataGridView3.Rows[i].Visible = false;
        }

        void _searchName()
        {
            for (int i = 0; i < dataGridView3.Rows.Count - 1; i++)
                if (Convert.ToString(dataGridView3.Rows[i].Cells[4].Value).ToLower().IndexOf(textBox14.Text.ToLower()) != -1)
                {
                    // dataGridView1.Rows[i].Visible = true;
                }
                else
                    dataGridView3.Rows[i].Visible = false;
        }

        void _searchPrise()
        {
            double ot;
            double doo;

            if (textBox3.Text.Length == 0)
                ot = 0;
            else
                ot = Convert.ToDouble(textBox3.Text);

            if (textBox10.Text.Length == 0)
                doo = 9999999;
            else
                doo = Convert.ToDouble(textBox10.Text);

            for (int i = 0; i < dataGridView3.Rows.Count - 1; i++)
                if (ot <= Convert.ToDouble(dataGridView3.Rows[i].Cells[3].Value) && doo >= Convert.ToDouble(dataGridView3.Rows[i].Cells[3].Value))
                {
                    //dataGridView1.Rows[i].Visible = true;
                }
                else
                    dataGridView3.Rows[i].Visible = false;
        }

        void _searchCombo()
        {
            for (int i = 0; i < dataGridView3.Rows.Count - 1; i++)
                if (Convert.ToString(dataGridView3.Rows[i].Cells[2].Value).ToLower().IndexOf(comboBox2.Text.ToLower()) != -1)
                {
                    //    dataGridView1.Rows[i].Visible = true;
                }
                else
                    dataGridView3.Rows[i].Visible = false;
        }

        void _all()
        {
            if (checkBox13.Checked == true)
                _searchDate();
            if (checkBox14.Checked == true)
                _searchText();
            if (checkBox15.Checked == true)
                _searchCombo();
            if (checkBox16.Checked == true)
                _searchPrise();
            if (checkBox21.Checked == true)
                _searchName();
        }

        private void button6_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < dataGridView3.Rows.Count - 1; i++)
                dataGridView3.Rows[i].Visible = true;
            _all();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < dataGridView3.Rows.Count - 1; i++)
                dataGridView3.Rows[i].Visible = true;
        }

        private void checkBox13_CheckedChanged(object sender, EventArgs e)
        {
            textBox8.Enabled = textBox9.Enabled = checkBox13.Checked;
        }

        private void checkBox14_CheckedChanged(object sender, EventArgs e)
        {
            textBox4.Enabled = checkBox14.Checked;
        }

        private void checkBox16_CheckedChanged(object sender, EventArgs e)
        {
            textBox10.Enabled = textBox3.Enabled = checkBox16.Checked;
        }

        private void button4_Click(object sender, EventArgs e)
        {
            GlobalNames.rezalt = false;
            Form6 form = new Form6();
            form.ShowDialog();

            int m = dataGridView1.Rows.Count - 1;

            if (GlobalNames.rezalt)
            {
                for (int i = 0; i < GlobalNames.one.Count; i++, m++)
                {
                    dataGridView1.Rows.Add();

                    dataGridView1.Rows[m].Cells[0].Value = "Дата оплаты";
                    for (int j = 1; j < 4; j++)
                    {
                        dataGridView1.Rows[m].Cells[j].Value = GlobalNames.one[i][j-1];
                    }
                }
            }
        }
        bool on = false;

        void addDream()
        {
            if (GlobalNames.Users[GlobalNames.nomerUsera].isAdmin != 1)
            {
                for (int i = 0; i < dataGridView1.Rows.Count - 1; )
                {
                    if (Convert.ToString(dataGridView1.Rows[i].Cells[2].Value) == "Мечта" && Convert.ToString(dataGridView1.Rows[i].Cells[0].Value) == "Дата оплаты")
                    {
                        dataGridView1.Rows.RemoveAt(i);
                    }
                    else
                        i += 1;
                }

                for (int i = 0; i < dataGridView2.Rows.Count - 1; i++)
                {
                    dataGridView1.Rows.Add();
                    dataGridView1.Rows[dataGridView1.Rows.Count - 2].Cells[0].Value = "Дата оплаты";
                    dataGridView1.Rows[dataGridView1.Rows.Count - 2].Cells[1].Value = dataGridView2.Rows[i].Cells[0].Value;
                    dataGridView1.Rows[dataGridView1.Rows.Count - 2].Cells[2].Value = "Мечта";
                    dataGridView1.Rows[dataGridView1.Rows.Count - 2].Cells[3].Value = dataGridView2.Rows[i].Cells[3].Value;
                }
            }
        }

        private void dataGridView2_CellValueChanged(object sender, DataGridViewCellEventArgs e)     //мечта
        {
            int i, j;
            double prise = 0;
            double have = 0;
            
            
            if (on)
            {
                i = dataGridView2.CurrentCell.RowIndex;
                j = dataGridView2.CurrentCell.ColumnIndex;

               // MessageBox.Show(Convert.ToString(i) + " " + Convert.ToString(j));

                if (j == 3)
                {
                    prise = Convert.ToDouble(dataGridView2.Rows[i].Cells[j].Value);
                    have = Convert.ToDouble(dataGridView2.Rows[i].Cells[j+1].Value);
                    if (prise != 0 && have != 0)
                    {
                        dataGridView2.Rows[i].Cells[j + 2].Value = Convert.ToString((have * 100) / prise);
                    }
                    else
                        dataGridView2.Rows[i].Cells[j + 2].Value = "0";
                }
                if (j == 4)
                {
                    prise = Convert.ToDouble(dataGridView2.Rows[i].Cells[j - 1].Value);
                    have = Convert.ToDouble(dataGridView2.Rows[i].Cells[j].Value);
                    if (prise != 0 && have != 0)
                    {
                        dataGridView2.Rows[i].Cells[j + 1].Value = Convert.ToString((have * 100) / prise);
                    }
                    else
                        dataGridView2.Rows[i].Cells[j + 1].Value = "0";
                }

                if (Convert.ToString(dataGridView2.Rows[i].Cells[0].Value).Length != 0 && Convert.ToString(dataGridView2.Rows[i].Cells[1].Value).Length != 0 && Convert.ToString(dataGridView2.Rows[i].Cells[2].Value).Length != 0 && Convert.ToString(dataGridView2.Rows[i].Cells[3].Value).Length != 0 && Convert.ToString(dataGridView2.Rows[i].Cells[4].Value).Length != 0 && Convert.ToString(dataGridView2.Rows[i].Cells[5].Value).Length != 0 )
                {
                    addDream();
                    haveSpendNeed();
                }
            }
        }

        private void tabPage3_Enter(object sender, EventArgs e)
        {
                on = true;
        }

        private void dataGridView1_CellValueChanged(object sender, DataGridViewCellEventArgs e)     //расход
        {

            if (on1)
            {
                haveSpendNeed();
            }
        }

        bool on1 = false;

        private void tabPage1_Enter(object sender, EventArgs e)
        {
            on1 = true;
        }

        private void dataGridView3_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            if (on1)
            {
                haveSpendNeed();
            }
        }

        private void сохранитьИВыйтиToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string name = GlobalNames.name;

            saveUser(name);

            this.Close();
        }

        private void выйтиБезСохраненияToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void справкаToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form9 form = new Form9();
            form.ShowDialog();
        }

        private void tabPage5_Enter(object sender, EventArgs e)
        {
            panel3.Visible = radioButton5.Checked;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            double all = 0;
            double max = -1;

            double eat = 0;
            double home = 0;
            double clothes = 0;
            double car = 0;
            double doctor = 0;
            double dreem = 0;

            double work = 0;
            double bank = 0;

            double other = 0;

            int step = 40;
            double k = 1;

			 IntPtr hwnd = panel1.Handle;
			 Graphics g = Graphics.FromHwnd( hwnd );
			 Brush brushWhite = new SolidBrush(Color.White);

             Brush brushGreen = new SolidBrush(Color.Green);
             Brush brushOrange = new SolidBrush(Color.Orange);
             Brush brushBlue = new SolidBrush(Color.Blue);
             Brush brushRed = new SolidBrush(Color.Red);
             Brush brushAqua = new SolidBrush(Color.DarkKhaki);
             Brush brushCoral = new SolidBrush(Color.Coral);
             Brush brushBlack = new SolidBrush(Color.Black);
             Brush brushDreem = new SolidBrush(Color.OliveDrab);

             Pen bluePen = new Pen(Color.Blue);
			 Pen blackPen = new Pen(Color.Black,2);
             Pen blackPen1 = new Pen(Color.Black);
			 Pen redPen = new Pen(Color.Red);
             g.FillRectangle(brushWhite, new RectangleF(0, 0, panel1.Width, panel1.Height));

             

            

           // g.FillRectangle(brushGreen, 30, 300, 60, 100); //еда

             /*g.DrawLine(blackPen1, 0, 380, 900, 380);
             /*g.FillPie(brushGreen, 10, 10, 500, 500, 0, 20);
             g.FillPie(brushYellow, 10, 10, 500, 500, 20, 50);

             g.DrawPie(redPen, 10, 10, 500, 500, 0, 50 );
             g.DrawPie(bluePen, 10, 10, 500, 500, 50, 20);
                Еда и продукты
                Жильё
                Одежда
                Транспорт
                Медицина
                Другое
              */


             if (radioButton4.Checked && (dataGridView1.Rows.Count - 1) != 0)    //расход
             {
                 for (int i = 0; i < dataGridView1.Rows.Count - 1; i++)
                 {
                     if (checkBox7.Checked && Convert.ToString(dataGridView1.Rows[i].Cells[2].Value) == "Еда и продукты" && Convert.ToString(dataGridView1.Rows[i].Cells[0].Value) != "Дата оплаты")
                     {
                         eat += Convert.ToDouble(dataGridView1.Rows[i].Cells[3].Value);
                         all += Convert.ToDouble(dataGridView1.Rows[i].Cells[3].Value);
                     }
                     if (checkBox8.Checked && Convert.ToString(dataGridView1.Rows[i].Cells[2].Value) == "Жильё" && Convert.ToString(dataGridView1.Rows[i].Cells[0].Value) != "Дата оплаты")
                     {
                         home += Convert.ToDouble(dataGridView1.Rows[i].Cells[3].Value);
                         all += Convert.ToDouble(dataGridView1.Rows[i].Cells[3].Value);
                     }
                     if (checkBox9.Checked && Convert.ToString(dataGridView1.Rows[i].Cells[2].Value) == "Одежда" && Convert.ToString(dataGridView1.Rows[i].Cells[0].Value) != "Дата оплаты")
                     {
                         clothes += Convert.ToDouble(dataGridView1.Rows[i].Cells[3].Value);
                         all += Convert.ToDouble(dataGridView1.Rows[i].Cells[3].Value);
                     }
                     if (checkBox10.Checked && Convert.ToString(dataGridView1.Rows[i].Cells[2].Value) == "Транспорт" && Convert.ToString(dataGridView1.Rows[i].Cells[0].Value) != "Дата оплаты")
                     {
                         car += Convert.ToDouble(dataGridView1.Rows[i].Cells[3].Value);
                         all += Convert.ToDouble(dataGridView1.Rows[i].Cells[3].Value);
                     }
                     if (checkBox11.Checked && Convert.ToString(dataGridView1.Rows[i].Cells[2].Value) == "Медицина" && Convert.ToString(dataGridView1.Rows[i].Cells[0].Value) != "Дата оплаты")
                     {
                         doctor += Convert.ToDouble(dataGridView1.Rows[i].Cells[3].Value);
                         all += Convert.ToDouble(dataGridView1.Rows[i].Cells[3].Value);
                     }
                     if (checkBox12.Checked && Convert.ToString(dataGridView1.Rows[i].Cells[2].Value) == "Другое" && Convert.ToString(dataGridView1.Rows[i].Cells[0].Value) != "Дата оплаты")
                     {
                         other += Convert.ToDouble(dataGridView1.Rows[i].Cells[3].Value);
                         all += Convert.ToDouble(dataGridView1.Rows[i].Cells[3].Value);
                     }
                     if (checkBox22.Checked && Convert.ToString(dataGridView1.Rows[i].Cells[2].Value) == "Мечта" && Convert.ToString(dataGridView1.Rows[i].Cells[0].Value) != "Дата оплаты")
                     {
                         dreem += Convert.ToDouble(dataGridView1.Rows[i].Cells[3].Value);
                         all += Convert.ToDouble(dataGridView1.Rows[i].Cells[3].Value);
                     }
                 }

                 if (radioButton1.Checked)  //столбиковая
                 {

                     if (eat > max)
                         max = eat;

                     if (home > max)
                         max = home;

                     if (clothes > max)
                         max = clothes;

                     if (car > max)
                         max = car;

                     if (doctor > max)
                         max = doctor;

                     if (dreem > max)
                         max = dreem;

                     if (other > max)
                         max = other;

                     

                     while (max > 370)
                     {
                         eat *= 0.9;
                         home *= 0.9;
                         clothes *= 0.9;
                         car *= 0.9;
                         doctor *= 0.9;
                         dreem *= 0.9;
                         max *= 0.9;
                         all *= 0.9;
                         other *= 0.9;
                         k *= 0.9;
                     }


                     for (int i = 380; i > 10; )
                     {
                         g.DrawLine(blackPen1, 0, i, 900, i);
                         g.DrawString(Convert.ToString( Convert.ToInt32( (400 - i) / k )), this.Font, brushBlack, new Point(0, i-15));
                         i -= 20;
                     }

                     g.FillRectangle(brushWhite, 450, 10, 200, 130);
                     g.DrawRectangle(redPen, 450, 10, 200, 130);

                     if (checkBox7.Checked)
                     {
                         g.FillRectangle(brushGreen, step, Convert.ToInt32(400 - eat), 30, 400 - Convert.ToInt32(400 - eat)); //еда
                         g.DrawString("Еда и продукты", this.Font, brushBlack, new Point(step - 25, 410));
                         step += 60;
                         g.DrawString("Еда и продукты " + Convert.ToString(Convert.ToInt32((eat * 100) / all + 0.2)) + " %", this.Font, brushGreen, new Point(465, 15));
                     }

                     if (checkBox8.Checked)
                     {
                         g.FillRectangle(brushOrange, step, Convert.ToInt32(400 - home), 30, 400 - Convert.ToInt32(400 - home)); //еда
                         g.DrawString("Жильё", this.Font, brushBlack, new Point(step, 410));
                         g.DrawString("Жильё " + Convert.ToString(Convert.ToInt32((home * 100) / all + 0.2)) + " %", this.Font, brushOrange, new Point(465, 30));
                         step += 60;
                     }

                     if (checkBox9.Checked)
                     {
                         g.FillRectangle(brushBlue, step, Convert.ToInt32(400 - clothes), 30, 400 - Convert.ToInt32(400 - clothes)); //еда
                         g.DrawString("Одежда", this.Font, brushBlack, new Point(step, 410));
                         step += 60;
                         g.DrawString("Одежда " + Convert.ToString(Convert.ToInt32((clothes * 100) / all + 0.2)) + " %", this.Font, brushBlue, new Point(465, 45));
                     }

                     if (checkBox10.Checked)
                     {
                         g.FillRectangle(brushRed, step, Convert.ToInt32(400 - car), 30, 400 - Convert.ToInt32(400 - car)); //еда
                         g.DrawString("Транспорт", this.Font, brushBlack, new Point(step, 410));
                         g.DrawString("Транспорт " + Convert.ToString(Convert.ToInt32((car * 100) / all + 0.2)) + " %", this.Font, brushRed, new Point(465, 60));
                         step += 60;
                     }

                     if (checkBox11.Checked)
                     {
                         g.FillRectangle(brushAqua, step, Convert.ToInt32(400 - doctor), 30, 400 - Convert.ToInt32(400 - doctor)); //еда
                         g.DrawString("Медицина", this.Font, brushBlack, new Point(step, 410));
                         step += 60;
                         g.DrawString("Медицина " + Convert.ToString(Convert.ToInt32((doctor * 100) / all + 0.2)) + " %", this.Font, brushAqua, new Point(465, 75));
                     }

                     if (checkBox22.Checked)
                     {
                         g.FillRectangle(brushDreem, step, Convert.ToInt32(400 - dreem), 30, 400 - Convert.ToInt32(400 - dreem)); //еда
                         g.DrawString("Мечта", this.Font, brushBlack, new Point(step, 410));
                         g.DrawString("Мечта " + Convert.ToString(Convert.ToInt32((dreem * 100) / all + 0.2)) + " %", this.Font, brushDreem, new Point(465, 90));
                         step += 60;
                     }

                     if (checkBox12.Checked)
                     {
                         g.FillRectangle(brushCoral, step, Convert.ToInt32(400 - other), 30, 400 - Convert.ToInt32(400 - other)); //еда
                         g.DrawString("Другое", this.Font, brushBlack, new Point(step, 410));
                         g.DrawString("Другое " + Convert.ToString(Convert.ToInt32((other * 100) / all + 0.2)) + " %", this.Font, brushCoral, new Point(465, 105));
                         step += 60;
                     }
                    
                     g.DrawLine(blackPen, 0, 400, 900, 400);
                 }
                 else    //круговая
                 {
                     g.FillRectangle(brushWhite, 450, 10, 200, 130);
                     g.DrawRectangle(redPen, 450, 10, 200, 130);
                     g.DrawString("Еда и продукты " + Convert.ToString(eat), this.Font, brushGreen, new Point(465, 15));
                     g.DrawString("Жильё " + Convert.ToString(home), this.Font, brushOrange, new Point(465, 30));
                     g.DrawString("Одежда " + Convert.ToString(clothes), this.Font, brushBlue, new Point(465, 45));
                     g.DrawString("Транспорт " + Convert.ToString(car), this.Font, brushRed, new Point(465, 60));
                     g.DrawString("Медицина " + Convert.ToString(doctor), this.Font, brushAqua, new Point(465, 75));
                     g.DrawString("Мечта " + Convert.ToString(dreem), this.Font, brushDreem, new Point(465, 90));
                     g.DrawString("Другое " + Convert.ToString(other), this.Font, brushCoral, new Point(465, 105));

                     int last, next = 0;

                     last = 0;
                     if (checkBox7.Checked)
                     {
                         next = Convert.ToInt32((((eat * 100) / all) * 360) / 100);
                         g.FillPie(brushGreen, 10, 10, 400, 400, last, next);
                     }

                     if (checkBox8.Checked)
                     {
                         last += next;
                         next = Convert.ToInt32((((home * 100) / all) * 360) / 100);
                         g.FillPie(brushOrange, 10, 10, 400, 400, last, next);
                     }

                     if (checkBox9.Checked)
                     {
                         last += next;
                         next = Convert.ToInt32((((clothes * 100) / all) * 360) / 100);
                         g.FillPie(brushBlue, 10, 10, 400, 400, last, next);
                     }

                     if (checkBox10.Checked)
                     {
                         last += next;
                         next = Convert.ToInt32((((car * 100) / all) * 360) / 100);
                         g.FillPie(brushRed, 10, 10, 400, 400, last, next);
                     }

                     if (checkBox11.Checked)
                     {
                         last += next;
                         next = Convert.ToInt32((((doctor * 100) / all) * 360) / 100);
                         g.FillPie(brushAqua, 10, 10, 400, 400, last, next);
                     }

                     if (checkBox22.Checked)
                     {
                         last += next;
                         next = Convert.ToInt32((((dreem * 100) / all) * 360) / 100);
                         g.FillPie(brushDreem, 10, 10, 400, 400, last, next);
                     }

                     if (checkBox12.Checked)
                     {
                         last += next;
                         next = Convert.ToInt32((((other * 100) / all) * 360) / 100);
                         g.FillPie(brushCoral, 10, 10, 400, 400, last, next);
                     }
                 }

             }
             else                         //доход
                 if ((dataGridView3.Rows.Count - 1)!=0)
             {
                 for (int i = 0; i < dataGridView3.Rows.Count - 1; i++)
                 {
                     if (checkBox17.Checked && Convert.ToString(dataGridView3.Rows[i].Cells[2].Value) == "Работа")
                     {
                         work += Convert.ToDouble(dataGridView3.Rows[i].Cells[3].Value);
                         all += Convert.ToDouble(dataGridView3.Rows[i].Cells[3].Value);
                     }
                     if (checkBox18.Checked && Convert.ToString(dataGridView3.Rows[i].Cells[2].Value) == "Банковские проценты")
                     {
                         bank += Convert.ToDouble(dataGridView3.Rows[i].Cells[3].Value);
                         all += Convert.ToDouble(dataGridView3.Rows[i].Cells[3].Value);
                     }
                     if (checkBox19.Checked && Convert.ToString(dataGridView3.Rows[i].Cells[2].Value) == "Другое")
                     {
                         other += Convert.ToDouble(dataGridView3.Rows[i].Cells[3].Value);
                         all += Convert.ToDouble(dataGridView3.Rows[i].Cells[3].Value);
                     }
                 }

                 if (radioButton1.Checked)  //столбиковая
                 {

                     if (work > max)
                         max = work;

                     if (bank > max)
                         max = bank;

                     if (other > max)
                         max = other;



                     while (max > 370)
                     {
                         work *= 0.9;
                         bank *= 0.9;
                         
                         max *= 0.9;
                         all *= 0.9;
                         other *= 0.9;
                         k *= 0.9;
                     }


                     for (int i = 380; i > 10; )
                     {
                         g.DrawLine(blackPen1, 0, i, 900, i);
                         g.DrawString(Convert.ToString(Convert.ToInt32((400 - i) / k)), this.Font, brushBlack, new Point(0, i - 15));
                         i -= 20;
                     }

                     g.FillRectangle(brushWhite, 450, 10, 200, 100);
                     g.DrawRectangle(redPen, 450, 10, 200, 100);

                     if (checkBox17.Checked)
                     {
                         g.FillRectangle(brushGreen, step, Convert.ToInt32(400 - work), 30, 400 - Convert.ToInt32(400 - work)); 
                         g.DrawString("Работа", this.Font, brushBlack, new Point(step, 410));
                         step += 110;
                         g.DrawString("Работа " + Convert.ToString(Convert.ToInt32((work * 100) / all + 0.2)) + " %", this.Font, brushGreen, new Point(465, 15));
                     }

                     if (checkBox18.Checked)
                     {
                         g.FillRectangle(brushOrange, step, Convert.ToInt32(400 - bank), 30, 400 - Convert.ToInt32(400 - bank)); 
                         g.DrawString("Банковские проценты", this.Font, brushBlack, new Point(step-25, 410));
                         g.DrawString("Банковские проценты " + Convert.ToString(Convert.ToInt32((bank * 100) / all + 0.2)) + " %", this.Font, brushOrange, new Point(465, 30));
                         step += 110;
                     }

                     if (checkBox19.Checked)
                     {
                         g.FillRectangle(brushCoral, step, Convert.ToInt32(400 - other), 30, 400 - Convert.ToInt32(400 - other)); 
                         g.DrawString("Другое", this.Font, brushBlack, new Point(step, 410));
                         step += 110;
                         g.DrawString("Другое " + Convert.ToString(Convert.ToInt32((other * 100) / all + 0.2)) + " %", this.Font, brushCoral, new Point(465, 45));
                     }
                     g.DrawLine(blackPen, 0, 400, 900, 400);
                 }
                 else    //круговая
                 {
                     g.FillRectangle(brushWhite, 450, 10, 200, 100);
                     g.DrawRectangle(redPen, 450, 10, 200, 100);
                     g.DrawString("Работа " + Convert.ToString(work), this.Font, brushGreen, new Point(465, 15));
                     g.DrawString("Банковские проценты " + Convert.ToString(bank), this.Font, brushOrange, new Point(465, 30));
                     g.DrawString("Другое " + Convert.ToString(other), this.Font, brushCoral, new Point(465, 90));

                     int last, next = 0;

                     last = 0;
                     if (checkBox17.Checked)
                     {
                         next = Convert.ToInt32((((work* 100) / all) * 360) / 100);
                         g.FillPie(brushGreen, 10, 10, 400, 400, last, next);
                     }

                     if (checkBox18.Checked)
                     {
                         last += next;
                         next = Convert.ToInt32((((bank * 100) / all) * 360) / 100);
                         g.FillPie(brushOrange, 10, 10, 400, 400, last, next);
                     }

                     if (checkBox19.Checked)
                     {
                         last += next;
                         next = Convert.ToInt32((((other * 100) / all) * 360) / 100);
                         g.FillPie(brushCoral, 10, 10, 400, 400, last, next);
                     }
                 }

             }
        }

        private void checkBox15_CheckedChanged(object sender, EventArgs e)
        {
            comboBox2.Enabled = checkBox15.Checked;
        }

        private void textBox2_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar != 8 && (e.KeyChar < 48 || e.KeyChar > 57) && e.KeyChar != 44)
                e.Handled = true;
        }

        private void textBox7_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar != 8 && (e.KeyChar < 48 || e.KeyChar > 57) && e.KeyChar != 44)
                e.Handled = true;
        }

        private void textBox3_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar != 8 && (e.KeyChar < 48 || e.KeyChar > 57) && e.KeyChar != 44)
                e.Handled = true;
        }

        private void textBox10_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar != 8 && (e.KeyChar < 48 || e.KeyChar > 57) && e.KeyChar != 44)
                e.Handled = true;
        }

        private void оПрограммеToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form8 form = new Form8();
            form.ShowDialog();
        }

        private void checkBox20_CheckedChanged(object sender, EventArgs e)
        {
            textBox13.Enabled = checkBox20.Checked;
        }

        private void checkBox21_CheckedChanged(object sender, EventArgs e)
        {
            textBox14.Enabled = checkBox21.Checked;
        }

        private void dataGridView2_RowsRemoved(object sender, DataGridViewRowsRemovedEventArgs e)
        {
            addDream();
        }

        private void radioButton4_CheckedChanged(object sender, EventArgs e)
        {
            panel3.Visible = !radioButton4.Checked;
        }

        private void radioButton5_CheckedChanged(object sender, EventArgs e)
        {
            panel3.Visible = !radioButton4.Checked;
        }

        private void сменаВалютыToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form7 form = new Form7();
            form.ShowDialog();
            if (GlobalNames.kurs != 1)
            {
                for (int i = 0; i < dataGridView1.Rows.Count-1; i++)
                {
                    dataGridView1.Rows[i].Cells[3].Value = Convert.ToDouble(dataGridView1.Rows[i].Cells[3].Value) * GlobalNames.kurs;
                }

                for (int i = 0; i < dataGridView3.Rows.Count-1; i++)
                {
                    dataGridView3.Rows[i].Cells[3].Value = Convert.ToDouble(dataGridView3.Rows[i].Cells[3].Value) * GlobalNames.kurs;
                }

                for (int i = 0; i < dataGridView2.Rows.Count-1; i++)
                {
                    dataGridView2.Rows[i].Cells[3].Value = Convert.ToDouble(dataGridView2.Rows[i].Cells[3].Value) * GlobalNames.kurs;
                    dataGridView2.Rows[i].Cells[4].Value = Convert.ToDouble(dataGridView2.Rows[i].Cells[4].Value) * GlobalNames.kurs;
                }

            }
        }



    }
}
