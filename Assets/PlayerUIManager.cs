using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerUIManager : MonoBehaviour
{
    public GameObject interactionUIElement; // UI element shown when near the shop
    public GameObject startFightUIElement; // UI element shown when near the shop
    public GameObject shop;                 // The shop UI/GameObject to activate when "E" is pressed

    private bool isNearShop = false;        // To track if the player is near the shop
    private bool isNearEnemy = false;
    private bool isUIShopOpen = false;

    private bool isFightStarted = false;

    AimStateManager aimStateManager;// Reference to call the enter and exit function for fight
    MovementStateManager movementStateManager;
    void Start()
    {
        aimStateManager = GetComponent<AimStateManager>();
        movementStateManager = GetComponent<MovementStateManager>();
        // Make sure the interaction UI is hidden at the start
        if (interactionUIElement != null)
        {
            interactionUIElement.SetActive(false);
        }

        if (startFightUIElement != null)
        {
            startFightUIElement.SetActive(false);
        }

        // Ensure the shop is inactive at the start
        if (shop != null)
        {
            shop.SetActive(false);
        }
    }

    void Update()
    {
        // If the player is near the shop and presses "E", toggle the shop UI
        if (isNearShop && Input.GetKeyDown(KeyCode.E))
        {
            if (shop != null)
            {
                // Toggle the shop GameObject's active state
                shop.SetActive(!shop.activeSelf);

                // Update the state of isUIShopOpen accordingly
                isUIShopOpen = shop.activeSelf;
            }
        }

        if (isNearEnemy && Input.GetKeyDown(KeyCode.Tab)) 
        {
            Debug.Log("Fight");
            aimStateManager.EnterFightMode();

            startFightUIElement.SetActive(false);
            isFightStarted = true;

            movementStateManager.SwitchState(movementStateManager.Fight);
        }
    }


    // This function is called when the player enters a trigger collider
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Shop"))
        {
            isNearShop = true; // Player is near the shop

            // Activate the interaction UI element
            if (interactionUIElement != null)
            {
                interactionUIElement.SetActive(true);
            }
        }
        if (other.CompareTag("Enemy") && !isFightStarted) 
        {
            isNearEnemy = true;

            if (startFightUIElement != null)
            {
                startFightUIElement.SetActive(true);
            }
        }
    }

    // This function is called when the player exits a trigger collider
    private void OnTriggerExit(Collider other)
    {
        // Check if the object we collided with has the tag "Shop"
        if (other.CompareTag("Shop"))
        {
            isNearShop = false; // Player is no longer near the shop

            // Deactivate the interaction UI element
            if (interactionUIElement != null)
            {
                interactionUIElement.SetActive(false);
            }

            // Optionally, deactivate the shop UI when leaving the trigger zone
            if (shop != null)
            {
                shop.SetActive(false);
            }
        }

        // For enemy
        if (other.CompareTag("Enemy"))
        {
            isNearEnemy = false;

            if (startFightUIElement != null)
            {
                startFightUIElement.SetActive(false);
            }
        }
    }
}
