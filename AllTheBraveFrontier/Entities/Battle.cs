using AllTheBraveFrontier.Utilities;

namespace AllTheBraveFrontier.Entities
{
    public class Battle
    {
        public Game Game { get; set; }
        public Player Player { get; set; }
        public Enemy Enemy { get; set; }
        public int Round { get; set; }

        public List<Hero> BattleParty { get; set; }
        public List<Hero> DeadHeroes { get; set; }
        Stack<(Hero, int)> HeroStack { get; set; }
        Queue<Hero> DamageQueue { get; set; }

        public Battle(Game game, Player player, Enemy enemy)
        {
            Game = game;
            Player = player;
            Enemy = enemy;
            Round = 1;

            BattleParty = new List<Hero>(Player.Party);
            DeadHeroes = new List<Hero>();
        }

        public bool DefeatedGoblin()
        {
            do
            {
                Console.WriteLine($"\n***** ROUND {Round} *****");
                HeroStack = PlayerTurn();
                // Todo: insert method to:
                // - check what specific class is Hero
                // - pop each class, executing each Hero's action
                Console.ReadLine();

            } while (!Enemy.IsDead() || DeadHeroes.Count != 5);

            return false;
        }

        private Stack<(Hero, int)> PlayerTurn()
        {
            HeroStack = new Stack<(Hero, int)>();
            List<Hero> SelectedHeroes = new List<Hero>();
            string finishPlayerTurnStr = string.Empty;
            bool finishPlayerTurn = false;
            string heroIndexStr = string.Empty;
            int heroIndex = 0;
            string heroActionStr = string.Empty;
            int heroAction = 0;

            Console.WriteLine($"\n***** {Player.Name}'s TURN *****");

            do
            {
                do
                {
                    Console.WriteLine("\nHERO PARTY:");
                    for (int i = 0; i < BattleParty.Count; i++)
                    {
                        Hero hero = BattleParty[i];
                        Console.WriteLine($"[{i + 1}] {hero.Name}");
                        Console.WriteLine($"{hero.Class} - {hero.EvolutionLine[hero.CurrentEvolution]}");
                        Console.WriteLine($"HP\t{hero.CurrentHP}/{hero.TotalHP}");
                        Console.WriteLine($"MAG\t{hero.MAG}\tRES\t{hero.RES}");
                        Console.WriteLine($"ATK\t{hero.ATK}\tCON\t{hero.CON}");
                    }

                    Console.WriteLine("\nHERO STACK:");
                    Utility.DisplayHeroesHP(SelectedHeroes);

                    Console.WriteLine("Choose your hero:");
                    heroIndexStr = Console.ReadLine();

                    if (Utility.ValidInput(heroIndexStr) && int.TryParse(heroIndexStr, out heroIndex) 
                            && heroIndex >= 1 && heroIndex <= BattleParty.Count)
                    {
                        Hero selectedHero = BattleParty[heroIndex - 1];

                        if (SelectedHeroes.Contains(selectedHero))
                        {
                            Console.WriteLine($"\n{selectedHero.Name} has already been selected. Please choose a different hero.");
                        }
                        else
                        {
                            #region hero action
                            do
                            {
                                Console.WriteLine($"\nChoose {selectedHero.Name}'s action:");
                                Console.WriteLine("[1] Basic Attack");
                                Console.WriteLine($"[2] Main Ability - {selectedHero.Ability}");
                                heroActionStr = Console.ReadLine();

                                if (int.TryParse(heroActionStr, out heroAction) && (heroAction == 1 || heroAction == 2))
                                {
                                    HeroStack.Push((selectedHero, heroAction));
                                    SelectedHeroes.Add(selectedHero);
                                }
                                else
                                {
                                    Console.WriteLine($"\nInvald input. Please enter a valid action number 1 or 2. Try again.");
                                }

                            } while (heroAction != 1 && heroAction != 2);
                            #endregion
                        }
                    }
                    else
                    {
                        Console.WriteLine($"\nInvald input. Please enter a valid hero number 1-{BattleParty.Count}. Try again.");
                    }

                } while (HeroStack.Count != BattleParty.Count);

                Console.WriteLine("\nFinal stack:");
                Utility.DisplayHeroesHP(SelectedHeroes);

                do
                {
                    Console.Write("\nSatisfied with your stack? [Y/N]");
                    finishPlayerTurnStr = Console.ReadLine()?.Trim().ToLower();

                    if (!Utility.ValidInput(finishPlayerTurnStr) || (!finishPlayerTurnStr.Equals("y") && !finishPlayerTurnStr.Equals("n")))
                        Console.WriteLine("Please input [Y] or [N]. Try again.");

                } while (!Utility.ValidInput(finishPlayerTurnStr) || (!finishPlayerTurnStr.Equals("y") && !finishPlayerTurnStr.Equals("n")));

                if(finishPlayerTurnStr.Equals("y"))
                {
                    finishPlayerTurn = true;
                }
                else
                {
                    SelectedHeroes.Clear();
                    HeroStack.Clear();
                }   

            } while (!finishPlayerTurn);

            return HeroStack;

        }

    }
}