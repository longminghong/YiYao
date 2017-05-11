using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Threading;

namespace YiYao
{
    public class HTTP
    {
        String ssn;
        String store_id;
        private void HttpPost(String SSNNumber,String Store_ID)
        {   
            if (SSNNumber== null)
            {
                return;
            }
            else
            {
                ssn = SSNNumber;
                store_id = Store_ID;
            }
            Uri uri = new Uri("http://localhost:3881/Financial.ashx");
            HttpWebRequest request = (HttpWebRequest)HttpWebRequest.Create(uri);
            request.Method = "POST";
            request.ContentType = "application/x-www-form-urlencoded";

            // 

            request.BeginGetRequestStream(new AsyncCallback(RequestProceed), request);
        }
        private void RequestProceed(IAsyncResult asyncResult)
        {   
            HttpWebRequest request = (HttpWebRequest)asyncResult.AsyncState;
            StreamWriter postDataWriter = new StreamWriter(request.EndGetRequestStream(asyncResult));
            postDataWriter.Write("ticker=NTES");
            postDataWriter.Write("&startdate=1-1-2009");
            
            postDataWriter.Close();
            request.BeginGetResponse(new AsyncCallback(ResponesProceed), request);
        }
        
        private void ResponesProceed(IAsyncResult asyncResult)
        {
            WebRequest request = (HttpWebRequest)asyncResult.AsyncState;
            HttpWebResponse response = (HttpWebResponse)request.EndGetResponse(asyncResult);
            StreamReader responseReader = new StreamReader(response.GetResponseStream());
            string responseString = responseReader.ReadToEnd();
            
            //Dispatcher.BeginInvoke(() => TimeSeries.ItemsSource = myTimeSeries);
        }

    }
}
