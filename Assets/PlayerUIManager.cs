using UnityEngine;
using UnityEngine.Animations.Rigging;

public class PlayerUIManager : MonoBehaviour
{
    public GameObject interactionUIElement; // UI element shown when near the shop
    public GameObject startFightUIElement; // UI element shown when near the enemy
    public GameObject fightPanel; // UI element shown when fighting
    public GameObject rifflePanel; 
    public GameObject shop;        // The shop UI/GameObject to activate when "E" is pressed

    public bool fighting = false;

    private bool isNearShop = false;        // To track if the player is near the shop
    private bool isNearEnemy = false;
    private bool isNearItem = false;
    private bool isUIShopOpen = false;
    private bool isHoldingItem = false;

    private GameObject currentItem;

    AimStateManager aimStateManager;// Reference to call the enter and exit function for fight
    MovementStateManager movementStateManager;

    public RigBuilder rigBuilder;

    public GameObject akModel;

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

        if (fightPanel != null)
        {
            fightPanel.SetActive(false);
        }

        if (rifflePanel != null)
        {
            rifflePanel.SetActive(false);
        }

        if (shop != null)
        {
            shop.SetActive(false);
        }
    }

    void Update()
    {
        // Toggle Shop UI when near the shop and press "E"
        if (isNearShop && Input.GetKeyDown(KeyCode.E))
        {
            ToggleShop();
        }

        // Toggle Fight Mode when near enemy and press "Tab"
        if (isNearEnemy && Input.GetKeyDown(KeyCode.Tab))
        {
            if (!fighting)
            {
                EnterFightMode();
            }
            else
            {
                ExitFightMode();
            }
        }

        // Interact with Item when near and press "E"
        if (isNearItem && Input.GetKeyDown(KeyCode.E))
        {
            PickupItem();
        }

        if (isHoldingItem && Input.GetKeyDown(KeyCode.Tab)) 
        {
            DropItem();
        }
    }

    // Function to toggle the shop UI
    void ToggleShop()
    {
        if (shop == null) return;

        // Toggle shop active state
        shop.SetActive(!shop.activeSelf);

        // Update shop UI open status
        isUIShopOpen = shop.activeSelf;
    }

    // Function to enter fight mode
    void EnterFightMode()
    {
        Debug.Log("Fight");
        fighting = true;

        // Make narrow camera
        aimStateManager.EnterFightMode();

        // Switch to fight animation
        movementStateManager.SwitchState(movementStateManager.Fight);

        // Disable start fight UI and enable fight panel
        if (startFightUIElement != null)
        {
            startFightUIElement.SetActive(false);
        }

        if (fightPanel != null)
        {
            fightPanel.SetActive(true);
        }
    }

    // Function to exit fight mode
    void ExitFightMode()
    {
        Debug.Log("Stopped Fighting");
        fighting = false;

        // Exit narrow camera mode
        aimStateManager.ExitFightMode();

        // Switch to idle animation
        movementStateManager.SwitchState(movementStateManager.Idle);

        // Disable fight UI panel
        if (fightPanel != null)
        {
            fightPanel.SetActive(false);
        }
    }

    // Function to pick up the item and switch to rifle mode
    void PickupItem()
    {
        if (currentItem == null || interactionUIElement == null) return;

        // Deactivate the current item and interaction UI
        currentItem.SetActive(false);
        interactionUIElement.SetActive(false);

        // Switch to rifle mode
        aimStateManager.EnterRiffleMode();

        // Enable and build rig
        if (rifflePanel != null)
        {
            rifflePanel.SetActive(true);
        }

        rigBuilder.enabled = true;
        rigBuilder.Build();

        isHoldingItem = true;

        // Switch to rifle animation
        movementStateManager.SwitchState(movementStateManager.Riffle);
    }

    // Function to drop the item 
    void DropItem()
    {
        aimStateManager.ExitRiffleMode();

        if (rifflePanel != null)
        {
            rifflePanel.SetActive(false);
        }

        currentItem.transform.position = new Vector3(this.transform.position.x, 0, this.transform.position.z);
        currentItem.SetActive(true);

        rigBuilder.enabled = false;

        isHoldingItem = false;

        akModel.SetActive(false);

        movementStateManager.SwitchState(movementStateManager.Idle);
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
        if (other.CompareTag("Enemy") && !fighting) 
        {
            isNearEnemy = true;

            if (startFightUIElement != null)
            {
                startFightUIElement.SetActive(true);
            }
        }
        if (other.CompareTag("Item") && !fighting)
        {
            isNearItem = true;
            currentItem = other.gameObject;

            if (interactionUIElement != null)
            {
                interactionUIElement.SetActive(true);
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

        // For item
        if (other.CompareTag("Item"))
        {
            isNearItem = false;

            if (interactionUIElement != null)
            {
                interactionUIElement.SetActive(false);
            }
        }
    }
}
