using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GEwatch
{
    public class WatchAddress
    {
        public int Value;
        public string Type;
        public string Specifier;
        public WatchAddress()
        {

        }
    }
    class WatchRowData
    {
        public bool comment;
        public WatchAddress Address;
        public string Name;
        public string Comment;
        public string Representation;
        public string sValueRead;
        public string sValueWrite;

        public WatchRowData()
        {
            comment = false;
            Address = new WatchAddress();
            Name = string.Empty;
            Comment = string.Empty;
            Representation = "";
            sValueRead = "0";
            sValueWrite = "0";
        }


    }
}
