using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace Units
{
    public class WizardUnit : Unit
    {
        public WizardUnit(int xPos, int yPos, int health, int speed, int range, char symbol, int attack, int faction, int maxHealth, string name) : base(xPos, yPos, health, faction, speed, attack, range, symbol, maxHealth, name)
        {

        }
        Random rng = new Random();

        //method that saves the unit into a file
        public override void save()
        {
            FileStream fs = new FileStream("saves/units/saves.game", FileMode.Append, FileAccess.Write);

            StreamWriter sw = new StreamWriter(fs);
            sw.WriteLine(saveFormatter());

            sw.Close();
            fs.Close();
        }

        //Method that generates and returns a formatted string for saving purposes
        private string saveFormatter()
        {
            string output = "";

            output = XPos + "," + YPos + "," + Health + "," + Speed + "," + AttackRange + "," + Symbol + "," + Attack + "," + Faction + "," + MaxHealth + "," + Name;

            return output;
        }

        //Method that moves unit towards passed in unit
        public override void movement(Unit moveToUnit, int mapSizeX, int mapSizeY)
        {
            this.IsAttacking = false; //Since this unit is moving it is not attacking so set relevant boolean false
            double currentHealth = this.health;
            double maximumHealth = this.maxHealth;
            double percentageHealth = currentHealth / maximumHealth;

            if (percentageHealth < 0.5) //Checks if the unit is under 50% health
            {
                Console.WriteLine("My health is below 50% " + health);
                int randomDirection = rng.Next(0, 4); //Randoms a direction for the unit to move to
                switch (randomDirection)
                {

                    case 0:
                        {
                            if (this.yPos - 1 > 0)
                            {

                                this.yPos -= 1; //Moves unit up

                            }
                            break;
                        }
                    case 1:
                        {
                            if (this.yPos + 1 < mapSizeY)
                            {

                                this.YPos += 1; //Moves unit down

                            }
                            break;
                        }
                    case 2:
                        {
                            if (this.xPos - 1 > 0)
                            {

                                this.xPos -= 1; //Moves unit left

                            }
                            break;
                        }
                    case 3:
                        {
                            if (this.xPos + 1 < mapSizeX)
                            {

                                this.xPos += 1; //Moves unit right

                            }
                            break;
                        }

                }

            }
            else
            {
                //determines if the unit is furthar in the y or the x
                if (Math.Abs(moveToUnit.XPos - this.XPos) > Math.Abs(moveToUnit.YPos - this.YPos))
                {
                    //determines left or right movement
                    if (moveToUnit.XPos - this.xPos > 0)
                    {
                        //ensures no out of bounds movement
                        if (this.xPos + 1 < mapSizeX)
                        {

                            this.XPos += 1; //Moves unit right

                        }

                    }
                    else if (moveToUnit.XPos - this.XPos < 0)
                    {

                        //ensures no out of bounds movement
                        if (this.xPos - 1 > 0)
                        {

                            this.xPos -= 1; //Moves unit left

                        }

                    }

                }
                else
                {
                    //determines up or down movement
                    if (moveToUnit.YPos - this.yPos > 0)
                    {

                        //ensures no out of bounds movement
                        if (this.yPos + 1 < mapSizeY)
                        {

                            this.yPos += 1; //Moves unit down

                        }

                    }
                    else if (moveToUnit.YPos - this.yPos < 0)
                    {

                        //ensures no out of bounds movement
                        if (this.yPos - 1 > 0)
                        {

                            this.yPos -= 1; //Moves unit up

                        }

                    }

                }

            }

        }

    }
}
