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

namespace GYM
{
    public partial class Crud : Form
    {
        public Crud()
        {
            InitializeComponent();
            LoadDataToGridView();
        }

        private void cross_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void buttonsearch_Click(object sender, EventArgs e)
        {
            if (enteridmem.Text != string.Empty)
            {
                string connectionString = "datasource=localhost;port=3306;username=root;password=12345;database=proj1";
                MySql.Data.MySqlClient.MySqlConnection conn = new MySql.Data.MySqlClient.MySqlConnection(connectionString);
                conn.Open();
                string Query = "SELECT * FROM memberdetail Where M_id = @Id ";
                MySqlCommand cmd = new MySqlCommand(Query, conn);
                cmd.Parameters.AddWithValue("@Id", enteridmem.Text);

                MySqlDataReader dr = cmd.ExecuteReader();
                if (dr.Read())
                {
                    showid.Text = dr["M_Id"].ToString();
                    showname.Text = dr["M_Name"].ToString();
                    showphone.Text = dr["M_Phone"].ToString();
                    showage.Text = dr["M_Age"].ToString();
                }

                else
                {
                    MessageBox.Show("No record found with this id", "No Data Found", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                conn.Close();
            }
            else
            {
                MessageBox.Show("Please enter employee id ", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void buttonupdate_Click(object sender, EventArgs e)
        {
            if (showid.Text != string.Empty && showname.Text != string.Empty && showage.Text != string.Empty && showphone.Text != string.Empty)
            {
                string connectionString = "datasource=localhost;port=3306;username=root;password=12345;database=proj1";

                MySql.Data.MySqlClient.MySqlConnection conn = new MySql.Data.MySqlClient.MySqlConnection(connectionString);
                conn.Open();
                string Query = "UPDATE memberdetail SET M_Name = @Mname, M_Age = @Mage, M_Phone = @Mph WHERE M_Id = @Mid";


                MySqlCommand cmd = new MySqlCommand(Query, conn);
                cmd.CommandType = CommandType.Text;
                cmd.Parameters.AddWithValue("@Mid", showid.Text);
                cmd.Parameters.AddWithValue("@Mname", showname.Text);
                cmd.Parameters.AddWithValue("@Mage", showage.Text);
                cmd.Parameters.AddWithValue("@Mph", showphone.Text);

                cmd.ExecuteNonQuery();
                MessageBox.Show("Record update successfully.", "Record Updated", MessageBoxButtons.OK, MessageBoxIcon.Information);
                LoadDataToGridView();
                conn.Close();
            }
            else
            {
                MessageBox.Show("Please enter value in all fields", "Invalid Data", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void buttondelete_Click(object sender, EventArgs e)
        {
            if (enteridmem.Text != string.Empty)
            {
                DialogResult dialogResult = MessageBox.Show("Are you sure you want to delete this member ? ", "Delete Member", MessageBoxButtons.YesNo, MessageBoxIcon.Asterisk);
                if (dialogResult == DialogResult.Yes)
                {
                    string connectionString = "datasource=localhost;port=3306;username=root;password=12345;database=proj1";

                    MySql.Data.MySqlClient.MySqlConnection conn = new MySql.Data.MySqlClient.MySqlConnection(connectionString);
                    conn.Open();
                    string Query = "DELETE FROM memberdetail WHERE M_Id = @Mid";

                    MySqlCommand cmd = new MySqlCommand(Query, conn);
                    // cmd.CommandType = CommandType.Text;
                    cmd.Parameters.AddWithValue("@Mid", enteridmem.Text);







                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Record deleted successfully.", "Record Deleted", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    enteridmem.Text = "";
                    LoadDataToGridView();

                }
            }
            else
            {
                MessageBox.Show("Please enter your id", "Invalid Data", MessageBoxButtons.OK, MessageBoxIcon.Information);
                LoadDataToGridView();
            }
        }

        private void buttonreset_Click(object sender, EventArgs e)
        {
            
            enteridmem.Text = "";
            showid.Text = "";
            showname.Text = "";
            showage.Text = "";
            showphone.Text = "";
            LoadDataToGridView();
        }

        private void dtgMemberdetail_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
        private void LoadDataToGridView()
        {
            string connectionString = "datasource=localhost;port=3306;username=root;password=12345;database=proj1";

            MySql.Data.MySqlClient.MySqlConnection conn = new MySql.Data.MySqlClient.MySqlConnection(connectionString);
            
                conn.Open();
                string Query = "SELECT * FROM memberdetail";
                MySqlCommand cmd = new MySqlCommand(Query, conn);

                DataTable dt = new DataTable();
                MySqlDataAdapter da = new MySqlDataAdapter(cmd);
                da.Fill(dt);

                dtgMemberdetail.DataSource = dt;
            
        }

        private void Crud_Load(object sender, EventArgs e)
        {
            LoadDataToGridView();
        }

        private void buttonback_Click(object sender, EventArgs e)
        {
            Log l = new Log();
            l.Show();
            this.Hide();
        }

        private void buttonAdd_Click(object sender, EventArgs e)
        {
            if (showid.Text == "" || showname.Text == "" || showphone.Text == "" || showage.Text == "")
            {
                MessageBox.Show("Please fill all the boxes");
            }

            else
            {
                try
                {

                    string connectionString = "datasource=localhost;port=3306;username=root;password=12345;database=proj1";

                    MySql.Data.MySqlClient.MySqlConnection conn = new MySql.Data.MySqlClient.MySqlConnection(connectionString);
                    conn.Open();
                    string query = "insert into memberdetail(M_Id,M_Name,M_Phone,M_Age) Values(@ID,@NAME,@PHONE,@AGE) ";
                    MySqlCommand cmd = new MySqlCommand(query, conn);
                    cmd.Parameters.AddWithValue("@ID", showid.Text);
                    cmd.Parameters.AddWithValue("@NAME", showname.Text);
                    cmd.Parameters.AddWithValue("@PHONE", showphone.Text);
                    cmd.Parameters.AddWithValue("@AGE", showage.Text);

                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Member Added! Enjoy:)");
                    showid.Text = "";
                    showname.Text = "";
                    showphone.Text = "";
                    showage.Text = "";
                    conn.Close();
                }
                catch (Exception Ex)
                {
                    MessageBox.Show(Ex.Message);
                }
            }
            LoadDataToGridView();
        }
    }
}
