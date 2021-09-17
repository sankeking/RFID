using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MySql.Data.MySqlClient;

namespace rent
{
    public partial class Form3 : Form
    {
        MySqlConnection conn = new MySqlConnection("server = localhost; user id = aaa; password = 123; database = test; charset = utf8;");
        public Form3()
        {
            InitializeComponent();
        }
        public static DateTime Now { get; }

        private void Form3_Load(object sender, EventArgs e)
        {
            label7.Text = DateTime.Now.ToString("yyyy/MM/dd HH:mm");
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
            conn.Open();
            MySqlCommand sele = new MySqlCommand("select card_id from card where card_id = '" + textBox1.Text + "'", conn);
            string a = sele.ExecuteScalar().ToString();
            MySqlCommand sss = new MySqlCommand("select room from card where card_id = '" + a + "'", conn);
            string b = sss.ExecuteScalar().ToString();

            if (a != null && b == "null")
            {
                MySqlCommand sel = new MySqlCommand("select state from room_state where room = '" + textBox2.Text + "'", conn);
                string st = sel.ExecuteScalar().ToString();

                if (st == "1")
                {
                    label3.Text = "房間已有人入住。";
                }
                else if (st == "null")
                {
                    MySqlCommand cmd = new MySqlCommand("update card set room = '" + textBox2.Text + "' where card_id = '" + textBox1.Text + "'", conn);
                    MySqlCommand ch = new MySqlCommand("update room_state set state = '1' where room = '" + textBox2.Text + "'", conn);
                    MySqlCommand ins = new MySqlCommand("insert into people(card_id, room, name, phone, check_in, check_out) values ('" + textBox1.Text + "','" + textBox2.Text + "','" + textBox3.Text + "','" + textBox4.Text + "','" + DateTime.Now.ToString("yyyy/MM/dd HH:mm") + "','null')", conn);
                    cmd.ExecuteNonQuery();
                    ch.ExecuteNonQuery();
                    ins.ExecuteNonQuery();
                    label3.Text = "房間為空房，已成功將卡設為房卡。";
                }

            }
            else
            {
                label3.Text = "卡已被註冊。";
            }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                label3.Text = "查無此卡號或房號。";
            }
            conn.Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Form1 form1 = new Form1();
            this.Hide();
            form1.Show();
        }
    }
}
