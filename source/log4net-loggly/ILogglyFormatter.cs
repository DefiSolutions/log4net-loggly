using System.Collections.Generic;
using log4net.Core;

namespace log4net.loggly
{
	public interface ILogglyFormatter
	{
		void AppendAdditionalLoggingInformation(ILogglyAppenderConfig unknown, LoggingEvent loggingEvent);
		string ToJson(LoggingEvent loggingEvent, string eventType);
        string ToJson(IEnumerable<LoggingEvent> loggingEvents, string eventType);
	}
}