using System;
using System.Net;
using System.Text;

namespace log4net.loggly
{
	public class LogglyClient : ILogglyClient
	{
		public virtual void Send(ILogglyAppenderConfig config, string inputKey, string message)
		{
            int maxRetryAllowed = 5;
            int totalRetries = 0;

            while (totalRetries < maxRetryAllowed)
            {
                totalRetries++;
                try
                {
                    var bytes = Encoding.UTF8.GetBytes(message);
                    var request = CreateWebRequest(config, string.IsNullOrWhiteSpace(inputKey) ? config.InputKey : inputKey);

                    using (var dataStream = request.GetRequestStream())
                    {
                        dataStream.Write(bytes, 0, bytes.Length);
                        dataStream.Flush();
                        dataStream.Close();
                    }

                    using (var webResponse = request.GetResponse())
                    {
                        webResponse.Close(); //noop    
                    }
                    break;
                }
                catch { }
            }
		}

		protected virtual HttpWebRequest CreateWebRequest(ILogglyAppenderConfig config, string inputKey)
		{
			var url = String.Format(config.RootUrl, inputKey, config.Tags);
			var request = (HttpWebRequest)WebRequest.Create(url);
			request.Method = "POST";
			request.ReadWriteTimeout = request.Timeout = config.TimeoutInSeconds * 1000;
			request.UserAgent = config.UserAgent;
			request.KeepAlive = true;
			request.ContentType = "application/json";
			return request;
		}
	}
}