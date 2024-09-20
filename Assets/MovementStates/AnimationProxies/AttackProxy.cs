using UnityEngine;

public class AttackProxy : MonoBehaviour
{
    private MovementStateManager movementStateManager;

    void Start()
    {
        movementStateManager = GetComponentInParent<MovementStateManager>();
    }

    public void Punch() => movementStateManager.Fight.TriggerMovingForward(movementStateManager, 1f);

    public void BackFromPunch() => movementStateManager.Fight.TriggerMovingForward(movementStateManager, -0.4f);

    public void Mavashi() => movementStateManager.Fight.TriggerMovingForward(movementStateManager, 0.6f);

    public void SetHitPosition() => CombatStateManager.Instance.weaponInHittingPosition = true;

    public void UnsetHitPosition() => CombatStateManager.Instance.weaponInHittingPosition = false;
}
