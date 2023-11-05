DROP TABLE Member;
DROP TABLE Customer;
DROP TABLE Activity;
DROP TABLE Activity_Info;

-- Create the Customer table
CREATE TABLE Customer (
    ID INT IDENTITY(1,1) PRIMARY KEY,
    name NVARCHAR(255) NOT NULL,
    email NVARCHAR(255) NOT NULL,
    phone NVARCHAR(20) NOT NULL,
    address NVARCHAR(255) NOT NULL,	
    status INT NOT NULL
);

-- Create the Member table
CREATE TABLE Member (
    ID INT IDENTITY(1,1) PRIMARY KEY,
    name NVARCHAR(255) NOT NULL,
    birthday DATE NOT NULL,
    status INT NOT NULL,
    customer_id INT NOT NULL,
    FOREIGN KEY (customer_id) REFERENCES Customer(ID)
);

-- Create the Activity_Info table
CREATE TABLE Activity_Info
(
    ID INT PRIMARY KEY IDENTITY(1,1),
    description DATE NOT NULL,
    address NVARCHAR(255) NOT NULL,	
    duration NVARCHAR(100) NOT NULL,
);

-- Create the Activity table
CREATE TABLE Activity
(
    ID INT PRIMARY KEY IDENTITY(1,1),
    name NVARCHAR(255) NOT NULL,
    scheduled_date TIME NOT NULL,
    available_spots INT NOT NULL,
    adult_price DECIMAL(10, 2) NULL,
    child_price DECIMAL(10, 2) NULL,
    discount DECIMAL(10, 2) NULL,
    activity_info_id INT FOREIGN KEY REFERENCES Activity_Info(ID)
);

-- Create the Organizer table
CREATE TABLE Organizer
(
    ID INT PRIMARY KEY IDENTITY(1,1),
    name NVARCHAR(255) NOT NULL,
    email NVARCHAR(255) NOT NULL,
    phone NVARCHAR(20) NOT NULL,
    address NVARCHAR(255) NOT NULL,	
);


