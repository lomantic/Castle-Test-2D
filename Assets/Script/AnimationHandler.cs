using UnityEngine;

public class AnimationHandler : MonoBehaviour
{
  Animator animator;
  private string currentState = "";

  void Start()
  {
    animator = GetComponent<Animator>();
  }

  public void ChangeAnimationState(string newState)
  {
    //Guard animation from inturrupting itself
    if (currentState == newState)
    {
      return;
    }
    else
    {
      animator.Play(newState);
      currentState = newState;
    }
  }
}
