using System;
using ConsoleBankApplication.models;
using System.Linq;
using System.Data;
using System.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Configuration.Json;
using Microsoft.Extensions.Configuration.EnvironmentVariables;
//using Microsoft.Data.Sqlite;
namespace ConsoleBankApplication.DAO
{
    public class AccountDAO
    {
       // string DataSource="DESKTOP-07ACERG";
        
        SqlConnection conn;
       // string connStr;
        
        public AccountDAO()
        {
            IConfiguration Configuration = new ConfigurationBuilder()
            .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
            .AddEnvironmentVariables()
            .Build();            
            var section= Configuration.GetSection("Connstring");
            var connStr = section.Value;
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

         public User Deposit(User ID,int AccNum,decimal DAmount)
         {  
            string selectQuery="SELECT AvailBal,AccNo FROM AccDetails where AccNo=" + AccNum +" and CusID=" + ID.UserID;
            SqlCommand selectCommand = new SqlCommand(selectQuery,conn);
            Console.WriteLine("User id "+ID.UserID);       
            Console.WriteLine("Acc num "+AccNum); 
            conn.Open();
            User User4=new User();
            SqlDataReader reader = selectCommand.ExecuteReader();
            if(reader.Read())
            {
            User4.Balance = reader.GetDecimal(0);    
            User4.AccNum = reader.GetInt32(1);          
            }
       //     Console.WriteLine("Before"+User4.Balance);
            User4.Balance= DAmount + User4.Balance  ;
         //   Console.WriteLine("After"+User4.Balance);
            conn.Close();
            conn.Open();
            string updatequery = "UPDATE AccDetails SET AvailBal=" +User4.Balance + "WHERE AccNo=" +AccNum +" and CusID=" + ID.UserID;
            SqlCommand updatecommand = new SqlCommand(updatequery, conn);
            int RowCount = updatecommand.ExecuteNonQuery();
            conn.Close();
            conn.Open();
            Console.WriteLine("Before Insert");
            DateTime now=DateTime.Now;
            Console.WriteLine("time  "+now);
            string insertQuery = "INSERT INTO TransactionDetails (AccNo,TransType,TransAmount,TransDate) Values (@AccNo,@TransType,@TransAmount,@TransDate)";
            SqlCommand insertcommand = new SqlCommand(insertQuery, conn);
            insertcommand.Parameters.AddWithValue("@AccNo", AccNum);
            insertcommand.Parameters.AddWithValue("@TransType", 'D');
            insertcommand.Parameters.AddWithValue("@TransAmount", DAmount);
            insertcommand.Parameters.AddWithValue("@TransDate", now);
            insertcommand.ExecuteNonQuery();  
            conn.Close();    
            Console.WriteLine("after Insert");
           // conn.Close();    
            return User4;  
         }   

         public User Withdraw(int AccNum1,decimal WAmount)
         {  
            
            string selectQuery="SELECT AvailBal FROM AccDetails where AccNo="+AccNum1;
            SqlCommand selectCommand = new SqlCommand(selectQuery,conn);

            User User5=new User();         
            conn.Open();
            SqlDataReader reader = selectCommand.ExecuteReader();
            if (reader.Read())
            {
    
            User5.Balance = reader.GetDecimal(0);  
            conn.Close();            
       
             }
             if(User5.Balance >= WAmount)
                    {
                            User5.Balance=  User5.Balance - WAmount ;
                            conn.Open();
                            string updatequery = "UPDATE AccDetails SET AvailBal=" +User5.Balance + "WHERE AccNo=" +AccNum1;
                            SqlCommand updatecommand = new SqlCommand(updatequery, conn);
                            int RowCount = updatecommand.ExecuteNonQuery();    
                            conn.Close();
                            conn.Open();
                            DateTime now=DateTime.Now; 
                            string insertQuery = "INSERT INTO TransactionDetails (AccNo,TransType,TransAmount,TransDate) Values (@AccNo,@TransType,@TransAmount,@TransDate)";
                            SqlCommand insertcommand = new SqlCommand(insertQuery, conn);
                        //    insertcommand.Parameters.AddWithValue("@TransID", 300);
                            insertcommand.Parameters.AddWithValue("@AccNo", AccNum1);
                            insertcommand.Parameters.AddWithValue("@TransType", 'W');
                            insertcommand.Parameters.AddWithValue("@TransAmount", WAmount);
                            insertcommand.Parameters.AddWithValue("@TransDate", now);
                            insertcommand.ExecuteNonQuery();  
                            conn.Close();    
                                                      
                    }
             else 
                    {
                        Console.WriteLine("You Haven't Sufficient Balance");
                    }

        return User5;
            
         }  

     public User Transfer(int SenderAccNum,int ReceiverAccNum,decimal TAmount)
        
         {  
        //Sender Side
        string selectQuery="SELECT AvailBal FROM Accdetails where AccNo="+SenderAccNum;
        SqlCommand selectCommand = new SqlCommand(selectQuery,conn);
        User User6=new User();         
        conn.Open();
        SqlDataReader reader = selectCommand.ExecuteReader();
        if (reader.Read())
        {
         User6.Balance = reader.GetDecimal(0);  
         conn.Close();            
        }
               if(User6.Balance >= TAmount)
                  {
                        User6.Balance=  User6.Balance - TAmount ;
                        conn.Open();
                        string updatequery = "UPDATE AccDetails SET AvailBal=" +User6.Balance + "WHERE AccNo=" +SenderAccNum;
                        SqlCommand updatecommand = new SqlCommand(updatequery, conn);    
                        int RowCount = updatecommand.ExecuteNonQuery();                           
                        conn.Close();    
                        Console.WriteLine("your Balancs(Sender) is :"+User6.Balance);
                 }
                 else 
                 {
                      Console.WriteLine("You Haven't Sufficient Balance");
                 }   
            conn.Open();
            //Receiver Side
            string selectQry="SELECT AvailBal FROM Accdetails where AccNo="+ReceiverAccNum;
            SqlCommand selectCmd = new SqlCommand(selectQry,conn);       
            SqlDataReader reader1 = selectCmd.ExecuteReader();
            if (reader1.Read())
            {
            User6.Balance = reader1.GetDecimal(0);       
            conn.Close();       
            }
            User6.Balance= TAmount + User6.Balance  ;
            conn.Open();
            string updateqry = "UPDATE AccDetails SET AvailBal=" +User6.Balance + "WHERE AccNo=" +ReceiverAccNum;
            SqlCommand updatecmd = new SqlCommand(updateqry, conn);
             int RowCount2 = updatecmd.ExecuteNonQuery();                  
            conn.Close();  
            return User6;
         }    

       public TransDetails[] Trans(int AccNo)
         {

            string countQuery = "SELECT  COUNT(*) FROM TransactionDetails  WHERE AccNo = "+ AccNo;
            SqlCommand countCommand = new SqlCommand(countQuery, conn);         
            conn.Open();

            Int32 num=(Int32)countCommand.ExecuteScalar();
            Console.WriteLine("Number of Count"+num);

            TransDetails[] trans1= new TransDetails[num];
            
            string tranQuery = "SELECT  TransID,AccNo,TransType,TransAmount ,TransDate FROM TransactionDetails WHERE AccNo = "+ AccNo;                           
            SqlCommand selectCommand = new SqlCommand(tranQuery, conn);    
            SqlDataReader reader1 = selectCommand.ExecuteReader();
            int i=0;
            Console.WriteLine("TransID   AccNO   TransType     TransAmount            TransDate ");
            while (reader1.Read())
                 {
                    TransDetails trans2=new TransDetails();
                    trans2.TransID=reader1.GetInt32(0); 
                    trans2.AccNo=reader1.GetInt32(1);
                    trans2.TransType=reader1.GetString(2);  
                    trans2.TransAmount=reader1.GetDecimal(3);  
                    trans2.TransDate=reader1.GetDateTime(4); 
		    

                    trans1[i] = trans2;
             //       Console.WriteLine(trans2.TransID);
             //       Console.WriteLine(trans2.TransDate);
                    Console.WriteLine( "  "+trans2.TransID+" \t"+trans2.AccNo+" \t"+ trans2.TransType+" \t "+trans2.TransAmount+" \t "+trans2.TransDate);
                    i++;  
                 }          
           
            conn.Close();
            return trans1;
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