using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibertyConsignmentAPI
{
    internal class LibertyException : Exception
    {
        /// <summary>
        /// Create an empty LibertyExceptions
        /// </summary>
        public LibertyException() { }

        /// <summary>
        /// Create a LibertyExceptions from an error message
        /// </summary>
        /// <param name="message">Error message</param>
        public LibertyException(string message) : base(message) { }

        /// <summary>
        /// Create a LibertyExceptions from message and another exception
        /// </summary>
        /// <param name="message">Error message</param>
        /// <param name="exception">Original Exception</param>
        public LibertyException(string message, Exception exception) : base(message, exception) { }
    }
}
