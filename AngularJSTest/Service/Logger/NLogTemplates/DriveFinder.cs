namespace AngularJSTest.Service.Logger.NLogTemplates
{
    using System.IO;
    using System.Linq;
    using System.Text;
    using NLog;
    using NLog.Config;
    using NLog.LayoutRenderers;

    /// <summary>
    /// The find available drive layout renderer.
    /// </summary>
    [LayoutRenderer("FindAvailableDrive")]
    public class FindAvailableDriveLayoutRenderer : LayoutRenderer
    {
        /// <summary>
        /// The valid drive.
        /// </summary>
        private string validDrive;

        /// <summary>
        /// Gets or sets the drive candidates.
        /// </summary>
        [DefaultParameter]
        public string DriveCandidates { get; set; }

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
            builder.Append(this.FindValidDrive());
        }

        /// <summary>
        /// The get estimated buffer size.
        /// </summary>
        /// <param name="logEvent">
        /// The log event.
        /// </param>
        /// <returns>
        /// The buffer size<see cref="int"/>.
        /// </returns>
        protected int GetEstimatedBufferSize(LogEventInfo logEvent)
        {
            return 2;
        }

        /// <summary>
        /// Find valid drive.
        /// </summary>
        /// <returns>
        /// The drive<see cref="string"/>.
        /// </returns>
        private string FindValidDrive()
        {
            if (!string.IsNullOrEmpty(this.validDrive))
            {
                return this.validDrive;
            }

            if (string.IsNullOrEmpty(this.DriveCandidates))
            {
                if (Directory.Exists("C:"))
                {
                    this.validDrive = "C:";
                }
                else
                {
                    if (Directory.Exists("D:"))
                    {
                        this.validDrive = "D:";
                    }
                }
                return this.validDrive;
            }

            this.validDrive = this.DriveCandidates.Select(c => $"{c}:").FirstOrDefault(Directory.Exists);
            return this.validDrive;
        }
    }
}

