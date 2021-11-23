using UnityEngine;

namespace DungeonCrawl.Actors.Characters
{
    public class RandomNameGenerator : MonoBehaviour
    {
        public static RandomNameGenerator Singleton { get; private set; }
        
        private void Awake()
        {
            if (Singleton != null)
            {
                Destroy(this);
                return;
            }
            Singleton = this;
        }
        
        private string[] _adjectives = {"", "", "Rusty", "Broken", "Amazing", "Powerful", "Great", 
            "Pristine", "Durable", "Worn", "Damaged", "Fragile", "Used"};

        private string[] _nouns = {"", "", "Power", "Might", "Luck", "Wisdom", "Strength", 
            "Dexterity", "Death", "Sorrow", "Protection", "Eternity", "Doom", "Delusion", "Divinity", "Sight", "Knowledge"};

        public string GenerateName(string typeName)
        {
            string randomAdjective = _adjectives[Utilities.Random.Next(_adjectives.Length)];
            string randomNoun = _nouns[Utilities.Random.Next(_nouns.Length)];
            randomNoun = randomNoun != "" ? "of " + randomNoun : "";
            return $"{randomAdjective} {typeName} {randomNoun}";
        }
    }
}