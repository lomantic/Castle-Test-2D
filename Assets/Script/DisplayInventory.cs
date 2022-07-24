using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.InputSystem;
//using UnityEngine.UI;

public class DisplayInventory : MonoBehaviour
{
  [SerializeField] UIStateSystem InventoryManger;
  [SerializeField] InventoryObject inventory;
  public int X_START;
  public int Y_START;
  public int X_SPACE_BETWEEN_ITEM;
  public int Y_SPACE_BETWEEN_ITEM;
  public int NUMBER_OF_COLUMN;

  [Header("Input Action")]
  [SerializeField] CanvasGroup InventoryScreen;
  Dictionary<InventorySlot, GameObject> itemDisplayed = new Dictionary<InventorySlot, GameObject>();

  void Start()
  {
    CreateDisplay();
  }
  void OnEnable()
  {
    InventoryManger.UIStateChangeEvent.AddListener(UpdateDisplay);
  }
  void OnDisable()
  {
    InventoryManger.UIStateChangeEvent.RemoveListener(UpdateDisplay);
  }

  // Update is called once per frame
  void Update()
  {
    //UpdateDisplay();
  }

  public void UpdateDisplay(bool screenEnable)
  {
    if (screenEnable)
    {
      InventoryScreen.alpha = 1;
      InventoryScreen.interactable = true;
      for (int i = 0; i < inventory.Container.Count; i++)
      {
        if (itemDisplayed.ContainsKey(inventory.Container[i]))
        {
          itemDisplayed[inventory.Container[i]].GetComponentInChildren<TextMeshProUGUI>().text = inventory.Container[i].amount.ToString("n0");
        }
        else
        {
          var obj = Instantiate(inventory.Container[i].item.prefab, Vector3.zero, Quaternion.identity, transform);
          obj.GetComponent<RectTransform>().localPosition = GetPosition(i);
          obj.GetComponentInChildren<TextMeshProUGUI>().text = inventory.Container[i].amount.ToString("n0");
          itemDisplayed.Add(inventory.Container[i], obj);
        }
      }
    }
    else
    {
      InventoryScreen.alpha = 0;
      InventoryScreen.interactable = false;
    }

  }

  private void CreateDisplay()
  {
    for (int i = 0; i < inventory.Container.Count; i++)
    {
      var obj = Instantiate(inventory.Container[i].item.prefab, Vector3.zero, Quaternion.identity, transform);
      obj.GetComponent<RectTransform>().localPosition = GetPosition(i);
      obj.GetComponentInChildren<TextMeshProUGUI>().text = inventory.Container[i].amount.ToString("n0");
      itemDisplayed.Add(inventory.Container[i], obj);
    }
  }
  private Vector3 GetPosition(int i)
  {
    return new Vector3(X_START + (X_SPACE_BETWEEN_ITEM * (i % NUMBER_OF_COLUMN)), Y_START + (-Y_SPACE_BETWEEN_ITEM * (i / NUMBER_OF_COLUMN)), 0f);
  }

}