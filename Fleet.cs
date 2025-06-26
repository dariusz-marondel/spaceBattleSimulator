namespace EclipseSimulatorApp;

public class Fleet
{
    
    public string fleetName;
    
    // Currently only functioning list
    public List<Ship> coreShips = new List<Ship>();

    // Currently in development - not functioning
    // public List<Ship> heavyShips;
    // public List<Ship> supportShips;
    // public List<Ship> vanguardShips;

    public int totalShipsEngaged;
    public int totalShipsDestroyed;
    public int totalShipsDisengaged;

    // Constructor
    public Fleet(string fleetName)
    {
        this.fleetName = fleetName;
        this.totalShipsDestroyed = 0;
        this.totalShipsDisengaged = 0;
        
    }

    // Methods
    
    
    // Allow a fleet to be able to construct ship objects of a given type
    public void ConstructShipsOfType(Ship shipType, int amount)
    {
        List<Ship> shipsTempList = new List<Ship>();
        
        
        for (int i = 0; i < amount; i++)
        {
            Ship shipUnderConstruction = new Ship(shipType);
            // Naming the exact ship
            
            shipUnderConstruction.AssignId($"BSS {fleetName.Substring(4, 5)} {i}");
            
            // For debugging and it works
            //Console.WriteLine(shipUnderConstruction._shipId);
            
            // This is really ineficient - look into int later
            shipsTempList.Add(shipUnderConstruction);
            
        }

        // foreach (Ship ship in shipsTempList)
        // {
        //     Console.WriteLine(ship._shipId);
        // }
        
        // reflect number of ships 
        this.totalShipsEngaged += amount;
        
        Console.WriteLine($"Constructed {amount} ships");
        Console.WriteLine($"total ship engagement: {totalShipsEngaged}");
        
        // Add ships constructed to a list
        this.coreShips.AddRange(shipsTempList);
        
    }

    public void DestroyedShipCleanup()
    {
        int shipsCleaned = 0;
        shipsCleaned = coreShips.RemoveAll(ship => ship.isDestroyed);
        
        this.totalShipsDestroyed += shipsCleaned;
    }
    
}