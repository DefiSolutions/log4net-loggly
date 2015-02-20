namespace log4net.loggly
{
	public interface ILogglyAppenderConfig
	{
		string RootUrl { get; set; }
		string InputKey { get; set; }
		string UserAgent { get; set; }
        string Tags { get; set; }
		int TimeoutInSeconds { get; set; }
	    void VerifyUrl();
	}
}