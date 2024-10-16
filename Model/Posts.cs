    using System.Security.Cryptography.X509Certificates;
    using System;

    namespace MiniputteProjekt.Model
    {
        public class Posts
        {
            public int PostsId { get; set; }

            public string Text { get; set; }
            public string Title { get; set; }
            public string Author { get; set; }
            public DateTime Date { get; set; }
            public int Votes { get; set; }

            public List<Comment> Comment { get; set; } = new();



        }

    }


   