namespace TennisProjekt24.Helpers
{
    public class TimeOnlyGenerator
    {

        private int _startTime = 8;
        private int _endTime = 22;

        public List<TimeOnly> TimeOnlyList
        {
            get
            {
                List<TimeOnly> list = new List<TimeOnly>();
                for(int start = _startTime; start <= _endTime; start++) 
                {
                    string timeString = start.ToString()+":00";
                    TimeOnly timeonly = TimeOnly.Parse(timeString);
                    list.Add(timeonly);
                }
                return list;
            }
        }

    }
}
