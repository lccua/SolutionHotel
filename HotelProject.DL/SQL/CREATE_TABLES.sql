DROP TABLE dbo.Organizer;
DROP TABLE dbo.Registration_Member;
DROP TABLE dbo.Registration;
DROP TABLE dbo.Member;
DROP TABLE dbo.Customer;
DROP TABLE dbo.Activity;
DROP TABLE dbo.Activity_Info;



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






-- Dummy data

-- 20 Sets of Dummy Inserts for Activity_Info table with formatted addresses
INSERT INTO Activity_Info (description, address, duration)
VALUES
    ('City Walking Tour', 'San Francisco|94105|Market St|75', 120),
    ('Cruise and Snorkeling', 'Maui|96732|Ocean Blvd|45', 180),
    ('Wine Tasting', 'Napa Valley|94558|Vineyard Rd|90', 90),
    ('Safari Adventure', 'Nairobi|00100|Savannah Ave|120', 120),
    ('Island Hopping', 'Phuket|83100|Beach Rd|150', 150),
    ('Historical Museum Tour', 'Rome|00120|Forum Ave|90', 90),
    ('Skiing in the Alps', 'Innsbruck|6020|Mountain Trail|120', 120),
    ('Wildlife Safari', 'Cape Town|8000|Savannah Rd|180', 180),
    ('Jungle Trekking', 'Amazon Rainforest|12345|Rainforest Trail|150', 150),
    ('Bicycle Tour', 'Amsterdam|1012|Canal St|90', 90),
    ('Desert Jeep Adventure', 'Dubai|12345|Desert Rd|120', 120),
    ('Scuba Diving', 'Great Barrier Reef|4871|Coral Reef Dr|180', 180),
    ('National Park Hike', 'Yellowstone|82190|Trail Rd|90', 90),
    ('Sightseeing Boat Tour', 'Venice|30100|Canal Grande|120', 120),
    ('Ziplining Adventure', 'Costa Rica|50602|Rainforest Rd|180', 180),
    ('Historical Castle Tour', 'Edinburgh|EH1 1RF|Castle Rd|90', 90),
    ('Hot Air Balloon Ride', 'Cappadocia|50180|Valley Rd|120', 120),
    ('Golf Retreat', 'Scottsdale|85251|Fairway Dr|180', 180),
    ('Horseback Riding', 'Wyoming|82009|Trail Ranch|90', 90),
    ('Cultural Food Tour', 'Tokyo|100-8103|Street Food St|120', 120);

-- 20 Sets of Dummy Inserts for Activity table with activity_info_id references and discount as 0
INSERT INTO Activity (name, scheduled_date, available_spots, adult_price, child_price, discount, activity_info_id)
VALUES
    ('Golden Gate Bridge Tour', '2023-12-20', 30, 40.00, 20.00, 10, 1),
    ('Tropical Paradise Cruise', '2023-12-25', 40, 60.00, 30.00, 0, 2),
    ('Winery Experience', '2023-12-30', 20, 25.00, 15.00, 5, 3),
    ('Safari Expedition', '2024-01-05', 25, 50.00, 30.00, 10, 4),
    ('Island Adventure Day', '2024-01-10', 35, 45.00, 25.00, 0, 5),
    ('Ancient Rome Tour', '2024-01-15', 20, 30.00, 15.00, 0, 6),
    ('Alpine Ski Experience', '2024-01-20', 15, 55.00, 35.00, 5, 7),
    ('Savannah Safari', '2024-01-25', 30, 65.00, 40.00, 0, 8),
    ('Amazon Adventure', '2024-01-30', 40, 75.00, 45.00, 10, 9),
    ('Canal Cruise', '2024-02-05', 25, 35.00, 20.00, 0, 10),
    ('Desert Expedition', '2024-02-10', 35, 50.00, 30.00, 0, 11),
    ('Underwater Exploration', '2024-02-15', 30, 60.00, 40.00, 5, 12),
    ('National Park Adventure', '2024-02-20', 20, 40.00, 25.00, 0, 13),
    ('Venetian Waterways Tour', '2024-02-25', 25, 35.00, 20.00, 10, 14),
    ('Canopy Zipline Thrill', '2024-03-01', 40, 45.00, 30.00, 0, 15),
    ('Medieval Castle Quest', '2024-03-05', 30, 35.00, 20.00, 5, 16),
    ('Aerial Balloon Experience', '2024-03-10', 20, 50.00, 35.00, 0, 17),
    ('Golf Getaway', '2024-03-15', 35, 75.00, 50.00, 10, 18),
    ('Western Trail Ride', '2024-03-20', 30, 45.00, 25.00, 0, 19),
    ('Japanese Culinary Tour', '2024-03-25', 40, 40.00, 25.00, 0, 20);

