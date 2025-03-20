namespace LibraryProject.Models
{
    public class Book
    {
        public int Id { get; }
        public string Title { get; private set; }
        public string Author { get; private set; }

        public Book(int id, string title, string author)
        {
            Id = id;
            SetTitle(title);
            SetAuthor(author);
        }

        public void SetTitle(string title)
        {
            if (string.IsNullOrWhiteSpace(title))
                throw new ArgumentException("Title cannot be empty.");
            Title = title;
        }

        public void SetAuthor(string author)
        {
            if (string.IsNullOrWhiteSpace(author))
                throw new ArgumentException("Author cannot be empty.");
            Author = author;
        }

        public override string ToString()
        {
            return $"{Id}: {Title} by {Author}";
        }
    }
}
