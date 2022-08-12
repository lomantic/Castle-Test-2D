using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;
using System.Runtime.Serialization;


public enum InterfaceType
{
  Inventory, Equipment, Chest
}

[CreateAssetMenu(fileName = "New Inventory", menuName = "Inventory System/Inventory")]
public class InventoryObject : ScriptableObject//, ISerializationCallbackReceiver
{
  public string savePath;
  private readonly string encryptionKey = "LittlePassword";
  public ItemDatabaseObject database { get; private set; }
  [SerializeField] InventoryUIStateSystem InventoryManger;
  public Inventory Container;
  public InterfaceType type;
  private Inventory newContainer;
  public InventorySlot[] GetSlots { get { return Container.Slots; } }
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
  public bool AddItem(Item _item, int _amount)
  {
    Debug.Log(string.Concat("너는 얻었다 ", _item.Name, "를 ..// 아이디는 ", _item.Id, " 이다 "));
    InventorySlot slot = FindItemOnInventory(_item);
    if (!DatabaseReader.GetItemById(_item.Id).stackable || slot == null)
    {
      if (EmptySlotCount <= 0)
      {
        return false;
      }
      else
      {
        SetEmptySlot(_item, _amount);
        InventoryManger.InventoryDataChanged();
        Debug.Log("다 완료 함수 종료 ");
        return true;
      }

    }
    Debug.Log("이미 있는 거고 쌓을 수 있어서 개수만 증량함  ");
    slot.AddAmount(_amount);
    InventoryManger.InventoryDataChanged();
    return true;


    // if (_item.buffs.Length > 0)
    // {
    //   SetEmptySlot(_item, _amount);
    //   InventoryManger.InventoryDataChanged();
    //   return;
    // }

    // for (int i = 0; i < GetSlots.Length; i++)
    // {
    //   if (GetSlots[i].item.Id == _item.Id)
    //   {
    //     GetSlots[i].AddAmount(_amount);
    //     InventoryManger.InventoryDataChanged();
    //     return;
    //   }
    // }
    // SetEmptySlot(_item, _amount);
    // InventoryManger.InventoryDataChanged();
  }

  public int EmptySlotCount
  {
    get
    {
      int counter = 0;
      for (int i = 0; i < GetSlots.Length; i++)
      {
        if (GetSlots[i].item.Id == "")
        {
          counter++;
        }
      }
      return counter;

    }
  }

  public InventorySlot FindItemOnInventory(Item _item)
  {
    Debug.Log("아이템을 찾는다 아이템 이름과 아이디  : " + _item.Id + " , " + _item.Name);
    for (int i = 0; i < GetSlots.Length; i++)
    {
      if (GetSlots[i].item.Id == _item.Id)
      {
        Debug.Log("아이템 찾음");
        return GetSlots[i];
      }
    }
    Debug.Log("아이템 못 찾음");
    return null;
  }
  public InventorySlot SetEmptySlot(Item _item, int _amount)
  {

    for (int i = 0; i < GetSlots.Length; i++)
    {
      if (GetSlots[i].item.Id == "")
      {
        Debug.Log("빈 슬롯 찾았다 채우기 아이템 대상 : " + _item.Name + " , " + _item.Id);
        GetSlots[i].UpdateSlot(_item, _amount);
        return GetSlots[i];
      }
    }
    //what to do when Full inventory
    return null;
  }

  public void SwapItem(InventorySlot item1, InventorySlot item2)
  {
    if (item2.CanPlaceInSlot(item1.ItemObject) && item1.CanPlaceInSlot(item2.ItemObject))
    {
      InventorySlot temp = new InventorySlot(item2.item, item2.amount);
      item2.UpdateSlot(item1.item, item1.amount);
      item1.UpdateSlot(temp.item, temp.amount);
    }
  }


