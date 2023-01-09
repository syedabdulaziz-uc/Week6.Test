using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Linq;

namespace Week6.Test
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            
        }

        private void button1_Click(object sender, EventArgs e)
        {
            using (SqlConnection conObj = new SqlConnection(ConfigurationManager.ConnectionStrings["InfiniteCon"].ConnectionString))
            {
                conObj.Open();
                SqlCommand cmdObj = new SqlCommand("InsertEmployee", conObj);
                cmdObj.CommandType = CommandType.StoredProcedure;

                // input parameters
                cmdObj.Parameters.AddWithValue("@EmployeeId", SqlDbType.Int).Value = int.Parse(txtEmployeeNo.Text);
                cmdObj.Parameters.AddWithValue("@EmployeeName", SqlDbType.VarChar).Value = txtEmployeeName.Text;
                cmdObj.Parameters.AddWithValue("@EmployeeSalary", SqlDbType.Money).Value = txtEmpSal.Text;
                cmdObj.Parameters.AddWithValue("@EmployeeType", SqlDbType.Char).Value = ContractorRadioButton.Text;
                

                // output parameter
                SqlParameter reslt = new SqlParameter("@Result", SqlDbType.Int);
                reslt.Direction = ParameterDirection.Output;
                cmdObj.Parameters.Add(reslt);

                // Execute procedure
                cmdObj.ExecuteNonQuery();

                int result = (int)reslt.Value;

                // Validate input
                if (txtEmployeeNo.Text.Length < 3 || txtEmployeeNo.Text.Length > 20)
                {
                    MessageBox.Show("Invalid EmployeeName. Length must be between 3 and 20 characters.");
                    return;
                }
                if (!decimal.TryParse(txtEmpSal.Text, out decimal employeeSalary) || employeeSalary < 25000)
                {
                    MessageBox.Show("Invalid EmployeeSalary. Salary must be at least 25000.");
                    return;
                }
                string employeeType = "";
                if (ContractorRadioButton.Checked)
                {
                    employeeType = "C";
                }
                else if (PermanentRadioButton.Checked)
                {
                    employeeType = "P";
                }
                else
                {
                    MessageBox.Show("Please select either Contractor or Permanent for EmployeeType.");
                    return;
                }

                    MessageBox.Show("Employee added successfully!");
                }
            }

        private void PermanentRadioButton_CheckedChanged(object sender, EventArgs e)
        {

        }
    }
    }

