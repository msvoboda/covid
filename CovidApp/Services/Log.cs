using System;
using System.Collections.Generic;
using System.Text;

namespace CovidApp.Services
{
    /// <summary>
    /// Facada na logovani - rozhrani
    /// </summary>
    public interface ILogger
    {
        void Info(string message, string tag = "");
        void Error(string message, string tag = "");
        void Debug(string message, string tag = "");
        void Warning(string message, string tag = "");
        void Write(string message, string tag = "");

    }

}
