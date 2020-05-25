using System;
namespace ConsoleBankApplication.models
{
    public class TransDetails
    {
            public int AccNo{get;set;} 
            public int TransID{get;set;} 
            public string TransType{get;set;} 
            public decimal TransAmount{get;set;} 
            public DateTime TransDate{get;set;}
            

    }
}