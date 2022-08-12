using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New item Database", menuName = "Inventory System/Items/Database")]
public class ItemDatabaseObject : ScriptableObject//, ISerializationCallbackReceiver
{
  public ItemObject[] ItemObjects;
  //public Dictionary<ItemObject, int> GetId = new Dictionary<ItemObject, int>();
  //public Dictionary<string, ItemObject> GetItem;

  // private void OnEnable()
  // {
  //   Debug.Log("들어간 정보 내용 ItemObjects ");
  //   for (int i = 0; i < GetItem.Count; i++)
  //   {
  //     Debug.Log("ItemObject " + i.ToString() + "번째 아이템이름과 키 :" + ItemObjects[i].data.Name + " , " + ItemObjects[i].data.Id);
  //   }
  // }

  // [ContextMenu("Update Id")]
  // public void UpdateId()
  // {


  //   for (int i = 0; i < ItemObjects.Length; i++)
  //   {
  //     //Debug.Log("오브젝트" + i + "번째 정보 ] 쌓을 수 있는 타입인가 : " + ItemObjects[i].stackable);
  //     string NEW_KEY = string.Concat("KEY_", i);
  //     //Debug.Log("검사 대상 : " + NEW_KEY);
  //     //GetId.Add(ItemObjects[i], i);
  //     // if (ItemObjects[i].data.Id != i)
  //     // {
  //     //   ItemObjects[i].data.Id = i;
  //     // }
  //     if (!GetItem.ContainsKey(NEW_KEY))
  //     {
  //       //Debug.Log("새로 추가함 " + NEW_KEY);
  //       ItemObjects[i].data.Id = NEW_KEY;
  //       GetItem.Add(NEW_KEY, ItemObjects[i]);
  //     }
  //   }
  //   Debug.Log("item Length in update :  " + ItemObjects.Length);
  //   Debug.Log("item Length in update Dic :  " + GetItem.Count);

  // }

  // public void OnAfterDeserialize()
  // {
  //   //변경이 생기면 그냥 매번 새로운 dic을 판다
  //   GetItem = new Dictionary<string, ItemObject>();
  //   //GetId = new Dictionary<ItemObject, int>();
  //   UpdateId();
  //   // for (int i = 0; i < ItemObjects.Length; i++)
  //   // {
  //   //   GetId.Add(ItemObjects[i], i);
  //   //   ItemObjects[i].data.Id = i;
  //   //   GetItem.Add(i, ItemObjects[i]);
  //   // }

  // }

  // public void OnBeforeSerialize()
  // {
  //   // #if UNITY_EDITOR
  //   //     //Debug.Log("item Length OnBeforeSerialize :  " + ItemObjects.Length);
  //   //     //GetItem = new Dictionary<int, ItemObject>();
  //   // #endif
  // }

}
