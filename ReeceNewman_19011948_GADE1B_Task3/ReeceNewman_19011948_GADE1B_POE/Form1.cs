using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

namespace Units
{
    public partial class frmBattleSim : Form
    {
        
        GameEngine gameEngine;

        public frmBattleSim()
        {
            InitializeComponent();
        }

        private void frmBattleSim_Load(object sender, EventArgs e)
        {

            gameEngine = new GameEngine(0,20,20,6); //Create a new instance of the game engine class
            lblMap.Text = gameEngine.Map.convertMap(); //Updates the map
            
            rtxUnitInfo.Text = gameEngine.getStats(gameEngine.Map.Units,gameEngine.Map.Buildings); //Updates the unit info
            
        }

        

        private void btnStart_Click(object sender, EventArgs e)
        {
            tmrOnly.Enabled = true; //Enables timer
            tmrOnly.Start(); //Starts timer
        }

        private void playGame(object sender, EventArgs e)
        {
            gameEngine.gameLogic(gameEngine.Map.Units, gameEngine.Map.Buildings); //Calls the game logic method from the game engine
            lblMap.Text = gameEngine.Map.convertMap(); //Updates map display on the form
            lblTimer.Text = Convert.ToString(Convert.ToInt32(lblTimer.Text) + 1); //updates the timer on the form
            rtxUnitInfo.Text = gameEngine.getStats(gameEngine.Map.Units, gameEngine.Map.Buildings); //updates units statson the form
        }

        private void btnPause_Click(object sender, EventArgs e)
        {
            tmrOnly.Stop(); //Pauses the timer
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
           //Creates all the nessasary directories
            Directory.CreateDirectory("saves");
            Directory.CreateDirectory("saves/units");
            Directory.CreateDirectory("saves/buildings");


            FileStream fs = new FileStream("saves/units/saves.game", FileMode.Create, FileAccess.Write); //opens a new filestream and creates and overrides file if it does not exist 
            fs.Close(); // closes filestream
            FileStream fs1 = new FileStream("saves/buildings/saves.game", FileMode.Create, FileAccess.Write); //opens a new filestream and creates and overrides file if it does not exist 
            fs1.Close(); // closes filestream

            //Loop runs through all the units and calls their save method
            for (int i = 0; i < gameEngine.Map.units.Length; i++)
            {
                gameEngine.Map.units[i].save();
            }

            //Loop runs through all the buildings and calls their save methods
            for (int k = 0; k < gameEngine.Map.Buildings.Length; k++)
            {
                gameEngine.Map.Buildings[k].save();
            }
        }

        private void btnRead_Click(object sender, EventArgs e)
        {
            gameEngine.Map.read(); //Calls the map read method to load a previous save
            tmrOnly.Enabled = true; //Enables the timer
            tmrOnly.Start(); //Starts the timer
        }
    }
}
