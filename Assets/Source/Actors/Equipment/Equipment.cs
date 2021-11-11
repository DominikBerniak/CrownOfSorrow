using System.Collections.Generic;
using Assets.Source.Core;
using UnityEngine;

namespace DungeonCrawl.Actors.Characters
{
    public class Equipment
    {
        public List<Item> Items { get; set; } = new List<Item>();

        public Item EquippedWeapon { get; set; }
        public Item EquippedArmor { get; set; }
        
        public bool IsEquipmentOnScreen;

        public void AddItem(Item item)
        {
            Items.Add(item);
        }

        public void RemoveItem(Item item)
        {
            Items.Remove(item);
        }

        public void ShowEquipment()
        {
            IsEquipmentOnScreen = true;
            UserInterface.Singleton.ShowEquipment(this);
            
        }
        public void HideEquipment()
        {
            IsEquipmentOnScreen = false;
            UserInterface.Singleton.HideEquipment();
        }
    }
    
}