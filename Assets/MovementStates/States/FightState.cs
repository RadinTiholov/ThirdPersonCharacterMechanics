using UnityEngine;
public class FightState : MovementBaseState
{
    private float attackMoveDuration = 0.2f; // Duration of the forward movement (in seconds)
    private float attackMoveTimer = 0f; // Timer to track the movement
    private bool isMovingForward = false; // Flag to indicate whether the character is moving forward
    private Vector3 attackMoveTarget; // The target position to move towards

    public override void EnterState(MovementStateManager movement)
    {
        // Set the "Fighting" animation state to true
        movement.anim.SetBool("Fighting", true);
    }

    public override void UpdateState(MovementStateManager movement)
    {
        // Set movement speed based on the vertical input
        if (movement.vInput < 0)
        {
            movement.currentMoveSpeed = movement.fightBackSpeed;
        }
        else
        {
            movement.currentMoveSpeed = movement.fightSpeed;
        }

        // When the mouse button is pressed (attack starts)
        if (Input.GetMouseButtonDown(0))
        {
            // Set "Attacking" to true
            movement.anim.SetBool("Attacking", true);

            // Start moving forward smoothly
            isMovingForward = true;
            attackMoveTimer = 0f;

            // Define the target forward position (e.g., 1 unit forward)
            attackMoveTarget = movement.transform.position + movement.transform.forward * 1f;
        }

        // Smooth forward movement while attacking
        if (isMovingForward)
        {
            // Update the timer
            attackMoveTimer += Time.deltaTime;

            // Calculate the interpolation factor (0 to 1)
            float t = attackMoveTimer / attackMoveDuration;

            // Move the character smoothly towards the target
            Vector3 newPosition = Vector3.Lerp(movement.transform.position, attackMoveTarget, t);
            movement.controller.Move(newPosition - movement.transform.position);

            // Stop the forward movement after the duration is complete
            if (attackMoveTimer >= attackMoveDuration)
            {
                isMovingForward = false;
            }
        }

        // When the mouse button is released (attack ends)
        if (Input.GetMouseButtonUp(0))
        {
            // Set "Attacking" to false
            movement.anim.SetBool("Attacking", false);
        }
    }

    public void ExitState(MovementStateManager movement, MovementBaseState state)
    {
        // Reset the "Fighting" animation state to false when exiting
        movement.anim.SetBool("Fighting", false);

        // Reset the "Attacking" state to false when exiting
        movement.anim.SetBool("Attacking", false);

        // Switch to the new state
        movement.SwitchState(state);
    }
}
