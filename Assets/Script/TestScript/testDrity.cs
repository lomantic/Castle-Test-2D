using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class testDrity : MonoBehaviour, ISerializationCallbackReceiver
{

  [SerializeField] someScriptableObj[] testObj;
  [SerializeField] ItemDatabaseObject DBObj;
  public void OnAfterDeserialize()
  {

  }

  public void OnBeforeSerialize()
  {
#if UNITY_EDITOR
    for (int i = 0; i < testObj.Length; i++)
    {
      testObj[i].setSomeIntHere = testObj[i].someInt;
      EditorUtility.SetDirty(testObj[i]);
    }
    for (int i = 0; i < DBObj.ItemObjects.Length; i++)
    {
      DBObj.ItemObjects[i].data.Id = string.Concat("KEY_", DBObj.ItemObjects[i].data.Name);
      EditorUtility.SetDirty(DBObj.ItemObjects[i]);
    }
#endif
  }

}