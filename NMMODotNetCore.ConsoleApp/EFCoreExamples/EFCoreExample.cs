using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;
using NMMODotNetCore.ConsoleApp.Dtos;


namespace NMMODotNetCore.ConsoleApp.EFCoreExamples
{
    internal class EFCoreExample
    {
        AppDbContext db = new AppDbContext();
        public void Run()
        {
            // Read();
            //Edit(11);
            //Create("Angle", " KhonNyo", " Content");
            //Update(1, "Devoloper intern", "Mg Mg", "Content");
            Delete(6);

        }



        private void Read()
        {

            var lst = db.Blogs.ToList();
            foreach (BlogDto item in lst)
            {
                Console.WriteLine(item.BlogId);
                Console.WriteLine(item.BlogTitle);
                Console.WriteLine(item.BlogAuthor);
                Console.WriteLine(item.BlogContent);
                Console.WriteLine("-----------------------");


            }

        }
        private void Edit(int id)
        {

            var item = db.Blogs.FirstOrDefault(x => x.BlogId == id);
            if (item == null)
            {

                Console.WriteLine("No Data found");
                return;
            }
            Console.WriteLine(item.BlogId);
            Console.WriteLine(item.BlogTitle);
            Console.WriteLine(item.BlogAuthor);
            Console.WriteLine(item.BlogContent);
            Console.WriteLine("-----------------------");

        }
        private void Create(string Angle, string KhonNyo, string Content)
        {
            var item = new BlogDto
            {
                BlogTitle = Angle,
                BlogAuthor = KhonNyo,
                BlogContent = Content,


            };
            db.Blogs.Add(item);
            var result = db.SaveChanges();
            string message = result > 0 ? "Creation is success" : "Creation is fail";
            Console.WriteLine(message);


        }
        private void Update(int id, string title, string author, string content)
        {
            var item = db.Blogs.FirstOrDefault(x => x.BlogId == id);
            if (item == null)
            {

                Console.WriteLine("No Data found");
                return;
            }
            item.BlogTitle = title;
            item.BlogAuthor = author;
            item.BlogContent = content;

            int result = db.SaveChanges();

            string message = result > 0 ? "Update is successful" : "Update is failed";
            Console.WriteLine(message);

        }
        private void Delete(int id)
        {
            var item = db.Blogs.FirstOrDefault(x => x.BlogId == id);
            if (item == null)
            {

                Console.WriteLine("No Data found");
                return;
            }
            db.Blogs.Remove(item);
            int result = db.SaveChanges();
            string message = result > 0 ? "Delete is successful" : "Delete is failed";
            Console.WriteLine(message);

        }
    }
}
