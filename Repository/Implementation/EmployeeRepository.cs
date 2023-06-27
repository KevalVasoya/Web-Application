using CommonModel.DBModel;
using Dapper;
using Repository.Interface;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;


namespace Repository
{
    public class EmployeeRepository : IEmployeeRepository
    {
        /// <summary>The connection string</summary>
        private readonly string connectionString;
        public EmployeeRepository()
        {
            connectionString = ConfigurationManager.ConnectionStrings["Default"].ConnectionString.ToString();
        }

        /// <summary>Adds the employee.</summary>
        /// <param name="employeeDetail">The employee detail.</param>
        public List<EmployeeDetails> AddEmployee(EmployeeDetails employeeDetail)
        {
            try
            {
                var parameter = new DynamicParameters();

                using (IDbConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    parameter.Add("@Name", employeeDetail.Name);
                    parameter.Add("@DesignationId", employeeDetail.DesignationId);
                    parameter.Add("@ProfilePicture", employeeDetail.ProfilePicture);
                    parameter.Add("@Salary", employeeDetail.Salary);
                    parameter.Add("@DateofBirth", employeeDetail.DateofBirth);
                    parameter.Add("@Email", employeeDetail.Email);
                    parameter.Add("@Address", employeeDetail.Address);
                    return connection.Query<EmployeeDetails>("EmployeeDetailsAddorEdit", parameter, commandType: CommandType.StoredProcedure).ToList();

                }
            }
            catch (Exception ex)
            {
                // Handle the exception, log the error, or perform any necessary actions.
                Console.WriteLine("An error occurred: " + ex.Message);
                throw; // Rethrow the exception to propagate it further if needed.
            }
        }

        /// <summary>Gets all data.</summary>
        public IList<EmployeeDetails> GetAllData()
        {
            string connectionString = ConfigurationManager.ConnectionStrings["Default"].ConnectionString;
            using (IDbConnection connection = new SqlConnection(connectionString))
            //using (var connection = new SqlConnection("Data Source=LAPTOP-MFLA2MGK\\SQLEXPRESS;Initial Catalog=Employees;Integrated Security=True"))
            {
                connection.Open();
                return connection.Query<EmployeeDetails>("EmployeeDetailsViewAll").ToList();
            }
        }

        /// <summary>Gets the designation.</summary>
        public IList<EmployeeDesignation> GetDesignation()
        {
            string connectionString = ConfigurationManager.ConnectionStrings["Default"].ConnectionString;
            using (IDbConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                return connection.Query<EmployeeDesignation>("GetEmployeeDesignations").ToList();
            }
        }

        /// <summary>Gets the employee by identifier.</summary>
        /// <param name="id">The identifier.</param>
        public EmployeeDetails GetEmployeeById(int id)
        {
            string connectionString = ConfigurationManager.ConnectionStrings["Default"].ConnectionString;
            using (IDbConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                string query = "SELECT * FROM EmployeeDetails WHERE Id = @Id";
                return connection.QueryFirstOrDefault<EmployeeDetails>(query, new { Id = id });
            }
        }

        /// <summary>Updates the employee.</summary>
        /// <param name="employee">The employee.</param>
        public void UpdateEmployee(EmployeeDetails employee)
        {
            try
            {
                var parameter = new DynamicParameters();
                string connectionString = ConfigurationManager.ConnectionStrings["Default"].ConnectionString;
                using (IDbConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    parameter.Add("@ID", employee.ID);
                    parameter.Add("@Name", employee.Name);
                    parameter.Add("@DesignationId", employee.DesignationId);
                    if(employee.ProfilePicture != null)
                    {
                        parameter.Add("@ProfilePicture", employee.ProfilePicture);
                    }
                    else
                    {
                        parameter.Add("@ProfilePicture", employee.ProfilePicture);
                    }
                    parameter.Add("@Salary", employee.Salary);
                    parameter.Add("@DateofBirth", employee.DateofBirth);
                    parameter.Add("@Email", employee.Email);
                    parameter.Add("@Address", employee.Address);
                    connection.Query<EmployeeDetails>("EmployeeDetailsUpdate", parameter, commandType: CommandType.StoredProcedure);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>Deletes the employee.</summary>
        /// <param name="id">The identifier.</param>
        public void DeleteEmployee(int id)
        {
            try
            {
                string connectionString = ConfigurationManager.ConnectionStrings["Default"].ConnectionString;
                using (IDbConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    var parameter = new DynamicParameters();
                    parameter.Add("@ID", id);
                    connection.Execute("EmployeeDetailsDeletebyID", parameter, commandType: CommandType.StoredProcedure);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("An error occurred: " + ex.Message);
                throw;
            }
        }
    }
}



