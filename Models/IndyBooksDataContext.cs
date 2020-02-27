using System;
using Microsoft.EntityFrameworkCore;

namespace IndyBooks.Models
{
    public class IndyBooksDataContext:DbContext
    {
        public IndyBooksDataContext(DbContextOptions<IndyBooksDataContext> options) : base(options) { }

        //TODO: Define DbSets for Collections representing all DB tables
        public DbSet<Book> Books { get; set; }
        public DbSet<Writer> Authors { get; set; }
    }
}
