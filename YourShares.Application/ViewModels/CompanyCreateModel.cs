namespace YourShares.Application.ViewModels
{
    public class CompanyCreateModel
    {
        public string CompanyName { get; set; }

        public string Address { get; set; }

        public string Capital { get; set; }

        public long TotalShares { get; set; }

        public long OptionPoll { get; set; }
    }
}