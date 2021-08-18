
using System;
using System.Data.Common;

using MySqlConnector;

using sqldsl.DBProvider;
//-----------------------------------------------------------------------------

Console.WriteLine ("MYSQL Fluent Interface / DSL Example\nby Tom Schröter");

//-----------------------------------------------------------------------------

const string MYSQLCONNECTION = "server=localhost; user=root; password=;";

const string QUERY_A = "SELECT * FROM classicmodels.customers WHERE country LIKE '%ital%'";
const string QUERY_B = "SELECT * FROM classicmodels.customers WHERE country LIKE '%bel%'";

//-----------------------------------------------------------------------------

Console.WriteLine ("\n\nClassic MYSQL \n");

//-----------------------------------------------------------------------------

try
{
    using ( MySqlConnection connection = new (MYSQLCONNECTION) )
    {
        connection.Open ();

        using ( MySqlCommand comand = new (QUERY_A, connection) )
            using ( MySqlDataReader reader = comand.ExecuteReader () )
                while ( reader.Read() )
                    printRow ( reader );

        Console.WriteLine ("Query A OK\n");

        using ( MySqlCommand comand = new (QUERY_B, connection) )
            using ( MySqlDataReader reader = comand.ExecuteReader () )
                while ( reader.Read() )
                    printRow ( reader );

        Console.WriteLine ("Query B OK\n");

        connection.Close ();
    }
}
catch ( MySqlException ex )
{
    Console.WriteLine ( ex.Message );
}
//-----------------------------------------------------------------------------

Console.WriteLine ("\n\nMYSQL DSL\n");

//-----------------------------------------------------------------------------
Providers
    .UseMYSQL   ( MYSQLCONNECTION )
    .onFailure  ( state => Console.WriteLine (state.errorMessage) )
    ?.query     ( QUERY_A, row => printRow (row) )
    ?.onFailure ( state => Console.WriteLine (state.errorMessage) )
    ?.onSuccess ( state => Console.WriteLine ("Query A OK\n") )
    ?.query     ( QUERY_B, row => printRow (row) )
    ?.onFailure ( state => Console.WriteLine (state.errorMessage) )
    ?.onSuccess ( state => Console.WriteLine ("Query B OK\n") )
    ?.close     ( )
    ;

//-----------------------------------------------------------------------------

Console.WriteLine ($"\r\nOK @ {DateTime.Now}");

//-----------------------------------------------------------------------------

void printRow (DbDataReader row)
=>
    Console.WriteLine
    (
        "{0}, {1}, {2}",
        row["customerName"],
        row["addressLine1"],
        row["city"]
    );
//-----------------------------------------------------------------------------
