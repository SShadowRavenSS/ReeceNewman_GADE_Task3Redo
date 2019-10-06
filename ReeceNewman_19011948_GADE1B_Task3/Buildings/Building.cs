﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Buildings
{
    abstract public class Building
    {
        //Variable declarations
        protected int faction;
        protected char symbol;
        protected int xPos;
        protected int yPos;
        protected int health;
        protected int maxHealth;

        public Building(int xPos, int yPos, int health, int faction, char symbol) //Constructor that takes in parameters andassigns them to the class variables
        {
            this.xPos = xPos;
            this.yPos = yPos;
            this.health = health;
            this.maxHealth = health;
            this.faction = faction;
            this.symbol = symbol;
        }

        //Provide abstract method defenitions
        abstract public void save();
        abstract public bool Death();
        abstract public override string ToString();

    }
}
