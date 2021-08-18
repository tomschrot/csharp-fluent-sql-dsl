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
        iFailure open  (string connection);
        iConnect close ();
    }
    //-------------------------------------------------------------------------

    public interface iFailure
    {
        iSuccess? onFailure (Action <iState>? onFailure = null);
    }
    //-------------------------------------------------------------------------

    public interface iSuccess: iExecute
    {
        iExecute onSuccess (Action <iState>? onSuccess = null);
    }
    //-------------------------------------------------------------------------

    public interface iExecute : iConnect
    {
        iFailure query (string queryString, Action <DbDataReader> onResult);

        async Task <iFailure> queryAsync (string queryString, Action<DbDataReader> onResult)
        =>  await Task.Run <iFailure> ( () => query (queryString, onResult) );

        iFailure querySync (string queryString, Action <DbDataReader> onResult)
        =>  Task.Run ( () => queryAsync (queryString, onResult) ).Result;
    }
    //-------------------------------------------------------------------------
    #nullable disable
}
