﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text.Json.Serialization;
using Newtonsoft.Json;

namespace CousantAssessment
{
    class Program
    {
        static void Main(string[] args)
        {
            var foodCategories = new List<string>();
            using (var rd = new StreamReader("generic-food.csv"))
            {
                while (!rd.EndOfStream)
                {
                    var splits = rd.ReadLine().Split(',');
                    var categoryIndex = splits.Length - 1;
                    foodCategories.Add(splits[categoryIndex]);
                }
            }
            var r = from e in foodCategories
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

            GetMondayNews();
            Console.ReadKey();

        }

        public static void GetMondayNews()
        {
            using(HttpClient client = new HttpClient())
            {
                string newsUri = "https://newsapi.org/v2/everything?q=nigeria&apiKey=3574206693d14c8996dbe331bcbc22ad";
                var response = client.GetAsync(newsUri).Result;
                var result = JsonConvert.DeserializeObject<ApiResponse>(response.Content.ReadAsStringAsync().Result);

                foreach (var item in result.articles)
                {
                    Console.WriteLine(item.title + " ........ " + item.publishedAt);
                }
            }
        }

        public class ApiResponse
        {
            public string status { get; set; }
            public string totalResults { get; set; }
            public Article[] articles { get; set; }
        }
        public class Article
        {
            public string title { get; set; }
            public string publishedAt { get; set; }
        }
    }
}
