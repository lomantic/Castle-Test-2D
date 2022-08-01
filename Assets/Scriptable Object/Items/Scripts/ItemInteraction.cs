using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ItemInteraction : MonoBehaviour, IBeginDragHandler, IEndDragHandler, IDragHandler, IPointerEnterHandler, IPointerExitHandler
{


  private Image _image;
  private void OnEnable()
  {
    //_image = gameObject.transform.GetChild(0).GetComponentInChildren<Image>();
    _image = GetComponent<Image>();
  }
  public void OnBeginDrag(PointerEventData eventData)
  {
    Debug.Log("OnBeginDrag : " + eventData);
  }

  public void OnDrag(PointerEventData eventData)
  {
    Debug.Log("OnDrag : " + eventData);
  }

  public void OnEndDrag(PointerEventData eventData)
  {
    Debug.Log("OnEndDrag : " + eventData);
  }

  public void OnPointerEnter(PointerEventData eventData)
  {
    Debug.Log("OnPointerEnter : " + eventData);
    _image.color = Color.black;
  }

  public void OnPointerExit(PointerEventData eventData)
  {
    Debug.Log("OnPointerExit : " + eventData);
    _image.color = Color.white;
  }

  // Start is called before the first frame update
  void Start()
  {

  }

  // Update is called once per frame
  void Update()
  {

  }
}
