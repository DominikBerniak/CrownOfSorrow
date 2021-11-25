using System;
using DungeonCrawl.Actors.Experience;
using DungeonCrawl.Core;

namespace DungeonCrawl.Actors.Characters
{
    public abstract class Character : Actor
    {
        public string Name { get; set; }
        public int MaxHealth { get; set; }
        private int _currentHealth;
        public int CurrentHealth
        {
            get { return _currentHealth; }

            set
            {
                if(value <= 0)
                {
                    _currentHealth = 0;
                }
                else
                {
                    _currentHealth = value;
                }
            }
        }
        public int AttackDmg { get; set; }
        public int Armor { get; set; }

        public Level Level { get; set; } = new Level();

        public Equipment Equipment = new Equipment();

        public void ApplyDamage(int damage)
        {
            int calculatedDmg = 2 * ((int)Math.Pow(damage, 2)) / (damage + Armor);
            CurrentHealth -= (calculatedDmg > damage? damage : calculatedDmg);
            if (this is Player)
            {
                AudioManager.Singleton.PlayPlayerHitSound();
            }
            else
            {
                AudioManager.Singleton.PlayMonsterHitSound();
            }
            if (CurrentHealth <= 0)
            {
                OnDeath();
                ActorManager.Singleton.DestroyActor(this);
            }
        }
        public void DropItem()
        {
            switch (Equipment.GetRandomItemType())
            {
                case Equipment.ItemTypes.Helmet:
                    ActorManager.Singleton.Spawn<Helmet>(Position);
                    break;
                case Equipment.ItemTypes.Chest:
                    ActorManager.Singleton.Spawn<ChestArmor>(Position);
                    break;
                case Equipment.ItemTypes.Gloves:
                    ActorManager.Singleton.Spawn<Gloves>(Position);
                    break;
                case Equipment.ItemTypes.Boots:
                    ActorManager.Singleton.Spawn<Boots>(Position);
                    break;
                case Equipment.ItemTypes.Weapon:
                    ActorManager.Singleton.Spawn<Weapon>(Position);
                    break;
                case Equipment.ItemTypes.Shield:
                    ActorManager.Singleton.Spawn<Shield>(Position);
                    break;
                case Equipment.ItemTypes.Consumable:
                    ActorManager.Singleton.Spawn<Consumable>(Position);
                    break;
            }
        }

        protected abstract void OnDeath();

        /// <summary>
        ///     All characters are drawn "above" floor etc
        /// </summary>
        public override int Z => -1;
    }
}
