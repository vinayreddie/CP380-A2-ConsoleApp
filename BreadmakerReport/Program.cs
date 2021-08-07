using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using RatingAdjustment.Services;
using BreadmakerReport.Models;

namespace BreadmakerReport
{
    class Program
    {
        static string dbfile = @".\data\breadmakers.db";
        static RatingAdjustmentService ratingAdjustmentService = new RatingAdjustmentService();

        static void Main(string[] args)
        {
            Console.WriteLine("Welcome to Bread World");
            var BreadmakerDb = new BreadMakerSqliteContext(dbfile);
            var BM = BreadmakerDb.Breadmakers
                .Select(r => new
                {
                    Desc = r.title,
                    Rev = r.Reviews.Count(),
                    Avg = (Double)BreadmakerDb.Reviews.Where(a => a.BreadmakerId == r.BreadmakerId).Select(a => a.stars).Sum() / r.Reviews.Count(),
                })
                .ToList();

            var BMList = BM
                .Select(r => new
                {
                    Desc = r.Desc,
                    Rev = r.Rev,
                    Avg = r.Avg,
                    Adj = ratingAdjustmentService.Adjust(r.Avg, r.Rev)
                })
                .OrderByDescending(r => r.Adj)
                .ToList();

            Console.WriteLine("[#]  Reviews Average  Adjust    Description");
            for (var j = 0; j < 3; j++)
            {
                var i = BMList[j];
                Console.WriteLine($"[{j + 1}]  {i.Rev,7} {Math.Round(i.Avg, 2),-7}  {Math.Round(i.Adj, 2),-6}    {i.Desc}");
            }
        }
    }
}
