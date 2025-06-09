namespace Malshinon.Models
{
    public class Report
    {
        private int _ReporterId;
        public int ReporterId
        {
            get { return _ReporterId; }
            set
            {
                try
                {
                    _ReporterId = value;
                }
                catch (Exception ex)
                {
                    throw new Exception(ex.Message);
                }
            }
        }

        private int _MentionId;
        public int MentionId
        {
            get { return _MentionId; }
            set
            {
                try
                {
                    _MentionId = value;
                }
                catch (Exception ex)
                {
                    throw new Exception(ex.Message);
                }
            }
        }

        private string? _Text;
        public string Text
        {
            get { return _Text!; }
            set { _Text = value; }
        }

        public Report(int reporterId, int mentionId, string text)
        {
            this._ReporterId = reporterId;
            this._MentionId = mentionId;
            this._Text = text;
        }
    }
}
