namespace ContentManagement.Model
{
    public class Library
    {
        public int? LibraryID { get; set; }
        public string LibraryName { get; set; }

        public Address Address { get; set; }
        public List<Book> Books { get; set; }
    }

    public class Book
    {
        public int BookID { get; set; }
        public string Title { get; set; }

        public int LibraryID { get; set; }
        public Library Library { get; set; }
    }

    public class Address
    {
        public int AddressID { get; set; }
        public string Street { get; set; }
        public string City { get; set; }
        public string PostalCode { get; set; }

        public int LibraryID { get; set; }
        public Library Library { get; set; }
    }

}
