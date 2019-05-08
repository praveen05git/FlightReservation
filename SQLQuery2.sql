

create table tblLogin_HMS03_Team7
(
	LoginID int primary key identity(100,1),
	uname varchar(100) unique,
	pwd varchar(100),
	roles varchar(50)
)

insert into tblLogin_HMS03_Team7 values('Batman','Bat','Manager' )
insert into tblLogin_HMS03_Team7 values('Superman','Super','Customer' )
insert into tblLogin_HMS03_Team7 values('Spiderman','Spider','Flight' )
insert into tblLogin_HMS03_Team7 values('pand','123','BonusMileApprover' )
select * from tblLogin_HMS03_Team7

create table tblEmployee_HMS03_Team7

(
	EmployeeID int identity(1000,1) primary key,
	LoginID int foreign key references tblLogin_HMS03_Team7(LoginID),
	Employee_Name varchar(50),
	Gender varchar(30),
	DateofBirth date,
	Age int,
	Designation varchar(50),
	Airlines varchar(50),
	Work_location varchar(50),
	Contact_Number bigint,
	EmailID varchar(50),
	Address varchar(200)	
)

insert into tblEmployee_HMS03_Team7 values (101,'sandhu','Male','1992-03-02',26,'Scheduler','sandlines','Chennai',8754686423,'uhdnas@gmail.com','sandroid')
select * from tblEmployee_HMS03_Team7

CREATE TABLE tblCustomer_HMS03_Team7

(
	CustomerID INT IDENTITY(2001,1) PRIMARY KEY,
	LoginID INT FOREIGN KEY REFERENCES tblLogin_HMS03_Team7(LoginID),
	Title VARCHAR(5),
	FirstName VARCHAR(20) NOT NULL,
	LastName VARCHAR(20) NOT NULL,
	DateOfBirth DATE,
	Gender VARCHAR(7) NOT NULL,
	StreetAddress VARCHAR(20),
	City VARCHAR(15),
	State VARCHAR(15),
	ZipCode INT,
	Nationality VARCHAR(15) DEFAULT('Indian'),
	MobileNumber BIGINT,
	AlternateNumber BIGINT,
	PhoneNumber BIGINT,
	Email VARCHAR(25),
	CompanyName VARCHAR(15),
	OfficeAddress VARCHAR(20),
	BonusMilePoints DECIMAL(18,2)
)

insert into tblCustomer_HMS03_Team7 values (102,'Mr','Sandeep','Rayudu','1996-04-18','Male','Tavvavari Street','Draksharamam','Andhra Pradesh',522262,default,8754686424,9087569823,null,'peed@gmail.com',null,null,null)

select * from tblCustomer_HMS03_Team7

CREATE TABLE tblAeroplane_HMS03_Team7
(
	PlaneRegistrationID INT PRIMARY KEY IDENTITY(100,1),
	DateOfCommencement DATE NOT NULL,
	DateOdMAnufacturing DATE NOT NULL,
	AirplaneSeries VARCHAR(60) NOT NULL,
	Airline VARCHAR(60),
	Capacity_Premium INT NOT NULL,
	Capacity_FirstClass INT NOT NULL,
	Capacity_Economy INT NOT NULL,
	TakeOffWeight INT NOT NULL,
	MaximumDistance DECIMAL,
	LocationOfPlane VARCHAR(60) NOT NULL,
	SchedulerID INT FOREIGN KEY REFERENCES tblEmployee_HMS03_Team7(EmployeeID),
	RecentUpdate DATETIME,
	ScheduleStatus VARCHAR(60) DEFAULT('Not Scheduled')
)

CREATE TABLE tblSchedule_HMS03_Team7
(
	ScheduleID INT IDENTITY(100,1) PRIMARY KEY,
	PlaneRegistrationID INT FOREIGN KEY REFERENCES tblAeroplane_HMS03_Team7(PlaneRegistrationID),
	Source VARCHAR(50) NOT NULL,
	Destination VARCHAR(50) NOT NULL,
	Departure DATETIME NOT NULL,
	Arrival DATETIME NOT NULL,
	Fare_Premium_Adult DECIMAL, 
	Fare_Economy_Adult DECIMAL,
	Fare_Firstclass_Adult DECIMAL,
	Fare_Premium_Child DECIMAL,
	Fare_Firstclass_Child DECIMAL,
	Fare_Economy_Child DECIMAL,
	Availability_Premium INT,
	Availability_Firstclass INT,
	Availability_Economy INT,
	TravelDistance DECIMAL,
	StatusofFlight VARCHAR(50),
	BonusMiles varchar(50),
	BonusMilesPoints DECIMAL
)

CREATE TABLE tblSearch_HMS03_Team7
(
	SearchID INT IDENTITY(100,1) PRIMARY KEY,
	Source VARCHAR(50) NOT NULL,
	Destination VARCHAR(50) NOT NULL,
	Departure DATE,
	Class VARCHAR(15),
	NoOfPassengers_Adult INT,
	NoOfPassengers_Child  INT
)

CREATE TABLE tblJourney_HMS03_Team7
(
	JourneyID INT IDENTITY(100,1) PRIMARY KEY,
	CustomerID INT FOREIGN KEY REFERENCES tblCustomer_HMS03_Team7(CustomerID),
	ScheduleID INT FOREIGN KEY REFERENCES tblSchedule_HMS03_Team7(ScheduleID),
	TotalBookingFare DECIMAL(18,2),
	NoOfPassengers_Adult INT,
	NoOfPassengers_Child  INT,
	PaymentStatus VARCHAR(15),
	BonusRequstStatus VARCHAR(15) DEFAULT('Not Requested'),
	BookingDate DATETIME,
	AdditionalBaggageCharge DECIMAL(18,2)

)

CREATE TABLE tblPassenger_HMS03_Team7
(
	PassengerID INT IDENTITY(100,1) PRIMARY KEY,
	JourneyID INT FOREIGN KEY REFERENCES tblJourney_HMS03_Team7(JourneyID),
	PassengerName VARCHAR(50),
	Gender VARCHAR(7),
	Age INT,
	NoOfBaggages INT,
	TotalWeightOfBaggages DECIMAL,
	CheckInStatus VARCHAR(15) DEFAULT('Not CheckedIn'),
	CheckOutDate DATETIME
)

CREATE TABLE tblPayment_HMS03_Team7
(
	TransactionID INT IDENTITY(200,1) PRIMARY KEY,
	JourneyID INT FOREIGN KEY REFERENCES tblJourney_HMS03_Team7(JourneyID),
	CardType VARCHAR(60),
	CardNumber BIGINT,
	CVV INT,
	ExpiryDate DATE,
	PaymentDate DATETIME,
	PaymentType VARCHAR(60),
	CustomerID INT FOREIGN KEY REFERENCES tblCustomer_HMS03_Team7(CustomerID),
	ScheduleID INT FOREIGN KEY REFERENCES tblSchedule_HMS03_Team7(ScheduleID)
)

create table tblBonusMilesRequest_HMS03_Team7
(
	BonusID INT IDENTITY(200,1) PRIMARY KEY,
	JourneyID INT FOREIGN KEY REFERENCES tblJourney_HMS03_Team7(JourneyID),
	BonusRequestDate datetime,
	BonusApprovalStatus varchar(50)
)
