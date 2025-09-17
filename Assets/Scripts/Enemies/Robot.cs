using StarterAssets;
using UnityEngine;
using UnityEngine.AI;

public class Robot : MonoBehaviour
{
    // Reference to the player (FirstPersonController handles movement and input)
    FirstPersonController player;

    // NavMeshAgent component for pathfinding and movement
    NavMeshAgent agent;

    // Tag used to identify the player object
    const string PLAYER_STRING = "Player";

    void Awake()
    {
        // Cache the NavMeshAgent component
        agent = GetComponent<NavMeshAgent>();
    }

    void Start()
    {
        // Find the player in the scene (assumes there is only one FirstPersonController)
        player = FindFirstObjectByType<FirstPersonController>();
    }

    void Update()
    {
        // If player is gone (e.g., died), do nothing
        if (!player) return;

        // Continuously update the robot's destination toward the player
        agent.SetDestination(player.transform.position);
    }

    void OnTriggerEnter(Collider other)
    {
        // If the robot collides with the player
        if (other.CompareTag(PLAYER_STRING))
        {
            // Trigger robot self-destruction (likely deals damage via EnemyHealth)
            EnemyHealth enemyHealth = GetComponent<EnemyHealth>();
            enemyHealth.SelfDestruct();
        }
    }
}
