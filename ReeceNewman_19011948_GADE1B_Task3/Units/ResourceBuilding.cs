using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace Buildings
{
    public class ResourceBuilding : Building
    {
        //variable declarations
        private string type;
        private int resourcePoolRemaining, generatedResources, resourcesPerRound, maxPool;

        

        //Constructor for resourcebuilding class
        public ResourceBuilding(int xPos, int yPos, int health, int faction, char symbol, int maxPool, int production) : base(xPos, yPos, health, faction, symbol)
        {
            this.resourcePoolRemaining = maxPool;
            this.maxPool = maxPool;
            generatedResources = 0;
            this.resourcesPerRound = production;
            type = "Wood";
        }

        //Overloaded constructor for resource building for if building is being loaded from memory
        public ResourceBuilding(int xPos, int yPos, int health, int faction, char symbol, int maxPool, int production, int maxHp) : base(xPos, yPos, health, faction, symbol)
        {
            this.maxHealth = maxHp;
            this.resourcePoolRemaining = maxPool;
            this.maxPool = maxPool;
            generatedResources = 0;
            this.resourcesPerRound = production;
            type = "Wood";
        }

        //Checks if the building is dead and returns relevant boolean
        public override bool Death()
        {
            bool isDead = false;

            if(this.Health > 0)
            {
                isDead = false;
            }
            else
            {
                isDead = true;
            }
            return isDead;
        }

        //Method that returns a formatted string of all the stats of the building
        public override string ToString()
        {
            string output = "\n" + "_______________________________________" + "\n" + "This unit is a ResourceBuilding of type: " + type + "\n" + "This Building's x Position is: " + (this.XPos + 1) + "\n" + "This Building's y Position is: " + (this.YPos + 1) + "\n" + "This Building's Health is: " + this.Health + "\n" + "This Building's Production is: " + this.resourcesPerRound + "\n" + "This Building has generated " + this.generatedResources + " " + type + "\n" + "This Building's Team is: Team " + this.Faction + "\n" + "This Building has a Remaining Resource Pool of: " + resourcePoolRemaining;


            return output;
        }

        //method that generates resources if the building still has anu left to generate
        public void GenerateResources()
        {
            //check to make sure there are still resources left
            if(resourcePoolRemaining != 0)
            {
                //checks if the pool will still have resources left after generating resources
                if (resourcePoolRemaining - resourcesPerRound < 0)
                {
                    //adds the pool if it is less than zero and makes pool equal zero
                    generatedResources += resourcePoolRemaining;
                    resourcePoolRemaining = 0;                  
                }
                else
                {
                    //adds the per round production if it is more than zero and subtracts the amount from the pool
                    generatedResources += resourcesPerRound;
                    resourcePoolRemaining -= resourcesPerRound;
                }
            }
        }

        //method that saves the building into the file
        public override void save()
        {
            FileStream fs = new FileStream("saves/buildings/saves.game", FileMode.Append, FileAccess.Write);

            StreamWriter sw = new StreamWriter(fs);
            sw.WriteLine(saveFormatter());

            sw.Close();
            fs.Close();
        }

        //Method that generates and returns a formatted string for saving purposes
        private string saveFormatter()
        {
            string output = "";

            output = XPos + "," + YPos + "," + Health + "," + type + "," + Symbol + "," + Faction + "," + MaxHealth + "," + resourcesPerRound + "," + resourcePoolRemaining + "," + generatedResources + "," + maxPool;

            return output;
        }
    }
}
