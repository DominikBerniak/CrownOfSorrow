using DungeonCrawl.Actors.Experience;
using DungeonCrawl.Core;

namespace DungeonCrawl.Actors.Characters
{
    public abstract class Character : Actor
    {
        public string Name { get; set; }
        public int MaxHealth { get; set; }
        public int _currentHealth;
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
            CurrentHealth -= damage;

            if (CurrentHealth <= 0)
            {
                // Die
                OnDeath();

                ActorManager.Singleton.DestroyActor(this);
            }
        }

        protected abstract void OnDeath();

        /// <summary>
        ///     All characters are drawn "above" floor etc
        /// </summary>
        public override int Z => -1;
    }
}
