using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Malshinon.Models
{
    public class Alert
    {
        private int _Target_ID;
        public int Target_ID
        {
            get { return _Target_ID; }
            set { _Target_ID = value; }
        }

        private string _TimeWindow;
        public string TimeWindow
        {
            get { return _TimeWindow; }
            set { _TimeWindow = value; }
        }

        private string _Reason;
        public string Reason
        {
            get { return _Reason; }
            set { _Reason = value; }
        }

        public Alert(int targetID, string timeWindow, string reason)
        {
            this._Target_ID = targetID;
            this._TimeWindow = timeWindow;
            this._Reason = reason;
        }

        public string Print()
        {
            string result = $"Target ID: {_Target_ID}   Time Window: {_TimeWindow}   Reason: {_Reason}";
            return result;
        }
    }
}
