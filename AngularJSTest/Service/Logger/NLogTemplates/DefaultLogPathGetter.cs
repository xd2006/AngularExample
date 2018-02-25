namespace AngularJSTest.Service.Logger.NLogTemplates
{
    using System;
    using System.Collections.Specialized;
    using System.Configuration;
    using System.Text;

    using NLog;
    using NLog.Config;
    using NLog.Internal;
    using NLog.LayoutRenderers;

    /// <summary>
    /// The default log path getter class.
    /// </summary>
    [LayoutRenderer("GetDefaultLogPath")]
    public class GetDefaultLogPathLayoutRenderer : LayoutRenderer
    {
        /// <summary>
        /// The app settings.
        /// </summary>
        private readonly NameValueCollection appSettings = System.Configuration.ConfigurationManager.AppSettings;

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
            return 20;
        }

        /// <summary>
        /// The get path from parameters.
        /// </summary>
        /// <returns>
        /// The <see cref="string"/>.
        /// </returns>
        private string GetPathFromParameters()
        {
            if (!string.IsNullOrEmpty(this.appSettings["LogFolder"]))
            {
                return this.appSettings["LogFolder"];
            }

            return "TestResults";
        }
    }
}
