using Assets.Source.Core;
using UnityEngine;
using DungeonCrawl.Core;
using System.Collections.Generic;
using DungeonCrawl.Actors.Experience;
using Random = UnityEngine.Random;

namespace DungeonCrawl.Actors.Characters
{
    public class Skeleton : Character
    {
        private float DelayCounter { get; set; }
        
        private List<string> Names = new List<string>()
        {
            "Scary Spooky Skeleton", "Crazy Bones", "Skelly", "Terrifying Skeleton", "Jack Skellington", "T-Bone"
        };

        public Skeleton()
        {
            Level.Number = 0;
            Experience.ExperiencePoints = 0;
            Name = Names[Utilities.Random.Next(Names.Count)];
            MaxHealth = Utilities.Random.Next(10,51);
            CurrentHealth = MaxHealth;
            AttackDmg = Utilities.Random.Next(5, 16);
            Armor = Utilities.Random.Next(11);
        }
       public override bool OnCollision(Actor anotherActor)
        {
            if (anotherActor is Player player)
            {
                if (player.IsChristmasTreeEquipped())
                {
                    return true;
                }
                Level.IfLevelUp(this.Experience,player);
                Level.GuesWhoAndChangeLevel(player,this);
                this.Experience.SetExperiencePoints(this);              
                UserInterface.Singleton.ShowFightScreen(player, this);
                this.Experience.DropExperience(player,this);
                Level.IfLevelUp(this.Experience,player);
                return CurrentHealth <= 0;
            }
            return false;
        }

       protected override void OnDeath()
        {
            DropItem();
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

        public override int DefaultSpriteId { get; set; } = 316;
        public override string DefaultName => "Skeleton";
    }
}











 