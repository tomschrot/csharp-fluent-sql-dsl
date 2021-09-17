
using System.Threading.Tasks;
using System.Collections.Generic;

namespace sqldsl.DBProvider {

    // static parts

    public sealed partial class MySql {
        //---------------------------------------------------------------------

        private static string _sConnection = string.Empty;
        //---------------------------------------------------------------------

        public static void useConnection (string connection)
        =>
            _sConnection = connection;

        //---------------------------------------------------------------------
        public static async Task < (iState state, List<T> data) >
        collect <T> (string query) where T : class, new()
        {
            (iState state, List<T> data) result =
            (
                null,
                new List <T> ()
            );

            var mapper = new ObjectMapper <T>
            {
                onObject = item => result.data.Add (item)
            };

            var success = new MySql      (_sConnection)
                              .onFailure (state => result.state = state);

            if (success is not null)
                (await success.queryAsync (query, mapper.create))
                ?.onFailure (state => result.state = state)
                ?.onSuccess (state => result.state = state)
                ?.close     ();

            return result;
        }
        //---------------------------------------------------------------------
    }
}
