using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class DatabaseReader : MonoBehaviour
{
  public ItemDatabaseObject itemDatabase;

  private static DatabaseReader DB_Reader;

  private void Awake()
  {
    if (DB_Reader == null)
    {
      DB_Reader = this;
      DontDestroyOnLoad(gameObject);
    }
    else
    {
      Destroy(gameObject);
    }
  }

  public static ItemObject GetItemById(string _id)
  {
    Debug.Log("찾는 물품은 " + _id);
    // foreach (ItemObject ItemObj in DB_Reader?.itemDatabase?.ItemObjects)
    // {
    //   Debug.Log("리더기 안에서 이러한 아이템 이 있음 :: " + ItemObj?.data?.Name + " " + ItemObj?.data?.Id);
    // }
    return DB_Reader.itemDatabase.ItemObjects.FirstOrDefault(i => i?.data?.Id == _id);
  }
}
