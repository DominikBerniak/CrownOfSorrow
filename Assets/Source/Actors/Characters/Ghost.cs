using Assets.Source.Core;
using UnityEngine;
using DungeonCrawl.Core;
using System;
using System.Collections.Generic;
using DungeonCrawl.Actors.Static;

namespace DungeonCrawl.Actors.Characters
{
    public class Ghost : Character
    {
        private float DelayCounter { get; set; }
        
        private List<string> Names = new List<string>()
        {
            "Ghostly Ghost", "Scary Ghost", "Simply a Ghost", "Ghoooooooost", "Spooky Ghost"
        };

        public Ghost()
        {
            Level.Number = 1;
            Name = Names[Utilities.Random.Next(Names.Count)];
            MaxHealth = Utilities.Random.Next(10,51);
            CurrentHealth = MaxHealth;
            AttackDmg = Utilities.Random.Next(5, 16);
            Armor = Utilities.Random.Next(11);
        }

        public override bool OnCollision(Actor anotherActor)
        {
            if (anotherActor is Player)
            {
                Player player = (Player) anotherActor;
                UserInterface.Singleton.ShowFightScreen(player, this);
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
            if (x_distance == 0)
            {
                y_step = y_distance / Math.Abs(y_distance);
                return (ghostX, ghostY - y_step);
            }
            if (y_distance == 0)
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
            var targetPosition = NextMove();
            var actorAtTargetPosition = ActorManager.Singleton.GetActorAt(targetPosition);
            if (actorAtTargetPosition == null || actorAtTargetPosition is Wall)
            {
                Position = targetPosition;
            }
            else if (actorAtTargetPosition.OnCollision(this))
            {
                Position = targetPosition;
            }
        }


        protected override void OnUpdate(float deltaTime)
        {
            if (PauseControl.Singleton.IsGamePaused)
            {
                return;
            }
            DelayCounter += deltaTime;
            if (DelayCounter >= 1)
            {
                DelayCounter = 0;
                ChangeGhostPosition();
            }

        }

        public override int DefaultSpriteId { get; set; } = 313;
        public override string DefaultName => "Ghost";


    }
}










