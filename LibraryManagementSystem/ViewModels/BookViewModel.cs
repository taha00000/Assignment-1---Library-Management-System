using LibraryManagementSystem.Domain;

namespace LibraryManagementSystem.ViewModels
{
    // This class is used to pass data between the controller and the view
    // Sometimes, domain objects do not have all the properties the view requires.
    // In this case, we did not have all the Available Authors in the Book domain object.
    // This situation requires that we create a data transfer object so that we can pass the required data to the view.
    public class BookViewModel
    {
        public int Id { get; set; }
        public string Title { get; set; } = default!;
        public string ISBN { get; set; } = default!;
        public List<int>? SelectedAuthorIds { get; set; } = new List<int>();
        public List<Author> AvailableAuthors { get; set; } = new List<Author>();
    }
}