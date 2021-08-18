
using System;

namespace sqldsl.DBProvider {

    public sealed partial class MySql {
        //---------------------------------------------------------------------

        public iSuccess onFailure (Action <iState> onFailure)
        {
            if ( !wasSuccess )
            {
                onFailure?.Invoke (this);
                close ();
                return null;
            }
            return this;
        }
        //---------------------------------------------------------------------

        public iExecute onSuccess (Action <iState> onSuccess)
        {
            if ( wasSuccess ) onSuccess?.Invoke (this);
            return this;
        }
        //---------------------------------------------------------------------
    }
}