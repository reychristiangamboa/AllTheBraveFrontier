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

            BattleParty = CloneHeroes(Player.Party);
            HeroStack = new Stack<(Hero, int)>();
            DamageQueue = new Queue<Hero>();
            DeadHeroes = new List<Hero>();
        }

        public bool CommenceBattle()
        {
            do
            {
                Console.WriteLine($"\n***** ROUND {Round} *****");

                HeroStack = PlayerTurn();
                ProcessPlayerTurn();

                if (Enemy.IsDead())
                    break;

                ProcessEnemyTurn();

                Round++;
            } while (DeadHeroes.Count != 5);

            if (Enemy.IsDead())
            {
                Console.WriteLine($"\nWELL DONE! {Enemy.Name} has been defeated!\n");
                SetEnemyDefeated();
                Console.ReadLine();
                Game.DisplayMainMenuScreen();
                return true;
            }
            else
            {
                Console.WriteLine($"\nOH NO! You lost to {Enemy.Name}. Come up with a better strategy next time!\n");
                Console.ReadLine();
                Game.DisplayMainMenuScreen();
                return false;
            }

        }

        private void ProcessPlayerTurn()
        {
            do
            {
                (Hero hero, int action) heroStack = HeroStack.Pop();
                Hero currentHero = heroStack.hero;
                int currentHeroAction = heroStack.action;

                #region hero actions
                if (currentHeroAction == 2)
                {
                    switch (heroStack.hero)
                    {
                        case WhiteMage:
                            currentHero.MainAbility(BattleParty.ToArray());
                            break;
                        case Rogue:
                            currentHero.MainAbility(Enemy);
                            break;
                        default:
                            break;
                    }
                }
                else
                    currentHero.BasicAttack(Enemy);
                #endregion

                if (Enemy.IsDead())
                    break;

                #region hero status
                if (currentHero.IsDead())
                {
                    Console.WriteLine($"\n{currentHero.Name} has died.\n");
                    BattleParty.Remove(currentHero);
                    DeadHeroes.Add(currentHero);
                }
                else
                    DamageQueue.Enqueue(currentHero);
                #endregion

            } while (HeroStack.Count != 0);
            Console.ReadLine();
        }

        private void ProcessEnemyTurn()
        {

            Console.WriteLine($"\n***** {Enemy.Name}'s TURN *****");

            do
            {
                Hero currentHero = DamageQueue.Dequeue();
                int enemyAction = Enemy.Gambit.Dequeue();

                #region enemy actions
                if (enemyAction == 1)
                    Enemy.BasicAttack(currentHero);
                else if (enemyAction == 2)
                    Enemy.MainAbility(currentHero);
                else
                    Console.WriteLine($"\n{Enemy.Name} SKIPS on {currentHero.Name}.");
                #endregion

                #region hero status
                if (currentHero.IsDead())
                {
                    Console.WriteLine($"\n{currentHero.Name} has died.\n");
                    BattleParty.Remove(currentHero);
                    DeadHeroes.Add(currentHero);
                }
                #endregion
            } while (DamageQueue.Count != 0);

            Enemy.ResetGambit();
            Console.ReadLine();

        }

        private Stack<(Hero, int)> PlayerTurn()
        {
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
                    Console.WriteLine("\nENEMY:");
                    Console.WriteLine(Enemy.ShowDetails());
                    Console.WriteLine($"{Enemy.Name}'s GAMBIT:");
                    Console.WriteLine(Enemy.ShowGambit());

                    Console.WriteLine("\nHERO PARTY:");
                    for (int i = 0; i < BattleParty.Count; i++)
                    {
                        Hero hero = BattleParty[i];

                        if (!SelectedHeroes.Contains(hero))
                        {
                            Console.WriteLine($"[{i + 1}] {hero.Name}");
                            Console.WriteLine($"{hero.Class} - {hero.EvolutionLine[hero.CurrentEvolution]}");
                            Console.WriteLine($"HP\t{hero.CurrentHP}/{hero.TotalHP}");
                            Console.WriteLine($"MAG\t{hero.MAG}\tRES\t{hero.RES}");
                            Console.WriteLine($"ATK\t{hero.ATK}\tCON\t{hero.CON}");
                        }
                    }

                    Console.WriteLine($"\nHERO STACK ({SelectedHeroes.Count}):");
                    Utility.DisplayHeroes(SelectedHeroes);

                    Console.WriteLine("Choose your hero:");
                    heroIndexStr = Console.ReadLine().Trim();

                    if (Utility.ValidInput(heroIndexStr) && int.TryParse(heroIndexStr, out heroIndex)
                            && heroIndex >= 1 && heroIndex <= BattleParty.Count)
                    {
                        Hero selectedHero = BattleParty[heroIndex - 1];

                        if (SelectedHeroes.Contains(selectedHero))
                            Console.WriteLine($"\n{selectedHero.Name} has already been selected. Please choose a different hero.");
                        else
                        {
                            #region hero action
                            do
                            {
                                Console.WriteLine($"\nChoose {selectedHero.Name}'s action:");
                                Console.WriteLine("[1] Basic Attack");
                                Console.WriteLine($"[2] Main Ability - {selectedHero.Ability}");
                                heroActionStr = Console.ReadLine().Trim();

                                if (int.TryParse(heroActionStr, out heroAction) && (heroAction == 1 || heroAction == 2))
                                {
                                    HeroStack.Push((selectedHero, heroAction));
                                    SelectedHeroes.Insert(0, selectedHero);
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
                Utility.DisplayHeroes(SelectedHeroes);

                do
                {
                    Console.Write("\nSatisfied with your stack? [Y/N]");
                    finishPlayerTurnStr = Console.ReadLine()?.Trim().ToLower();

                    if (!Utility.ValidInput(finishPlayerTurnStr) || (!finishPlayerTurnStr.Equals("y") && !finishPlayerTurnStr.Equals("n")))
                        Console.WriteLine("Please input [Y] or [N]. Try again.");

                } while (!Utility.ValidInput(finishPlayerTurnStr) || (!finishPlayerTurnStr.Equals("y") && !finishPlayerTurnStr.Equals("n")));

                if (finishPlayerTurnStr.Equals("y"))
                    finishPlayerTurn = true;
                else
                    HeroStack.Clear();

                SelectedHeroes.Clear();

            } while (!finishPlayerTurn);

            return HeroStack;

        }

        private void SetEnemyDefeated()
        {
            switch (Enemy)
            {
                case Goblin:
                    Game.DefeatedGoblin = true;
                    break;
                case Lizardman:
                    Game.DefeatedLM = true;
                    break;
                case SkeletalDragon:
                    Game.DefeatedSD = true;
                    break;
                default:
                    break;
            }
        }

        private List<Hero> CloneHeroes(List<Hero> heroes)
        {
            List<Hero> clones = new List<Hero>();
            foreach (Hero h in heroes)
                clones.Add(h.Clone());

            return clones;
        }

    }
}