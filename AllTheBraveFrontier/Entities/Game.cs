using AllTheBraveFrontier.Utilities;

namespace AllTheBraveFrontier.Entities
{
    public class Game
    {
        public Player Player { get; set; }
        public List<Hero> InitialHeroes { get; set; }
        public Battle Battle { get; set; }
        public Goblin GoblinProp { get; set; }
        public Lizardman LizardmanProp { get; set; }
        public SkeletalDragon SkeletalDragonProp { get; set; }
        public Dragonsire DragonsireProp { get; set; }
        public bool DefeatedGoblin { get; set; }
        public bool DefeatedLM { get; set; }
        public bool DefeatedSD { get; set; }
        private string PlayerInput { get; set; } = string.Empty;

        public Game()
        {
            Player = new Player();
            InitialHeroes = new List<Hero>();
            DefeatedGoblin = false;
            DefeatedLM = false;
            DefeatedSD = false;
        }

        public void InitializeGame()
        {
            Console.WriteLine("Welcome to All the Brave Frontier!");

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

                Console.WriteLine("\nHere are the heroes that have made the cut:");

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

            Console.WriteLine("\n***** ALL THE BRAVE FRONTIER *****");
            Console.WriteLine("[S]TART GAME");
            Console.WriteLine("[H]EROES");
            Console.WriteLine("[R]ESTART");
            Console.WriteLine("[Q]UIT");

            PlayerInput = Console.ReadLine()?.Trim().ToLower();

            switch (PlayerInput)
            {
                case "s":
                    DisplayChooseEnemyScreen();
                    break;
                case "h":
                    DisplayHeroes();
                    break;
                case "q":
                    Environment.Exit(0);
                    break;
            }
        }

        private void DisplayHeroes()
        {
            Console.WriteLine("\n***** HEROES *****");
            Console.WriteLine($"ROSTER ({Player.HeroRoster.Count}):");
            Utility.DisplayHeroes(Player.HeroRoster);
            Console.WriteLine("\nPARTY:");
            Utility.DisplayHeroes(Player.Party);

            #region player input
            do
            {
                string choices = Player.HeroRoster.Count > 0 ? "\n[F]USION | [M]ANAGE | [B]ACK" : "\n[B]ACK";
                Console.Write($"\n{choices}");
                PlayerInput = Console.ReadLine()?.Trim().ToLower();

                switch (PlayerInput)
                {
                    case "f":
                        DisplayHeroFusion();
                        break;
                    case "b":
                        DisplayMainMenuScreen();
                        break;
                    case "m":
                        DisplayManageHeroes();
                        break;
                    default:
                        Console.WriteLine("\nInvalid input. Please try again.");
                        break;
                }
            } while (!Utility.ValidInput(PlayerInput) ||
                !PlayerInput.Equals("b") && !PlayerInput.Equals("f") && !PlayerInput.Equals("m") ||
                (PlayerInput.Equals("f") || PlayerInput.Equals("m")) && Player.HeroRoster.Count == 0);
            #endregion
        }

