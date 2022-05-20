using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Item
{
    public int id;
    public enum ItemType
    {
        Common,
        Rare,
        Epic,
        Legends,
    }

    public ItemType itemType;

    public int amount;

    public Sprite GetSprite()
    {
        switch (itemType)
        {
            default:
            case ItemType.Common: return ItemAssets.Instance.common;
            case ItemType.Rare: return ItemAssets.Instance.rare;
            case ItemType.Epic: return ItemAssets.Instance.epic;
        }
    }

}
