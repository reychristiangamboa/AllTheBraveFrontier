using AllTheBraveFrontier.Utilities;

namespace AllTheBraveFrontier.Entities
{
    public class Game
    {
        public Player player { get; set; }
        public List<Hero> InitialHeroes { get; set; }

        public Game()
        {
            player = new Player();
            InitialHeroes = new List<Hero>();
        }

        public void InitializeGame()
        {
            Console.WriteLine("Welcome to All the Brave Frontier!");

            do
            {
                Console.Write("Please enter your name, player:");
                player.Name = Console.ReadLine();

                if (string.IsNullOrEmpty(player.Name))
                {
                    Console.WriteLine("Please provide a name. Try again.");
                }

            } while (string.IsNullOrWhiteSpace(player.Name));

            Console.WriteLine($"\nWelcome, {player.Name}!\n");

            ChooseInitialHeroes();
        }

        private void ChooseInitialHeroes()
        {
            string satisfiedInput = string.Empty;
            string heroChoice = string.Empty;

            do
            {
                if (satisfiedInput.ToLower().Equals("n"))
                {
                    player.Party.Clear();
                    InitialHeroes.Clear();
                    Console.Clear();
                }

                InitialHeroes = GenerateHeroes();
                Console.WriteLine($"\nGreetings, {player.Name}! Your intial heroes are being generated...\n");

                Utility.DisplayHeroes(InitialHeroes);

                Console.WriteLine("\nYour heroes are generated...");
                Console.WriteLine("Please choose 5 heroes among the list by typing in their name.\n");
                do
                {
                    Console.Write("Hero name: ");
                    heroChoice = Console.ReadLine().ToLower();

                    #region hero choice validations
                    if (!Utility.ValidInput(heroChoice))
                    {
                        Console.WriteLine("Please input a name. Try again.\n");
                    }
                    else if (Utility.IsHeroExists(heroChoice, player.Party)) // hero already exists in party
                    {
                        Console.WriteLine("Hero already exists in your party. Try again.\n");
                    }
                    else if (!Utility.IsHeroExists(heroChoice, InitialHeroes)) // hero not in the initial list
                    {
                        Console.WriteLine("Hero doesn't exist from the list. Try again.\n");
                    }
                    else
                    {
                        Hero? hero = Utility.FindHero(heroChoice, InitialHeroes);
                        player.Party.Add(hero);
                        Console.WriteLine($"{hero.Name}\t{hero.Class} ADDED!\n");
                    }
                    #endregion

                } while (!Utility.ValidInput(heroChoice) || (Utility.IsHeroExists(heroChoice, player.Party) && player.Party.Count < 5)
                            || !Utility.IsHeroExists(heroChoice, InitialHeroes));

                Console.WriteLine("\nHere are the heroes that have made the cut:\n");

                Utility.DisplayHeroes(player.Party);

                do
                {
                    Console.Write("Satisfied with your party? [Y/N]");
                    satisfiedInput = Console.ReadLine().ToLower();

                    if (!Utility.ValidInput(satisfiedInput) || (!satisfiedInput.Equals("y") && !satisfiedInput.Equals("n")))
                        Console.WriteLine("Please input [Y] or [N]. Try again.");

                } while (!Utility.ValidInput(satisfiedInput) || (!satisfiedInput.Equals("y") && !satisfiedInput.Equals("n")));

            } while (satisfiedInput.Equals("n"));
        }

        private List<Hero> GenerateHeroes()
        {

            List<Hero> heroes = new List<Hero>();

            for (int i = 0; i < 25; i++)
            {
                heroes.Add(GenerateHero());
            }

            return heroes;
        }

        private Hero? GenerateHero()
        {
            Random rnd = new Random();
            int value = rnd.Next(1, 3);

            switch (value)
            {
                case 1:
                    return new WhiteMage();
                case 2:
                    return new Rogue();
            }

            return null;
        }


    }
}