using System.Collections.Generic;
using Assets.Source.Core;

namespace DungeonCrawl.Actors.Characters
{
    public class Equipment
    {
        public List<Item> Items { get; set; } = new List<Item>();

        public bool IsEquipmentOnScreen;

        public void AddItem(Item item)
        {
            Items.Add(item);
        }

        public void ShowEquipment()
        {
            IsEquipmentOnScreen = true;
            UserInterface.Singleton.ShowEquipment(Items);
        }
        public void HideEquipment()
        {
            IsEquipmentOnScreen = false;
            UserInterface.Singleton.HideEquipment();
        }
    }
    
}