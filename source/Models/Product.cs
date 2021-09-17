using System;

namespace sqldsl.Models {

    public class Product
    {
        public string  productName	        { get; set; }
        public string  productScale	        { get; set; }
        public string  productVendor	    { get; set; }
        public string  productDescription   { get; set; }

        public decimal MSRP                 { get; set; }

        override
        public string ToString ()
        =>
            $"{productName}\n"
           +$"Scale: {productScale} "
           +$"Price: {MSRP}\n"
           +$"{productDescription}\n"
           ;
    }
}
