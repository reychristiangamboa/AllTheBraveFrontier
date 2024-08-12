using RandomNameGeneratorLibrary;

namespace AllTheBraveFrontier.Utilities
{
    public static class Utility
    {
        public static double RandomRange(int from, int to)
        {
            Random rnd = new Random();
            return Convert.ToDouble(rnd.Next(from, to));
        }

        public static string GenerateName()
        {
            PersonNameGenerator generator = new PersonNameGenerator();
            return generator.GenerateRandomFirstName();
        }
    }
}