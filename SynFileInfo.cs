using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;
using System.Windows.Forms;

namespace XMLY
{
    public class SynFileInfo
    {
        public string DocID
        {
            get;
            set;
        }

        public string Album
        {
            get;
            set;
        }

        public string DocName
        {
            get;
            set;
        }

        public int duration
        {
            get;
            set;
        }

        public long FileSize
        {
            get;
            set;
        }

        public string SynSpeed
        {
            get;
            set;
        }

        public string SynProgress
        {
            get;
            set;
        }

        public string DownPath
        {
            get;
            set;
        }

        public string SavePath
        {
            get;
            set;
        }
        public bool Selected { get; set; }
        [XmlIgnore]
        public DataGridViewRow RowObject
        {
            get;
            set;
        }

        public bool isComplete
        {
            get;
            set;
        }

        public DateTime LastTime
        {
            get;
            set;
        }

        public SynFileInfo()
        {
        }

        public SynFileInfo(object[] objectArr)
        {
            int num = 0;
            this.Selected = Convert.ToBoolean(objectArr[num].ToString());
            num++;
            this.DocID = objectArr[num].ToString();
            num++;
            this.Album = objectArr[num].ToString();
            num++;
            this.DocName = objectArr[num].ToString();
            num++;
            this.duration = (int)Convert.ToInt16(objectArr[num]);
            num++;
            this.FileSize = Convert.ToInt64(objectArr[num]);
            num++;
            this.SynSpeed = objectArr[num].ToString();
            num++;
            this.SynProgress = objectArr[num].ToString();
            num++;
            this.DownPath = objectArr[num].ToString();
            num++;
            this.SavePath = objectArr[num].ToString();
            num++;
            this.isComplete = Convert.ToBoolean(objectArr[num]);
            num++;
            this.RowObject = (DataGridViewRow)objectArr[num];
        }
    }
}
