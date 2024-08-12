using AllTheBraveFrontier.Entities;

Console.WriteLine("Welcome to All the Brave Frontier!");

Player player = new Player();

Console.Write("\nPlease enter your name, player:");
player.Name = Console.ReadLine();

Console.WriteLine($"\nWelcome, {player.Name}!\n");
Console.WriteLine("Your intial heroes are being generated...\n");

List<Hero> initialHeroes = GenerateHeroes();
DisplayHeroes(initialHeroes);

Console.WriteLine("Your heroes are generated...");
Console.WriteLine("Please choose 5 heroes among the list by typing in their name.");

string input = string.Empty;
do
{
    Console.Write("Hero name: ");
    input = Console.ReadLine();

    if(IsHeroExists(input, player.Party)) // hero already exists in party
    {
        Console.WriteLine("\nHero already exists in your party. Try again.\n");
    }
    else if(!IsHeroExists(input, initialHeroes)) // hero not in the initial
    {
        Console.WriteLine("\nHero doesn't exist from the list. Try again.\n");
    }
    else
    {
        Hero? hero = FindHero(input, initialHeroes);
        player.Party.Add(hero);
        Console.WriteLine($"\n{hero.Name}\t{hero.Class} ADDED!");

        string reminder = player.Party.Count == 4 ? "Please add 1 more hero.\n" : $"Please add {5-player.Party.Count} more heroes.\n";
        Console.WriteLine(reminder);
    }
} while((IsHeroExists(input, player.Party) && player.Party.Count != 5) || !IsHeroExists(input, initialHeroes));

Console.WriteLine("Here are the heroes that have made the cut. Satisfied?");
DisplayHeroes(player.Party);

Console.ReadLine();

static Hero? FindHero(string name, List<Hero> heroes)
{
    foreach(Hero hero in heroes)
    {
        if(hero.Name.ToLower().Equals(name.ToLower()))
        {
            return hero;
        }
    }
    return null;
}

static bool IsHeroExists(string name, List<Hero> heroes)
{
    foreach(Hero hero in heroes)
    {
        if(hero.Name.ToLower().Equals(name.ToLower()))
        {
            return true;
        }
    }
    return false;
}

static void DisplayHeroes(List<Hero> heroes)
{
    foreach(Hero c in heroes)
    {
        Console.WriteLine($"\n{c.Name}\n{c.Class}");
        Console.WriteLine($"MAG\t{c.MAG}\tRES\t{c.RES}");
        Console.WriteLine($"ATK\t{c.ATK}\tCON\t{c.CON}");
    }
}

static List<Hero> GenerateHeroes() 
{

    List<Hero> heroes = new List<Hero>();

    for (int i = 0; i < 25; i++) 
    {
        heroes.Add(GenerateHero());
    }

    return heroes;
}

static Hero? GenerateHero()
{
    Random rnd = new Random();
    int value = rnd.Next(1,3);

    switch (value)
    {
        case 1:
            return new WhiteMage();
        case 2:
            return new Rogue();
    }

    return null;
}

