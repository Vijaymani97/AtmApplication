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

         public User Transfer(int SenderAccNum,int ReceiverAccNum,decimal TAmount)
         {  
        //Sender Side
        string selectQuery="SELECT AvailBal FROM Accdetails where AccNo="+SenderAccNum;
        SqlCommand selectCommand = new SqlCommand(selectQuery,conn);

        User User6=new User();         
        conn.Open();
        SqlDataReader reader = selectCommand.ExecuteReader();
        while (reader.Read())
        {
    
        User6.Balance = reader.GetDecimal(0);              
       
        }
               if(User6.Balance >= TAmount)
                    {
                        User6.Balance=  User6.Balance - TAmount ;

                    string updatequery = "UPDATE AccDetails SET AvailBal=" +User6.Balance + "WHERE AccNo=" +SenderAccNum;
                    SqlCommand updatecommand = new SqlCommand(updatequery, conn);
                    
                    conn.Close();    
                    Console.WriteLine("your Balancs is :"+User6.Balance);
                        

                    }
                 else 
                     {
                        Console.WriteLine("You Haven't Sufficient Balance");
                     }   
            conn.Close();
            //Receiver Side
            string selectQry="SELECT AvailBal FROM Accdetails where AccNo="+ReceiverAccNum;
            SqlCommand selectCmd = new SqlCommand(selectQry,conn);
         //  User User6=new User();         
            conn.Open();
            SqlDataReader reader1 = selectCmd.ExecuteReader();
            while (reader1.Read())
            {
            User6.Balance = reader1.GetDecimal(0);              
            }
            User6.Balance= TAmount + User6.Balance  ;
            string updateqry = "UPDATE AccDetails SET AvailBal=" +User6.Balance + "WHERE AccNo=" +ReceiverAccNum;
            SqlCommand updatecmd = new SqlCommand(updateqry, conn);
            conn.Close();  
            return User6;
         }    

     /*  publicTransaction[] Transactions(UserID)
         {

            stringcountQuery = "SELECT  COUNT(*) FROM Transactions WHERE UserID = "+ ID.UserID;
            SqliteCommandcountCommand = newSqliteCommand(countQuery, conn);         
            conn.Open();

            Int64num2=(Int64)countCommand.ExecuteScalar();

            Transaction[] transaction1= newTransaction[num2];
            
            stringtranQuery = "SELECT  TranID,AccountNO,TranType,TranAmount,Balance FROM Transactions WHERE UserID = "+ ID.UserID;                           
            SqliteCommandselectCommand = newSqliteCommand(tranQuery, conn);    
            SqliteDataReaderreader1 = selectCommand.ExecuteReader();

            inti=0;
            Console.WriteLine("tranID AccountNO Trantype TranAmount Balance");
            while (reader1.Read())
                            {
            Transactiontran2=newTransaction();
            tran2.TranID=reader1.GetInt32(0); 
            tran2.AccountNO=reader1.GetInt32(1);
            tran2.TranType=reader1.GetString(2);  
            tran2.TranAmount=reader1.GetInt32(3);  
            tran2.Balance=reader1.GetInt32(4); 

            transaction1[i] = tran2;
            Console.WriteLine( "  "+tran2.TranID+"     "+tran2.AccountNO+"    "+ tran2.TranType+"      "+tran2.TranAmount+"      "+ tran2.Balance );

            i++;            
            
                            }

            conn.Close();
            //
            returntransaction1;


        }   
 */


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