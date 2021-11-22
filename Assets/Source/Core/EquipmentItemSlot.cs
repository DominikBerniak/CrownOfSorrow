using Assets.Source.Core;
using DungeonCrawl.Actors.Characters;
using Source.Core;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class EquipmentItemSlot : MonoBehaviour
{
    public Image itemImage;
    public GameObject itemInfoUi;
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
        if (_item == null)
        {
            return;
        }
        _item.UseItem();
        ClearSlot();
        if (Fight.Singleton.isFighting)
        {
            Fight.Singleton.isUsingItem = false;
            Fight.Singleton.isAfterAttack = true;
            Fight.Singleton.ExitUseItemButton.gameObject.SetActive(false);
            UserInterface.Singleton.HideUseItemUi();
            return;
        }
        UserInterface.Singleton.UpdateEquipment();
    }

    public void ShowItemInfo()
    {
        if (_item != null)
        {
            itemInfoUi.SetActive(true);
            var itemInfoImage = itemInfoUi.transform.Find("ItemSlot/ItemImage").GetComponent<Image>();
            itemInfoImage.sprite = _item.GetSprite();
            itemInfoImage.enabled = true;
            itemInfoUi.transform.Find("ItemName").GetComponent<TextMeshProUGUI>().text = _item.Name;
            itemInfoUi.transform.Find("ItemStats").GetComponent<TextMeshProUGUI>().text = $"{_item.StatName} : {_item.StatPower}";
        }
    }

    public void HideItemInfo()
    {
        itemInfoUi.SetActive(false);
    }
}
