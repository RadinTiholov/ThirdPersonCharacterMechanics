using UnityEngine;

public class RiffleWalkState : MovementBaseState
{
    public override void EnterState(MovementStateManager movement)
    {
        movement.anim.SetBool("RiffleWalk", true);
    }

    public override void UpdateState(MovementStateManager movement)
    {
        //if (Input.GetKey(KeyCode.LeftShift))
        //{
        //    ExitState(movement, movement.Run);
        //}
        //else if (Input.GetKeyDown(KeyCode.LeftControl))
        //{
        //    ExitState(movement, movement.Crouch);
        //}
        //else if (movement.dir.magnitude < 0.1f)
        //{
        //    ExitState(movement, movement.Idle);
        //}
        if (movement.dir.magnitude < 0.1f)
        {
            ExitState(movement, movement.Riffle);
        }

        if (movement.vInput < 0)
        {
            movement.currentMoveSpeed = movement.walkBackSpeed;
        }
        else
        {
            movement.currentMoveSpeed = movement.walkSpeed;
        }
        if (Input.GetMouseButtonDown(0))
        {
            movement.shootingManager.EnableMuzzle();
        }
        if (Input.GetMouseButtonUp(0))
        {
            movement.shootingManager.DesableMuzzle();
        }
        //if (Input.GetKeyDown(KeyCode.Space))
        //{
        //    movement.previousState = this;
        //    ExitState(movement, movement.Jump);
        //}
    }
    void ExitState(MovementStateManager movement, MovementBaseState state)
    {
        movement.anim.SetBool("RiffleWalk", false);
        movement.SwitchState(state);
    }
}
