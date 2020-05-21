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
       // string DataSource="DESKTOP-07ACERG";
        
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
        
                User3.Balance=reader.GetDecimal(0);             
 
            }
            conn.Close();
            Console.WriteLine("your Balancs is :"+User3.Balance);
            return User3;
 
        }

        
         public User Deposit(int AccNum,decimal DAmount)
         {  
        
            string selectQuery="SELECT AvailBal FROM Accdetails where AccNo="+AccNum;
            SqlCommand selectCommand = new SqlCommand(selectQuery,conn);

            User User4=new User();         
            conn.Open();
            SqlDataReader reader = selectCommand.ExecuteReader();
            while (reader.Read())
            {

                 User4.Balance = reader.GetDecimal(0);              

            }
            Console.WriteLine("Before"+User4.Balance);
            User4.Balance= DAmount + User4.Balance  ;
           Console.WriteLine("After"+User4.Balance);

            string updatequery = "UPDATE AccDetails SET AvailBal=" +User4.Balance + "WHERE AccNo=" +AccNum;
            SqlCommand updatecommand = new SqlCommand(updatequery, conn);
            conn.Close();    
          
             return User4;
 
            
         }   
         public User Withdraw(int AccNum1,decimal WAmount)
         {  
        
        string selectQuery="SELECT AvailBal FROM Accdetails where AccNo="+AccNum1;
        SqlCommand selectCommand = new SqlCommand(selectQuery,conn);

       User User5=new User();         
        conn.Open();
        SqlDataReader reader = selectCommand.ExecuteReader();
        while (reader.Read())
        {
    
        User5.Balance = reader.GetDecimal(0);              
       
        }
        if(User5.Balance > WAmount)
                    {
                        User5.Balance=  User5.Balance - WAmount ;

                    string updatequery = "UPDATE AccDetails SET AvailBal=" +User5.Balance + "WHERE AccNo=" +AccNum1;
                    SqlCommand updatecommand = new SqlCommand(updatequery, conn);
                    

                    conn.Close();    
                   
                        

                    }
        else 
        {
            Console.WriteLine("You Haven't Sufficient Balance");
        }
    
          
        return User5;
            
         }  

      /*   public User Transfer(int SenderAccNum,int ReciverAccNum,decimal TAmount)
         {  
        
        string selectQuery="SELECT AvailBal FROM Accdetails where AccNo="+SenderAccNum;
        SqlCommand selectCommand = new SqlCommand(selectQuery,conn);

       User User6=new User();         
        conn.Open();
        SqlDataReader reader = selectCommand.ExecuteReader();
        while (reader.Read())
        {
    
        User6.Balance = reader.GetDecimal(0);              
       
        }
        if(User6.Balance > TAmount)
                    {
                        User5.Balance=  User5.Balance - WAmount ;

                    string updatequery = "UPDATE AccDetails SET AvailBal=" +User5.Balance + "WHERE AccNo=" +AccNum1;
                    SqlCommand updatecommand = new SqlCommand(updatequery, conn);
                    

                    conn.Close();    
                    Console.WriteLine("your Balancs is :"+User5.Balance);
                        

                    }
        else 
        {
            Console.WriteLine("You Haven't Sufficient Balance");
        }   */

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