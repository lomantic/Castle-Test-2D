using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu(fileName = "New UI System", menuName = "Inventory System/UISystem/InventoryUI")]
public class InventoryUIStateSystem : UIStateSystem
{


  public void Awake()
  {
    type = UIType.Inventory;
  }

  private void OnEnable()
  {

    uiEnabled = false;
    if (UIStateChangeEvent == null)
    {
      UIStateChangeEvent = new UnityEvent<bool>();
    }
  }

}