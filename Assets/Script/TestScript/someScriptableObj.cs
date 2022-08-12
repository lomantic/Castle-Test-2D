using UnityEngine;

[CreateAssetMenu(fileName = "New test sc obj", menuName = "Test/test sc obj")]
public class someScriptableObj : ScriptableObject, Isome
{
  public int someInt;
  public int setSomeIntHere;

  public void AddValues(int baseValue)
  {
    someInt += baseValue;
  }


}