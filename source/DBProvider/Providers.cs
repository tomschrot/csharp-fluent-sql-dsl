using System;

namespace sqldsl.DBProvider
{
    public static class Providers
    {
        public static iConnect MYSQL => new MySql ();

        public static iVerify  UseMYSQL (string connection) => new MySql (connection);
    }
}
