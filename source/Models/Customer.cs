using System;

namespace sqldsl.Models {

    public sealed class Customer
    {
        public string  customerName	            { get; set; }
        public string  contactLastName	        { get; set; }
        public string  contactFirstName	        { get; set; }
        public string  addressLine1	            { get; set; }
        public string  city	                    { get; set; }
        public string  country	                { get; set; }

        public decimal creditLimit              { get; set; }

        public int     salesRepEmployeeNumber   { get; set; }

        override
        public string ToString ()
        =>
            $"{customerName}\n"
           +$"{contactFirstName} {contactLastName}\n"
           +$"{addressLine1}\n"
           +$"{city}, {country}\n\n"
           ;
    }
}