        private void DisplayHeroFusion()
        {
            string partyHeroIndexStr = string.Empty;
            int partyHeroIndex = 0;
            string rosterHeroIndexStr = string.Empty;
            int rosterHeroIndex = 0;

            Console.WriteLine("\n***** FUSION CENTER *****");

            do
            {
                List<Hero> rosterFusionCandidates = Player.HeroRoster.Where(rosterHero => Player.Party.Any(partyHero => rosterHero.GetType() == partyHero.GetType())).ToList();

                Console.WriteLine($"ROSTER ({Player.HeroRoster.Count}):");
                Utility.DisplayHeroes(Player.HeroRoster);
                Console.WriteLine("\nPARTY:");
                Utility.DisplayHeroes(Player.Party);

                do
                {
                    Console.WriteLine("[F]USE HEROES | [B]ACK");
                    PlayerInput = Console.ReadLine()?.Trim().ToLower();
                    if (!Utility.ValidInput(PlayerInput) || !PlayerInput.Equals("f") && !PlayerInput.Equals("b"))
                        Console.WriteLine("Invalid input. Try again.");
                } while (!Utility.ValidInput(PlayerInput) || !PlayerInput.Equals("f") && !PlayerInput.Equals("b"));

                if (PlayerInput.Equals("f"))
                {
                    if (rosterFusionCandidates.Count == 0)
                    {
                        Console.WriteLine("\nNo possible fusions detected. Earn more heroes.");
                        break;
                    }
                    else
                    {
                        Console.WriteLine("\nFUSION CANDIDATES:");
                        for (int i = 0; i < rosterFusionCandidates.Count; i++)
                        {
                            Hero hero = rosterFusionCandidates[i];

                            Console.WriteLine($"[{i + 1}] {hero.Name}");
                            Console.WriteLine($"{hero.Class} - {hero.EvolutionLine[hero.CurrentEvolution]}");
                            Console.WriteLine($"HP\t{hero.CurrentHP}/{hero.TotalHP}");
                            Console.WriteLine($"MAG\t{hero.MAG}\tRES\t{hero.RES}");
                            Console.WriteLine($"ATK\t{hero.ATK}\tCON\t{hero.CON}");
                        }

                        Console.WriteLine("Choose hero from the fusion candidates:");
                        rosterHeroIndexStr = Console.ReadLine();

                        if (Utility.ValidInput(rosterHeroIndexStr)
                        && int.TryParse(rosterHeroIndexStr, out rosterHeroIndex)
                        && rosterHeroIndex >= 1 && rosterHeroIndex <= rosterFusionCandidates.Count)
                        {
                            Hero rosterHero = rosterFusionCandidates[rosterHeroIndex - 1];

                            Console.WriteLine("You have chosen:");
                            Console.WriteLine(rosterHero.ShowDetails());


                            List<Hero> partyFusionCandidates = Player.Party.Where(partyHero => partyHero.GetType() == rosterHero.GetType()).ToList();

                            Console.WriteLine("\nHERO PARTY:");
                            for (int i = 0; i < partyFusionCandidates.Count; i++)
                            {

                                Hero hero = partyFusionCandidates[i];

                                Console.WriteLine($"[{i + 1}] {hero.Name}");
                                Console.WriteLine($"{hero.Class} - {hero.EvolutionLine[hero.CurrentEvolution]}");
                                Console.WriteLine($"HP\t{hero.CurrentHP}/{hero.TotalHP}");
                                Console.WriteLine($"MAG\t{hero.MAG}\tRES\t{hero.RES}");
                                Console.WriteLine($"ATK\t{hero.ATK}\tCON\t{hero.CON}");
                            }

                            Console.WriteLine("Choose hero from the party:");
                            partyHeroIndexStr = Console.ReadLine();

                            if (Utility.ValidInput(partyHeroIndexStr)
                            && int.TryParse(partyHeroIndexStr, out partyHeroIndex)
                            && partyHeroIndex >= 1 && partyHeroIndex <= partyFusionCandidates.Count)
                            {
                                Hero partyHero = partyFusionCandidates[partyHeroIndex - 1];

                                Player.Party.Insert(partyHeroIndex, rosterHero.Fusion(partyHero));

                                Player.HeroRoster.Remove(rosterHero);
                                Player.Party.Remove(partyHero);

                                Console.ReadLine();
                            }
                        }
                    }
                }

            } while (!PlayerInput.Equals("b"));

            DisplayHeroes();

        }

