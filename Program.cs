using EclipseSimulatorApp;

void Main()
{
    // Initialize Weaponry and ship templates hardcoded for debugging
    static void InitializeWeaponTemplates()
    {
        Console.WriteLine("Welcome to the Weapon Constructor!");
        // Random random = new Random();
        
        // simplified random weapon constructor - i am leaving
            // control flow for the moment an user interface will be introduced
        // bool finishedConstructingWeapons = false;
    
        
        // Hardcoded to ease myself on debugging
            // Also before actual UI introduction is really tideous to move 
        string[] weaponNames = ["Red Laser"];
        int[] weaponDamage = [12];
        double[] weaponAccuracy = [0.6];
        DamageType[] damageType = [DamageType.Energy];
        
        // For a moment of inroducing user control
        // while (!finishedConstructingWeapons)
        
        // Loop registering all weapons from hardcoded arrays above 
        for (int weapon = 0; weapon < weaponDamage.Length; weapon++)
        {
            Console.WriteLine($"{weaponNames[weapon]} is about to be constructed!");
            WeaponLibrary.RegisterWeapon(weaponNames[weapon], 
                new Weapon(weaponDamage[weapon], weaponAccuracy[weapon], damageType[weapon]));
            
            Console.WriteLine(($"Successfully constructed weapon called: {weaponNames[weapon]}" +
                               $"that inflicts {weaponDamage[weapon]}, with {weaponAccuracy} accuracy."));
        }
        
    }
    
    // Whole section below is a complete mess - but will do for the debugging
    Console.WriteLine("Begin a simulation test!");
    InitializeWeaponTemplates();
    
    Console.WriteLine("Fighting empires are about to be constructed!");
    
    
    
    // Add control flow statements for incorrect inputs or
    // inputs that have not been before registered as weapons
    
    Dictionary<string, Ship> shipTemplates = new Dictionary<string, Ship>
    {
        { "lightCruiser", new Ship(["Red Laser", "Red Laser", "Red Laser", "Red Laser"], 800) },
        { "lightDestroyer", new Ship(["Red Laser", "Red Laser", "Red Laser"], 400) },
        { "lightFighter", new Ship(["Red Laser", "Red Laser"], 250) }
    };
    
    List<Ship> empireShips = new List<Ship>(shipTemplates.Values);

    
    // Hard creation of the empires
    Empire terranEmpire = new Empire("Terran Empire", empireShips);
    Empire descendantsOfDrifters = new Empire("Descendants of Drifters", empireShips);

    int attackCounter = 0;

    while (terranEmpire.StillHasPotential() && descendantsOfDrifters.StillHasPotential())
    {
        // Code the battle logic 
        
        Console.WriteLine("Drifters attacked the Terrans again!");
        
        descendantsOfDrifters.AttackTarget(terranEmpire, attackCounter);
        attackCounter++;
    }
    
    Console.WriteLine($"The great war has ended after {attackCounter} attacks performed by Drifters on the Terran Empire.");
    Console.WriteLine($"Both fleets have been decimated leaving Terrans with {terranEmpire.GetPotential()}" +
                      $" and Drifters with {descendantsOfDrifters.GetPotential()}.");
    Console.ReadKey();
}

Main();