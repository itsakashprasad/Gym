using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace GYM
{
    public partial class Log : Form
    {
        public Log()
        {
            InitializeComponent();
            this.KeyPress += Log_KeyPress;
            this.AcceptButton = Loginbut1;
            
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void Resetbut2_Click(object sender, EventArgs e)
        {
            UidTb.Text = "";
            PassTb.Text = "";
        }
        private void Log_KeyPress(object sender, KeyPressEventArgs e)
        {
            // Check if the pressed key is Enter
            if (e.KeyChar == (char)Keys.Enter)
            {
                // Trigger the Login button click event
                Loginbut1_Click(sender, e);
            }
        }

        private void Loginbut1_Click(object sender, EventArgs e)
        {


            string connectionString = "datasource=localhost;port=3306;username=root;password=12345;database=proj1" ;

            MySql.Data.MySqlClient.MySqlConnection conn = new MySql.Data.MySqlClient.MySqlConnection(connectionString);
           
            {

                try
                {
                    conn.Open();
                    string query = "SELECT user_id, password FROM membertbl WHERE user_id=@user AND password=@Password";
                    MySqlCommand cmd = new MySqlCommand(query, conn);
                    cmd.Parameters.AddWithValue("@user", UidTb.Text.Trim());
                    cmd.Parameters.AddWithValue("@Password", PassTb.Text.Trim());

                    using (MySqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            //MessageBox.Show("Login successful!");

                            Crud c = new Crud();
                            c.Show();
                            this.Hide();
                        }
                        else
                        {
                            MessageBox.Show("Invalid username or password!");
                        }
                    }


                    conn.Close();
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Login failed: " + ex.Message);
                }
            }
        }


        private void pictureBox4_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void UidTb_TextChanged(object sender, EventArgs e)
        {

        }

        private void PassTb_TextChanged(object sender, EventArgs e)
        {

        }
    }

}
