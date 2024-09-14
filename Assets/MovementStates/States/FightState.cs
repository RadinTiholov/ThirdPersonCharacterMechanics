using UnityEditor;
using UnityEngine;
public class FightState : MovementBaseState
{
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
