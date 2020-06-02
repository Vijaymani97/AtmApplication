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
       public SqlConnection conn;
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
            return User3;
        }
         public User Deposit(User ID,int DAccNum,decimal DAmount)
         {
            string selectQuery="SELECT AvailBal,AccNo FROM AccDetails where AccNo=" + DAccNum +" and CusID=" + ID.UserID;
            SqlCommand selectCommand = new SqlCommand(selectQuery,conn);
            conn.Open();
            User User4=new User();
            SqlDataReader reader = selectCommand.ExecuteReader();
            if(reader.Read())
            {
            User4.Balance = reader.GetDecimal(0);
            User4.DAccNum = reader.GetInt32(1);
            }
            User4.Balance= DAmount + User4.Balance  ;
            conn.Close();
            conn.Open();
            string updatequery = "UPDATE AccDetails SET AvailBal=" +User4.Balance + "WHERE AccNo=" +DAccNum +" and CusID=" + ID.UserID;
            SqlCommand updatecommand = new SqlCommand(updatequery, conn);
            updatecommand.ExecuteNonQuery();
            conn.Close();
            conn.Open();
            DateTime now=DateTime.Now;
            const string insertQuery = "INSERT INTO TransactionDetails (AccNo,TransType,TransAmount,TransDate) Values (@AccNo,@TransType,@TransAmount,@TransDate)";
            SqlCommand insertcommand = new SqlCommand(insertQuery, conn);
            insertcommand.Parameters.AddWithValue("@AccNo", DAccNum);
            insertcommand.Parameters.AddWithValue("@TransType", 'D');
            insertcommand.Parameters.AddWithValue("@TransAmount", DAmount);
            insertcommand.Parameters.AddWithValue("@TransDate", now);
            insertcommand.ExecuteNonQuery();
            conn.Close();
            return User4;
        }
        public User Withdraw(User ID, int WAccNum, decimal WAmount)
         {
            string selectQuery="SELECT AvailBal FROM AccDetails where AccNo="+WAccNum +" and CusID=" + ID.UserID;
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
                            User5.Balance -= WAmount;
                            conn.Open();
                            string updatequery = "UPDATE AccDetails SET AvailBal=" +User5.Balance + "WHERE AccNo=" +WAccNum +" and CusID=" + ID.UserID;
                            SqlCommand updatecommand = new SqlCommand(updatequery, conn);
                            updatecommand.ExecuteNonQuery();
                            conn.Close();
                            conn.Open();
                            DateTime now=DateTime.Now;
                            const string insertQuery = "INSERT INTO TransactionDetails (AccNo,TransType,TransAmount,TransDate) Values (@AccNo,@TransType,@TransAmount,@TransDate)";
                            SqlCommand insertcommand = new SqlCommand(insertQuery, conn);
                        //    insertcommand.Parameters.AddWithValue("@TransID", 300);
                            insertcommand.Parameters.AddWithValue("@AccNo", WAccNum);
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
            User6.Balance -= TAmount;
            conn.Open();
            string updatequery = "UPDATE AccDetails SET AvailBal=" +User6.Balance + "WHERE AccNo=" +SenderAccNum;
            SqlCommand updatecommand = new SqlCommand(updatequery, conn);
            updatecommand.ExecuteNonQuery();
            Console.WriteLine("your Balancs(Sender) is :"+User6.Balance);
            conn.Close();
            conn.Open();
            DateTime now=DateTime.Now;
            const string insertQuery = "INSERT INTO TransactionDetails (AccNo,TransType,TransAmount,TransDate) Values (@AccNo,@TransType,@TransAmount,@TransDate)";
            SqlCommand insertcommand = new SqlCommand(insertQuery, conn);
            insertcommand.Parameters.AddWithValue("@AccNo", SenderAccNum);
            insertcommand.Parameters.AddWithValue("@TransType", 'W');
            insertcommand.Parameters.AddWithValue("@TransAmount", TAmount);
            insertcommand.Parameters.AddWithValue("@TransDate", now);
            insertcommand.ExecuteNonQuery();
            conn.Close();
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
            updatecmd.ExecuteNonQuery();
            conn.Close();
            conn.Open();
            DateTime now1=DateTime.Now;
            const string insertQuery1 = "INSERT INTO TransactionDetails (AccNo,TransType,TransAmount,TransDate) Values (@AccNo,@TransType,@TransAmount,@TransDate)";
            SqlCommand insertcommand1 = new SqlCommand(insertQuery1, conn);
            insertcommand1.Parameters.AddWithValue("@AccNo", ReceiverAccNum);
            insertcommand1.Parameters.AddWithValue("@TransType", 'D');
            insertcommand1.Parameters.AddWithValue("@TransAmount",TAmount);
            insertcommand1.Parameters.AddWithValue("@TransDate", now1);
            insertcommand1.ExecuteNonQuery();
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
                var trans2 = new TransDetails
                {
                    TransID = reader1.GetInt32(0),
                    AccNo = reader1.GetInt32(1),
                    TransType = reader1.GetString(2),
                    TransAmount = reader1.GetDecimal(3),
                    TransDate = reader1.GetDateTime(4)
                };
                trans1[i] = trans2;
             //       Console.WriteLine(trans2.TransID);
             //       Console.WriteLine(trans2.TransDate);
                    Console.WriteLine( "  "+trans2.TransID+" \t"+trans2.AccNo+" \t"+ trans2.TransType+" \t "+trans2.TransAmount+" \t "+trans2.TransDate);
                    i++;
                 }
            conn.Close();
            return trans1;
        }
        public void ChangePin(User ID,int newpin)
        {
            string changeQuery = "UPDATE  Login SET Pin="+newpin+" where CusID = "+ ID.UserID;
            SqlCommand updateCommand = new SqlCommand(changeQuery, conn);
            conn.Open();
            SqlDataReader sqlDataReaders = updateCommand.ExecuteReader();
            if (sqlDataReaders != null)
            {
            }
            conn.Close();
             }
    }
}