using System;
using System.Linq;
using System.Data;
using System.Data.SqlClient;
namespace ConsoleBankApplication
{
        public static class Program
    {
       public static void Main(string[] args)
        {
            if (args is null)
            {
                throw new ArgumentNullException(nameof(args));
            }

            Bank obj = new Bank();
            obj.Atmapp();
        }
    }
}
