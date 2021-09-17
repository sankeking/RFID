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
    public partial class Form4 : Form
    {
        MySqlConnection conn = new MySqlConnection("server = localhost; user id = aaa; password = 123; database = test; charset = utf8;");
        public Form4()
        {
            InitializeComponent();
        }
        public static DateTime Now { get; }

        private void Form4_Load(object sender, EventArgs e)
        {
            label4.Text = DateTime.Now.ToString("yyyy/MM/dd HH:mm");
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
            conn.Open();
            MySqlCommand sel = new MySqlCommand("select card_id from card where card_id = '" + textBox1.Text + "'", conn);
            string a = sel.ExecuteScalar().ToString();
            MySqlCommand sele = new MySqlCommand("select room from card where card_id = '" + a + "'", conn);
            string b = sele.ExecuteScalar().ToString();

            if (a != null && b != "null")
            {
                MySqlCommand cmd = new MySqlCommand("update card set room = 'null' where card_id = '" + textBox1.Text + "'", conn);
                MySqlCommand ch = new MySqlCommand("update room_state set state = 'null' where room = '" + b + "'", conn);
                MySqlCommand ins = new MySqlCommand("update people set check_out = '" + DateTime.Now.ToString("yyyy/MM/dd HH:mm") + "'where check_out = 'null' and card_id = '" + textBox1.Text + "'", conn);
                cmd.ExecuteNonQuery();
                ch.ExecuteNonQuery();
                ins.ExecuteNonQuery();
                label3.Text = "房間已成功退房。";
                    
            }
            else 
            {
                label3.Text = "卡已為空。";
            }
                
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                label3.Text = "查無此卡號。";
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
