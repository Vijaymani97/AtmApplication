namespace ConsoleBankApplication.models
{
    public class User
    {
            public int Pin{get;set;} 
            public int UserID{get;set;} 
            public string Name{get;set;} 
             public int AccNum{get;set;} 
             public int AccNum1{get;set;} 
             public int SenderAccNum{get;set;} 
             public int ReciverAccNum{get;set;} 
            public decimal TotalAmount{get;set;}
            public decimal Balance{get;set;}
            public decimal DAmount{get;set;}
             public decimal WAmount{get;set;}
              public decimal TAmount{get;set;}
            

    }
}