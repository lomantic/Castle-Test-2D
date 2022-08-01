using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New item Database", menuName = "Inventory System/Items/Database")]
public class ItemDatabaseObject : ScriptableObject, ISerializationCallbackReceiver
{
  public ItemObject[] Items;
  //public Dictionary<ItemObject, int> GetId = new Dictionary<ItemObject, int>();
  public Dictionary<int, ItemObject> GetItem = new Dictionary<int, ItemObject>();

  public void OnAfterDeserialize()
  {
    //GetId = new Dictionary<ItemObject, int>();

    for (int i = 0; i < Items.Length; i++)
    {
      //GetId.Add(Items[i], i);
      Items[i].Id = i;
      GetItem.Add(i, Items[i]);
    }
  }

  public void OnBeforeSerialize()
  {
    GetItem = new Dictionary<int, ItemObject>();
  }
}