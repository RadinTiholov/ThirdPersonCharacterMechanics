public class RiffleState : MovementBaseState
{
    public override void EnterState(MovementStateManager movement)
    {
        movement.anim.SetBool("Riffle", true);
    }

    public override void UpdateState(MovementStateManager movement)
    {
        throw new System.NotImplementedException();
    }
}
