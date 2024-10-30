

namespace A2Chinook.Models
{
    public partial class Track
    {
        public string FormattedTime
        {
            get
            {
                int totalSeconds = Milliseconds / 1000;
                int minutes = totalSeconds / 60;
                int seconds = totalSeconds % 60;
                return $"{minutes}:{seconds:D2} "; 
            }
        }

        public string SizeInMB
        {
            get
            {
                if (Bytes.HasValue)
                {
                    double megabytes = Bytes.Value / (1024.0 * 1024.0); // 
                    return $"{megabytes:F2} MB";
                }
                else
                {
                    return "N/A";
                }
            }
        }
    }
}