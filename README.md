Space Battle Simulator
--About--

  This app is my approach to take Eclipse (wonderful boardgame btw) on a date and answer a question what if 
  instead of space battle at home we could have actual space battle for hundreds of millions ships. I aimed 
  to create actual Ship objects instead of some chicky-chaky strategy to represent huge battles mathematically. 

  I just wanted to experience actually huge space clashes instead of 20 ships fighting using dices. Don't get me wrong.
  I love the game and spending time with my friends with it. It just doesn't have the right scale. So i rescaled it myself.
  
  Technicals: ConsoleApp developed in .NET SDK 9.0.6.

--Current State--
  I flattened the commit but honestly there were few versions representing different approaches.
  Current solution gives the App an opportunity to be sort of game or 

  Currently the App supports:
  - Multiple empires with simplified resources/potential system
  - Empires has method exposed for clashing with each other, construct fleets for battles, and return fleet to home planets.
  - Due to lack of the actual interface (and with my front-end skill we are not getting one soonish XD) two empires are preprogrammed for now.
  - Fleet class that bridges Ships and empire and sorts out ship from template constructor for clarity.
  - Battle class that could have actually been refactored into method in either Fleet or one of Empire's method but for now it stays.
  - Objectified ships with references to weapon library and very basic statistics.
  - Simplistic weapon system for now contains damage and accuracy. 
  - Exposed methods for every actions and instantiation (so technically there is a way to operate the app just no UI).


--Roadmap--

  1. Implement varying cost for different ship classes because for now Cruiser cost the same as Fighter and the results are absurd.
  2. Get rid of debugging logging in methods and replace with actual structure of battle report.
  3. Use Mongo Db connection to store the reports.
  4. Get rid of this riduculous battle class.
  5. Upgrade weaponry system utilizing damage types.
  6. Introduce armor and shields.
  7. Possibly use Math.NET to more elaborate randomisation. 
  8. Refactor to webApp and thus introduce user interface.
  9. Once "interfaced" get rid of hardcoded instances.
  10. Refactor into coroutines.
  11. Add donations for charity gateway. 
