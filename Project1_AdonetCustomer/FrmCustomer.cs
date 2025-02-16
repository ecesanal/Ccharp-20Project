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

namespace Project1_AdonetCustomer
{
    public partial class FrmCustomer : Form
    {
        public FrmCustomer()
        {
            InitializeComponent();
        }

        SqlConnection sqlConnection = new SqlConnection("Server=ECE-SANAL\\MSSQLSERVER01;User Id=sa;Password=0120;initial catalog=DbCustomer;integrated security=true");

        private void btnList_Click(object sender, EventArgs e)
        {
            sqlConnection.Open();
            SqlCommand sqlCommand = new SqlCommand("Select CustomerId,CustomerName,CustomerSurname,CustomerBalance,CustomerStatus, CityName From TblCustomer Inner Join TblCity On TblCity.CityId=TblCustomer.CustomerCity", sqlConnection);
            SqlDataAdapter dataAdapter = new SqlDataAdapter(sqlCommand);
            DataTable dataTable = new DataTable();
            dataAdapter.Fill(dataTable);
            dataGridView1.DataSource = dataTable;
            sqlConnection.Close();
        }

        private void procedure_Click(object sender, EventArgs e)
        {
            sqlConnection.Open();
            SqlCommand sqlCommand = new SqlCommand("Execute CustomerListWithCity", sqlConnection);
            SqlDataAdapter dataAdapter = new SqlDataAdapter(sqlCommand);
            DataTable dataTable = new DataTable();
            dataAdapter.Fill(dataTable);
            dataGridView1.DataSource = dataTable;
            sqlConnection.Close();
        }

        private void FrmCustomer_Load(object sender, EventArgs e)
        {
            SqlCommand sqlCommand = new SqlCommand("Select * From TblCity", sqlConnection);
            SqlDataAdapter dataAdapter = new SqlDataAdapter(sqlCommand);
            DataTable dataTable = new DataTable();
            dataAdapter.Fill(dataTable);
            cmbCity.ValueMember = "CityId";
            cmbCity.DisplayMember = "CityName";
            cmbCity.DataSource = dataTable;
        }

        private void btnCreate_Click(object sender, EventArgs e)
        {
            sqlConnection.Open();
            SqlCommand sqlCommand = new SqlCommand("Insert into TblCustomer " +
                "(CustomerName,CustomerSurname,CustomerCity,CustomerBalance,CustomerStatus) " +
                "values (@customerName,@customerSurname,@customerCity,@customerBalance,@customerStatus)", sqlConnection);
            sqlCommand.Parameters.AddWithValue("@customerName", txtCustomerName.Text);
            sqlCommand.Parameters.AddWithValue("@customerSurname", txtCustomerSurname.Text);
            sqlCommand.Parameters.AddWithValue("@customerCity", cmbCity.SelectedValue);
            sqlCommand.Parameters.AddWithValue("@customerBalance", txtCustomerBalance.Text);
            if (rdbActive.Checked)
            {
                sqlCommand.Parameters.AddWithValue("@customerStatus", true);
            } if (rdbPassive.Checked)
            {
                sqlCommand.Parameters.AddWithValue("@customerStatus", false);
            }
            sqlCommand.ExecuteNonQuery();
            sqlConnection.Close();
            MessageBox.Show("Müşteri Başarılı Bir Şekilde Eklendi");
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            sqlConnection.Open();
            SqlCommand command = new SqlCommand("Delete from TblCustomer where CustomerId=@customerId", sqlConnection);
            command.Parameters.AddWithValue("@customerId", txtCustomerId.Text);
            command.ExecuteNonQuery();
            sqlConnection.Close();
            MessageBox.Show("Müşteri Başarılı Bir Şekilde Silindi", "Uyarı!",
                MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            sqlConnection.Open();
            SqlCommand sqlCommand = new SqlCommand("Update TblCustomer Set " +
                "CustomerName = @customerName, CustomerSurname = @customerSurname, " +
                "CustomerCity = @customerCity, CustomerBalance = @customerBalance, " +
                "CustomerStatus = @customerStatus where CustomerId = @customerId", sqlConnection);

            sqlCommand.Parameters.AddWithValue("@customerName", txtCustomerName.Text);
            sqlCommand.Parameters.AddWithValue("@customerSurname", txtCustomerSurname.Text);
            sqlCommand.Parameters.AddWithValue("@customerCity", cmbCity.SelectedValue);
            sqlCommand.Parameters.AddWithValue("@customerBalance", txtCustomerBalance.Text);
            sqlCommand.Parameters.AddWithValue("@customerId", txtCustomerId.Text);

            if (rdbActive.Checked)
            {
                sqlCommand.Parameters.AddWithValue("@customerStatus", true);
            }
            if (rdbPassive.Checked)
            {
                sqlCommand.Parameters.AddWithValue("@customerStatus", false);
            }

            sqlCommand.ExecuteNonQuery();
            sqlConnection.Close();
            MessageBox.Show("Müşteri Başarıyla Güncellendi");
        }


        private void btnSearch_Click(object sender, EventArgs e)
        {
            sqlConnection.Open();
            SqlCommand command = new SqlCommand("Select * from TblCustomer Where CustomerName=@customerName", sqlConnection);
            command.Parameters.AddWithValue("@customerName", txtCustomerName.Text);
            SqlDataAdapter dataAdapter = new SqlDataAdapter(command);
            DataTable dataTable = new DataTable();
            dataAdapter.Fill(dataTable);
            dataGridView1.DataSource = dataTable;
            sqlConnection.Close();
        }
    }
}
