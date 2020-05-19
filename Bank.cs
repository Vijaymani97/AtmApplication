using System;
using System.Linq;
using System.Data;
using System.Data.SqlClient;
//using System.Data;
//using Microsoft.Data.Sqlite;
using ConsoleBankApplication.DAO;
using ConsoleBankApplication.models;

namespace ConsoleBankApplication
{
    public class Bank
    {
    int menu1 =0 ;
     int menu2 = 0;
     public int UserID = 0;
     AccountDAO accountdao;
     public Bank()
     {
         this.accountdao =new AccountDAO();
     }
     int Pin = 0;   
         //login menu
         public static void showMenu1()
         {
             Console.WriteLine(" ------------------------");
             Console.WriteLine("|   C ATM Main Menu      |");
             Console.WriteLine("|                        |");
             Console.WriteLine("| 1. Login to ATM card   |");
             Console.WriteLine("| 2. Exit                |");
             Console.WriteLine("|                        |");
             Console.WriteLine(" ------------------------");
             Console.Write("Enter your option: ");            
         }
        //main menu
         public static void showMenu2()
         {
             Console.WriteLine(" ------------------------------------------");
             Console.WriteLine("|          C ATM BANK LIMITED              |");
             Console.WriteLine("|           Customer Banking               |");        
             Console.WriteLine("|    1. Deposit Funds                      |");
             Console.WriteLine("|    2. Withdraw Funds                     |");
             Console.WriteLine("|    3. Transfer Funds                     |");
             Console.WriteLine("|    4. Check Account balance              |");
             Console.WriteLine("|    5. Display Account Transaction Log    |");
             Console.WriteLine("|    6. Change Pin                         |");
             Console.WriteLine("|                                          |");
             Console.WriteLine("|    0.Save & Exit                         |");
             Console.WriteLine(" ------------------------------------------");
             Console.Write("Enter your option: ");
         }   
 
        //main operation funtion
        public void  MainAtm()
         {
           
                      


            
                 showMenu1();
                 
            
                 menu1 = Convert.ToInt32( Console.ReadLine());
                
 
                     switch (menu1)
                     {
                         case 1:
                            String connStr = "Data Source=DESKTOP-07ACERG;Initial Catalog=ATM;Integrated Security=True";
                            SqlConnection conn = new SqlConnection(connStr);
                            


                             Console.Write("Enter ATM UserID: ");
                             UserID = Convert.ToInt32(Console.ReadLine());
                             Console.Write("Enter  PIN: ");
                             Pin = Convert.ToInt32(Console.ReadLine());
                             User User1=this.accountdao.Login(UserID,Pin);


                             
                             //
                             if(User1.Name != null)                            
                             {
                                
                                

                             
                                
                                  showMenu2();

                                  menu2 = Convert.ToInt32( Console.ReadLine());
                                  
                                                                                      
                                        switch (menu2)
                                        {
                                            
                                            case 1:
                                                Deposit();
                                                break;
                                            case 2:
                                                Withdraw();
                                                break;
                                            case 3:
                                                Transfer();
                                                break;
                                            case 4:
                                                User User3=this.accountdao.CheckBalance(User1);
                                                break;    
                                            case 5:
                                                Transaction();
                                                break; 
                                            case 6:
    


                                                //ChangePin
                                                Console.Write("Enter New Pin : ");
                                                int NewPin = Convert.ToInt32(Console.ReadLine());
                                                User User2=this.accountdao.ChangePin(User1,NewPin);
                                                break; 


                                               
                                               

                                            case 0:
                                                Console.WriteLine("  logout succesfully.");
                                                break;
                                            default:
                                                Console.WriteLine("Invalid Option Entered.");
                                                break;
                                        }
                                    }

                            else 
                            {
                                

                                Console.WriteLine("Invalid PIN.");
                            }
                            
                            break;
                        case 2:
                            break;
                        default:
                            Console.WriteLine("Invalid Option Entered.");
                            break;
                    }
                
                     Console.WriteLine("Thank you for using C ATM Bank. ");

         } 
         public void Deposit()
         { /*
            
        Console.Write("Enter Account Number : ");
        int AccNo = Convert.ToInt32(Console.ReadLine());
        Console.Write("Enter Amount : ");
        int Amount = Convert.ToInt32(Console.ReadLine());
        int DAmount;

        String connStr = "Data Source=DESKTOP-07ACERG;Initial Catalog=ATM;Integrated Security=True";
        SqlConnection conn = new SqlConnection(connStr);
        string selectQuery="SELECT AvailBal FROM Accdetails where AccNo="+AccNo;
        SqlCommand selectCommand = new SqlCommand(selectQuery,conn);

       int[] Balance = new int[1];          
        conn.Open();
        SqlDataReader reader = selectCommand.ExecuteReader();
        while (reader.Read())
        {
    
        Balance[0] = reader.GetInt32(0);              
        Console.WriteLine(reader.GetName(0));
        }
        DAmount = Amount + Balance[0];

        string updatequery = "UPDATE AccDetails SET AvailBal="+WAmount+" WHERE AccNo="+AccNo;
        SqlCommand updatecommand = new SqlCommand(updatequery, conn);
        

        Conn.Close();    

            */
         

         }

         public static void Withdraw()
         {

         }

         public static void Transfer()
         {


         }

         public  void CheckBalance(User currentuser)
         {  /*
           String connStr = "Data Source=DESKTOP-07ACERG;Initial Catalog=ATM;Integrated Security=True";
            SqlConnection conn = new SqlConnection(connStr);
            
             string selectQuery = "SELECT AvailBal FROM AccDetails where CusID = "+ currentuser.UserID;
             Console.WriteLine(selectQuery);
             SqlCommand selectCommand = new SqlCommand(selectQuery, conn);

             int[] BalanceCheck = new int[1];          
             conn.Open();
             SqlDataReader reader = selectCommand.ExecuteReader();
             while (reader.Read())
             {
            
                User2 =  reader.GetInt32(0);              
                Console.WriteLine(reader.GetName(0));
             }
                            
             conn.Close();
             Console.WriteLine("your Balancs is : "+BalanceCheck[0]);    */
             
  
         } 

         public static void Transaction()
         {

         }

         public static void ChangePin()
         {


         }

    }





}