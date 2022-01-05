namespace Bookshelf.Models
{
    public class Shelf
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public List<ShelfBind> ShelfBinds { get; set; }

        public Shelf() { }
        
        public Shelf(int id,string name)
        {
            Id = id;
            Name = name;
        }
    }
}