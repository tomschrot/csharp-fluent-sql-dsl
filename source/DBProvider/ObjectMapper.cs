
using System;
using System.Reflection;
using System.Data.Common;
using System.Collections.Generic;

namespace sqldsl.DBProvider {

    public sealed class ObjectMapper <T> where T : class, new()
    {
        //---------------------------------------------------------------------
        private const BindingFlags
                        CONSTRAINTS =
                              BindingFlags.Instance
                            | BindingFlags.Public
                            | BindingFlags.SetProperty
                            | BindingFlags.GetProperty
                            ;
        //---------------------------------------------------------------------

        private Dictionary <string, PropertyInfo> _propertyItems = new ();

        //---------------------------------------------------------------------

        public Action <T> onObject { get; set; }

        //---------------------------------------------------------------------

        public ObjectMapper ()
        =>
            collectProperties ();

        public ObjectMapper (Action <T> onObject): this()
        =>
            this.onObject = onObject;

        //---------------------------------------------------------------------

        public ObjectMapper <T> collectProperties (BindingFlags flags = CONSTRAINTS)
        {
            _propertyItems.Clear ();

            typeof         ( T )
            .GetProperties ( flags )
            .@foreach      ( property =>

                _propertyItems.Add
                (
                    key  : property.Name,
                    value: property
                )
            );

            return this;
        }
        //---------------------------------------------------------------------
        public void create (DbDataReader row) => createFrom (row);

        public ObjectMapper <T> createFrom (DbDataReader row)
        {
            var obj = new T ();

            foreach (var pair in _propertyItems)
                pair.Value
                .SetValue (obj, row [pair.Key]);

            onObject?.Invoke (obj);

            return this;
        }
        //---------------------------------------------------------------------
    }
}
