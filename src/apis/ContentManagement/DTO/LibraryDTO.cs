namespace ContentManagement.DTO
{
    public class BookDTO
    {
        public string Title { get; set; }
    }

    public class AddressDTO
    {
        public string Street { get; set; }
        public string City { get; set; }
        public string PostalCode { get; set; }
    }

    public class LibraryDTO
    {
        public string LibraryName { get; set; }
        public AddressDTO Address { get; set; }
        public List<BookDTO> Books { get; set; }
    }

}
