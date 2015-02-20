using System;

namespace log4net.loggly
{
	public class LogglyAppenderConfig: ILogglyAppenderConfig
	{
		private string _rootUrl;
		public string RootUrl
		{
			get { return _rootUrl; }
			set
			{
				//TODO: validate http and uri
				 _rootUrl = BuildUrl(value);
			}
		}

		public string InputKey { get; set; }

		public string UserAgent { get; set; }

		public int TimeoutInSeconds { get; set; }
	    public void VerifyUrl()
	    {
            if (!_rootUrl.EndsWith("tag/") && !String.IsNullOrEmpty(Tags))
            {
                _rootUrl += "/tag/{1}";
            }
	    }

	    public string Tags { get; set; }

		public LogglyAppenderConfig()
		{
			UserAgent = "loggly-log4net-appender";
			TimeoutInSeconds = 30;
		}

	    private string BuildUrl(string rootUrl)
	    {
            if (!rootUrl.EndsWith("/"))
            {
                rootUrl += "/";
            }
            if (!rootUrl.EndsWith("inputs/"))
            {
                rootUrl += "inputs/{0}";
            }
	        return rootUrl;
	    }
	}
}