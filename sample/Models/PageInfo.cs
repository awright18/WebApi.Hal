namespace HalSample.Models
{
    
    public class PageInfo
    {
        public int Page { get; set; } = 1;
        public int Limit { get; set; } = 1;

        public int Offset {
            get
            {
                if (Page == 1)
                {
                    return 0;
                }

                return (Page - 1) * Limit;
            }
        }
    }
}