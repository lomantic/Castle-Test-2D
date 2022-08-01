using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;

[CreateAssetMenu(fileName = "New Inventory", menuName = "Inventory System/Inventory")]
public class InventoryObject : ScriptableObject//, ISerializationCallbackReceiver
{
  public string savePath;
  private readonly string encryptionKey = "LittlePassword";
  public ItemDatabaseObject database { get; private set; }
  [SerializeField] InventoryUIStateSystem InventoryManger;
  public Inventory Container;
  private Inventory newContainer;
  AsyncOperationHandle inventoryHandle;

  private void OnEnable()
  {
    Addressables.LoadAssetAsync<ItemDatabaseObject>("Inventory Database").Completed +=
    (AsyncOperationHandle<ItemDatabaseObject> Obj) =>
    {
      inventoryHandle = Obj;
      database = Obj.Result;
    };
  }
  private void OnDisable()
  {
    Addressables.Release(inventoryHandle);
  }
  public void AddItem(Item _item, int _amount)
  {
    if (_item.buffs.Length > 0)
    {
      SetEmptySlot(_item, _amount);
      InventoryManger.StoreNewItem();
      return;
    }

    for (int i = 0; i < Container.Items.Length; i++)
    {
      if (Container.Items[i].Id == _item.Id)
      {
        Container.Items[i].AddAmount(_amount);
        InventoryManger.StoreNewItem();
        return;
      }
    }
    SetEmptySlot(_item, _amount);
    InventoryManger.StoreNewItem();
  }

  public InventorySlot SetEmptySlot(Item _item, int _amount)
  {
    for (int i = 0; i < Container.Items.Length; i++)
    {
      if (Container.Items[i].Id <= -1)
      {
        Container.Items[i].UpdateSlot(_item.Id, _item, _amount);
        return Container.Items[i];
      }
    }
    //what to do when Full inventory
    return null;
  }

  public void MoveItem(InventorySlot item1, InventorySlot item2)
  {
    InventorySlot temp = new InventorySlot(item2.Id, item2.item, item2.amount);
    item2.UpdateSlot(item1.Id, item1.item, item1.amount);
    item1.UpdateSlot(temp.Id, temp.item, temp.amount);
  }

  public void RemoveItem(Item _item)
  {
    for (int i = 0; i < Container.Items.Length; i++)
    {
      if (Container.Items[i].item == _item)
      {
        Container.Items[i].UpdateSlot(-1, null, 0);
      }
    }
  }

  [ContextMenu("Save")]
  public void Save()
  {
    string saveData = JsonUtility.ToJson(Container, true);
    saveData = EncryptDecrypt(saveData);
    BinaryFormatter bf = new BinaryFormatter();
    FileStream file = File.Create(string.Concat(Application.persistentDataPath, savePath));
    bf.Serialize(file, saveData);
    file.Close();
  }
  [ContextMenu("Load")]
  public void Load()
  {
    if (File.Exists(string.Concat(Application.persistentDataPath, savePath)))
    {
      BinaryFormatter bf = new BinaryFormatter();
      FileStream file = File.Open(string.Concat(Application.persistentDataPath, savePath), FileMode.Open);
      JsonUtility.FromJsonOverwrite(EncryptDecrypt(bf.Deserialize(file).ToString()), newContainer);
      for (int i = 0; i < newContainer.Items.Length; i++)
      {
        Container.Items[i].UpdateSlot(newContainer.Items[i].Id, newContainer.Items[i].item, newContainer.Items[i].amount);
      }
      file.Close();
      InventoryManger.StoreNewItem();
    }
  }
  [ContextMenu("Clear")]
  public void Clear()
  {
    Container.Clear();
    InventoryManger.StoreNewItem();
  }


  // public void OnAfterDeserialize()
  // {
  //   for (int i = 0; i < Container.Items.Count; i++)
  //   {
  //     Container.Items[i].item = database.GetItem[Container.Items[i].Id];
  //   }
  // }

  // public void OnBeforeSerialize()
  // {
  // }
  private string EncryptDecrypt(string data)
  {
    string modifiedData = "";
    for (int i = 0; i < data.Length; i++)
    {
      modifiedData += (char)(data[i] ^ encryptionKey[i % encryptionKey.Length]);
    }
    return modifiedData;
  }
}
[System.Serializable]
public class Inventory
{
  public InventorySlot[] Items = new InventorySlot[25];
  public void Clear()
  {
    for (int i = 0; i < Items.Length; i++)
    {
      Items[i].UpdateSlot(-1, new Item(), 0);
    }
  }
}

[System.Serializable]
public class InventorySlot
{
  public ItemType[] AllowedItems = new ItemType[0];
  public UserInterface parent;
  public Item item;
  public int Id;
  public int amount;
  public InventorySlot()
  {
    Id = -1;
    item = new Item();
    amount = 0;
  }
  public InventorySlot(int _id, Item _item, int _amount)
  {
    Id = _id;
    item = _item;
    amount = _amount;
  }
  public void UpdateSlot(int _id, Item _item, int _amount)
  {
    Id = _id;
    item = _item;
    amount = _amount;
  }
  public void AddAmount(int value)
  {
    amount += value;
  }

  public bool CanPlaceInSlot(ItemObject _item)
  {
    if (AllowedItems.Length <= 0)
    {
      return true;
    }
    for (int i = 0; i < AllowedItems.Length; i++)
    {
      if (_item.type == AllowedItems[i])
      {
        return true;
      }
    }
    return false;
  }
}