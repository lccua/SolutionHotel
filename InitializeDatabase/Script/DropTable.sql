IF EXISTS (SELECT * FROM sys.tables WHERE name = 'Organizer' AND type = 'U')
    DROP TABLE dbo.Organizer;

IF EXISTS (SELECT * FROM sys.tables WHERE name = 'Registration_Member' AND type = 'U')
    DROP TABLE dbo.Registration_Member;

IF EXISTS (SELECT * FROM sys.tables WHERE name = 'Registration' AND type = 'U')
    DROP TABLE dbo.Registration;

IF EXISTS (SELECT * FROM sys.tables WHERE name = 'Member' AND type = 'U')
    DROP TABLE dbo.Member;

IF EXISTS (SELECT * FROM sys.tables WHERE name = 'Customer' AND type = 'U')
    DROP TABLE dbo.Customer;

IF EXISTS (SELECT * FROM sys.tables WHERE name = 'Activity' AND type = 'U')
    DROP TABLE dbo.Activity;

IF EXISTS (SELECT * FROM sys.tables WHERE name = 'Activity_Info' AND type = 'U')
    DROP TABLE dbo.Activity_Info;
