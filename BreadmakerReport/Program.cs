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
            var BMList = BreadmakerDb.Breadmakers
                // TODO: add LINQ logic ...
                //       ...
                .ToList();

            Console.WriteLine("[#]  Reviews Average  Adjust    Description");
            for (var j = 0; j < 3; j++)
            {
                var i = BMList[j];
                // TODO: add output
                // Console.WriteLine( ... );
            }
        }
    }
}
