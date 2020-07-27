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
            var foods = new List<string>();
            using (var rd = new StreamReader("generic-food.csv"))
            {
                while (!rd.EndOfStream)
                {
                    var splits = rd.ReadLine().Split(',');
                    var categoryIndex = splits.Length - 1;
                    foods.Add(splits[categoryIndex]);
                }
            }
            var r = from e in foods
                    where e != "CATEGORY"
                    group e by new { e } into g
                    select new
                    {
                        Category = g.Key.e,
                        Count = g.Count()
                    };

            //print category in order of highest category count
            foreach (var foodCategory in r.OrderByDescending(r => r.Count))
                Console.WriteLine(foodCategory.Category + " ........ " + foodCategory.Count);

        }

    }
}
