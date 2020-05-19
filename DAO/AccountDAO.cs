using System;
using ConsoleBankApplication.models;
using System.Linq;
using System.Data;
using System.Data.SqlClient;
//using Microsoft.Data.Sqlite;
namespace ConsoleBankApplication.DAO
{
    public class AccountDAO
    {
        string DataSource="DESKTOP-07ACERG";
        
        SqlConnection conn;
        
        public AccountDAO()
        {
          String connStr = "Data Source=DESKTOP-07ACERG;Initial Catalog=ATM;Integrated Security=True";
         conn = new SqlConnection(connStr);
            
        }
        public User Login(int UserID,int Pin)
    
        {
            string selectQuery = "SELECT CusID,CusName FROM Login where CusID = "+UserID + " AND Pin = " + Pin;
                             
             SqlCommand selectCommand = new SqlCommand(selectQuery, conn);

            
            conn.Open();
             
            SqlDataReader reader = selectCommand.ExecuteReader();
            User user1=new User();

            while (reader.Read())
            {
                
            
                user1.UserID=reader.GetInt32(0);
                user1.Name=reader.GetString(1);

            }
            conn.Close();
            return user1;

        }

        public User CheckBalance(User ID)
        {
            string checkQuery = "SELECT AvailBal FROM AccDetails where CusID = "+ ID.UserID;
                             
            SqlCommand selectCommand = new SqlCommand(checkQuery, conn);
 
            
            conn.Open();
            SqlDataReader reader = selectCommand.ExecuteReader();
            User User3=new User();
            while (reader.Read())
            {
        
                User3.Balance=reader.GetFloat(0);             
 
            }
            conn.Close();
            Console.WriteLine("your Balancs is :"+User3.Balance);
            return User3;
 
        }
        public User ChangePin(User ID,int newpin)
        {
            string changeQuery = "UPDATE  Login SET Pin="+newpin+" where CusID = "+ ID.UserID;
                             
            SqlCommand updateCommand = new SqlCommand(changeQuery, conn);
 
            
            conn.Open();
            SqlDataReader reader = updateCommand.ExecuteReader();
            User user2=new User();
            
            conn.Close();
            Console.WriteLine( " your pin has been changed sucessfully");
            return user2;
 
        }
    }
}