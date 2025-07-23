using System;

class Program
{
    static void Main(string[] args)
    {
        List<Video> videos = new List<Video>();

        Video video1 = new Video("Intro to C#", "John Smith", 300);
        video1.AddComment(new Comment("Alice", "Great explanation!"));
        video1.AddComment(new Comment("Bob", "Very helpful."));
        video1.AddComment(new Comment("Cathy", "Thanks!"));

        Video video2 = new Video("OOP Concepts", "Jane Doe", 450);
        video2.AddComment(new Comment("Dan", "Clear and concise."));
        video2.AddComment(new Comment("Ella", "Loved this!"));
        video2.AddComment(new Comment("Fred", "More like this please."));

        videos.Add(video1);
        videos.Add(video2);

        foreach (Video video in videos)
        {
            Console.WriteLine($"Title: {video.Title}");
            Console.WriteLine($"Author: {video.Author}");
            Console.WriteLine($"Length: {video.LengthInSeconds} seconds");
            Console.WriteLine($"Comments ({video.GetCommentCount()}):");
            foreach (Comment comment in video.GetComments())
            {
                Console.WriteLine($" - {comment.CommenterName}: {comment.Text}");
            }
            Console.WriteLine();
        }
    }
}