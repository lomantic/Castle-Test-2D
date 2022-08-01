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
  //might change later 
  [SerializeField] Player player;
  [SerializeField] InventoryUIStateSystem InventoryManger;
  public InventoryObject inventory;

  [Header("Input Action")]
  [SerializeField] CanvasGroup InventoryScreen;
  public Dictionary<GameObject, InventorySlot> itemDisplayed = new Dictionary<GameObject, InventorySlot>();

  void Start()
  {
    for (int i = 0; i < inventory.Container.Items.Length; i++)
    {
      inventory.Container.Items[i].parent = this;
    }
    CreateSlots();
    AddEvent(gameObject, EventTriggerType.PointerEnter, delegate { OnEnterInterface(gameObject); });
    AddEvent(gameObject, EventTriggerType.PointerExit, delegate { OnExitInterface(gameObject); });
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
    foreach (KeyValuePair<GameObject, InventorySlot> _slot in itemDisplayed)
    {
      if (_slot.Value.Id >= 0)
      {
        _slot.Key.transform.GetChild(0).GetComponentInChildren<Image>().sprite =
        inventory.database.GetItem[_slot.Value.item.Id].uiDisplay;
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
  //     for (int i = 0; i < inventory.Container.Items.Count; i++)
  //     {
  //       InventorySlot slot = inventory.Container.Items[i];
  //       if (itemDisplayed.ContainsKey(slot))
  //       {
  //         itemDisplayed[slot].GetComponentInChildren<TextMeshProUGUI>().text = slot.amount.ToString("n0");
  //       }
  //       else
  //       {
  //         var obj = Instantiate(inventoryPrefab, Vector3.zero, Quaternion.identity, transform);
  //         obj.transform.GetChild(0).GetComponentInChildren<Image>().sprite = inventory.database.GetItem[slot.item.Id].uiDisplay;
  //         obj.GetComponent<RectTransform>().localPosition = GetPosition(i);
  //         obj.GetComponentInChildren<TextMeshProUGUI>().text = slot.amount.ToString("n0");
  //         itemDisplayed.Add(slot, obj);
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
    player.mouseItem.ui = obj.GetComponent<UserInterface>();

  }
  public void OnExitInterface(GameObject obj)
  {
    player.mouseItem.ui = null;
  }
  public void OnEnter(GameObject obj)
  {
    Debug.Log("Enter");
    player.mouseItem.hoverObj = obj;
    if (itemDisplayed.ContainsKey(obj))
    {
      player.mouseItem.hoverItem = itemDisplayed[obj];
    }

  }
  public void OnExit(GameObject obj)
  {
    Debug.Log("Exit");
    player.mouseItem.hoverObj = null;
    player.mouseItem.hoverItem = null;

    //obj.GetComponent<Image>().color = Color.white;
  }
  public void OnDragStart(GameObject obj)
  {
    Debug.Log("Drag Start");
    var mouseObject = new GameObject();
    var rt = mouseObject.AddComponent<RectTransform>();
    rt.sizeDelta = new Vector2(55, 55);
    mouseObject.transform.SetParent(transform.parent);
    if (itemDisplayed[obj].Id >= 0)
    {
      var img = mouseObject.AddComponent<Image>();
      img.sprite = inventory.database.GetItem[itemDisplayed[obj].Id].uiDisplay;
      img.raycastTarget = false;
    }
    player.mouseItem.obj = mouseObject;
    player.mouseItem.item = itemDisplayed[obj];

  }
  public void OnDragEnd(GameObject obj)
  {
    Debug.Log("Drag End");

    var itemOnMouse = player.mouseItem;
    var mouseHoverItem = itemOnMouse.hoverItem;
    var mouseHoverObj = itemOnMouse.hoverObj;
    var GetItemObject = inventory.database.GetItem;

    if (itemOnMouse.ui != null)
    {
      if (mouseHoverObj)
      {
        if (mouseHoverItem.CanPlaceInSlot(GetItemObject[itemDisplayed[obj].Id]) && (mouseHoverItem.item.Id <= -1 || (mouseHoverItem.item.Id >= 0 && itemDisplayed[obj].CanPlaceInSlot(GetItemObject[mouseHoverItem.item.Id]))))
        {
          Debug.Log("Swap");
          inventory.MoveItem(itemDisplayed[obj], mouseHoverItem.parent.itemDisplayed[mouseHoverObj]);
          InventoryManger.StoreNewItem();
        }

      }
    }
    else
    {
      inventory.RemoveItem(itemDisplayed[obj].item);
      InventoryManger.StoreNewItem();
    }
    Destroy(itemOnMouse.obj);
    itemOnMouse.item = null;

  }
  public void OnDrag(GameObject obj)
  {
    if (player.mouseItem.obj != null)
    {
      Debug.Log("Draging");
      player.mouseItem.obj.GetComponent<RectTransform>().position = Mouse.current.position.ReadValue();
    }
  }



}
//might change later
public class MouseItem
{
  public UserInterface ui;
  public GameObject obj;
  public InventorySlot item;
  public InventorySlot hoverItem;
  public GameObject hoverObj;
}