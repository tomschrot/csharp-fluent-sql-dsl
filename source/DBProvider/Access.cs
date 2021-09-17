
using System.Threading.Tasks;
using System.Collections.Generic;

namespace sqldsl.DBProvider {

    public static class Database {
        //---------------------------------------------------------------------
        public static async Task < (iState state, List<T> data) >
        collect <T>
        (
            string connection,
            string query

        )   where T : class, new()
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

            (await  Providers
                    .MYSQL       (connection)
                    .onFailure   (state => result.state = state)
                    ?.queryAsync (query, mapper.create)
            )
            ?.onFailure (state => result.state = state)
            ?.onSuccess (state => result.state = state)
            ?.close     ();

            return result;
        }
        //---------------------------------------------------------------------
    }
}
