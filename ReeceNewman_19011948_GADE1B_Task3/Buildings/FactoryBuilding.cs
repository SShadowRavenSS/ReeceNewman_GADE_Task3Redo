using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace Buildings
{
    public class FactoryBuilding : Building
    {
        private int productionSpeed, spawnX, spawnY;
        private string unitType;
        Random rng = new Random();

        public int XPos { get => base.xPos; set => base.xPos = value; }
        public int YPos { get => base.yPos; set => base.yPos = value; }
        public int Health { get => base.health; set => base.health = value; }
        public int MaxHealth { get => base.maxHealth; }
        public int Faction { get => base.faction; }
        public char Symbol { get => base.symbol; }

        //Constructor for factorybuilding class
        public FactoryBuilding(int xPos, int yPos, int health, int faction, char symbol, string unitType, bool bottomMap) : base(xPos, yPos, health, faction, symbol)
        {
            //Checks if the passed in bool is true or false and then sets the spawn points based on if thefactory building is at the bottom of the map or not
            if(bottomMap == false)
            {
                spawnX = xPos;
                spawnY = yPos + 1;
            }
            else
            {
                spawnX = xPos;
                spawnY = yPos - 1;
            }

            this.unitType = unitType;
            this.productionSpeed = 5;
        }

        //Overloaded constructor forfactory building for if building is being loaded from memory
        public FactoryBuilding(int xPos, int yPos, int health, int faction, char symbol, string unitType, bool bottomMap, int maxHp) : base(xPos, yPos, health, faction, symbol)
        {
            //Checks if the passed in bool is true or false and then sets the spawn points based on if thefactory building is at the bottom of the map or not
            if (bottomMap == false)
            {
                spawnX = xPos;
                spawnY = yPos + 1;
            }
            else
            {
                spawnX = xPos;
                spawnY = yPos - 1;
            }

            this.maxHealth = maxHp;
            this.unitType = unitType;
            this.productionSpeed = 5;
        }
        //accessor for productionspeed
        public int ProductionSpeed { get => productionSpeed; }

        //Checks if the building is dead and returns relevant boolean
        public override bool Death()
        {
            bool isDead = false;

            if (this.Health > 0)
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
            string output = "\n" + "_______________________________________" + "\n" + "This unit is a FactoryBuilding of type: " + unitType + "\n" + "This Building's x Position is: " + (this.XPos + 1) + "\n" + "This Building's y Position is: " + (this.YPos + 1) + "\n" + "This Building's Health is: " + this.Health + "\n" + "This Building's Production Speed is: " + this.ProductionSpeed + "\n" + "This Building's Team is: Team " + this.Faction;


            return output;
        }

        //Generates a unit in the same team as this building
        public Units.Unit SpawnUnits()
        {
            //checks this buildings faction
            int team = this.faction;

            //checks if the building is producing ranged or melee units 
            if(this.unitType == "RangedUnit")
            {
                //based on the team it is in, it creates and returns the unit
                if(team == 0)
                {
                    Units.Unit createdUnit = new Units.RangedUnit(spawnX, spawnY, 100, 2, 3, 'R', 10, team, 100, "Archer");
                    return createdUnit;
                }
                else
                {
                    Units.Unit createdUnit = new Units.RangedUnit(spawnX, spawnY, 100, 2, 3, 'r', 10, team, 100, "Archer");
                    return createdUnit;
                }
                
            }
            else 
            {
                //based on the team it is in, it creates and returns the unit
                if (team == 0)
                {
                    Units.Unit createdUnit = new Units.MeleeUnit(spawnX, spawnY, 120, 1, 1, 'M', 20, team, 120, "Knight");
                    return createdUnit;
                }
                else
                {
                    Units.Unit createdUnit = new Units.MeleeUnit(spawnX, spawnY, 120, 1, 1, 'm', 20, team, 120, "Knight");
                    return createdUnit;
                }
            }

            
        }
        //saves the building in the file
        public override void save()
        {
            FileStream fs = new FileStream("saves/buildings/saves.game", FileMode.Append, FileAccess.Write);

            StreamWriter sw = new StreamWriter(fs);
            sw.WriteLine(saveFormatter());

            sw.Close();
            fs.Close();
        }
        //formats the string for saving 
        private string saveFormatter()
        {
            string output = "";

            output = XPos + "," + YPos + "," + Health + "," + unitType + "," + Symbol + "," + Faction + "," + MaxHealth;

            return output;
        }
    }
}
