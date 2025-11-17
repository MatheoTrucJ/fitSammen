namespace FitSammenWebClient.Models
{
    public class TrainingDate
    {
        public DateOnly Date { get; set; }
        public bool IsAvailable { get; set; }
        public string Comment { get; set; }

        public TrainingDate(DateOnly date, bool isAvailable, string comment)
        {
            Date = date;
            IsAvailable = isAvailable;
            Comment = comment;
        }
    }
}
