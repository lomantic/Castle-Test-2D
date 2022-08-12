using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class GroundItem : MonoBehaviour, ISerializationCallbackReceiver
{
  public ItemObject item;

  private void Start()
  {
    Debug.Log(item.name + "아이템 이름 과 키 등등 정보 : " + item.data.Id + " " + item.data.Name);
  }

  public void OnAfterDeserialize()
  {
  }

  public void OnBeforeSerialize()
  {
#if UNITY_EDITOR
    GetComponentInChildren<SpriteRenderer>().sprite = item.uiDisplay;
    EditorUtility.SetDirty(GetComponentInChildren<SpriteRenderer>());
#endif
  }
}
