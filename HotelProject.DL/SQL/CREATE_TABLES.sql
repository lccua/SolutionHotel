DROP TABLE Member;
DROP TABLE Customer;

-- Create the Customer table
CREATE TABLE Customer (
    ID INT IDENTITY(1,1) PRIMARY KEY,
    name NVARCHAR(255) NOT NULL,
    email NVARCHAR(255) NOT NULL,
    phone NVARCHAR(20) NOT NULL,
    address NVARCHAR(255) NOT NULL,
    number_of_members INT NOT NULL,	
    status INT NOT NULL
);

-- Create the Member table
CREATE TABLE Member (
    ID INT IDENTITY(1,1) PRIMARY KEY,
    name NVARCHAR(255) NOT NULL,
    birthday DATE NOT NULL,
    customer_id INT NOT NULL,
    FOREIGN KEY (customer_id) REFERENCES Customer(ID)
);
