namespace EclipseSimulatorApp;

public class Empire
{
    public string empireName;
    private List<Ship> _shipTemplates;
    private uint _militaryPotential;
    private static readonly uint SingleBattlePotentialCapper = 1000;
    
    private Random randomSizeGenerator = new Random();
    
    // Constructor
    public Empire(string empireName, List<Ship> shipTemplates)
    {
        this.empireName = empireName;
        this._shipTemplates = shipTemplates;
        this._militaryPotential = 1000000000; // Each empire starts with identical potential
    }
    
    // Random gen method for triggering battles
    public void AttackTarget(Empire targetEmpire, int attacksIterator)
    {
        // Random generation of fleet structure
        
            // Determine max battle size 

            uint battleSizeCap = (this._militaryPotential + targetEmpire._militaryPotential) 
                                 / (2*SingleBattlePotentialCapper);
            
            // Determine size of the battle based on random number generator

            int battleSize = (int)(battleSizeCap * randomSizeGenerator.NextDouble());
            Console.WriteLine($"Battle size: {battleSize}");
            
            uint attackerFleetSize = (uint)Math.Min(battleSize, this._militaryPotential);
            Console.WriteLine(attackerFleetSize);
            uint defenderFleetSize = (uint)Math.Min(battleSize, targetEmpire._militaryPotential);
            Console.WriteLine(defenderFleetSize);
            
            Console.WriteLine("Battle Size and fleet sizes for both empires has been set");
            
        // Construct fleets

        Fleet attackerFleet = this.ConstructFleet(attackerFleetSize, $"1st {this.empireName} armada");
        Console.WriteLine($"Successfully created fleet called {attackerFleet.fleetName}");
        Fleet defenderFleet = targetEmpire.ConstructFleet(defenderFleetSize, $"1st {targetEmpire.empireName} armada");
        Console.WriteLine($"Successfully created fleet called {defenderFleet.fleetName}");
        
        Console.WriteLine($"Both fleets have been constructed commencing battle no. {attacksIterator}");
        
        // Battle logic 
        
        Battle currentBattle = new Battle(attackerFleet, defenderFleet);
        currentBattle.Battling();
        
        currentBattle.ReportBattleResults();
        
        // Post Report to a database

        // Reflect battle results in military potential
        
        // Why is it so hard for C# to use implicit casting?
        this._militaryPotential += (uint)attackerFleet.totalShipsEngaged;
        targetEmpire._militaryPotential += (uint)defenderFleet.totalShipsEngaged;
        
        Console.WriteLine("Ships that survived the battle have safely return to home starport");
        
    }
    
    // Method for constructing fleet based on fleetsize randomized
    private Fleet ConstructFleet(uint fleetSize, string fleetName)
    {
        // Declare an end product
        Fleet tempFleet = new Fleet(fleetName); 
        
        // Randomize multipliers for all ship classes
            // In future version refactor to loop
            
            // Prolly needs changing so it reflect that cruiser consume more fleet size than fighter
        double cruiserMultiplier = randomSizeGenerator.NextDouble();
        double destroyerMultiplier = randomSizeGenerator.NextDouble() * (1 - cruiserMultiplier);
        double fighterMultiplier = 1 - (cruiserMultiplier + destroyerMultiplier);
        
        // Seriezable
        double[] shipMultipliers = [cruiserMultiplier, destroyerMultiplier, fighterMultiplier];
        
        // Construct all ships based on Empire's dictionary

        for (int i = 0; i < shipMultipliers.Length; i++)
        {
            // Casting to round
            int tempAmount = (int)(shipMultipliers[i]*fleetSize);
            tempFleet.ConstructShipsOfType(_shipTemplates[i], tempAmount);
        }
        
        
        // Reduce empire's military potential for the amount used to create a fleet
            // Potential losses resulted in multiplier rounding are considered production inefficiencies
            // and are another little randomness introduced. 
        _militaryPotential -= fleetSize;
        
        return tempFleet;
    }

    public bool StillHasPotential()
    {
        bool potentialMeter = this._militaryPotential > 1000;
        return potentialMeter;
    }

    public uint GetPotential()
    {
        return this._militaryPotential;
    }
}