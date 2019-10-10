using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ConsolaEfCore
{
    class Program
    {
        static void Main(string[] args)
        {
            using (var db = new BloggingContext())
            {

                // Create
                Console.WriteLine("Inserting a new blog");
                //db.Blogs.Add(new Blog { Url = "http://blogs.msdn.com/adonet" });
                db.Add(new Blog { Url = "http://blogs.msdn.com/adonet" });
                var rec=db.SaveChanges();



                // Read

                Console.WriteLine("Querying for a blog");
                var blog = db.Blogs.OrderBy(b => b.BlogId).First();
                //    .OrderBy(b => b.BlogId).ThenByDescending(t=>t.Url)
                //    .AsQueryable();
                //blog.Where(t=>t.Url.StartsWith("#" )  && t.Posts.Count>5 ).ToList();

                //var existenAlgunBlog = db.Blogs.Any();

                //var existenAlgunBlogConMas20POst = db.Blogs.Any(t=>t.Posts.Count>20);

                //var blogsCantidad = db.Blogs.Count();

                /////
                //var blogs = db.Blogs.ToList();
                //var otroBlog=blogs.OrderBy(b => b.BlogId).ThenByDescending(t => t.Url)
                //    .AsQueryable();


                // Update
                Console.WriteLine("Updating the blog and adding a post");
                blog.Url = "https://devblogs.microsoft.com/dotnet";
                blog.Posts.Add(
                    new Post
                    {
                        Title = "Hello World",
                        Content = "I wrote an app using EF Core!"
                    });

                blog.Posts.AddRange(
                    new List<Post>()
                    { 
                    new Post
                    {
                        Title = "Hello World",
                        Content = "I wrote an app using EF Core!"
                    }
                    ,new Post
                    {
                        Title = "Hello World",
                        Content = "I wrote an app using EF Core!"
                    }
                    }
                    );

                db.SaveChanges();

                //var blog1 = db.Blogs.Find(1);
                //blog1.Posts = new List<Post>();
                //db.SaveChanges();

                // Delete
                Console.WriteLine("Delete the blog");
                //db.Remove(blog);
                //db.SaveChanges();
            }
        }
    }

        #region Domain
        public class Blog
        {
            public int BlogId { get; set; }
            public string Url { get; set; }

            public List<Post> Posts { get; set; } = new List<Post>();
        }

        public class Post
        {
            public int PostId { get; set; }
            public string Title { get; set; }
            public string Content { get; set; }

            //public int BlogId { get; set; }
            //public Blog Blog { get; set; }
        }
        #endregion

        #region Infrastructure Data
        public class BloggingContext : DbContext
        {
            public DbSet<Blog> Blogs { get; set; }
            public DbSet<Post> Posts { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder options)
        {
            options.UseSqlServer(@"Server=(localdb)\mssqllocaldb;Database=Blogging;Integrated Security=True");
        }
    }
        #endregion

    
}
