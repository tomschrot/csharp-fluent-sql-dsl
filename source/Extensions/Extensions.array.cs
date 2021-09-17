using System;

namespace sqldsl {

    public static partial class Extensions {
        //---------------------------------------------------------------------

        public static T[] @foreach <T> (this T[] array, Action <T> handle = null)
        {
            for
            (
                int n = 0;
                n < array.Length;
                handle ( array [n++] )
            );

            return array;
        }
        //---------------------------------------------------------------------
    }
}
