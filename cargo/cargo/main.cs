using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace cargo
{
    public partial class main : Form
    {
        public main()
        {
            InitializeComponent();
            Timer timer = new Timer();
            timer.Interval = 1000;
            timer.Tick += new EventHandler(timer1_Tick);
            timer.Start();
        }


        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }


        private void groupBox1_Enter_1(object sender, EventArgs e)
        {

        }

        private void btnLogout_Click_1(object sender, EventArgs e)
        {
            Form1 form = new Form1();
            form.Show();
            this.Hide();
        }


        private void label2_Click(object sender, EventArgs e)
        {

        }
        private void timer1_Tick(object sender, EventArgs e)
        {
            lbTime.Text = DateTime.Now.ToString("HH:mm:ss");
        }



        private void btnChangeBin_Click(object sender, EventArgs e)
        {
            int rowIndex = dataGridView1.CurrentCell.RowIndex;
            int id = Convert.ToInt32(dataGridView1.Rows[rowIndex].Cells[0].Value);

            int dostepnosc= Convert.ToInt32(dataGridView1.Rows[rowIndex].Cells[5].Value);

            dostepnosc = (dostepnosc == 1) ? 0 : 1;
            dataGridView1.Rows[rowIndex].Cells[5].Value = dostepnosc;

            SqlConnection connection = new SqlConnection("Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=|DataDirectory|usersDB.mdf;Integrated Security=True");

                connection.Open();
                SqlCommand command = new SqlCommand("UPDATE cars SET dostepnosc=@dostepnosc WHERE id=@id", connection);
                command.Parameters.AddWithValue("@dostepnosc", dostepnosc);
                command.Parameters.AddWithValue("@id", id);
                command.ExecuteNonQuery();
                connection.Close();

        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void main_Load(object sender, EventArgs e)
        {
            SqlConnection connection = new SqlConnection("Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=|DataDirectory|usersDB.mdf;Integrated Security=True");
            SqlDataAdapter adapter = new SqlDataAdapter("SELECT * FROM czesci", connection);
            DataTable table = new DataTable();
            adapter.Fill(table);
            dataGridView2.DataSource = table;
            // TODO: Ten wiersz kodu wczytuje dane do tabeli 'usersDBDataSet1.czesci' . Możesz go przenieść lub usunąć.
            this.czesciTableAdapter.Fill(this.usersDBDataSet1.czesci);
            // TODO: Ten wiersz kodu wczytuje dane do tabeli 'usersDBDataSet.cars' . Możesz go przenieść lub usunąć.
            this.carsTableAdapter.Fill(this.usersDBDataSet.cars);

        }

        private void tbPg_Click(object sender, EventArgs e)
        {

        }

        private void txtSzukajModel_TextChanged(object sender, EventArgs e)
        {
            string szukanaFraza = txtSzukajModel.Text;
            if (dataGridView2.DataSource != null && dataGridView2.DataSource is DataTable)
            {
                (dataGridView2.DataSource as DataTable).DefaultView.RowFilter = $"model_samochodu LIKE '%{szukanaFraza}%'";
            }
            else
            {
                MessageBox.Show("Brak ustawionego źródła danych dla dataGridView2.");
            }
        }

        private void btnRemove_Click(object sender, EventArgs e)
        {

            string connectionString = "Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=|DataDirectory|usersDB.mdf;Integrated Security=True";
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                foreach (DataGridViewRow row in dataGridView2.SelectedRows)
                {
                    int userId = Convert.ToInt32(row.Cells[0].Value);

                    string selectQuery = "SELECT admin FROM [Table] WHERE ID = @ID";
                    SqlCommand selectCommand = new SqlCommand(selectQuery, connection);
                    selectCommand.Parameters.AddWithValue("@ID", userId);
                    int? adminValue = selectCommand.ExecuteScalar() as int?;

                    if (adminValue == null || adminValue == 1)
                    {
                        int currentQuantity = Convert.ToInt32(row.Cells[4].Value);
                        int newQuantity = currentQuantity - 1;

                        if (newQuantity < 0)
                        {
                            MessageBox.Show("Nie można odjąć więcej niż jest dostępne.");
                        }
                        else
                        {
                            row.Cells[4].Value = newQuantity;

                            int selectedItemId = Convert.ToInt32(row.Cells[0].Value);
                            string updateQuery = "UPDATE czesci SET ilosc = @ilosc WHERE ID = @ID";
                            SqlCommand command = new SqlCommand(updateQuery, connection);
                            command.Parameters.AddWithValue("@ilosc", newQuantity);
                            command.Parameters.AddWithValue("@ID", selectedItemId);
                            int rowsAffected = command.ExecuteNonQuery();
                            if (rowsAffected > 0)
                            {
                                MessageBox.Show("Zmiany zostały zapisane w bazie danych.");
                            }
                        }
                    }
                    else
                    {
                        MessageBox.Show("Nie masz uprawnień do wykonania tej operacji dla wiersza o ID " + userId + ".");
                    }
                }

                connection.Close();
            }
        }

            private void btnAdd_Click(object sender, EventArgs e)
        {
            string connectionString = "Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=|DataDirectory|usersDB.mdf;Integrated Security=True";
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                foreach (DataGridViewRow row in dataGridView2.SelectedRows)
                {
                    int userId = Convert.ToInt32(row.Cells[0].Value);

                    string selectQuery = "SELECT admin FROM [Table] WHERE ID = @ID";
                    SqlCommand selectCommand = new SqlCommand(selectQuery, connection);
                    selectCommand.Parameters.AddWithValue("@ID", userId);
                    int? adminValue = selectCommand.ExecuteScalar() as int?;

                    if (adminValue == null || adminValue == 1)
                    {
                        int currentQuantity = Convert.ToInt32(row.Cells[4].Value);
                        int newQuantity = currentQuantity + 1;

                        row.Cells[4].Value = newQuantity;

                        int selectedItemId = Convert.ToInt32(row.Cells[0].Value);
                        string updateQuery = "UPDATE czesci SET ilosc = @ilosc WHERE ID = @ID";
                        SqlCommand command = new SqlCommand(updateQuery, connection);
                        command.Parameters.AddWithValue("@ilosc", newQuantity);
                        command.Parameters.AddWithValue("@ID", selectedItemId);
                        int rowsAffected = command.ExecuteNonQuery();
                        if (rowsAffected > 0)
                        {
                            MessageBox.Show("Zmiany zostały zapisane w bazie danych.");
                        }
                    }
                    else
                    {
                        MessageBox.Show("Nie masz uprawnień do wykonania tej operacji dla wiersza o ID " + userId + ".");
                    }
                }

                connection.Close();
            }
        }

        private void dataGridView2_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
    }
    }
