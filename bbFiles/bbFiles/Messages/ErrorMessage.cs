using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace bbFiles.Messages
{
    /// <summary>
    /// Class used for MVVM Light Messenger to send error messages to MainWindow.
    /// </summary>
    public class ErrorMessage
    {
        /// <summary>
        /// Gets or sets the title of error message.
        /// </summary>
        /// <value>
        /// The title.
        /// </value>
        public string Title { get; set; }

        /// <summary>
        /// Gets or sets the error message.
        /// </summary>
        /// <value>
        /// The error.
        /// </value>
        public string Error { get; set; }
    }
}
