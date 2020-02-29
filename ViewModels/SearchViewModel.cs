using System;
namespace IndyBooks.ViewModels
{
    public class SearchViewModel
    {
        public String Title { get; set; }

        //Properties needed for searching
        //TODO : Notice how the ViewModel doesn't need any changes
        //        since it only deals with the Search view

        public String AuthorLastName { get; set; }
        public decimal MinPrice { get; set; }
        public decimal MaxPrice { get; set; }
        public bool HighPrice { get; set; }

    }
}
