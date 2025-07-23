using System;

class Program
{
    static void Main(string[] args)
      {
        // Create a list to hold the video objects
        List<Video> videos = new List<Video>();

        // First video and its comments
        Video video1 = new Video("Intro to C#", "John Smith", 300);
        video1.AddComment(new Comment("Alice", "Great explanation!"));
        video1.AddComment(new Comment("Bob", "Very helpful."));
        video1.AddComment(new Comment("Cathy", "Thanks!"));

        // Second video and its comments
        Video video2 = new Video("OOP Concepts", "Jane Doe", 450);
        video2.AddComment(new Comment("Dan", "Clear and concise."));
        video2.AddComment(new Comment("Ella", "Loved this!"));
        video2.AddComment(new Comment("Fred", "More like this please."));

        // Third video and its comments
        Video video3 = new Video("Encapsulation in C#", "Mike Johnson", 380);
        video3.AddComment(new Comment("Grace", "Very informative."));
        video3.AddComment(new Comment("Hannah", "Awesome breakdown."));
        video3.AddComment(new Comment("Isaac", "This helped a lot, thanks."));

        // Add all videos to the list
        videos.Add(video1);
        videos.Add(video2);
        videos.Add(video3);

        // Display all video information and their comments
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

            Console.WriteLine(new string('-', 40));
        }
    }
}