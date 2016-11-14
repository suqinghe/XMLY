using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace XMLY
{
    public static class LogHelper
    {
        public static void WriteLog(string content)
        {
            var path = string.Format("{0}/{1}/{2}", Application.StartupPath, DateTime.Now.ToString("yyyy-MM-dd"), "Error.txt");

            var fl = new FileInfo(path);
            if (!fl.Directory.Exists)
                fl.Directory.Create();

            var sb = new StringBuilder();
            sb.AppendLine("================系统错误日志==================");
            sb.AppendLine(content);
            sb.AppendFormat("================{0}===================", DateTime.Now);
            sb.AppendLine();

            File.AppendAllText(path, sb.ToString());
        }
    }
}
