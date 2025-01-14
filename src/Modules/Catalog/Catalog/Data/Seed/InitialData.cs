namespace Catalog.Data.Seed;

public static class InitialData
{
    public static IEnumerable<Product> Products => new List<Product>
    {
        Product.Create(
            Guid.Parse("9f8dd03e-1298-4070-adc0-c21dcd894ecd"),
            "The Catcher in the Rye",
            "J.D. Salinger's classic novel about teenage alienation and loss of innocence in postwar America",
            new List<string> { "Books", "Fiction", "Classics", "Literary Fiction" },
            19.99m
        ),

        Product.Create(
            Guid.Parse("1d1c4f0c-c6d4-4b78-8b72-f56bcad592e9"),
            "1984",
            "George Orwell's dystopian masterpiece about totalitarianism and surveillance society",
            new List<string> { "Books", "Fiction", "Classics", "Science Fiction" },
            15.99m
        ),

        Product.Create(
            Guid.Parse("7e8dd5c8-87fa-4ae5-94db-4e783b357423"),
            "Clean Code",
            "Robert C. Martin's guide to writing better, maintainable code and professional software craftsmanship",
            new List<string> { "Books", "Technology", "Programming", "Software Development" },
            49.99m
        ),

        Product.Create(
            Guid.Parse("6c4d2d45-f167-4025-9193-51f0f3f997c2"),
            "Sapiens: A Brief History of Humankind",
            "Yuval Noah Harari's exploration of human history from the Stone Age to the present",
            new List<string> { "Books", "Non-Fiction", "History", "Anthropology" },
            24.99m
        ),

        Product.Create(
            Guid.Parse("f89d47c3-b15e-4f3e-9637-f54c91f8f357"),
            "The Design of Everyday Things",
            "Don Norman's insights into human-centered design and the psychology of everyday objects",
            new List<string> { "Books", "Design", "Psychology", "Non-Fiction" },
            29.99m
        ),

        Product.Create(
            Guid.Parse("2c6e7f8d-af2c-4c46-98df-12f9c2f41c43"),
            "Dune",
            "Frank Herbert's epic science fiction masterpiece about politics, religion, and ecology",
            new List<string> { "Books", "Fiction", "Science Fiction", "Fantasy" },
            22.99m
        ),

        Product.Create(
            Guid.Parse("4b9c3d2e-1f8a-4b7c-9e5d-8f6a7b4c3d2e"),
            "The Pragmatic Programmer",
            "Andrew Hunt and David Thomas's practical guide to software development and career growth",
            new List<string> { "Books", "Technology", "Programming", "Professional Development" },
            44.99m
        ),

        Product.Create(
            Guid.Parse("8a7b6c5d-4e3f-2d1e-9f8a-7b6c5d4e3f2d"),
            "To Kill a Mockingbird",
            "Harper Lee's Pulitzer Prize-winning novel about justice and racial inequality in the American South",
            new List<string> { "Books", "Fiction", "Classics", "Literary Fiction" },
            17.99m
        ),

        Product.Create(
            Guid.Parse("3f2e1d4c-5b6a-7c8d-9e0f-1a2b3c4d5e6f"),
            "The Art of Computer Programming",
            "Donald Knuth's comprehensive treatise on computer programming algorithms",
            new List<string> { "Books", "Technology", "Computer Science", "Academic" },
            89.99m
        ),

        Product.Create(
            Guid.Parse("5e4d3c2b-1a9b-8c7d-6e5f-4d3c2b1a9b8c"),
            "Introduction to Algorithms",
            "CLRS's fundamental text on computer algorithms and computational complexity",
            new List<string> { "Books", "Technology", "Computer Science", "Academic" },
            79.99m
        )
    };
}