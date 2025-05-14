using UnityEngine;

public class ItemPickup : MonoBehaviour
{
    private LimbSwapFunctionality limbSwapFunctionality; // Your MotorSkills script
    private bool playerInRange = false;

    private void Start()
    {
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        if (player != null)
        {
            limbSwapFunctionality = player.GetComponent<LimbSwapFunctionality>();
        }
        else
        {
            Debug.LogWarning("Player not found in scene!");
        }
    }

    private void Update()
    {
        if (playerInRange && Input.GetKeyDown(KeyCode.E))
        {
            Item thisItem = GetComponent<Item>();

            if (thisItem == null)
            {
                Debug.LogWarning("No Item component found on this GameObject!");
                return;
            }

            if (!limbSwapFunctionality.IsItemHeld(thisItem))
            {
                limbSwapFunctionality.EquipItemTo(thisItem, "right");
                thisItem.transform.SetParent(limbSwapFunctionality.transform);
            }
            else
            {
                Debug.Log("Item is already held.");
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerInRange = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerInRange = false;
        }
    }
}
