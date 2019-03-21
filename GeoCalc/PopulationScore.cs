namespace GeoCalc
{
    internal class PopulationScore
    {
        public PopulationScore(int males, int females, int households)
        {
            Males = males;
            Females = females;
            Households = households;
        }

        public int Total => Males + Females;

        public int Males { get; }

        public int Females { get; }

        public int Households { get; }

        public override string ToString()
        {
            return $"{Total} {Males} {Females} {Households}";
        }
    }
}