using System.ComponentModel.DataAnnotations;

namespace LibraryManagementSystem.Domain;

public class Author : BaseModel
{
    // DO NOT MODIFY ABOVE THIS LINE
    // Add public Name property here with type 'string?' (nullable string)
    [Required]
    [StringLength(100)]
    public string? Name { get; set; }

    // Add public Books property here with type 'ICollection<Book>' (collection of Book)
    // An author may have written multiple books.
    // This will make the relationship between Book and Author many-to-many
    public ICollection<Book> Books { get; set; } = new List<Book>();
    
    // DO NOT MODIFY BELOW THIS LINE

    public string BooksToString()
    {
        // DO NOT MODIFY ABOVE THIS LINE
        // This method should return a string with the names of the books of the author separated by commas
        // If the author has multiple books, the names should be separated by commas and the last name should be preceded by 'and'
        // If the author has only one book, the name should be returned as is
        // If the author has no books, an empty string should be returned
        var titles = Books
            .Select(b => b.Title)
            .Where(t => !string.IsNullOrWhiteSpace(t))
            .ToList();

        if (titles.Count == 0) return string.Empty;
        if (titles.Count == 1) return titles[0];
        if (titles.Count == 2) return $"{titles[0]} and {titles[1]}";

        var allButLast = string.Join(", ", titles.Take(titles.Count - 1));
        return $"{allButLast} and {titles.Last()}";
        // DO NOT MODIFY BELOW THIS LINE
    }
}