using System;

namespace sqldsl {

    public static partial class Extensions {
        //---------------------------------------------------------------------
        public static T @apply <T>
        (
            this T          @this,
            Action <T>      handle = null
        )
        {
            handle?.Invoke (@this);
            return @this;
        }
        //---------------------------------------------------------------------

        public static T @check <T>
        (
            this T          @this,
            Func <T, bool>  @if,
            Action <T>      @then = null,
            Action <T>      @else = null
        )
        {
            if ( @if (@this) )
                @then?.Invoke (@this);
            else
                @else?.Invoke (@this);

            return @this;
        }
        //---------------------------------------------------------------------
    }
}
