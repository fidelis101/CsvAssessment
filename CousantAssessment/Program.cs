using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace CousantAssessment
{
    class Program
    {
        static void Main(string[] args)
        {
            var foods = new List<food>();
            using (var rd = new StreamReader("generic-food.csv"))
            {
                var firstRow = true;
                while (!rd.EndOfStream)
                {
                    if(!firstRow)
                    {
                        var splits = rd.ReadLine().Split(',');
                        var categoryIndex = splits.Length - 1;
                        foods.Add(new food { Category = splits[categoryIndex] });
                        firstRow = false;
                    }
                    firstRow = false;
                }
            }
            var r = from e in foods
                    group e by new { e.Category } into g
                    select new
                    {
                        g.Key.Category,
                        Count = g.Count()
                    };

            //print category
            foreach (var foodCategory in r.OrderByDescending(r=>r.Count))
                Console.WriteLine(foodCategory.Category + "........" + foodCategory.Count);

        }

        class food
        {
            public string Category { get; set; }
        }
    }
}