        private void DisplayManageHeroes()
        {
            string partyHeroIndexStr = string.Empty;
            int partyHeroIndex = 0;
            string rosterHeroIndexStr = string.Empty;
            int rosterHeroIndex = 0;

            Console.WriteLine("\n***** MANAGE HEROES *****");
            do
            {
                Console.WriteLine($"ROSTER ({Player.HeroRoster.Count}):");
                Utility.DisplayHeroes(Player.HeroRoster);
                Console.WriteLine("\nPARTY:");
                Utility.DisplayHeroes(Player.Party);
                do
                {
                    Console.WriteLine("[S]WAP HEROES | [B]ACK");
                    PlayerInput = Console.ReadLine()?.Trim().ToLower();
                    if (!Utility.ValidInput(PlayerInput) || !PlayerInput.Equals("s") && !PlayerInput.Equals("b"))
                        Console.WriteLine("Invalid input. Try again.");
                } while (!Utility.ValidInput(PlayerInput) || !PlayerInput.Equals("s") && !PlayerInput.Equals("b"));

                if (PlayerInput.Equals("s"))
                {
                    Console.WriteLine("\nHERO PARTY:");
                    for (int i = 0; i < Player.Party.Count; i++)
                    {
                        Hero hero = Player.Party[i];

                        Console.WriteLine($"[{i + 1}] {hero.Name}");
                        Console.WriteLine($"{hero.Class} - {hero.EvolutionLine[hero.CurrentEvolution]}");
                        Console.WriteLine($"HP\t{hero.CurrentHP}/{hero.TotalHP}");
                        Console.WriteLine($"MAG\t{hero.MAG}\tRES\t{hero.RES}");
                        Console.WriteLine($"ATK\t{hero.ATK}\tCON\t{hero.CON}");
                    }

                    do
                    {
                        Console.WriteLine("Choose hero from the party:");
                        partyHeroIndexStr = Console.ReadLine();
                        if (!Utility.ValidInput(partyHeroIndexStr)
                                || !int.TryParse(partyHeroIndexStr, out partyHeroIndex)
                                || int.Parse(partyHeroIndexStr) <= 0 && int.Parse(partyHeroIndexStr) >= Player.Party.Count)
                            Console.WriteLine("Invalid input. Try again.");
                    } while (!Utility.ValidInput(partyHeroIndexStr)
                                || !int.TryParse(partyHeroIndexStr, out partyHeroIndex)
                                || int.Parse(partyHeroIndexStr) <= 0 && int.Parse(partyHeroIndexStr) >= Player.Party.Count);

                    if (Utility.ValidInput(partyHeroIndexStr)
                        && int.TryParse(partyHeroIndexStr, out partyHeroIndex)
                        && partyHeroIndex >= 1 && partyHeroIndex <= Player.Party.Count)
                    {
                        Hero partyHero = Player.Party[partyHeroIndex - 1];

                        Console.WriteLine("\nHERO ROSTER:");
                        for (int i = 0; i < Player.HeroRoster.Count; i++)
                        {
                            Hero hero = Player.HeroRoster[i];

                            Console.WriteLine($"[{i + 1}] {hero.Name}");
                            Console.WriteLine($"{hero.Class} - {hero.EvolutionLine[hero.CurrentEvolution]}");
                            Console.WriteLine($"HP\t{hero.CurrentHP}/{hero.TotalHP}");
                            Console.WriteLine($"MAG\t{hero.MAG}\tRES\t{hero.RES}");
                            Console.WriteLine($"ATK\t{hero.ATK}\tCON\t{hero.CON}");
                        }

                        do
                        {
                            Console.WriteLine("Choose hero from the roster:");
                            rosterHeroIndexStr = Console.ReadLine();
                            if (!Utility.ValidInput(rosterHeroIndexStr)
                                    || !int.TryParse(rosterHeroIndexStr, out rosterHeroIndex)
                                    || int.Parse(rosterHeroIndexStr) <= 0 && int.Parse(rosterHeroIndexStr) >= Player.Party.Count)
                                Console.WriteLine("Invalid input. Try again.");
                        } while (!Utility.ValidInput(rosterHeroIndexStr)
                                || !int.TryParse(rosterHeroIndexStr, out partyHeroIndex)
                                || int.Parse(rosterHeroIndexStr) <= 0 && int.Parse(rosterHeroIndexStr) >= Player.Party.Count);

                        if (Utility.ValidInput(rosterHeroIndexStr)
                        && int.TryParse(rosterHeroIndexStr, out rosterHeroIndex)
                        && rosterHeroIndex >= 1 && rosterHeroIndex <= Player.HeroRoster.Count)
                        {
                            Hero rosterHero = Player.HeroRoster[rosterHeroIndex - 1];

                            Player.Party.Insert(partyHeroIndex, rosterHero);
                            Player.HeroRoster.Insert(rosterHeroIndex, partyHero);
                            Player.Party.Remove(partyHero);
                            Player.HeroRoster.Remove(rosterHero);

                            Console.WriteLine($"{partyHero.Name} -> ROSTER.");
                            Console.WriteLine($"{rosterHero.Name} -> PARTY.\n");
                            Console.ReadLine();
                        }
                    }
                }
            } while (!PlayerInput.Equals("b"));

            DisplayHeroes();
        }

