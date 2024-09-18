using UnityEngine;

// This class exists as a proxy to relay Animation Events to the MovementStateManager component.
// Since Animation Events can only call functions that are on the same GameObject as the Animator component,
// this script allows Animation Events on the child (or any GameObject with the Animator) to communicate with 
// the MovementStateManager, which may be located on a parent or another GameObject. It provides methods (Jumped and JumpForce) 
// that can be triggered by Animation Events, forwarding them to the corresponding functions in the MovementStateManager.
public class JumpEventProxy : MonoBehaviour
{
    private MovementStateManager movementStateManager;

    private void Awake()
    {
        movementStateManager = GetComponentInParent<MovementStateManager>();
    }

    public void Jumped() => movementStateManager.Jumped();

    public void JumpForce() => movementStateManager.JumpForce();
}
