using Assets.Source.Core;
using UnityEngine;
using DungeonCrawl.Core;
using System.Collections.Generic;

namespace DungeonCrawl.Actors.Characters
{
    public class Skeleton : Character
    {
        private float DelayCounter { get; set; }

        public Skeleton()
        {
            Level.Number = 1;
            Name = "Skelly";
            //CurrentHealth = Utilities.Random.Next(5, 51);
            MaxHealth = 10;
            CurrentHealth = MaxHealth;
            AttackDmg = 5;
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
            Debug.Log("Well, I was already dead anyway...");
        }

        public (int, int) GetNewPosition()
        {
            (int, int)[] tab = new (int, int)[4];
            List<(int, int)> newCords = new List<(int, int)>();
            int amountOfDirections = 4;
            int CordX = Position.x;
            int CordY = Position.y;

            tab[0] = (CordX - 1, CordY);
            tab[1] = (CordX + 1, CordY);
            tab[2] = (CordX, CordY - 1);
            tab[3] = (CordX, CordY + 1);


            for (int z = 0; z < amountOfDirections; z++)
            {

                var actorAtTargetPosition = ActorManager.Singleton.GetActorAt(tab[z]);
                if (actorAtTargetPosition is Player)
                {
                    return (tab[z].Item1, tab[z].Item2); 
                }
                if (actorAtTargetPosition == null)
                {
                    newCords.Add(tab[z]);
                }
            }
            if (newCords.Count == 0)
            {
                return (Position.x, Position.y);
            }
            (int, int) nextMove = newCords[Random.Range(0, newCords.Count)];
            return nextMove;
        }

        public void MoveSkeleton()
        {
            var targetPosition = GetNewPosition();
            var actorAtTargetPosition = ActorManager.Singleton.GetActorAt(targetPosition);
            if (actorAtTargetPosition == null)
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
                MoveSkeleton();
            }

        }
        public override int DefaultSpriteId => 316;
        public override string DefaultName => "Skeleton";
    }


}











 