using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class LimbSwapFunctionality : MonoBehaviour
{   
    public Item leftArm;
    public Item rightArm;
    public Item tailArm;
    public Transform leftHand;
    public Transform rightHand;
    public Transform tailHand;

    public void EquipItemToLimb(Item item, string limb)
    {
        //When something is grabbed it will notify in Debug which limb and what item
        if (limb == "left")
        {
            leftArm = item;
            item.transform.SetParent(leftHand);
        }


        else if (limb == "right")
            {
                rightArm = item;
                item.transform.SetParent(rightHand);
            }


        else if (limb == "tail")
            {
            tailArm = item;
            item.transform.SetParent(tailHand);
            }

            //Snap item to correct pos and rot
            item.transform.localPosition = Vector3.zero;
            item.transform.localRotation = Quaternion.identity;

            Debug.Log("Equipped " + item.itemName + " to " + limb + "!");
        
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

        //Equip item to target limb
        EquipItemToLimb(itemToMove, gettingLimb);

        //Remove from "Giving Limb"
        if (givinglimb == "left arm") leftArm = null;
        else if (givinglimb == "right arm") rightArm = null;
        else if (givinglimb == "tail") tailArm = null;

        Debug.Log("Moved " + itemToMove + " from " + givinglimb + " to " + gettingLimb + ".");

    }
}
