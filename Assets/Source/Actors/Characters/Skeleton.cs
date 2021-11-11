using Assets.Source.Core;
using DungeonCrawl.Actors.Experience;
using UnityEngine;

namespace DungeonCrawl.Actors.Characters
{
    public class Skeleton : Character
    {
        public Skeleton()
        {
            Level.Number = 1;
            Name = "Skelly";
            MaxHealth = Utilities.Random.Next(5, 51);
            AttackDmg = 5;
        }
       public override bool OnCollision(Actor anotherActor)
        {
            if (anotherActor is Player)
            {
                Player player = (Player) anotherActor;
                UserInterface.Singleton.ShowFightScreen(player, this);
                return MaxHealth <= 0;
            }

            return false;
        }

       protected override void OnDeath()
        {
            Debug.Log("Well, I was already dead anyway...");
        }

        public override int DefaultSpriteId => 316;
        public override string DefaultName => "Skeleton";
        
    }
}