  public void RemoveItem(Item _item)
  {
    for (int i = 0; i < GetSlots.Length; i++)
    {
      if (GetSlots[i].item == _item)
      {
        GetSlots[i].UpdateSlot(null, 0);
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

    // IFormatter formatter = new BinaryFormatter();
    // Stream stream = new FileStream(string.Concat(Application.persistentDataPath, savePath), FileMode.Create, FileAccess.Write);
    // formatter.Serialize(stream, Container);
    // stream.Close();
  }
  [ContextMenu("Load")]
  public void Load()
  {
    if (File.Exists(string.Concat(Application.persistentDataPath, savePath)))
    {
      //Debug.Log("저장 파일 찾았다");
      BinaryFormatter bf = new BinaryFormatter();
      FileStream file = File.Open(string.Concat(Application.persistentDataPath, savePath), FileMode.Open);
      //Debug.Log("새 컨테이너 개수는 : " + newContainer.Slots.Length);
      newContainer = JsonUtility.FromJson<Inventory>(EncryptDecrypt(bf.Deserialize(file).ToString()));
      for (int i = 0; i < newContainer.Slots.Length; i++)
      {
        GetSlots[i].UpdateSlot(newContainer.Slots[i].item, newContainer.Slots[i].amount);
      }
      file.Close();
      //Debug.Log("슬롯 갱신 함");
      InventoryManger.InventoryDataChanged();
    }
    // if (File.Exists(string.Concat(Application.persistentDataPath, savePath)))
    // {
    //   IFormatter formatter = new BinaryFormatter();
    //   Stream stream = new FileStream(string.Concat(Application.persistentDataPath, savePath), FileMode.Open, FileAccess.Read);
    //   Inventory newContainer = (Inventory)formatter.Deserialize(stream);
    //   for (int i = 0; i < newContainer.Slots.Length; i++)
    //   {
    //     GetSlots[i].UpdateSlot(newContainer.Slots[i].item, newContainer.Slots[i].amount);
    //   }
    //   stream.Close();
    //   InventoryManger.InventoryDataChanged();
    // }
  }
  [ContextMenu("Clear")]
  public void Clear()
  {
    Container.Clear();
    InventoryManger.InventoryDataChanged();
  }


  // public void OnAfterDeserialize()
  // {
  //   for (int i = 0; i < GetSlots.Count; i++)
  //   {
  //     GetSlots[i].item = database.GetItem[GetSlots[i].Id];
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
  public InventorySlot[] Slots = new InventorySlot[25];
  public void Clear()
  {
    for (int i = 0; i < Slots.Length; i++)
    {
      Slots[i].RemoveItem();
    }
  }
}

public delegate void SlotUpdated(InventorySlot _slot);

[System.Serializable]
public class InventorySlot
{
  public ItemType[] AllowedItems = new ItemType[0];
  public Item item;
  public int amount;
  [System.NonSerialized]
  public UserInterface parent;
  [System.NonSerialized]
  public UserInterface slotDisplay;
  [System.NonSerialized]
  public SlotUpdated OnBeforeUpdate;
  [System.NonSerialized]
  public SlotUpdated OnAfterUpdate;

  public ItemObject ItemObject
  {
    get
    {
      if (item.Id != "" && item.Id.ToString() != "-1" && item.Id != null)
      {
        // 여기가 문제네?
        Debug.Log("지금 읽고 있는 아이템 아이디 : " + item.Id);
        //return parent.inventory.database.GetItem[item.Id];
        return DatabaseReader.GetItemById(item.Id);
      }
      return null;
    }
  }

  public InventorySlot()
  {
    UpdateSlot(new Item(), 0);
  }
  public InventorySlot(Item _item, int _amount)
  {
    UpdateSlot(_item, _amount);
  }
  public void UpdateSlot(Item _item, int _amount)
  {
    if (OnBeforeUpdate != null)
    {
      OnBeforeUpdate.Invoke(this);
    }
    item = _item;
    amount = _amount;
    //Debug.Log("옮겼다. 옮기는 대상 : " + _item.Name + " , " + _item.Id + " 옮긴대상 item: " + item.Name + " , " + item.Id);
    if (OnAfterUpdate != null)
    {
      OnAfterUpdate.Invoke(this);
    }
  }
  public void RemoveItem()
  {
    UpdateSlot(new Item(), 0);
  }
  public void AddAmount(int value)
  {
    UpdateSlot(item, amount += value);
  }

  public bool CanPlaceInSlot(ItemObject _itemObject)
  {
    if (AllowedItems.Length <= 0 || _itemObject == null || _itemObject.data.Id == "")
    {
      return true;
    }
    for (int i = 0; i < AllowedItems.Length; i++)
    {
      Debug.Log("배치 가능한 물품 테스트 중.. ");
      if (_itemObject.type == AllowedItems[i])
      {
        return true;
      }
    }
    return false;
  }
}