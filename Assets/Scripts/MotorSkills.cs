using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class LimbSwapFunctionality : MonoBehaviour
{   
    public Item leftArm;
    public Item rightArm;
    public Item tailArm;

    private string[] limbs = {"right", "tail", "left"};
    private int currentLimbIndex = 0;
    private Item heldItem;

    void Update()
    { 
        float scroll = Input.mouseScrollDelta.y;

        if (scroll > 0f)
        {
            currentLimbIndex = (currentLimbIndex + 1) % limbs.Length;
            CurrentLimb();
        }
        else if (scroll < 0f)
        {
            currentLimbIndex = (currentLimbIndex - 1 + limbs.Length) % limbs.Length;
            CurrentLimb();
        }
    }
    public void EquipItemTo(Item item, string limb)
    {
        heldItem = item;
        currentLimbIndex = System.Array.IndexOf(limbs, limb);
        CurrentLimb();
    }

    public void SwapHand(string givinglimb, string gettingLimb)
    {
        Item itemToMove = null;

        //Getting item from other limb
        if (givinglimb == "left") itemToMove = leftArm;
        else if (givinglimb == "right") itemToMove = rightArm;
        else if (givinglimb == "tail") itemToMove = tailArm;

        //Stop if there isnt anything to move
        if (itemToMove == null)
        {
            Debug.Log("No item in " + givinglimb + " to move!");
            return;
        }
    }

    private void CurrentLimb()
    {
        //Clear all limbs
        rightArm = null;
        leftArm = null;
        tailArm = null;

        //Place held item into current slot
        string limb = limbs[currentLimbIndex];
        switch (limb)
        {
            case "right":
                rightArm = heldItem;
                break;
            case "left":
                leftArm = heldItem;
                break;
            case "tail":
                tailArm = heldItem;
                break;
        }
        Debug.Log($"Item moved to: {limb}");
    }
}
