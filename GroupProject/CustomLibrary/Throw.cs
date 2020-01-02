using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CustomLibrary
{
    public class Throw
    {
        public static void IfNullOrWhiteSpace(string source, string message)
        {
            if (string.IsNullOrWhiteSpace(source))
            {
                throw new ArgumentException(message);
            }
        }

        public static void IfNull(object source, string parameter)
        {
            if (source == null)
            {
                throw new ArgumentNullException(parameter);
            }
        }
    }
}
