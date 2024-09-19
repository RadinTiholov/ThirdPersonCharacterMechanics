using UnityEngine;

public class EnemyAnimationManager : MonoBehaviour
{
    [HideInInspector] public Animator anim;

    private void Awake()
    {
        anim = GetComponentInChildren<Animator>();
    }

    public void Hit() => anim.SetTrigger("Hit");
}
