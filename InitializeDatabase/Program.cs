using InitializeDatabase;
using System;
using System.Data.SqlClient;
using System.IO;

internal class Program
{
    static void Main(string[] args)
    {
        string connectionString = @"Data Source=DESKTOP-C2MADIB;Initial Catalog=HotelDB;Integrated Security=True";

        DatabaseInitializer initializer = new DatabaseInitializer(connectionString);
        initializer.InitializeDatabase();

        Console.WriteLine("Initialization completed");
        Console.WriteLine("Log file can be found at: C:\\Users\\lucac\\source\\repos\\SolutionHotel\\InitializeDatabase\\Log\\log.txt");
    }
}