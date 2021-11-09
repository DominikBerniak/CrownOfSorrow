using Assets.Source.Core;
using UnityEngine;

namespace DungeonCrawl.Actors.Characters
{
    public class Skeleton : Character
    {
        public Skeleton()
        {
            Health = Utilities.Random.Next(5, 51);
            AttackDmg = 5;
        }
       public override bool OnCollision(Actor anotherActor)
        {
            if (anotherActor is Player)
            {
                UserInterface.Singleton.ShowFightScreen("Skeleton");
                ApplyDamage(anotherActor.AttackDmg);
                return Health <= 0;
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
