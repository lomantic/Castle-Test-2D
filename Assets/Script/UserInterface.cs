using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.InputSystem;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.Events;
//using UnityEngine.UI;

public abstract class UserInterface : MonoBehaviour
{
  [SerializeField] InventoryUIStateSystem InventoryManger;
  public InventoryObject inventory;

  [Header("Input Action")]
  [SerializeField] CanvasGroup InventoryScreen;
  public Dictionary<GameObject, InventorySlot> slotsOnInterface = new Dictionary<GameObject, InventorySlot>();

  void Start()
  {
    for (int i = 0; i < inventory.GetSlots.Length; i++)
    {
      inventory.GetSlots[i].parent = this;
      inventory.GetSlots[i].OnAfterUpdate += OnSlotUpdate;
    }
    CreateSlots();
    AddEvent(gameObject, EventTriggerType.PointerEnter, delegate { OnEnterInterface(gameObject); });
    AddEvent(gameObject, EventTriggerType.PointerExit, delegate { OnExitInterface(gameObject); });
  }

  private void OnSlotUpdate(InventorySlot _slot)
  {
  }

  void OnEnable()
  {
    InventoryManger.AddItemEvent.AddListener(UpdateSlots);
    InventoryManger.UIStateChangeEvent.AddListener(OpenCloseIventory);

  }
  void OnDisable()
  {
    InventoryManger.AddItemEvent.RemoveListener(UpdateSlots);
    InventoryManger.UIStateChangeEvent.RemoveListener(OpenCloseIventory);
  }

  // Update is called once per frame
  void Update()
  {

  }

  public void UpdateSlots(bool _)
  {
    foreach (KeyValuePair<GameObject, InventorySlot> _slot in slotsOnInterface)
    {
      if (_slot.Value.item.Id != "" && _slot.Value.item.Id != null && _slot.Value.item.Id.ToString() != "-1")
      {
        _slot.Key.transform.GetChild(0).GetComponentInChildren<Image>().sprite = (_slot.Value?.ItemObject)?.uiDisplay;
        //inventory.database.GetItem[_slot.Value.item.Id].uiDisplay;
        _slot.Key.transform.GetChild(0).GetComponentInChildren<Image>().color = new Color(1, 1, 1, 1);
        _slot.Key.GetComponentInChildren<TextMeshProUGUI>().text = _slot.Value.amount == 1 ? "" : _slot.Value.amount.ToString("n0");
      }
      else
      {
        _slot.Key.transform.GetChild(0).GetComponentInChildren<Image>().sprite = null;
        _slot.Key.transform.GetChild(0).GetComponentInChildren<Image>().color = new Color(1, 1, 1, 0);
        _slot.Key.GetComponentInChildren<TextMeshProUGUI>().text = "";
      }
    }
  }

  // public void UpdateDisplay(bool screenEnable)
  // {
  //   if (screenEnable)
  //   {
  //     InventoryScreen.alpha = 1;
  //     InventoryScreen.interactable = true;
  //     for (int i = 0; i < inventory.GetSlots.Count; i++)
  //     {
  //       InventorySlot slot = inventory.GetSlots[i];
  //       if (slotsOnInterface.ContainsKey(slot))
  //       {
  //         slotsOnInterface[slot].GetComponentInChildren<TextMeshProUGUI>().text = slot.amount.ToString("n0");
  //       }
  //       else
  //       {
  //         var obj = Instantiate(inventoryPrefab, Vector3.zero, Quaternion.identity, transform);
  //         obj.transform.GetChild(0).GetComponentInChildren<Image>().sprite = inventory.database.GetItem[slot.item.Id].uiDisplay;
  //         obj.GetComponent<RectTransform>().localPosition = GetPosition(i);
  //         obj.GetComponentInChildren<TextMeshProUGUI>().text = slot.amount.ToString("n0");
  //         slotsOnInterface.Add(slot, obj);
  //       }
  //     }
  //   }
  //   else
  //   {
  //     InventoryScreen.alpha = 0;
  //     InventoryScreen.interactable = false;
  //   }

  // }
  private void OpenCloseIventory(bool screenEnable)
  {
    if (screenEnable)
    {
      InventoryScreen.blocksRaycasts = true;
      InventoryScreen.alpha = 1;
      InventoryScreen.interactable = true;

    }
    else
    {
      InventoryScreen.blocksRaycasts = false;
      InventoryScreen.alpha = 0;
      InventoryScreen.interactable = false;

    }
  }

