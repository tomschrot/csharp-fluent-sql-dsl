
using System;
using MySqlConnector;
using System.Data.Common;

namespace sqldsl.DBProvider {

    public sealed partial class MySql {
        //---------------------------------------------------------------------

        public iVerify query (string query, Action <DbDataReader> onResult)
        {
            MySqlCommand    cmd     = null;
            MySqlDataReader reader  = null;

            wasSuccess = false;
            if ( !isConnected ) return this;

            try
            {
                cmd     = new MySqlCommand (query, _connection);
                reader  = cmd.ExecuteReader ();

                while ( reader.Read() ) onResult (reader);

                wasSuccess   = true;
                errorCode    = 0;
                errorMessage = "OK";
            }
            catch (MySqlException ex)
            {
                wasSuccess   = false;
                errorCode    = ex.Number;
                errorMessage = errorMessageToString (ex, query);
            }
            finally
            {
                cmd   ?.Dispose();
                reader?.DisposeAsync ();
            }

            return this;
        }
        //---------------------------------------------------------------------
    }
}