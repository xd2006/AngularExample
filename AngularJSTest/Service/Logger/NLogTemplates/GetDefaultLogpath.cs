namespace AngularJSTest.Service.Logger.NLogTemplates
{
    using System;
    using System.Collections.Specialized;
    using System.Configuration;
    using System.Text;

    using NLog;
    using NLog.Internal;
    using NLog.LayoutRenderers;

    /// <summary>
    /// The default logpath getter class.
    /// </summary>
    [LayoutRenderer("GetDefaultLogPath")]
    public class GetDefaultLogPath : LayoutRenderer
    {
        /// <summary>
        /// The _app settings.
        /// </summary>
        private static readonly NameValueCollection AppSettings = System.Configuration.ConfigurationManager.AppSettings;

        /// <summary>
        /// The append.
        /// </summary>
        /// <param name="builder">
        /// The builder.
        /// </param>
        /// <param name="logEvent">
        /// The log event.
        /// </param>
        protected override void Append(StringBuilder builder, LogEventInfo logEvent)
        {
            builder.Append(this.GetPathFromParameters());
        }

        /// <summary>
        /// The get estimated buffer size.
        /// </summary>
        /// <param name="logEvent">
        /// The log event.
        /// </param>
        /// <returns>
        /// The <see cref="int"/>.
        /// </returns>
        protected int GetEstimatedBufferSize(LogEventInfo logEvent)
        {
            return 2;
        }

        /// <summary>
        /// The get path from parameters.
        /// </summary>
        /// <returns>
        /// The <see cref="string"/>.
        /// </returns>
        private string GetPathFromParameters()
        {
            if (!string.IsNullOrEmpty(AppSettings["LogFolder"]))
            {
                return AppSettings["LogFolder"];
            }

            return "C:\\TestResults\\";
        }
    }
}
