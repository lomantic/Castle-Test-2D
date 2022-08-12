// using System;
// using System.Collections;
// using System.Collections.Generic;
// using UnityEngine;
// using TMPro;
// using UnityEngine.InputSystem;
// using UnityEngine.UI;
// using UnityEngine.EventSystems;
// using UnityEngine.Events;
// //using UnityEngine.UI;

// public class DisplayInventory : MonoBehaviour
// {
//   //might change later 

//   [SerializeField] InventoryUIStateSystem InventoryManger;
//   [SerializeField] InventoryObject inventory;
//   [SerializeField] Camera mainCamera;
//   public GameObject inventoryPrefab;
//   public int X_START;
//   public int Y_START;
//   public int X_SPACE_BETWEEN_ITEM;
//   public int Y_SPACE_BETWEEN_ITEM;
//   public int NUMBER_OF_COLUMN;

//   [Header("Input Action")]
//   [SerializeField] CanvasGroup InventoryScreen;
//   Dictionary<GameObject, InventorySlot> slotsOnInterface = new Dictionary<GameObject, InventorySlot>();

//   void Start()
//   {
//     CreateSlots();
//   }
//   void OnEnable()
//   {
//     InventoryManger.AddItemEvent.AddListener(UpdateSlots);
//     InventoryManger.UIStateChangeEvent.AddListener(OpenCloseIventory);

//   }
//   void OnDisable()
//   {
//     InventoryManger.AddItemEvent.RemoveListener(UpdateSlots);
//     InventoryManger.UIStateChangeEvent.RemoveListener(OpenCloseIventory);
//   }

//   // Update is called once per frame
//   void Update()
//   {

//   }

//   public void UpdateSlots(bool _)
//   {
//     foreach (KeyValuePair<GameObject, InventorySlot> _slot in slotsOnInterface)
//     {
//       if (_slot.Value.Id >= 0)
//       {
//         _slot.Key.transform.GetChild(0).GetComponentInChildren<Image>().sprite =
//         inventory.database.GetItem[_slot.Value.item.Id].uiDisplay;
//         _slot.Key.transform.GetChild(0).GetComponentInChildren<Image>().color = new Color(1, 1, 1, 1);
//         _slot.Key.GetComponentInChildren<TextMeshProUGUI>().text = _slot.Value.amount == 1 ? "" : _slot.Value.amount.ToString("n0");
//       }
//       else
//       {
//         _slot.Key.transform.GetChild(0).GetComponentInChildren<Image>().sprite = null;
//         _slot.Key.transform.GetChild(0).GetComponentInChildren<Image>().color = new Color(1, 1, 1, 0);
//         _slot.Key.GetComponentInChildren<TextMeshProUGUI>().text = "";
//       }
//     }
//   }

//   // public void UpdateDisplay(bool screenEnable)
//   // {
//   //   if (screenEnable)
//   //   {
//   //     InventoryScreen.alpha = 1;
//   //     InventoryScreen.interactable = true;
//   //     for (int i = 0; i < inventory.Container.Items.Count; i++)
//   //     {
//   //       InventorySlot slot = inventory.Container.Items[i];
//   //       if (slotsOnInterface.ContainsKey(slot))
//   //       {
//   //         slotsOnInterface[slot].GetComponentInChildren<TextMeshProUGUI>().text = slot.amount.ToString("n0");
//   //       }
//   //       else
//   //       {
//   //         var obj = Instantiate(inventoryPrefab, Vector3.zero, Quaternion.identity, transform);
//   //         obj.transform.GetChild(0).GetComponentInChildren<Image>().sprite = inventory.database.GetItem[slot.item.Id].uiDisplay;
//   //         obj.GetComponent<RectTransform>().localPosition = GetPosition(i);
//   //         obj.GetComponentInChildren<TextMeshProUGUI>().text = slot.amount.ToString("n0");
//   //         slotsOnInterface.Add(slot, obj);
//   //       }
//   //     }
//   //   }
//   //   else
//   //   {
//   //     InventoryScreen.alpha = 0;
//   //     InventoryScreen.interactable = false;
//   //   }

//   // }
//   private void OpenCloseIventory(bool screenEnable)
//   {
//     if (screenEnable)
//     {
//       InventoryScreen.blocksRaycasts = true;
//       InventoryScreen.alpha = 1;
//       InventoryScreen.interactable = true;

//     }
//     else
//     {
//       InventoryScreen.blocksRaycasts = false;
//       InventoryScreen.alpha = 0;
//       InventoryScreen.interactable = false;

//     }
//   }

