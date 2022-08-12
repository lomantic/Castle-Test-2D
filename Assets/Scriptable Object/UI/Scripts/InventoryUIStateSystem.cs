using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu(fileName = "New UI System", menuName = "Inventory System/UISystem/InventoryUI")]
public class InventoryUIStateSystem : UIStateSystem
{
  [System.NonSerialized]
  public UnityEvent<bool> AddItemEvent;
  public void Awake()
  {
    type = UIType.Inventory;
  }
  private void OnEnable()
  {
    uiEnabled = false;
    if (AddItemEvent == null)
    {
      AddItemEvent = new UnityEvent<bool>();
    }

    if (UIStateChangeEvent == null)
    {
      UIStateChangeEvent = new UnityEvent<bool>();
    }

  }

  public void InventoryDataChanged()
  {
    AddItemEvent.Invoke(true);
  }


}