  public abstract void CreateSlots();

  protected void AddEvent(GameObject obj, EventTriggerType type, UnityAction<BaseEventData> action)
  {
    EventTrigger trigger = obj.GetComponent<EventTrigger>();
    var eventTrigger = new EventTrigger.Entry();
    eventTrigger.eventID = type;
    eventTrigger.callback.AddListener(action);
    trigger.triggers.Add(eventTrigger);
  }
  public void OnEnterInterface(GameObject obj)
  {
    MouseData.interfaceMouseIsOver = obj.GetComponent<UserInterface>();

  }
  public void OnExitInterface(GameObject obj)
  {
    MouseData.interfaceMouseIsOver = null;
  }
  public void OnEnter(GameObject obj)
  {
    //Debug.Log("Enter");
    MouseData.slotHoveredOver = obj;
    // if (slotsOnInterface.ContainsKey(obj))
    // {
    //   MouseData.hoverItem = slotsOnInterface[obj];
    // }

  }
  public void OnExit(GameObject obj)
  {
    //Debug.Log("Exit");
    MouseData.slotHoveredOver = null;
    //MouseData.hoverObj = null;
    //MouseData.hoverItem = null;

    //obj.GetComponent<Image>().color = Color.white;
  }
  public void OnDragStart(GameObject obj)
  {
    MouseData.tempItemBeingDragged = CreateTempItem(obj);
    //MouseData.item = slotsOnInterface[obj];

  }
  public GameObject CreateTempItem(GameObject obj)
  {
    GameObject mouseObject = null;
    if (slotsOnInterface[obj].item.Id != "")
    {
      //Debug.Log("Drag Start");
      mouseObject = new GameObject();
      var rt = mouseObject.AddComponent<RectTransform>();
      rt.sizeDelta = new Vector2(55, 55);
      mouseObject.transform.SetParent(transform.parent);
      var img = mouseObject.AddComponent<Image>();
      img.sprite = slotsOnInterface[obj].ItemObject.uiDisplay;
      //inventory.database.GetItem[slotsOnInterface[obj].item.Id].uiDisplay;
      img.raycastTarget = false;

    }
    return mouseObject;
  }
  public void OnDragEnd(GameObject obj)
  {
    //Debug.Log("Drag End");

    // var itemOnMouse = MouseData;
    // var mouseHoverItem = itemOnMouse.hoverItem;
    // var mouseHoverObj = itemOnMouse.hoverObj;
    // var GetItemObject = inventory.database.GetItem;

    Destroy(MouseData.tempItemBeingDragged);
    if (MouseData.interfaceMouseIsOver == null)
    {
      slotsOnInterface[obj].RemoveItem();
      InventoryManger.InventoryDataChanged();
      return;
    }
    if (MouseData.slotHoveredOver)
    {
      InventorySlot mouseHoverSlotData = MouseData.interfaceMouseIsOver.slotsOnInterface[MouseData.slotHoveredOver];
      inventory.SwapItem(slotsOnInterface[obj], mouseHoverSlotData);
      InventoryManger.InventoryDataChanged();
    }
    // if (itemOnMouse.ui != null)
    // {
    //   if (mouseHoverObj)
    //   {
    //     if (mouseHoverItem.CanPlaceInSlot(GetItemObject[slotsOnInterface[obj].Id]) && (mouseHoverItem.item.Id <= -1 || (mouseHoverItem.item.Id >= 0 && slotsOnInterface[obj].CanPlaceInSlot(GetItemObject[mouseHoverItem.item.Id]))))
    //     {
    //       Debug.Log("Swap");
    //       inventory.SwapItem(slotsOnInterface[obj], mouseHoverItem.parent.slotsOnInterface[mouseHoverObj]);
    //       InventoryManger.InventoryDataChanged();
    //     }

    //   }
    // }
    // else
    // {
    //   inventory.RemoveItem(slotsOnInterface[obj].item);
    //   InventoryManger.InventoryDataChanged();
    // }

    //itemOnMouse.item = null;

  }
  public void OnDrag(GameObject obj)
  {
    if (MouseData.tempItemBeingDragged != null)
    {
      //Debug.Log("Draging");
      MouseData.tempItemBeingDragged.GetComponent<RectTransform>().position = Mouse.current.position.ReadValue();
    }
  }



}
//might change later
public static class MouseData
{
  public static UserInterface interfaceMouseIsOver;
  public static GameObject tempItemBeingDragged;
  public static GameObject slotHoveredOver;
}