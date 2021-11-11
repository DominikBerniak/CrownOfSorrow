using Assets.Source.Core;
using UnityEngine;
using System.Linq;
using DungeonCrawl.Core;
using System;
using System.Collections.Generic;

namespace DungeonCrawl.Actors.Characters
{
    public class Skeleton : Character
    {
        private float DelayCounter { get; set; }

        public Skeleton()
        {
            Health = Utilities.Random.Next(5, 51);
            AttackDmg = 5;
        }
       public override bool OnCollision(Actor anotherActor)
        {
            if (anotherActor is Player)
            {
                Player player = (Player) anotherActor;
                UserInterface.Singleton.ShowFightScreen("Skeleton");
                ApplyDamage(player.AttackDmg);
                return Health <= 0;
            }

            return false;
        }

       protected override void OnDeath()
        {
            Debug.Log("Well, I was already dead anyway...");
        }

        public (int, int) checkMoveOptions()
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
                if (actorAtTargetPosition is Actors.Characters.Player)
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
            (int, int) nextMove = newCords[UnityEngine.Random.Range(0, newCords.Count)];
            return nextMove;
        }

        public void MoveSkeleton()
        {
            Position = checkMoveOptions();
        }


        protected override void OnUpdate(float deltatime)
        {
            DelayCounter += deltatime;
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











 