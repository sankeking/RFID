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
    public partial class Form5 : Form
    {
        MySqlConnection conn = new MySqlConnection("server = localhost; user id = aaa; password = 123; database = test; charset = utf8;");
        public Form5()
        {
            InitializeComponent();
        }

        private void Form5_Load(object sender, EventArgs e)
        {
            conn.Open();
            view();
            conn.Close();
        }
        private void view()
        {
            DataTable view = new DataTable();
            MySqlDataAdapter adapter = new MySqlDataAdapter("select * from people ", conn);
            MySqlCommandBuilder cb = new MySqlCommandBuilder();
            adapter.Fill(view);
            dataGridView1.DataSource = view;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Form1 form1 = new Form1();
            this.Hide();
            form1.Show();
        }
    }
}
