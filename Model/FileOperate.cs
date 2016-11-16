using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XMLY
{
    public class FileOperate
    {
        private const double KBCount = 1024.0;

        private const double MBCount = 1048576.0;

        private const double GBCount = 1073741824.0;

        private const double TBCount = 1099511627776.0;

        public static string GetAutoSizeString(double size, int roundCount)
        {
            if (1024.0 > size)
            {
                return Math.Round(size, roundCount) + "B";
            }
            if (1048576.0 > size)
            {
                return Math.Round(size / 1024.0, roundCount) + "KB";
            }
            if (1073741824.0 > size)
            {
                return Math.Round(size / 1048576.0, roundCount) + "MB";
            }
            if (1099511627776.0 > size)
            {
                return Math.Round(size / 1073741824.0, roundCount) + "GB";
            }
            return Math.Round(size / 1099511627776.0, roundCount) + "TB";
        }
    }
}
