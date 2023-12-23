
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
    description NVARCHAR(255) NOT NULL,
    address NVARCHAR(255) NOT NULL,	
    duration INT NOT NULL,
);

-- Create the Activity table
CREATE TABLE Activity
(
    ID INT PRIMARY KEY IDENTITY(1,1),
    name NVARCHAR(255) NOT NULL,
    scheduled_date NVARCHAR(255) NOT NULL,
    available_spots INT NOT NULL,
    adult_price DECIMAL(10, 2) NULL,
    child_price DECIMAL(10, 2) NULL,
    discount INT NULL,
    activity_info_id INT FOREIGN KEY REFERENCES Activity_Info(ID)
);

-- Create the Registration table
CREATE TABLE Registration (
    ID INT PRIMARY KEY IDENTITY(1,1),
    total_price DECIMAL NOT NULL,
    customer_id INT NOT NULL,
    activity_id INT NOT NULL,
    FOREIGN KEY (customer_id) REFERENCES Customer(ID), 
    FOREIGN KEY (activity_id) REFERENCES Activity(ID)  
);

-- Create the RegistrationMember table
CREATE TABLE Registration_Member (
    registration_id INT,
    member_id INT,
    PRIMARY KEY (registration_id, member_id),

    FOREIGN KEY (registration_id) REFERENCES Registration(ID),
    FOREIGN KEY (member_id) REFERENCES Member(ID)
);

-- Create the Organizer table with username and password
CREATE TABLE Organizer
(
    ID INT PRIMARY KEY IDENTITY(1,1),
    username NVARCHAR(50) NOT NULL UNIQUE, -- Unique constraint to ensure usernames are unique
    password_hash NVARCHAR(MAX) NOT NULL,
    name NVARCHAR(255) NOT NULL,
    email NVARCHAR(255) NOT NULL,
    phone NVARCHAR(20) NOT NULL,
    address NVARCHAR(255) NOT NULL
);