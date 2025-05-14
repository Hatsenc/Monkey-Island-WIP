using UnityEngine;

public class ItemPickup : MonoBehaviour
{
    private bool isPlayerInRange = false;  // Tracks if the player is within the pickup range

    void OnTriggerEnter(Collider other)
    {
        // Check if the object entering the trigger is the player
        if (other.CompareTag("Player"))
        {
            isPlayerInRange = true;
        }
    }

    void OnTriggerExit(Collider other)
    {
        // Check if the object exiting the trigger is the player
        if (other.CompareTag("Player"))
        {
            isPlayerInRange = false;
        }
    }

    void Update()
    {
        // When the player presses "E" and is in range, the item will be picked up
        if (isPlayerInRange && Input.GetKeyDown(KeyCode.E))
        {
            // Call the EquipItem method from the player's LimbSwapFunctionality script
            LimbSwapFunctionality motorSkills = GameObject.FindWithTag("Player").GetComponent<LimbSwapFunctionality>();
            Item item = GetComponent<Item>();

            if (motorSkills != null && item != null)
            {
                motorSkills.EquipItemTo(item, "right");  // Equip the item to the player's right hand (default)
                item.transform.SetParent(GameObject.FindWithTag("Player").transform);  // Parent the item to the player
                item.gameObject.SetActive(true);  // Make sure the item is visible in the game world

                Debug.Log("Item picked up.");
            }
            else
            {
                Debug.LogWarning("MotorSkills or Item is missing!");
            }
        }
    }
}
