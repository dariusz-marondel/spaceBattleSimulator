namespace EclipseSimulatorApp;


public class Battle
{
    private Fleet _blueFleet;
    private Fleet _redFleet;
    
    // Constructor
    public Battle(Fleet blueFleet, Fleet redFleet)
    {
        _blueFleet = blueFleet;
        _redFleet = redFleet;
    }
    
    // Methods

    public void Battling()
    {
        bool fighting = true;
        int battleRoundCounter = 0;
        int engagementCounterRed = _redFleet.totalShipsEngaged;
        int engagementCounterBlue = _blueFleet.totalShipsEngaged;

        while (fighting)
        {

            // One round of battle
            // for current state of development lets just assume only core phase is active
            // Core phase:
            // set up battleQ
            // Change battle queue to store both ship and its fleet information


            if (_blueFleet?.coreShips == null || _redFleet?.coreShips == null)
                return;

            // Combine all ships into one list with their fleet information
            
            // Combine all ships into one list with their fleet information
            var allShips = _blueFleet.coreShips.Select(ship => (ship, isBlue: true))
                .Concat(_redFleet.coreShips.Select(ship => (ship, isBlue: false)))
                .ToList();
            
            // Create battleQ - for now as a list. 
                // In the future lets investigate option to use Queue class from System.Collections  

            // Randomly shuffle battleQ
            var battleQ = allShips.OrderBy(_ => Random.Shared.Next());


            // using var enumerator = battleQ.GetEnumerator();
            // Console.WriteLine("BattleQ: " + enumerator);
                    
            // for each ship in the Queue, included parallelism for higher randomness
            Parallel.ForEach(battleQ, (ship) =>
            {
                // if ship is engaged and not destroyed
                if (ship.ship.isEngaged && !ship.ship.isDestroyed)
                {
                    // For Blue ships attack reds and vice vers - designing that took me 
                    if (ship.isBlue) // Tutaj pytanie do kogoś ogarniętego, czy z punktu widzenia compilera ma znaczenie
                        // jeśli ja tworzę dodatkową zmienną żeby to przechować czy nie ma to żadnego znaczenia. 
                    {
                        ship.ship.Attack(_redFleet.coreShips);
                    }
                    else
                    {
                        ship.ship.Attack(_blueFleet.coreShips);
                    }

                }
            });


            // Clean up destroyed 

            _blueFleet.DestroyedShipCleanup();
            _redFleet.DestroyedShipCleanup();

            // Increase counter by 1 per round
            battleRoundCounter++;

            // Print information every 1000 rounds - modify set of info per debugging need
            if (battleRoundCounter % 10 == 0 && battleRoundCounter != 0)
            {
                Console.WriteLine("BattleRound: " + battleRoundCounter);
                Console.WriteLine("Red Fleet: " + _redFleet.coreShips.Count);
                Console.WriteLine("Blue Fleet: " + _blueFleet.coreShips.Count);
                Console.WriteLine("BattleQ: " + _blueFleet.coreShips.Count);
            }

            if (_blueFleet.totalShipsDestroyed >= engagementCounterBlue || _redFleet.totalShipsDestroyed >= engagementCounterRed)
            {
                fighting = false;
            }

        }
        
        Console.WriteLine("Battle has been concluded");
    }

    public void ReportBattleResults()
    {
        Console.WriteLine(_blueFleet.fleetName, _blueFleet.totalShipsDestroyed, _blueFleet.totalShipsEngaged);
        Console.WriteLine(_redFleet.fleetName, _redFleet.totalShipsDestroyed, _redFleet.totalShipsEngaged);
        void PublishToDatabase()
        {
            
        }
    }
}