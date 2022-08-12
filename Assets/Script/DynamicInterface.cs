using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class DynamicInterface : UserInterface
{
  [SerializeField] int X_START;
  [SerializeField] int Y_START;
  [SerializeField] int X_SPACE_BETWEEN_ITEM;
  [SerializeField] int Y_SPACE_BETWEEN_ITEM;
  [SerializeField] int NUMBER_OF_COLUMN;
  [SerializeField] GameObject inventoryPrefab;

  public override void CreateSlots()
  {
    //not necessary but for caution purpose

    slotsOnInterface = new Dictionary<GameObject, InventorySlot>();
    for (int i = 0; i < inventory.GetSlots.Length; i++)
    {
      InventorySlot slot = inventory.GetSlots[i];
      var obj = Instantiate(inventoryPrefab, Vector3.zero, Quaternion.identity, transform);
      obj.GetComponent<RectTransform>().localPosition = GetPosition(i);

      AddEvent(obj, EventTriggerType.PointerEnter, delegate { OnEnter(obj); });
      AddEvent(obj, EventTriggerType.PointerExit, delegate { OnExit(obj); });
      AddEvent(obj, EventTriggerType.BeginDrag, delegate { OnDragStart(obj); });
      AddEvent(obj, EventTriggerType.EndDrag, delegate { OnDragEnd(obj); });
      AddEvent(obj, EventTriggerType.Drag, delegate { OnDrag(obj); });

      slotsOnInterface.Add(obj, slot);

    }

    // for (int i = 0; i < inventory.Container.Items.Count; i++)
    // {
    //   InventorySlot slot = inventory.Container.Items[i];
    //   var obj = Instantiate(inventoryPrefab, Vector3.zero, Quaternion.identity, transform);
    //   obj.transform.GetChild(0).GetComponentInChildren<Image>().sprite = inventory.database.GetItem[slot.Id].uiDisplay;
    //   obj.GetComponent<RectTransform>().localPosition = GetPosition(i);
    //   obj.GetComponentInChildren<TextMeshProUGUI>().text = slot.amount.ToString("n0");
    //   slotsOnInterface.Add(slot, obj);
    // }

  }
  private Vector3 GetPosition(int i)
  {
    return new Vector3(X_START + (X_SPACE_BETWEEN_ITEM * (i % NUMBER_OF_COLUMN)), Y_START + (-Y_SPACE_BETWEEN_ITEM * (i / NUMBER_OF_COLUMN)), 0f);
  }
}