//   private void CreateSlots()
//   {
//     //not necessary but for caution purpose

//     slotsOnInterface = new Dictionary<GameObject, InventorySlot>();
//     for (int i = 0; i < inventory.Container.Items.Length; i++)
//     {
//       InventorySlot slot = inventory.Container.Items[i];
//       var obj = Instantiate(inventoryPrefab, Vector3.zero, Quaternion.identity, transform);
//       obj.GetComponent<RectTransform>().localPosition = GetPosition(i);

//       AddEvent(obj, EventTriggerType.PointerEnter, delegate { OnEnter(obj); });
//       AddEvent(obj, EventTriggerType.PointerExit, delegate { OnExit(obj); });
//       AddEvent(obj, EventTriggerType.BeginDrag, delegate { OnDragStart(obj); });
//       AddEvent(obj, EventTriggerType.EndDrag, delegate { OnDragEnd(obj); });
//       AddEvent(obj, EventTriggerType.Drag, delegate { OnDrag(obj); });

//       slotsOnInterface.Add(obj, slot);

//     }

//     // for (int i = 0; i < inventory.Container.Items.Count; i++)
//     // {
//     //   InventorySlot slot = inventory.Container.Items[i];
//     //   var obj = Instantiate(inventoryPrefab, Vector3.zero, Quaternion.identity, transform);
//     //   obj.transform.GetChild(0).GetComponentInChildren<Image>().sprite = inventory.database.GetItem[slot.Id].uiDisplay;
//     //   obj.GetComponent<RectTransform>().localPosition = GetPosition(i);
//     //   obj.GetComponentInChildren<TextMeshProUGUI>().text = slot.amount.ToString("n0");
//     //   slotsOnInterface.Add(slot, obj);
//     // }

//   }

//   private void AddEvent(GameObject obj, EventTriggerType type, UnityAction<BaseEventData> action)
//   {
//     EventTrigger trigger = obj.GetComponent<EventTrigger>();
//     var eventTrigger = new EventTrigger.Entry();
//     eventTrigger.eventID = type;
//     eventTrigger.callback.AddListener(action);
//     trigger.triggers.Add(eventTrigger);
//   }

//   public void OnEnter(GameObject obj)
//   {
//     Debug.Log("Enter");
//     MouseData.hoverObj = obj;
//     if (slotsOnInterface.ContainsKey(obj))
//     {
//       MouseData.hoverItem = slotsOnInterface[obj];
//     }

//   }
//   public void OnExit(GameObject obj)
//   {
//     Debug.Log("Exit");
//     MouseData.hoverObj = null;
//     MouseData.hoverItem = null;

//     //obj.GetComponent<Image>().color = Color.white;
//   }
//   public void OnDragStart(GameObject obj)
//   {
//     Debug.Log("Drag Start");
//     var mouseObject = new GameObject();
//     var rt = mouseObject.AddComponent<RectTransform>();
//     rt.sizeDelta = new Vector2(55, 55);
//     mouseObject.transform.SetParent(transform.parent);
//     if (slotsOnInterface[obj].Id >= 0)
//     {
//       var img = mouseObject.AddComponent<Image>();
//       img.sprite = inventory.database.GetItem[slotsOnInterface[obj].Id].uiDisplay;
//       img.raycastTarget = false;
//     }
//     MouseData.obj = mouseObject;
//     MouseData.item = slotsOnInterface[obj];

//   }
//   public void OnDragEnd(GameObject obj)
//   {
//     Debug.Log("Drag End");
//     if (MouseData.hoverObj)
//     {
//       Debug.Log("Swap");
//       inventory.SwapItem(slotsOnInterface[obj], slotsOnInterface[MouseData.hoverObj]);
//       InventoryManger.InventoryDataChanged();
//     }
//     else
//     {
//       inventory.ReSwapItem(slotsOnInterface[obj].item);
//       InventoryManger.InventoryDataChanged();
//     }
//     Destroy(MouseData.obj);
//     MouseData.item = null;

//   }
//   public void OnDrag(GameObject obj)
//   {
//     if (MouseData.obj != null)
//     {
//       Debug.Log("Draging");
//       MouseData.obj.GetComponent<RectTransform>().position = Mouse.current.position.ReadValue();
//     }
//   }

//   private Vector3 GetPosition(int i)
//   {
//     return new Vector3(X_START + (X_SPACE_BETWEEN_ITEM * (i % NUMBER_OF_COLUMN)), Y_START + (-Y_SPACE_BETWEEN_ITEM * (i / NUMBER_OF_COLUMN)), 0f);
//   }

// }
// //might change later
