using Newtonsoft.Json;
using Programowanie4.Models;

namespace Programowanie4;
class Category
{
    public string category { get; set; }
    public double min { get; set; }
    public double max { get; set; }
    public double avg { get; set; }
}

public class Program
{
    static void Display(IEnumerable<Product> products)
    {
    List < Product > list = products.ToList();
    list?.ForEach(phone => Console.WriteLine($"Id: {phone.id}, Name: {phone.title}"));
    }
    static void Main()
    {
        var json = File.ReadAllText("products_ext.json");
        if (json is not null)
        {
            List<Product> phones = JsonConvert.DeserializeObject<List<Product>>(value: json);
        //zad 1
            var p1 = phones.DistinctBy(p1 => p1.category);

            Display(p1);
        //zad 2
            var p2 = phones.Where(p => p.title.ToLower().Contains("Iphone".ToLower())).Sum(p => p.price * p.stock);

            Console.WriteLine(p2);
        //zad 3
            var p3 = phones.OrderBy(p => p.price).Take(3);
            var p4 = phones.OrderByDescending(p => p.price).Take(3);
            Display(p3);
            Display(p4);
        //zad 4
            var p5 = phones.GroupBy(p => p.category)
                .Select(p => new Category 
                    { 
                    category = p.Key,
                    min = p.Min(p => p.price),
                    max = p.Max(p => p.price),
                    avg = p.Average(p => p.price)
                    }).ToList();

            for (int i = 0; i < p5.Count; i++)
            {
                Console.WriteLine($"cat name: {p5[i].category}, min:{p5[i].min}, max: {p5[i].max}, avg: {p5[i].avg}");
            }
            // zad 5

            var p6 = phones.Where(p => p.category == "smartphones").OrderByDescending(p => p.discountPercentage).Take(3);

            Display(p6);

            var p7 = phones.Where(p => p.category == "skincare").Sum(p => p.price * p.stock);

            Console.WriteLine(p7);
        }
    }
}








