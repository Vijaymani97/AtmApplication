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
         public static void LoginMenu()
         {
             Console.WriteLine("    ATM Main Menu     ");
             Console.WriteLine("                       ");
             Console.WriteLine(" 1. Login              ");
             Console.WriteLine(" 2. Exit               ");
             Console.WriteLine("                       ");
             Console.Write("Enter your option: ");            
         }
        //main menu
         public static void MainMenu()
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
        public void  Atmapp()
         {
                 LoginMenu();
                 menu1 = Convert.ToInt32( Console.ReadLine());
                     switch (menu1)
                     {
                         //Login
                         case 1:
                            String connStr = "Data Source=DESKTOP-07ACERG;Initial Catalog=ATM;Integrated Security=True";
                            SqlConnection conn = new SqlConnection(connStr);
                            Console.Write("Enter ATM UserID: ");
                             UserID = Convert.ToInt32(Console.ReadLine());
                             Console.Write("Enter  PIN: ");
                             Pin = Convert.ToInt32(Console.ReadLine());
                             User User1=this.accountdao.Login(UserID,Pin);
                        //      DataTime time=DataTime.Now;
                        //     Console.WriteLine(time.ToString("yyyy--mm-dd h:mm:ss tt"));
                             if(User1.Name != null)                            
                             {
                                Console.WriteLine("Hi    "+User1.Name+ "!!!");
                               
                                MainMenu();
                                menu2 = Convert.ToInt32( Console.ReadLine());
                                     switch (menu2)
                                        {
                                            //Operations
                                            case 1:
                                                   //deposit
                                                Console.Write("Enter Account Number : ");
                                                int AccNum = Convert.ToInt32(Console.ReadLine());
                                                Console.Write("Enter Deposit Amount : ");
                                                decimal DAmount = Convert.ToDecimal(Console.ReadLine());
                                                User User4=this.accountdao.Deposit(AccNum,DAmount);
                                                Console.WriteLine("your Balancs is :"+User4.Balance);
                                                break;
                                            case 2:
                                            //withdraw
                                                Console.Write("Enter Account Number : ");
                                                int AccNum1= Convert.ToInt32(Console.ReadLine());
                                                Console.Write("Enter Withdraw Amount : ");
                                                decimal WAmount = Convert.ToDecimal(Console.ReadLine());
                                                User User5=this.accountdao.Withdraw(AccNum1,WAmount);
                                                 Console.WriteLine("your Balancs is :"+User5.Balance);
                                                break;
                                            case 3:
                                            //transfer
                                                Console.Write("Enter Sender Account Number : ");
                                                int SenderAccNum= Convert.ToInt32(Console.ReadLine());
                                                Console.Write("Enter Receiver Account Number : ");
                                                int ReceiverAccNum= Convert.ToInt32(Console.ReadLine());
                                                Console.Write("Enter Transfer Amount : ");
                                                decimal TAmount = Convert.ToDecimal(Console.ReadLine());
                                                User User6=this.accountdao.Transfer(SenderAccNum,ReceiverAccNum,TAmount);
                                                Console.WriteLine("your Balancs(Receiver) is :"+User6.Balance);
                                                break;
                                            case 4:
                                                User User3=this.accountdao.CheckBalance(User1);
                                                break;    
                                            case 5:
                                              //  Transaction;
                                                Console.Write("Enter Account Number : ");
                                                int AccNo= Convert.ToInt32(Console.ReadLine());
                                              TransDetails[] trans3=this.accountdao.Trans(AccNo);

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
                
                    

         } 
         
    }
}

        


      
