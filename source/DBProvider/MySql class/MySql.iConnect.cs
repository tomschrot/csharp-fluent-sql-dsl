
using MySqlConnector;

namespace sqldsl.DBProvider {

    public sealed partial class MySql {
        //---------------------------------------------------------------------

        public iVerify open (string connection)
        {
            close();
            _connection = new MySqlConnection (connection);

            try
            {
                _connection.Open();
                wasSuccess   = true;
                isConnected  = true;
                errorCode    = 0;
                errorMessage = "OK";
            }
            catch (MySqlException ex)
            {
                wasSuccess   = false;
                isConnected  = false;
                errorCode    = ex.Number;
                errorMessage = errorMessageToString (ex);
            }

            return this;
        }
        //---------------------------------------------------------------------

        public iConnect close ()
        {
            _connection?.Close();
            _connection?.Dispose();
            _connection = null;
            isConnected = false;
            return this;
        }
        //---------------------------------------------------------------------
    }
}