using Assets.Source.Core;
using DungeonCrawl.Actors.Characters;
using Source.Core;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class EquipmentItemSlot : MonoBehaviour
{
    public Image itemImage;
    public GameObject ItemInfoUi;
    Item _item;

    public void AddItem(Item newItem)
    {
        _item = newItem;
        itemImage.sprite = _item.GetSprite();
        itemImage.enabled = true;
    }

    public void ClearSlot()
    {
        _item = null;
        itemImage.sprite = null;
        itemImage.enabled = false;
        HideItemInfo();
    }

    public void UseItem()
    {
        if (_item != null)
        {
            _item.UseItem();
            ClearSlot();
            if (Fight.Singleton.isFighting)
            {
                Fight.Singleton.isUsingItem = false;
                Fight.Singleton.isAfterAttack = true;
                UserInterface.Singleton.HideUseItemUi();
                return;
            }
            UserInterface.Singleton.UpdateEquipment();    

        }
    }

    public void ShowItemInfo()
    {
        if (_item != null)
        {
            ItemInfoUi.SetActive(true);
            var itemInfoImage = ItemInfoUi.transform.Find("ItemSlot/ItemImage").GetComponent<Image>();
            itemInfoImage.sprite = _item.GetSprite();
            itemInfoImage.enabled = true;
            ItemInfoUi.transform.Find("ItemName").GetComponent<TextMeshProUGUI>().text = _item.Name;
            ItemInfoUi.transform.Find("ItemStats").GetComponent<TextMeshProUGUI>().text = $"{_item.StatName} : {_item.StatPower}";
        }
    }

    public void HideItemInfo()
    {
        ItemInfoUi.SetActive(false);
    }
}
