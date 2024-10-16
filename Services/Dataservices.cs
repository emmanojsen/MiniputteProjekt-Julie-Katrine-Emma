using Microsoft.EntityFrameworkCore;
using MiniputteProjekt.Model;
using MiniputteProjekt.Data;

namespace MiniputteProjekt.Service
    {
        public class DataServices
        {
            private PostsContext db { get; }

            public DataServices(PostsContext db)
            {
                this.db = db;
            }

        /// <summary>
        /// Seeder noget data i databasen hvis det er nødvendigt.
        /// </summary>
        public void SeedData()
        {
            try
            {
                // Check for existing posts
                Posts post = db.Posts.FirstOrDefault();
                if (post == null)
                {
                    Posts newPost = new Posts
                    {
                        Title = "Welcome to Miniputte Reddit",
                        Text = "This is the first post!",
                        Author = "Admin",
                        Date = DateTime.Now,
                        Votes = 10
                    };
                    db.Posts.Add(newPost);
                    db.SaveChanges(); // Save the new post first

                    // Add a comment to the post
                    db.Comments.Add(new Comment
                    {
                        Text = "Nice post!",
                        PostsId = newPost.PostsId,
                        Author = "User1",
                        Date = DateTime.Now,
                        Votes = 5
                    });

                    // Add a vote to the post
                    db.Votes.Add(new Vote
                    {
                        PostsId = newPost.PostsId,
                        IsUpvote = true
                    });
                }

                db.SaveChanges();
            }
            catch (DbUpdateException ex)
            {
                // Log the inner exception
                Console.WriteLine(ex.InnerException?.Message);
                throw; // Rethrow the exception after logging
            }
        }
        


            // Additional methods to retrieve data can be added here if needed
        }
    }


