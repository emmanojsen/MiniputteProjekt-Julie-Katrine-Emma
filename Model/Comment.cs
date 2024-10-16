namespace MiniputteProjekt.Model
{
    
        public class Comment
        {
            public int CommentId { get; set; }

            public string Text { get; set; }
            public int PostsId { get; set; }
            public string Author { get; set; }
            public DateTime Date { get; set; }
            public int Votes { get; set; }
            // public Thread Thread { get; set; }
        }
    }


