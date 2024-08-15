using AllTheBraveFrontier.Utilities;

namespace AllTheBraveFrontier.Entities
{
    public class Game
    {
        public Player Player { get; set; }
        public List<Hero> InitialHeroes { get; set; }
        public Battle Battle { get; set; }
        public Goblin Goblin { get; set; }
        public bool DefeatedGoblin { get; set; }
        public bool DefeatedLizardman { get; set; }

        public Game()
        {
            Player = new Player();
            InitialHeroes = new List<Hero>();
            DefeatedGoblin = false;
            DefeatedLizardman = false;
        }

        public void InitializeGame()
        {
            Console.WriteLine("\t\t\tWelcome to All the Brave Frontier!");

            do
            {
                Console.Write("Please enter your name, player:");
                Player.Name = Console.ReadLine();

                if (string.IsNullOrEmpty(Player.Name))
                    Console.WriteLine("Please provide a name. Try again.");

            } while (!Utility.ValidInput(Player.Name));

            Console.WriteLine($"\nWelcome, {Player.Name}!\n");

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
                    Player.Party.Clear();
                    InitialHeroes.Clear();
                }

                InitialHeroes = GenerateHeroes();
                Console.WriteLine($"\nGreetings, {Player.Name}! Your intial heroes are being generated...\n");

                Utility.DisplayHeroes(InitialHeroes);

                Console.WriteLine("\nYour heroes are generated...");
                Console.WriteLine("Please choose 5 heroes among the list by typing in their name.\n");
                do
                {
                    Console.Write("Hero name: ");
                    heroChoice = Console.ReadLine().Trim().ToLower();

                    #region hero choice validations
                    if (!Utility.ValidInput(heroChoice))
                        Console.WriteLine("Please input a name. Try again.\n");
                    else if (Utility.IsHeroExists(heroChoice, Player.Party))
                        Console.WriteLine("Hero already exists in your party. Try again.\n");
                    else if (!Utility.IsHeroExists(heroChoice, InitialHeroes))
                        Console.WriteLine("Hero doesn't exist from the list. Try again.\n");
                    else
                    {
                        Hero? hero = Utility.FindHero(heroChoice, InitialHeroes);
                        Player.Party.Add(hero);
                        Console.WriteLine($"{hero.Name}\t{hero.Class} ADDED!\n");
                    }
                    #endregion

                } while (!Utility.ValidInput(heroChoice) || (Utility.IsHeroExists(heroChoice, Player.Party) && Player.Party.Count < 5)
                            || !Utility.IsHeroExists(heroChoice, InitialHeroes));

                Console.WriteLine("\nHere are the heroes that have made the cut:\n");

                Utility.DisplayHeroes(Player.Party);

                do
                {
                    Console.Write("Satisfied with your party? [Y/N]");
                    satisfiedInput = Console.ReadLine()?.Trim().ToLower();

                    if (!Utility.ValidInput(satisfiedInput) || (!satisfiedInput.Equals("y") && !satisfiedInput.Equals("n")))
                        Console.WriteLine("Please input [Y] or [N]. Try again.");

                } while (!Utility.ValidInput(satisfiedInput) || (!satisfiedInput.Equals("y") && !satisfiedInput.Equals("n")));

            } while (satisfiedInput.Equals("n"));

            InitialHeroes.Clear();
            DisplayMainMenuScreen();
        }

        public void DisplayMainMenuScreen()
        {
            string playerChoice = string.Empty;

            Console.WriteLine("\n***** ALL THE BRAVE FRONTIER *****");
            Console.WriteLine("[S]TART GAME");
            Console.WriteLine("S[E]TTINGS");
            Console.WriteLine("[R]ESTART");
            Console.WriteLine("[Q]UIT");

            playerChoice = Console.ReadLine()?.Trim().ToLower();

            switch (playerChoice)
            {
                case "s":
                    DisplayChooseEnemyScreen();
                    break;
                case "q":
                    Environment.Exit(0);
                    break;
            }
        }

        private void DisplayChooseEnemyScreen()
        {
            string playerChoice = string.Empty;

            Console.WriteLine("\n***** CHOOSE ENEMY *****");
            Console.WriteLine("[G]OBLIN");
            Console.WriteLine("[B]ACK");

            playerChoice = Console.ReadLine()?.Trim().ToLower();

            switch (playerChoice)
            {
                case "g":
                    Goblin = new Goblin();
                    Battle = new Battle(this, Player, Goblin);
                    Battle.CommenceBattle();
                    break;
                case "b":
                    DisplayMainMenuScreen();
                    break;
            }

        }

        private List<Hero> GenerateHeroes()
        {

            List<Hero> heroes = new List<Hero>();

            for (int i = 0; i < 25; i++)
                heroes.Add(GenerateHero());

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