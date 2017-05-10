using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebService
{
    class MTMWebServerDataResult<T>
    {
        public Error error { get; set; }
        public T data { get; set; }
    }
    public class Error
    {
        public int code { get; set; }
        public string message { get; set; }
    }
}
