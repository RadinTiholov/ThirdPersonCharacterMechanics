using UnityEngine;

public class EnemyMovementManager : MonoBehaviour
{
    public GameObject player;       // Reference to the player
    public float moveSpeed = 1f;    // Speed of the enemy's movement
    public float stoppingDistance = 3f; // Distance to stop from the player

    private PlayerUIManager playerUIManager;
    private Animator enemyAnimator; // Reference to the enemy's Animator component
    private bool isMoving;          // Boolean to track if the enemy is moving

    private void Start()
    {
        playerUIManager = player.GetComponent<PlayerUIManager>();
        enemyAnimator = GetComponentInChildren<Animator>();  // Get the Animator component on the enemy
    }

    void Update()
    {
        if (playerUIManager.fighting)
        {
            // Calculate the direction vector from the enemy to the player
            Vector3 direction = player.transform.position - transform.position;

            // Calculate the distance between the enemy and the player
            float distance = direction.magnitude;

            // Rotate the enemy to face the player
            Quaternion lookRotation = Quaternion.LookRotation(direction);
            transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * 5f); // Smooth rotation

            // Check if the enemy should move
            if (distance > stoppingDistance)
            {
                isMoving = true;

                // Move the enemy smoothly towards the player using MoveTowards
                Vector3 targetPosition = player.transform.position - direction.normalized * stoppingDistance;
                transform.position = Vector3.MoveTowards(transform.position, targetPosition, moveSpeed * Time.deltaTime);
            }
            else
            {
                isMoving = false;
            }

            // Apply animation based on whether the enemy is moving or not
            enemyAnimator.SetBool("isMoving", isMoving);
        }
    }
}
