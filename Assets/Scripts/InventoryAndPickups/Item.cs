using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Scriptable object/Item")]
public class Item : ScriptableObject
{
    public Sprite itemIcon;
    public ItemType type;

    public enum ItemType
    {
        tempItem1
    }
}
