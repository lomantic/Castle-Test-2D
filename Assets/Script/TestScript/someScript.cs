using UnityEngine;
public class someScript : MonoBehaviour, Isome
{
  public int someInt;
  public void AddValues(int baseValue)
  {
    someInt += baseValue;
  }


}