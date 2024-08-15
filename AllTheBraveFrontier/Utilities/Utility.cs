using AllTheBraveFrontier.Entities;
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

        public static Hero? FindHero(string name, List<Hero> heroes)
        {
            foreach (Hero hero in heroes)
            {
                if (hero.Name.ToLower().Equals(name))
                {
                    return hero;
                }
            }
            return null;
        }

        public static bool IsHeroExists(string name, List<Hero> heroes)
        {
            foreach (Hero hero in heroes)
            {
                if (hero.Name.ToLower().Equals(name))
                {
                    return true;
                }
            }
            return false;
        }

        public static void DisplayHeroes(List<Hero> heroes)
        {
            foreach (Hero c in heroes)
                Console.WriteLine(c.ShowDetails());
        }

        public static bool ValidInput(string input)
        {
            return !string.IsNullOrEmpty(input) || !string.IsNullOrWhiteSpace(input);
        }
    }
}