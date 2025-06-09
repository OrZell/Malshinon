namespace Malshinon.Models
{
    public class Person
    {
        private string? _FirstName;
        public string FirstName
        {
            get { return _FirstName!; }
            set { _FirstName = value; }
        }

        private string? _LastName;
        public string LastName
        {
            get { return _LastName!; }
            set { _LastName = value; }
        }

        private string? _SecretCode;
        public string SecretCode
        {
            get { return _SecretCode!; }
            set { _SecretCode = value; }
        }

        private int _Type;
        public int Type
        {
            get { return _Type; }
            set
            {
                try
                {
                    _Type = value;
                }
                catch (FormatException ex)
                {
                    throw new FormatException(ex.Message);
                }
            }
        }

        private int _NumReports;
        public int NumReports
        {
            get { return _NumReports; }
            set
            {
                try
                {
                    _NumReports = value;
                }
                catch (FormatException ex)
                {
                    throw new FormatException(ex.Message);
                }
            }
        }

        private int _NumMentions;
        public int NumMentions
        {
            get { return _NumMentions; }
            set
            {
                try
                {
                _NumMentions = value;
                }
                catch (FormatException ex)
                {
                    throw new FormatException(ex.Message);
                }
            }
        }

        public Person(string firstName, string lastName, )
        {

        }
    }
}
