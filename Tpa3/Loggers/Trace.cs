using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Tpa3.Loggers
{
   public class Trace :ILogger
    {
        private TraceSource mySource = new TraceSource("TraceSourceApp");
        string FileName = "Trace";
        public void Write(bool append = false)
        {
            string Filename = Assembly.GetExecutingAssembly().GetName().Name + "Trace" + ".log";

            // Log file header line
            string logHeader = Filename + " is Trace.";
            if (!File.Exists(Filename))
            {
                WriteLine(mySource.Name.ToString() + logHeader, false);
            }
            
        }
        private void WriteLine(string text, bool append = true)
        {
            try
            {
                using (StreamWriter Writer = new StreamWriter(FileName, append, Encoding.UTF8))
                {
                    if (text != "") Writer.WriteLine(text);
                }
            }
            catch
            {
                throw;
            }
        }
        public void WriteFormattedLog()
        {
            mySource.TraceEvent(TraceEventType.Error, 1,
                      "Error message.");
            mySource.TraceEvent(TraceEventType.Warning, 2,
                "Warning message.");
        }
       public  void Fatal(string mess)
        {
            mySource.TraceEvent(TraceEventType.Critical, 3,
                "Critical message.");
            mySource.TraceInformation(mess);
        }
       public  void Error(string mess)
        {
            mySource.TraceEvent(TraceEventType.Error, 4,
                "Error message.");
            mySource.TraceInformation(mess);
        }
        public void Trac(string text)
        {
            mySource.TraceEvent(TraceEventType.Information, 1,
               "Error message.");
            mySource.TraceInformation(text);
        }
    }
}