        private void DisplayChooseEnemyScreen()
        {
            bool win = false;

            Console.WriteLine("\n***** CHOOSE ENEMY *****");
            Console.WriteLine("[G]OBLIN");
            Console.WriteLine("[L]IZARDMAN");
            if (DefeatedGoblin && DefeatedLM)
                Console.WriteLine("[S]KELETAL DRAGON");
            if (DefeatedSD)
                Console.WriteLine("[D]RAGONSIRE");
            Console.WriteLine("[B]ACK");

            PlayerInput = Console.ReadLine()?.Trim().ToLower();

            switch (PlayerInput)
            {
                case "g":
                    GoblinProp = new Goblin();
                    Battle = new Battle(this, Player, GoblinProp);
                    win = Battle.CommenceBattle();
                    RewardHeroes(win, GoblinProp);
                    DisplayMainMenuScreen();
                    break;
                case "l":
                    LizardmanProp = new Lizardman();
                    Battle = new Battle(this, Player, LizardmanProp);
                    win = Battle.CommenceBattle();
                    RewardHeroes(win, LizardmanProp);
                    DisplayMainMenuScreen();
                    break;
                case "s":
                    SkeletalDragonProp = new SkeletalDragon();
                    Battle = new Battle(this, Player, SkeletalDragonProp);
                    win = Battle.CommenceBattle();
                    RewardHeroes(win, SkeletalDragonProp);
                    DisplayMainMenuScreen();
                    break;
                case "d":
                    DragonsireProp = new Dragonsire();
                    Battle = new Battle(this, Player, DragonsireProp);
                    win = Battle.CommenceBattle();
                    RewardHeroes(win, DragonsireProp);
                    DisplayMainMenuScreen();
                    break;
                case "b":
                    DisplayMainMenuScreen();
                    break;
            }

        }

        private void RewardHeroes(bool win, Enemy e)
        {
            List<Hero> newHeroes = new List<Hero>();
            int numHeroes = 0;

            if (win)
            {
                switch (e)
                {
                    case Goblin:
                        numHeroes = 3;
                        break;
                    case Lizardman:
                        numHeroes = 6;
                        break;
                    case SkeletalDragon:
                        numHeroes = 9;
                        break;
                    case Dragonsire:
                        numHeroes = 12;
                        break;
                }
            }
            else
                numHeroes = 1;


            for (int i = 0; i < numHeroes; i++)
            {
                Hero h = GenerateHero();
                newHeroes.Add(h);
                Player.HeroRoster.Add(h);
            }

            Console.WriteLine("\nMeet your new heroes:\n");
            Utility.DisplayHeroes(newHeroes);
            Console.WriteLine("\nCheck them out in your HEROES section.\n");
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
            int value = rnd.Next(1, 6);

            switch (value)
            {
                case 1:
                    return new WhiteMage();
                case 2:
                    return new BlackMage();
                case 3:
                    return new RedMage();
                case 4:
                    return new Constable();
                case 5:
                    return new Rogue();
            }

            return null;
        }


    }
}