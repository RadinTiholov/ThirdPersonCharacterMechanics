using UnityEngine;

public class RiffleState : MovementBaseState
{
    public override void EnterState(MovementStateManager movement)
    {
        movement.anim.SetBool("Riffle", true);
    }

    public override void UpdateState(MovementStateManager movement)
    {
        if (movement.dir.magnitude > 0.1f)
        {

            movement.SwitchState(movement.RiffleWalk);
            //if (Input.GetKey(KeyCode.LeftShift))
            //{
            //    movement.SwitchState(movement.Run);
            //}
            //else
            //{
            //    movement.SwitchState(movement.Walk);
            //}
        }
        //if (Input.GetKeyDown(KeyCode.LeftControl))
        //{
        //    movement.SwitchState(movement.Crouch);
        //}
        //if (Input.GetKeyDown(KeyCode.Space))
        //{
        //    movement.previousState = this;
        //    movement.SwitchState(movement.Jump);
        //}
        if (Input.GetMouseButtonDown(0))
        {
            movement.shootingManager.EnableMuzzle();
        }
        if (Input.GetMouseButtonUp(0))
        {
            movement.shootingManager.DesableMuzzle();
        }
    }
}