-- Dummy inserts for Customer table with formatted telephone numbers and addresses
INSERT INTO Customer (name, email, phone, address, status)
VALUES
    ('John Doe', 'john.doe@example.com', '0412457889', '123 Main St|10001|Ave A|1', 1),
    ('Alice Smith', 'alice.smith@example.com', '0412457890', '456 Elm St|20002|Bvld B|1', 1),
    ('Bob Johnson', 'bob.johnson@example.com', '0412457891', '789 Oak St|30003|Ct C|1', 1),
    ('Eva Williams', 'eva.williams@example.com', '0412457892', '234 Maple St|40004|Dv D|1', 1),
    ('Mike Davis', 'mike.davis@example.com', '0412457893', '567 Birch St|50005|Ev E|1', 1),
    ('Lisa Wilson', 'lisa.wilson@example.com', '0412457894', '890 Cedar St|60006|Fv F|1', 1),
    ('David Lee', 'david.lee@example.com', '0412457895', '345 Pine St|70007|Gv G|1', 1),
    ('Mary Taylor', 'mary.taylor@example.com', '0412457896', '678 Oak St|80008|Hv H|1', 1),
    ('Kevin Moore', 'kevin.moore@example.com', '0412457897', '456 Elm St|90009|Iv I|1', 1),
    ('Helen Adams', 'helen.adams@example.com', '0412457898', '123 Main St|10010|Jv J|1', 1),
    ('William Wilson', 'william.wilson@example.com', '0412457899', '890 Cedar St|11011|Kv K|1', 1),
    ('Nancy Hall', 'nancy.hall@example.com', '0412457800', '234 Maple St|12012|Lv L|1', 1),
    ('Michael Johnson', 'michael.johnson@example.com', '0412457801', '567 Birch St|13013|Mv M|1', 1),
    ('Laura White', 'laura.white@example.com', '0412457802', '123 Main St|14014|Nv N|1', 1),
    ('Daniel Smith', 'daniel.smith@example.com', '0412457803', '456 Elm St|15015|Ov O|1', 1),
    ('Sarah Davis', 'sarah.davis@example.com', '0412457804', '678 Oak St|16016|Pv P|1', 1),
    ('Thomas Lee', 'thomas.lee@example.com', '0412457805', '345 Pine St|17017|Qv Q|1', 1),
    ('Jennifer Moore', 'jennifer.moore@example.com', '0412457806', '890 Cedar St|18018|Rv R|1', 1),
    ('Richard Adams', 'richard.adams@example.com', '0412457807', '123 Main St|19019|Sv S|1', 1),
    ('Catherine Wilson', 'catherine.wilson@example.com', '0412457808', '678 Oak St|20020|Tv T|1', 1);

-- Dummy inserts for Member table with customer_id references and status set to 1
INSERT INTO Member (name, birthday, status, customer_id)
VALUES
    ('Sophia Brown', '2000-03-15', 1, 1),
    ('James Jackson', '1995-08-27', 1, 2),
    ('Emma White', '1999-11-05', 1, 3),
    ('Christopher Miller', '1990-12-20', 1, 4),
    ('Olivia Hall', '1998-05-10', 1, 5),
    ('Matthew Adams', '2002-02-03', 1, 6),
    ('Ava Smith', '1996-07-21', 1, 7),
    ('Liam Taylor', '1994-04-12', 1, 8),
    ('Isabella Wilson', '1998-10-30', 1, 9),
    ('Michael Davis', '2001-01-25', 1, 10),
    ('Sophia Brown', '2000-03-15', 1, 11),
    ('James Jackson', '1995-08-27', 1, 12),
    ('Emma White', '1999-11-05', 1, 13),
    ('Christopher Miller', '1990-12-20', 1, 14),
    ('Olivia Hall', '1998-05-10', 1, 15),
    ('Matthew Adams', '2002-02-03', 1, 16),
    ('Ava Smith', '1996-07-21', 1, 17),
    ('Liam Taylor', '1994-04-12', 1, 18),
    ('Isabella Wilson', '1998-10-30', 1, 19),
    ('Michael Davis', '2001-01-25', 1, 20);
