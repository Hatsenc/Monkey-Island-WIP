using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemPickup : MonoBehaviour
{
    private bool playerNear = false;
    private LimbSwapFunctionality monkeyref;

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("Player entered item trigger");
            playerNear = true;
            monkeyref = other.GetComponent<LimbSwapFunctionality>();
            if (monkeyref == null)
            {
                Debug.LogWarning("MotorSkills.cs not found on platyer!");
            }
        }   
    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("Player exited item trigger");
            playerNear = false;
            monkeyref = null;
        }   
    }

    void Update()
    {
        if (playerNear)
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                if (monkeyref != null)
                {
                    Item itemComponent = GetComponent<Item>();
                    if (itemComponent != null)
                    {
                        Debug.Log("Item component found, equiping to right hand");
                        monkeyref.EquipItemToLimb(itemComponent, "right");
                        this.enabled = false;
                    }
                    else
                    {
                        Debug.LogWarning("Item component is missing on thsi object!");
                    }
                }
            }
        }
    }
}
