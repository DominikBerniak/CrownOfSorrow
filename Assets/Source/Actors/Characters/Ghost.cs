using Assets.Source.Core;
using UnityEngine;
using System.Linq;
using DungeonCrawl.Core;
using System;
using System.Collections.Generic;

namespace DungeonCrawl.Actors.Characters
{
    

    public class Ghost : Character
    {
        private float DelayCounter { get; set; }

        public Ghost()
        {
            CurrentHealth = Utilities.Random.Next(5, 51);
            AttackDmg = 5;
        }

        public override bool OnCollision(Actor anotherActor)
        {
            if (anotherActor is Player)
            {
                Player player = (Player)anotherActor;
                // UserInterface.Singleton.ShowFightScreen("Ghost");
                ApplyDamage(player.AttackDmg);
                return CurrentHealth <= 0;
            }
            return false;
        }

        protected override void OnDeath()
        {
            Debug.Log("I will hunt You down forever.....");
        }

        public void MoveGhost()
        {

            
        }

        protected override void OnUpdate(float deltatime)
        {
            DelayCounter += deltatime;
            if (DelayCounter >= 1)
            {
                DelayCounter = 0;
                MoveGhost();
            }

        }

        public override int DefaultSpriteId => 00000000000; //Przypisanie Id duchowi;
        public override string DefaultName => "Ghost";


    }
}










