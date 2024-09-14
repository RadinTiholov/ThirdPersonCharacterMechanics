public class FightState : MovementBaseState
{
    public override void EnterState(MovementStateManager movement)
    {
        movement.anim.SetBool("Fighting", true);
    }

    public override void UpdateState(MovementStateManager movement)
    {
        if (movement.vInput < 0)
        {
            movement.currentMoveSpeed = movement.fightBackSpeed;
        }
        else
        {
            movement.currentMoveSpeed = movement.fightSpeed;
        }
    }

    void ExitState(MovementStateManager movement, MovementBaseState state)
    {
        movement.anim.SetBool("Fighting", false);
        movement.SwitchState(state);
    }
}
