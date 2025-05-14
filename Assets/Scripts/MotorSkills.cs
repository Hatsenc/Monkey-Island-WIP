using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using System.Security.Cryptography;
using Unity.VisualScripting;
using UnityEditor.Callbacks;
using UnityEngine;
using UnityEngine.UIElements;

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

        if (heldItem != null && Input.GetKeyDown(KeyCode.Q))
        {
            DropItem();
        }
    }

    public void EquipItemTo(Item item, string limb)
    {
        heldItem = item;
        Rigidbody rb = heldItem.GetComponent<Rigidbody>();
        BoxCollider bc = heldItem.GetComponentInChildren<BoxCollider>();
        if (rb != null)
        {
            rb.isKinematic = true;
            rb.useGravity = false;
            bc.isTrigger = false;
        }
        currentLimbIndex = System.Array.IndexOf(limbs, limb);
        CurrentLimb();
        
    }

    public bool IsItemHeld(Item item)
    {
        return item != null && (item == leftArm || item == rightArm || item == tailArm);
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

    public void DropItem()
    {
        if (heldItem == null)
            return;

        heldItem.transform.SetParent(null);

        Rigidbody rb = heldItem.GetComponent<Rigidbody>();
        if (rb == null)
        {
            rb = heldItem.gameObject.AddComponent<Rigidbody>();
        }

        rb.isKinematic = false;
        rb.useGravity = true;

        // Clear the item from the current limb slot
        string limb = limbs[currentLimbIndex];
        switch (limb)
        {
            case "right":
                rightArm = null;
                break;
            case "left":
                leftArm = null;
                break;
            case "tail":
                tailArm = null;
                break;
        }

        Debug.Log("Item Dropped!");
        heldItem = null;
    }

}
