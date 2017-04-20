using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebService
{
    class WebSocketSingleton
    {
        private static WebSocketSingleton instance;
        private static object _lock = new object();

        private WebSocketSingleton()
        {

        }
        public static WebSocketSingleton GetInstance()
        {
            if (instance == null)
            {
                lock (_lock)
                {
                    if (instance == null)
                    {
                        instance = new WebSocketSingleton();
                    }
                }
            }
            return instance;
        }
    }

}
