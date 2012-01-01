//
// provides a convenient way to have a log file
//

using System ;
using System.IO ;

namespace Gleed2D.Core
{

    public class Logger
    {
        private static Logger instance;
        public static Logger Instance
        {
            get
            {
                if (instance == null) instance = new Logger();
                return instance;
            }
        }

        string logfilename = System.Windows.Forms.Application.StartupPath + "\\log.txt";
        StreamWriter sw;

        public Logger()
        {
            sw = new StreamWriter(logfilename, false);
            sw.WriteLine(box("Log File created."));
            sw.Close();
        }


        public void log(string message)
        {

            sw = new StreamWriter(logfilename, true);
            sw.WriteLine(box(message));
            sw.Close();            
        }

        string box(string message)
        {
            return DateTime.Now + "." + DateTime.Now.Millisecond.ToString("000") + " - " + message;
        }

    }

}