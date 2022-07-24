using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public enum UIType
{
  Inventory, WorldMap, Equipment
}
public abstract class UIStateSystem : ScriptableObject
{
  public bool uiEnabled = false;
  public UIType type;
  [System.NonSerialized]
  public UnityEvent<bool> UIStateChangeEvent;

  private void OnEnable()
  {

    uiEnabled = false;
    if (UIStateChangeEvent == null)
    {
      UIStateChangeEvent = new UnityEvent<bool>();
    }
  }

  public void ChangeUIState()
  {
    uiEnabled = !uiEnabled;
    //Debug.Log("작동중 " + type);
    UIStateChangeEvent.Invoke(uiEnabled);
  }
}