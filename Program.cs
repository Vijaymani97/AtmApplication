using System;
using System.Linq;
using System.Data;
using System.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Configuration.Json;
using Microsoft.Extensions.Configuration.EnvironmentVariables; 


namespace ConsoleBankApplication
{
    
    class Program
    {

        static void Main(string[] args)
        {
                

           Bank obj = new Bank();
           obj.Atmapp();
           

            
        }
    }
}
