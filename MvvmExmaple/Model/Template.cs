using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;

namespace MvvmExmaple
{
    #region Datastruct Members
    public class Createby
    {
        public string id { get; set; }
        public string loginFlag { get; set; }
        public string roleNames { get; set; }
        public bool admin { get; set; }
    }

    public class UpdateBy
    {
        public string id { get; set; }
        public string loginFlag { get; set; }
        public string roleNames { get; set; }
        public bool admin { get; set; }
    }

    public class Owner
    {
        public string id { get; set; }
        public string name { get; set; }
        public int sort { get; set; }
        public bool hasChildren { get; set; }
        public string type { get; set; }
        public string parentId { get; set; }
    }

    public class InfoOwnerbody
    {
        public string id { get; set; }
        public bool isNewRecord { get; set; }
        public string remarks { get; set; }
        public string createDate { get; set; }
        public string updateDate { get; set; }
        public string parentIds { get; set; }
        public string name { get; set; }
        public int sort { get; set; }
        public string area { get; set; }
        public string code { get; set; }
        public string type { get; set; }
        public string grade { get; set; }
        public string address { get; set; }
        public string zipCode { get; set; }
        public string master { get; set; }
        public string phone { get; set; }
        public string fax { get; set; }
        public string email { get; set; }
        public string useable { get; set; }
        public string primaryPerson { get; set; }
        public string deputyPerson { get; set; }
        public string childDeptList { get; set; }
        public string parentId { get; set; }
    }
    #endregion



    public class TempleInfobody
    {
        public TempleInfoData data { get; set; }
    }

    public class TempleInfoData
    {
        public string id { get; set; }
        public Createby createby { get; set; }
        public string createDate { get; set; }
        public UpdateBy updateBy { get; set; }
        public string updateDate { get; set; }

        public Owner owner { get; set; }
        public string info { get; set; }

        public string url { get; set; }
        public string detail { get; set; }
    }

    public class TempleInfo : INotifyPropertyChanged
    {
        public bool success { get; set; }
        public string errorCode { get; set; }
        public string msg { get; set; }
        public TempleInfobody body{ get; set; };

        public TempleInfobody BODY
        {
            get
            {
                return body;
            }
            set
            {
                body = value;
                OnPropertyChanged("body");
            }
        }

        #region INotifyPropertyChanged Members
        public event PropertyChangedEventHandler PropertyChanged1;
        private void OnPropertyChanged(string propertyName)
        {
            if (PropertyChanged1 != null)
            {
                PropertyChanged1(this, new PropertyChangedEventArgs(propertyName));
            }
        }
        #endregion

    }


}
