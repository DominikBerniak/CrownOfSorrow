using System.Security.Cryptography;

namespace DungeonCrawl.Actors.Characters
{
    public abstract class Item
    {
        public abstract void UseItem(Player player, int utility);

    }
}