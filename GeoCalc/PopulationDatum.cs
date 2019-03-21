using Microsoft.SqlServer.Types;

namespace GeoCalc
{
    internal class PopulationDatum
    {
        public PopulationDatum(string line, int srid)
        {
            var parsed = line.Split('\t');

            var latitude = double.Parse(parsed[0]);
            var longitude = double.Parse(parsed[1]);

            Point = SqlGeography.Point(latitude, longitude, srid);

            Males = int.Parse(parsed[2]);
            Females = int.Parse(parsed[3]);
            Households = int.Parse(parsed[4]);
        }

        public SqlGeography Point { get; }

        public int Males { get; }
        
        public int Females { get; }

        public int Households { get; }
    }
}