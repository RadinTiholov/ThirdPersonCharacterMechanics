using UnityEngine;

public class HeadCollision : MonoBehaviour
{
    public Animator anim;

    private void Start()
    {
        if (anim == null) 
        {
            Debug.Log("Please assign animtor!");
        }
    }

    // This method is called when another object enters a trigger collider attached to this object
    private void OnTriggerEnter(Collider other)
    {
        // Check if the other object is the player's weapon
        if (other.CompareTag("PlayerWeapon"))
        {
            if (CombatStateManager.Instance.weaponInHittingPosition) 
            {
                anim.SetTrigger("Hit");
            }
        }
    }
}
