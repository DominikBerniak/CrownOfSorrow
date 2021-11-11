using Assets.Source.Core;
using UnityEngine;
using DungeonCrawl.Core;
using System;

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
                //UserInterface.Singleton.ShowFightScreen("Ghost");
                return CurrentHealth <= 0;
            }
            return false;
        }

        protected override void OnDeath()
        {
            Debug.Log("I will hunt You down forever.....");
        }

        public (int, int) FindDirection(int ghostX, int ghostY, (int, int) playerLocation)
        {
            int x_step;
            int y_step;
            int x_distance = ghostX - playerLocation.Item1;
            int y_distance = ghostY - playerLocation.Item2;

            if (x_distance == 0 && y_distance == 0)
            {
                return (ghostX, ghostY);
            }
            else if (x_distance == 0)
            {
                y_step = y_distance / Math.Abs(y_distance);
                return (ghostX, ghostY - y_step);
            }
            else if (y_distance == 0)
            {
                x_step = x_distance / Math.Abs(x_distance);
                return (ghostX - x_step, ghostY);
            }

            x_step = x_distance / Math.Abs(x_distance);
            y_step = y_distance / Math.Abs(y_distance);

            return (ghostX - x_step, ghostY - y_step);
        }

        public (int,int) NextMove()
        {
            int ghostX = Position.x;
            int ghostY = Position.y;
            int ghostActivationRadius = 8;
 
            
            var playerLocation = GameObject.Find("Player").GetComponent<Player>().Position;
            if (Math.Abs(ghostX - playerLocation.x) <= ghostActivationRadius && Math.Abs(ghostY - playerLocation.y) <= ghostActivationRadius)
            {
                return FindDirection(ghostX,ghostY,playerLocation);
            }
            return (ghostX, ghostY);
        }
        
        public void ChangeGhostPosition()
        {
            Position = NextMove();
        }


        protected override void OnUpdate(float deltatime)
        {
            DelayCounter += deltatime;
            if (DelayCounter >= 1)
            {
                DelayCounter = 0;
                ChangeGhostPosition();
            }

        }

        public override int DefaultSpriteId => 313;
        public override string DefaultName => "Ghost";


    }
}










