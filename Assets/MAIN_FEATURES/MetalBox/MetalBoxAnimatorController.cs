using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MetalBoxAnimatorController : MonoBehaviour
{
   public Animator MetalBoxAnimator;
   
   private string Hit = "Hit";

   public void PlayHit()
   {
      MetalBoxAnimator.SetTrigger(Hit);
   }
}
