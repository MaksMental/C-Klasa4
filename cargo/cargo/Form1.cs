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
using System.Runtime.InteropServices;
using System.Configuration;

namespace cargo
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        SqlConnection conn = new SqlConnection("Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=|DataDirectory|usersDB.mdf;Integrated Security=True");
        private void timer1_Tick(object sender, EventArgs e)
        {

        }

        private void dateTimePicker2_ValueChanged(object sender, EventArgs e)
        {
        }

        private void lbLogin_Click(object sender, EventArgs e)
        {

        }

        private void btnConnect_Click(object sender, EventArgs e)
        {
            String username, password;
            username = txtLogin.Text;
            password = txtPassword.Text;
       

            try
            {
                conn.Open();
                String querry = "SELECT  * FROM  [Table] WHERE login = '"+txtLogin.Text+"'AND password ='"+txtPassword.Text+"'";
                SqlDataAdapter sda = new SqlDataAdapter(querry, conn);

                DataTable dtable = new DataTable();
                sda.Fill(dtable);

                if(dtable.Rows.Count > 0)
                {
                    username = txtLogin.Text;
                    password = txtPassword.Text;

                    main form2 = new main();
                    form2.Show();
                    this.Hide();
                }
                else
                {
                    MessageBox.Show("Wprowadziłeś nieprawidłowe dane. Spróbuj ponownie !");
                    txtLogin.Clear();
                    txtPassword.Clear();
                    txtLogin.Focus();

                }

            }
            catch
            {
                MessageBox.Show("Error xd");
            }
            finally
            {
                conn.Close();
            }
        }


            private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void txtPassword_TextChanged(object sender, EventArgs e)
        {
            txtPassword.UseSystemPasswordChar = true;
        }

        private void cbxShowPassword_CheckedChanged(object sender, EventArgs e)
        {
            if (cbxShowPassword.Checked)
            {
                txtPassword.UseSystemPasswordChar = false;
            }
            else
            {
                txtPassword.UseSystemPasswordChar = true;
            }
           
        }

        private void txtLogin_TextChanged(object sender, EventArgs e)
        {

        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }
    }
}
