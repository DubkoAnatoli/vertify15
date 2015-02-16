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
    public partial class Form6 : Form
    {
        public Form6()
        {
            InitializeComponent();
        }

        private void button2_Click(object sender, EventArgs e)
        {


            string name = GlobalNames.name;
            using (StreamWriter m = File.CreateText(name + "\\" + name + ".one"))   //мечта
            {
                for (int i = 0; i < dataGridView1.Rows.Count - 1; i++)
                {
                    for (int j = 0; j < 3; j++)
                    {
                        m.WriteLine(dataGridView1.Rows[i].Cells[j].Value);
                    }
                }
                m.Close();
            }

            GlobalNames.one.Clear();

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


            GlobalNames.rezalt = true;
            this.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
            GlobalNames.rezalt = false;
        }

        private void Form6_Load(object sender, EventArgs e)
        {
            for (int i = 0; i < GlobalNames.one.Count; i++)
            {
                dataGridView1.Rows.Add();
                for (int j = 0; j < 3; j++)
                {
                    dataGridView1.Rows[i].Cells[j].Value = GlobalNames.one[i][j];
                }
            }
        }
    }
}
