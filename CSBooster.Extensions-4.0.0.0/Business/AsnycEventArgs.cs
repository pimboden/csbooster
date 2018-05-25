namespace _4screen.CSB.Extensions.Business
{
    public class TrackEventEventArgs : System.ComponentModel.AsyncCompletedEventArgs
    {
        private string strResults;

        internal TrackEventEventArgs(string Results, System.Exception exception, bool cancelled, object userState) : base(exception, cancelled, userState)
        {
            strResults = Results;
        }

        public string Results
        {
            get
            {
                RaiseExceptionIfNecessary();
                return strResults;
            }
        }
    }
}