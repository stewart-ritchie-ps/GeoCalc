using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using Microsoft.SqlServer.Types;

namespace GeoCalc
{
    class Program
    {
        static void Main(string[] args)
        {
            SqlServerTypes.Utilities.LoadNativeAssemblies(AppDomain.CurrentDomain.BaseDirectory);

            const int srid = 4326;

            var population = new List<PopulationDatum>();

            using (var s = typeof(Program).Assembly.GetManifestResourceStream("GeoCalc.Data.popcounts.txt"))
            using (var r = new StreamReader(s))
            {
                while (!r.EndOfStream)
                {
                    var line = r.ReadLine();

                    if (line == null) continue;

                    population.Add(new PopulationDatum(line, srid));
                }
            }

            var high = SqlGeography.Point(54.942380, -1.671109, srid);
            var low = SqlGeography.Point(51.929182, -1.071103, srid);

            const int radiusInMetres = 10000;

            var highScore = population
                .Where(p => p.Point.STDistance(high).Value <= radiusInMetres)
                .GroupBy(_ => 0)
                .Select(g => new PopulationScore(g.Sum(_ => _.Males), g.Sum(_ => _.Females), g.Sum(_ => _.Households)))
                .First();

            var lowScore = population
                .Where(p => p.Point.STDistance(low).Value <= radiusInMetres)
                .GroupBy(_ => 0)
                .Select(g => new PopulationScore(g.Sum(_ => _.Males), g.Sum(_ => _.Females), g.Sum(_ => _.Households)))
                .First();

        }
    }
}
