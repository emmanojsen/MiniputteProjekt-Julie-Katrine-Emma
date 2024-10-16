namespace MiniputteProjekt.Model
{
   
        public class Vote
        {
            public int VoteId { get; set; }
            public int PostsId { get; set; }
            public int? CommentId { get; set; }
            public bool IsUpvote { get; set; }
        }
    }

   
