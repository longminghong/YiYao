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
        const String server_host = "http://test.o4bs.com/api/members/identitycard";
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
            Uri uri = new Uri(server_host);
            HttpWebRequest request = (HttpWebRequest)HttpWebRequest.Create(uri);
            request.Method = "POST";
            request.ContentType = "application/json";
            request.Headers.Add("appkey","097e8751c3c183edf602f867a5326559");
            request.Headers.Add("appid", "SyccthZn");

            

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
