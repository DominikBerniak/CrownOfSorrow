using Assets.Source.Core;
using UnityEngine;
using DungeonCrawl.Core;
using DungeonCrawl.Actors.Experience;
using System.Collections.Generic;
using Random = UnityEngine.Random;

namespace DungeonCrawl.Actors.Characters
{
    public class Healer : Character
    {
        private float DelayCounter { get; set; }

        public Healer()
        {
        }
        public override bool OnCollision(Actor anotherActor)
        {
            if (anotherActor is Player)
            {
                Player player = (Player)anotherActor;
                player.CurrentHealth = player.MaxHealth;
                UserInterface.Singleton.ShowFightScreen(player, this);
            }
            return false;
        }
        protected override void OnDeath() { }

        public void ExchangeExp(Player player)
        {
            if(player.Experience.ExperiencePoints >= 10)
            {
                if(player.CurrentHealth < player.MaxHealth)
                {
                     player.Experience.ExperiencePoints -= 10;
                     player.CurrentHealth += 20;
                }   
                else
                {
                    Debug.Log("Your wounds are healed...Come back later...")
                }
            }
            else
            {
                Debug.Log("Sorry, You have nothing to exchange....")
            }
                
        }



        public override int DefaultSpriteId { get; set; } = 78;
        public override string DefaultName => "Healer";
    }
}











