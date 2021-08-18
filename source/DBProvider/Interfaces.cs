using System;
using System.Data.Common;
using System.Threading.Tasks;

namespace sqldsl.DBProvider
{
    #nullable enable
    //-------------------------------------------------------------------------

    public interface iState
    {
        bool   isConnected  { get; }
        bool   wasSuccess   { get; }
        int    errorCode    { get; }
        string errorMessage { get; }
    }
    //-------------------------------------------------------------------------

    public interface iConnect
    {
        iVerify  open  (string connection);
        iConnect close ();
    }
    //-------------------------------------------------------------------------

    public interface iVerify: iExecute
    {
        iVerify? onFailure (Action <iState>? onFailure);
        iExecute onSuccess (Action <iState>? onSuccess);
    }
    //-------------------------------------------------------------------------

    public interface iExecute : iConnect
    {
        iVerify query (string queryString, Action <DbDataReader> onResult);

        async Task <iVerify> queryAsync (string queryString, Action<DbDataReader> onResult)
        =>  await Task.Run <iVerify> ( () => query (queryString, onResult) );

        iVerify querySync (string queryString, Action <DbDataReader> onResult)
        =>  Task.Run ( () => queryAsync (queryString, onResult) ).Result;
    }
    //-------------------------------------------------------------------------
    #nullable disable
}
