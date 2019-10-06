using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Units
{
    public class GameEngine
    {
        //variable declarations
        private Map map;
        int counter = 0;
        public Map Map { get => map; }
        
        //Constructor for gameEngine
        public GameEngine(int numberOfUnits, int mapSizeX, int mapSizeY, int numOfBuildings)
        {
            //Creates new map and generates new battlefield
            map = new Map(numberOfUnits,mapSizeX,mapSizeY,numOfBuildings);
            map.newBattlefield();
            
        } 

        //Method that controls the game on every tick of the timer
        public void gameLogic(Unit[] unit, Buildings.Building[] bldings)
        {
            
            bool resetCounter = false;

            for (int i = 0; i < unit.Length; i++)
            {
                unit[i].death(); //Check for death and set relevant variable
                unit[i].IsAttacking = false; 

                if (unit[i].IsDead == false) //If unit is not dead
                {
                    Unit closest = unit[i].closestUnit(unit); //Determines the closest unit to this unit and stores that unit in 'closest'

                    if (unit[i].attackingRange(closest) == false || unit[i].Health / unit[i].MaxHealth * 100 < 25) //If the unit is below 25% hp or is not in range of the closest enemy 
                    {
                        unit[i].movement(closest, map.MapSizeX, map.MapSizeY); //Move

                        map.populateMap(); //Refresh Map
                    }
                    else if (unit[i].Faction != closest.Faction) //If the unit is not part of the same team
                    {
                        unit[i].combat(closest); //Do combat
                        map.populateMap(); //Refreh map
                    }

                }

                
            }

            //loop that runs through all the buildings 
            for (int p = 0; p < bldings.Length; p++)
            {
                string type = bldings[p].GetType().ToString();
                string[] typeArr = type.Split('.');
                type = typeArr[typeArr.Length - 1];

                //Checks type of building
                if (type == "ResourceBuilding")
                {
                    //creates temp instance of resourcebuilding
                    Buildings.ResourceBuilding rblding = (Buildings.ResourceBuilding)bldings[p];

                    //calls the generate resource method
                    rblding.GenerateResources();
                    
                }
                else
                {
                    //creates temp instance of factorybuilding
                    Buildings.FactoryBuilding fblding = (Buildings.FactoryBuilding)bldings[p];
                    
                    //checks if the building should be generating a unit
                    if(fblding.ProductionSpeed <= counter)
                    {
                        //If so create a temp unit and save it in the map array after resizing it
                        Unit temp = fblding.SpawnUnits();
                        Array.Resize(ref map.units, map.Units.Length + 1);
                        map.Units[map.Units.Length - 1] = temp;
                        resetCounter = true; //Set boolean to reset counter
                    }
                    else
                    {
                        //if not set boolean not to reset counter
                        resetCounter = false;
                    }
                    
                }
            }

            //checks whether counter should be reset or incrimented
            if(resetCounter == true)
            {
                resetCounter = false;
                counter = 0;
            }
            else
            {
                ++counter;
                
            }
            
        }

        //Method that 'fetches' all the buildings and all the unit stats and returns the formatted string of it all combined
        public string getStats(Unit[] unit, Buildings.Building[] bldnings)
        {
            string stats = "";
            for (int i = 0; i < unit.Length; i++)
            {
                stats += unit[i].ToString();
            }
            for (int k = 0; k < bldnings.Length; k++)
            {
                stats += bldnings[k].ToString();
            }
            
            return stats;
        }
    }
}
