using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace Teste_3
{
    public partial class Logon : Form
    {
        string IDUSUARIO;

        public Logon()
        {
            InitializeComponent();
        }

        private void Logon_Load(object sender, EventArgs e)
        {
            textBox2.Text = "";
            textBox2.PasswordChar = '*';
        }

        private void button1_Click(object sender, EventArgs e)
        {
            SqlConnection conn = new SqlConnection("Data Source=WD007ASC;Initial Catalog=CRC;Persist Security Info=True;User ID=crc;Password=crc");
            conn.Open();
            SqlCommand cmd = new SqlCommand("SELECT * FROM USUARIO WHERE USUARIO = '" + textBox1.Text + "' AND SENHA = '" + textBox2.Text + "'",conn);
            SqlDataReader dr;
            dr = cmd.ExecuteReader();
            int count = 0;
            while(dr.Read())
            {
                count += 1;
                IDUSUARIO = dr[0].ToString();
            }

            if (count == 1)
            {
                
                //MessageBox.Show("idusuario "+IDUSUARIO);
                Form2 f2 = new Form2(IDUSUARIO);
                f2.Show();
                this.Hide();
            }
            else if (count > 0)
            {
                MessageBox.Show("Duplicate username and password");
            }
            else
            {
                MessageBox.Show("Usuário e senha incorretos!");
            }

            textBox1.Clear();
            textBox2.Clear();

            

        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
