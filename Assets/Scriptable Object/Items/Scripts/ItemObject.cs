using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ItemType
{
  Food, Hat, Clothes, Shoes, Weapon, Sheild, Default
}
public enum Attributes
{
  STR, INT, DEX, LUK
}
public abstract class ItemObject : ScriptableObject
{
  public int Id;
  public Sprite uiDisplay;
  public ItemType type;
  [TextArea(15, 20)]
  public string description;
  public ItemBuff[] buffs;

  // Not necessary but for convienience of creating Item
  public Item CreateItem()
  {
    Item newItem = new Item(this);
    return newItem;
  }
}

[System.Serializable]
public class Item
{
  public string Name;
  public int Id;
  public ItemBuff[] buffs;
  public Item()
  {
    Name = "";
    Id = -1;
    buffs = null;
  }
  public Item(ItemObject item)
  {
    Name = item.name;
    Id = item.Id;
    //item.buffs 로 하면 안되는 이유 array는 포인터를 저장함 해당 문구로 작성시 ItemObj의 buffs를 가리키게 됨 
    // 즉 편집 저장에 문제가 발생 그러니 Item class의 buff를 지칭하게 하기 위해서는 새로운 ItemBuff array를 만들어야함 
    buffs = new ItemBuff[item.buffs.Length];
    for (int i = 0; i < buffs.Length; i++)
    {
      buffs[i] = new ItemBuff(item.buffs[i].min, item.buffs[i].max)
      {
        attribute = item.buffs[i].attribute
      };
    }
  }
}
[System.Serializable]
public class ItemBuff
{
  public Attributes attribute;
  public int value;
  public int min;
  public int max;
  public ItemBuff(int _min, int _max)
  {
    min = _min;
    max = _max;
    GenerateValue();
  }
  public void GenerateValue()
  {
    value = UnityEngine.Random.Range(min, max);
  }
}