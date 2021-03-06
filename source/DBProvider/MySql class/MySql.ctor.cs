
using MySqlConnector;

namespace sqldsl.DBProvider {

    public sealed partial class MySql:  iState,
                                        iConnect,
                                        iFailure,
                                        iSuccess,
                                        iExecute
    {
        //---------------------------------------------------------------------

        public bool     isConnected  { get; private set; } = false;
        public bool     wasSuccess	 { get; private set; } = true;
        public int      errorCode    { get; private set; } = 0;
        public string   errorMessage { get; private set; } = string.Empty;

        public iState   status       { get => this; }
        public iConnect connection   { get => this; }

        //---------------------------------------------------------------------

        internal MySql () {}
        internal MySql (string connection) => open (connection);

        //---------------------------------------------------------------------

        private string  errorMessageToString (MySqlException ex, string query = "")
        =>
            string.Format
            (
                "ERROR {0} {1}\n\n{2}\n",
                ex.Number,
                ex.Message,
                query
            );
        //---------------------------------------------------------------------
    }
}