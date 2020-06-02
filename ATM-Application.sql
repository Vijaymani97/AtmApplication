use ATM;
--CREATE TABLE Login (
--	"CusID" int  NOT NULL ,
--	"Pin" int NOT NULL ,
--	"CusName" nvarchar (10) NOT NULL ,
--	CONSTRAINT "PK_Cus" PRIMARY KEY  CLUSTERED 
--	(
--		"CusID"
--	));

--insert Login values(10001,2121,'Kavi');
--insert Login values(10002,1997,'Karthi');
--insert Login values(10003,1998,'Viswa');
--insert Login values(10004,6363,'Balaji');
--insert Login values(10005,2020,'Praba');
--insert Login values(10006,1122,'Prem');
--insert Login values(10007,7878,'saran');
--insert Login values(10008,2024,'Vijay');
--select * from Login;

--CREATE TABLE AccDetails (
--	"CusID" int  NOT NULL ,
--	"AccNO" int NOT NULL ,
--	"AvailBal" money  NOT NULL ,
--	CONSTRAINT "PK_AccNO" PRIMARY KEY  CLUSTERED ("AccNO"),
--	CONSTRAINT "FK_CusID" FOREIGN KEY ("CusID") REFERENCES "Login" ("CusID")
--	);

--insert AccDetails values(10001,5678920,25000);
--insert AccDetails values(10002,5678921,5000);
--insert AccDetails values(10003,5678922,30000);
--insert AccDetails values(10002,5678924,15000);
--insert AccDetails values(10001,5678925,50000);
--insert AccDetails values(10003,5678926,35000);
--insert AccDetails values(10005,5678927,65000);
--insert AccDetails values(10004,5678928,2000);
--insert AccDetails values(10006,5678929,40000);
--insert AccDetails values(10007,5678930,2500);
--insert AccDetails values(10008,5678931,80000);
--insert AccDetails values(10006,5678932,5000);
--insert AccDetails values(10003,5678933,45000);
--insert AccDetails values(10007,5678923,55000);

--select * from AccDetails;
--use ATM;
--CREATE TABLE TransactionDetails (
--	"TransID" int  NOT NULL Identity(1,1),
--	"AccNO" int NOT NULL ,
--	"TransType"  char(1) check ( TransType in ('W','D')), 
--	"TransAmount" money  NOT NULL ,
--	"TransDate" date NOT NULL,
--	CONSTRAINT "PK_TransID" PRIMARY KEY  CLUSTERED ("TransID"),
--	CONSTRAINT "FK_AccNO" FOREIGN KEY ("AccNO") REFERENCES  "AccDetails" ("AccNO")
--	);


select * from TransactionDetails;
--insert TransactionDetails values(5678923,'W',1000,'7/4/2019');
insert TransactionDetails values(5678920,'W',1000,'4/15/2018');
insert TransactionDetails values(5678921,'D',10000,'10/4/2019');
insert TransactionDetails values(5678922,'W',500,'7/5/2020');
insert TransactionDetails values(5678922,'D',1500,'5/24/2019');
insert TransactionDetails values(5678927,'D',2000,'11/20/2018');
insert TransactionDetails values(5678928,'W',1000,'4/7/2020');
insert TransactionDetails values(5678929,'W',1200,'3/20/2019');
insert TransactionDetails values(5678931,'D',3000,'10/10/2018');

insert TransactionDetails values(5678920,'D',3000,'4/15/2018');
insert TransactionDetails values(5678921,'D',5000,'10/4/2019');
insert TransactionDetails values(5678922,'W',1500,'7/5/2020');
insert TransactionDetails values(5678922,'W',3500,'5/24/2019');

select * from sys.tables;
		