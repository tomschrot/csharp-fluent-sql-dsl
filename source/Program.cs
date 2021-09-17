
using System.Collections.Generic;
using System;
using sqldsl.Models;
using sqldsl.DBProvider;

using static sqldsl.Extensions;

//-----------------------------------------------------------------------------

Console.WriteLine ("MYSQL Fluent Interface / DSL Example\nby Tom Schröter");

//-----------------------------------------------------------------------------

const string MYSQLCONNECTION = "server=localhost; user=root; password=;";

const string QUERY_A = "SELECT * FROM classicmodels.customerx WHERE country LIKE '%ital%'";
const string QUERY_B = "SELECT * FROM classicmodels.products WHERE productName LIKE '%ford%'";

//-----------------------------------------------------------------------------

(await Database.collect <Customer> (MYSQLCONNECTION, QUERY_A))
.check
(
    @if   : my => my.state.errorCode == 0,
    @then : my => showList (my.data),
    @else : my => Console.WriteLine (my.state.errorMessage)
);
//-----------------------------------------------------------------------------

(await Database.collect <Product> (MYSQLCONNECTION, QUERY_B))
.check
(
    @if   : my => my.state.errorCode == 0,
    @then : my => showList (my.data)
);
//-----------------------------------------------------------------------------

Console.WriteLine ($"\r\nOK @ {DateTime.Now}");

//-----------------------------------------------------------------------------
void showList<T> (IEnumerable<T> items)
{
    foreach (var item in items)
        Console.WriteLine (item);
}