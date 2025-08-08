using New_cusromer_registeration.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace New_cusromer_registeration.DAL
{
    public class CustomerRepos
    {
        private string connString = ConfigurationManager.ConnectionStrings["CustomerDB"].ConnectionString;

        public List<Customer> GetAll_Customers()
        {
            List<Customer> customers = new List<Customer>();
            using (SqlConnection con = new SqlConnection(connString))
            {
                SqlCommand cmd = new SqlCommand("SELECT * FROM Customers", con);
                con.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    customers.Add(new Customer
                    {
                        CustomerId = Convert.ToInt32(reader["CustomerId"]),
                        FirstName = reader["FirstName"].ToString(),
                        LastName = reader["LastName"].ToString(),
                        Email = reader["Email"].ToString(),
                        Phone = reader["Phone"].ToString(),
                        DateOfBirth = Convert.ToDateTime(reader["DateOfBirth"])
                    });
                }
            }
            return customers;
        }

        public Customer GetCustomerBy_Id(int id)
        {
            Customer cust = null;
            using (SqlConnection con = new SqlConnection(connString))
            {
                SqlCommand cmd = new SqlCommand("SELECT * FROM Customers WHERE CustomerId=@id", con);
                cmd.Parameters.AddWithValue("@id", id);
                con.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                if (reader.Read())
                {
                    cust = new Customer
                    {
                        CustomerId = Convert.ToInt32(reader["CustomerId"]),
                        FirstName = reader["FirstName"].ToString(),
                        LastName = reader["LastName"].ToString(),
                        Email = reader["Email"].ToString(),
                        Phone = reader["Phone"].ToString(),
                        DateOfBirth = Convert.ToDateTime(reader["DateOfBirth"])
                    };
                }
            }
            return cust;
        }


        public int Add_Customer(Customer cust)
        {
            using (SqlConnection con = new SqlConnection(connString))
            {
                SqlCommand cmd = new SqlCommand(@"INSERT INTO Customers 
                (FirstName, LastName, Email, Phone, DateOfBirth) 
                VALUES (@FirstName, @LastName, @Email, @Phone, @DateOfBirth); 
                SELECT SCOPE_IDENTITY();", con);
                cmd.Parameters.AddWithValue("@FirstName", cust.FirstName);
                cmd.Parameters.AddWithValue("@LastName", cust.LastName);
                cmd.Parameters.AddWithValue("@Email", cust.Email);
                cmd.Parameters.AddWithValue("@Phone", cust.Phone);
                cmd.Parameters.AddWithValue("@DateOfBirth", cust.DateOfBirth);
                con.Open();
                return Convert.ToInt32(cmd.ExecuteScalar());
            }
        }

        public bool Update_Customer(Customer cust)
        {
            using (SqlConnection con = new SqlConnection(connString))
            {
                SqlCommand cmd = new SqlCommand(@"UPDATE Customers SET 
                    FirstName=@FirstName, LastName=@LastName, Email=@Email, 
                    Phone=@Phone, DateOfBirth=@DateOfBirth 
                    WHERE CustomerId=@CustomerId", con);
                cmd.Parameters.AddWithValue("@FirstName", cust.FirstName);
                cmd.Parameters.AddWithValue("@LastName", cust.LastName);
                cmd.Parameters.AddWithValue("@Email", cust.Email);
                cmd.Parameters.AddWithValue("@Phone", cust.Phone);
                cmd.Parameters.AddWithValue("@DateOfBirth", cust.DateOfBirth);
                cmd.Parameters.AddWithValue("@CustomerId", cust.CustomerId);
                con.Open();
                return cmd.ExecuteNonQuery() > 0;
            }
        }

        public bool Delete_Customer(int id)
        {
            using (SqlConnection con = new SqlConnection(connString))
            {
                SqlCommand cmd = new SqlCommand("DELETE FROM Customers WHERE CustomerId=@id", con);
                cmd.Parameters.AddWithValue("@id", id);
                con.Open();
                return cmd.ExecuteNonQuery() > 0;
            }
        }
    }
}