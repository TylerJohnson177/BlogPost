using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Transactions;
using BlogPost.Models;

namespace BlogPost
{
    class Program
    {
        static void Main(string[] args)
        {

            var context = new DataContext();
            string option;
            
            do
            {
                Console.WriteLine("Enter (1) To display all blogs");
                Console.WriteLine("Enter (2) To add a blog");
                Console.WriteLine("Enter (3) To display the posts of an existing blog");
                Console.WriteLine("Enter (4) To add a post to an existing blog");
                Console.WriteLine("Enter (5) to close program");
            
                option = Console.ReadLine();

            
                switch (option)
                {
                    case "1":
                        List<Blog> blogs = new List<Blog>();

                        blogs = context.Blogs.ToList();
                        Console.WriteLine($"Blogs Found: {blogs.Count}");
                        
                        for (int i = 0; i < blogs.Count; i++)
                        {
                            Console.WriteLine($"{blogs[i].Name}");
                        }
                        Console.WriteLine();
                        break;
                    case "2":
                        Blog blog = new Blog();
                        Console.WriteLine("Enter a name for your blog");
                        string Name = "";
                        Name = Console.ReadLine();
                        blog.Name = Name;
                        
                        context.Blogs.Add(blog);

                        break;
                    case "3" :

                        List<Post> posts = context.Posts.ToList();
                        List<Blog> blogs1 = context.Blogs.ToList();
                        
                        Console.WriteLine("Enter the blogs posts to display");
                        Console.WriteLine("(0) Posts from all blogs");

                        for (int i = 0; i < blogs1.Count; i++)
                        {
                            Console.WriteLine($"({blogs1[i].BlogId}) Posts from {blogs1[i].Name}");
                        }
                        
                        int blogId = 0;

                        try
                        {
                            blogId = int.Parse(Console.ReadLine());
                        }
                        catch (Exception e)
                        {
                            Console.WriteLine("Selection must be an integer");
                        }

                        if (blogId == 0)
                        {
                            Console.WriteLine($"Posts Found: {posts.Count}");
                            
                            for (int i = 0; i < posts.Count; i++)
                            {
                                Console.WriteLine($"Blog Name: {posts[i].blog.Name}, Post Title: {posts[i].Title}, Post Content: {posts[i].Content}");
                            }

                            Console.WriteLine();
                        }
                        else
                        {
                            
                            Console.WriteLine($"Posts Found: {posts.Where(posts => posts.BlogId == blogId).Count()}");
                            
                            for (int i = 0; i < posts.Count; i++)
                            {
                                if (posts[i].BlogId == blogId)
                                {
                                        Console.WriteLine($"Blog Name: {posts[i].blog.Name}, Post Title: {posts[i].Title}, Post Content: {posts[i].Content}");
                                }
                            }
                        }
                        break;
                    case "4":
                        Console.WriteLine("Select the blog you would like to post to");
                        
                        List<Blog> blogs2 = new List<Blog>();
                        
                        blogs2 = context.Blogs.ToList();
                        
                        for (int i = 0; i < blogs2.Count; i++)
                        {
                            Console.WriteLine($"({blogs2[i].BlogId}) {blogs2[i].Name}");
                        }
                        
                        int blogId2 = 0;
                        try
                        {
                            blogId2 = int.Parse(Console.ReadLine());
                        }
                        catch (Exception e)
                        {
                            Console.WriteLine("Selection must be an integer");
                        }

                        for (int i = 0; i < blogs2.Count; i++)
                        {
                            if (blogs2[i].BlogId == blogId2)
                            {
                                try
                                {
                                    Post post = new Post();
                                    Console.WriteLine("Enter a title for your post");
                                    post.Title = Console.ReadLine();
                                    Console.WriteLine("Enter content for your post");
                                    post.Content = Console.ReadLine();
                                    post.blog = blogs2[i];
                                    
                                    context.Posts.Add(post);
                                }
                                catch (Exception e)
                                {
                                    Console.WriteLine(e);
                                    throw;
                                }

                                if (blogId2 > blogs2.Count)
                                {
                                    Console.WriteLine("Blog not found");
                                }
                            }
                        }
                        break;
                }

                context.SaveChanges();

            } while (option != "5");
            
        }
    }
}
