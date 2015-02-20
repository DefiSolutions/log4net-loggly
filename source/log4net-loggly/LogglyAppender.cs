using log4net.Appender;
using log4net.Core;

namespace log4net.loggly
{
	public class LogglyAppender : AppenderSkeleton
	{
		public static readonly string InputKeyProperty = "LogglyInputKey";

		public static ILogglyFormatter Formatter = new LogglyFormatter();
		public static ILogglyClient Client = new LogglyClient();

		protected ILogglyAppenderConfig Config = new LogglyAppenderConfig();

        public string EventType { get; set; }
		public virtual string RootUrl { set { Config.RootUrl = value; } }
        public virtual string InputKey { set { Config.InputKey = value; } }
        public virtual string UserAgent { set { Config.UserAgent = value; } }
        public virtual int TimeoutInSeconds { set { Config.TimeoutInSeconds = value; } }
        public virtual string Tags { set { Config.Tags = value; } }

	    public override void ActivateOptions()
	    {
	        base.ActivateOptions();
            Config.VerifyUrl();
	    }

	    protected override void Append(LoggingEvent loggingEvent)
		{
            Formatter.AppendAdditionalLoggingInformation(Config, loggingEvent);
			Client.Send(Config, Config.InputKey, Formatter.ToJson(loggingEvent, EventType));
		}
	}
}
