
namespace sqldsl.DBProvider {

    public static class Providers
    {
        public static iConnect MYSQL ()                  => new MySql ();
        public static iFailure MYSQL (string connection) => new MySql (connection);
    }
}
