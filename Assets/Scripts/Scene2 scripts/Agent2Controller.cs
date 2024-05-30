using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Agent2Controller : MonoBehaviour
{
    public float moveSpeed = 6f; // Speed of the sphere's movement
    public float rotationSpeed = 2f; // Speed of rotation
    public float waypointChangeDistance = 0.1f; // Distance threshold to consider waypoint reached
    public GameObject waypointPrefab; // Prefab for waypoints
    public LayerMask obstacleLayer; // Layer mask for obstacle detection
    public GameObject exit;

    private Vector3[] waypoints = new Vector3[5]; // Array to store waypoint positions
    public int currentWaypointIndex = 0; // Index of the current waypoint target
    private Rigidbody rb;
    private bool obstacleReached = false;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        // Spawn waypoints
        SpawnWaypoints();
    }

    void FixedUpdate()
    {
        if (!obstacleReached)
        {
            // Move the sphere towards the current waypoint target
            MoveTowardsWaypoint();

            // Rotate the sphere based on its velocity
            RotateSphere();
        }
        else
        {
            /*// Move in the opposite direction if obstacle is reached
            MoveInOppositeDirection();*/

            MoveTowardsWaypoint();
        }
    }

    void SpawnWaypoints()
    {
        for (int i = 0; i < waypoints.Length - 1; i++)
        {
            // Randomly generate a position for each waypoint within the plane bounds
            Vector3 randomPosition = new Vector3(Random.Range(-9f, 1.5f), 0.5f, Random.Range(-8, 8f));
            // Instantiate the waypoint prefab at the random position
            Instantiate(waypointPrefab, randomPosition, Quaternion.identity);
            // Store the waypoint position in the array
            waypoints[i] = randomPosition;
        }
        // Now add the exit as the final waypoint
        waypoints[waypoints.Length - 1] = exit.transform.position;
    }

    void MoveTowardsWaypoint()
    {
        // Calculate direction towards the current waypoint
        Vector3 direction = waypoints[currentWaypointIndex] - transform.position;
        direction.y = 0f; // Ignore vertical component

        // Move the sphere towards the current waypoint
        rb.MovePosition(transform.position + direction.normalized * moveSpeed * Time.fixedDeltaTime);

        // Check if the sphere has reached the current waypoint
        if (direction.magnitude < waypointChangeDistance)
        {
            // Sphere reached the waypoint, rotate towards the next waypoint
            RotateTowardsNextWaypoint();
        }
    }

    void RotateTowardsNextWaypoint()
    {
        currentWaypointIndex = currentWaypointIndex + 1; // Move to the next waypoint
        if(currentWaypointIndex == waypoints.Length)
        {
            Debug.Log("We found the goal!");
            Destroy(this.gameObject);
            return;
        } else {
            Debug.Log("Moving to waypoint " + currentWaypointIndex);
        }
        Vector3 direction = waypoints[currentWaypointIndex] - transform.position;
        Quaternion targetRotation = Quaternion.LookRotation(direction);
        transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
        obstacleReached = false; // Reset obstacleReached flag
    }

    void MoveInOppositeDirection()
    {
        rb.velocity = -transform.forward * moveSpeed;
    }

    void RotateSphere()
    {
        if (rb.velocity != Vector3.zero)
        {
            // Calculate target rotation based on velocity direction
            Quaternion targetRotation = Quaternion.LookRotation(rb.velocity);

            // Smoothly rotate the sphere towards the target rotation
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Obstacle"))
        {
            // Collided with an obstacle, destroy it
            Destroy(other.gameObject);
            // Move towards the next waypoint
            RotateTowardsNextWaypoint();
        }
        else if (other.CompareTag("wall"))
        {

            // Collided with a wall, move in the opposite direction
            Vector3 oppositeDirection = -rb.velocity.normalized;
            rb.velocity = oppositeDirection * moveSpeed;
            obstacleReached = true; // Set obstacleReached flag to prevent continuous collision

        }
    }

}
