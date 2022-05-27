using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Item
{
    public enum ItemType
    {
        FRAGMENT1,
        FRAGMENT2,
        FRAGMENT3,
        FRAGMENT4,
        FRAGMENT5,
        FRAGMENT6
    }

    public ItemType itemType;

    public string value;

    public bool isDragged = false;

    public Sprite GetSprite()
    {
        switch (itemType)
        {
            default:
            case ItemType.FRAGMENT1: return ItemAssets.Instance.fragment1;
            case ItemType.FRAGMENT2: return ItemAssets.Instance.fragment2;
            case ItemType.FRAGMENT3: return ItemAssets.Instance.fragment3;
            case ItemType.FRAGMENT4: return ItemAssets.Instance.fragment4;
            case ItemType.FRAGMENT5: return ItemAssets.Instance.fragment5;
            case ItemType.FRAGMENT6: return ItemAssets.Instance.fragment6;
        }
    }

}
