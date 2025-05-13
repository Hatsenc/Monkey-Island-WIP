using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScrollSwap : MonoBehaviour
{
   public LimbSwapFunctionality monkey;

   private string[] limbs = {"left", "right", "tail"};
   private int currentLimbIndex = 0;

    void Update()
    {
        float scroll = Input.GetAxis("Mouse ScrollWheel");

        if (scroll > 0f)
        {
            SwapItemForw();
        }
        
        else if (scroll < 0f)
        {
            SwapItemBack();
        }
    }

    void SwapItemForw()
    {
        //Move to next limb, loops through all 3 limbs
        string givinglimb = limbs[currentLimbIndex];
        int nextIndex = (currentLimbIndex + 1) % limbs.Length;
        string gettingLimb = limbs[nextIndex];

        currentLimbIndex = nextIndex;
    }

    void SwapItemBack()
    {
        string givinglimb = limbs[currentLimbIndex];
        int prevIndex = (currentLimbIndex - 1 + limbs.Length) % limbs.Length;
        string gettingLimb = limbs[prevIndex];

        monkey.SwapHand(givinglimb, gettingLimb);

        currentLimbIndex = prevIndex;
    }
}
