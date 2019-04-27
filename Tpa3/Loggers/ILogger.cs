using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tpa3.Loggers
{
   public interface ILogger
    {
        void Error(string mess);
        void Fatal(string mess);
        void Trac(String mess);
        void Write(bool a);

    }
}
