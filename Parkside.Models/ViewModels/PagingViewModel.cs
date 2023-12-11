namespace Parkside.Models.ViewModels
{
    public class PagingViewModel<T>
    {
        public int Count { get; set; }
        public int NumberOfPages { get; set; }
        public List<T> Items { get; set; } = new(new List<T>());
    }
}
