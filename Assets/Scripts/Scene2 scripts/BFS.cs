using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class BFS : MonoBehaviour
{

    public float moveSpeed = 2f; // Speed of the sphere's movement
    public float rotationSpeed = 2f; // Speed of rotation
    public GameObject waypointPrefab; // Prefab for waypoints

    private List<Vector3> waypoints = new List<Vector3>(); // List to store waypoint positions
    private int currentWaypointIndex = 0; // Index of the current waypoint target
    private Rigidbody rb;
    private Queue<Vector3> path = new Queue<Vector3>(); // Queue to store BFS path

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        // Spawn waypoints and build graph
        BuildGraph();
        // Find BFS path from start to end waypoint
        FindPathBFS(transform.position, waypoints[currentWaypointIndex]);
    }

    void FixedUpdate()
    {
        if (path.Count > 0)
        {
            // Move towards the next waypoint in the path
            MoveTowardsWaypoint(path.Peek());
        }
    }

    void BuildGraph()
    {
        // Spawn waypoints and add their positions to the list
        for (int i = 0; i < 10; i++)
        {
            Vector3 randomPosition = new Vector3(Random.Range(-9.5f, 9.5f), 0.5f, Random.Range(-9.5f, 9.5f));
            Instantiate(waypointPrefab, randomPosition, Quaternion.identity);
            waypoints.Add(randomPosition);
        }
    }

    void FindPathBFS(Vector3 start, Vector3 end)
    {
        // Perform BFS traversal to find the shortest path from start to end waypoint
        Queue<Vector3> frontier = new Queue<Vector3>();
        Dictionary<Vector3, Vector3> cameFrom = new Dictionary<Vector3, Vector3>();

        frontier.Enqueue(start);
        cameFrom[start] = start;

        while (frontier.Count > 0)
        {
            Vector3 current = frontier.Dequeue();
            if (current == end)
            {
                // Reconstruct path
                while (current != start)
                {
                    path.Enqueue(current);
                    current = cameFrom[current];
                }
                path.Enqueue(start);
                path.Reverse();
                break;
            }

            // Explore neighbors
            foreach (Vector3 neighbor in GetNeighbors(current))
            {
                if (!cameFrom.ContainsKey(neighbor))
                {
                    frontier.Enqueue(neighbor);
                    cameFrom[neighbor] = current;
                }
            }
        }
    }

    List<Vector3> GetNeighbors(Vector3 position)
    {
        // Get neighboring waypoints within a certain radius
        List<Vector3> neighbors = new List<Vector3>();
        foreach (Vector3 waypoint in waypoints)
        {
            if (Vector3.Distance(position, waypoint) < 5f && position != waypoint)
            {
                neighbors.Add(waypoint);
            }
        }
        return neighbors;
    }

    void MoveTowardsWaypoint(Vector3 target)
    {
        // Calculate direction towards the target waypoint
        Vector3 direction = target - transform.position;
        direction.y = 0f; // Ignore vertical component

        // Move the sphere towards the target waypoint
        rb.MovePosition(transform.position + direction.normalized * moveSpeed * Time.fixedDeltaTime);

        // Check if the sphere has reached the target waypoint
        if (direction.magnitude < 0.1f)
        {
            // Remove the reached waypoint from the path
            path.Dequeue();
            if (path.Count > 0)
            {
                // Find BFS path to the next waypoint
                FindPathBFS(transform.position, path.Peek());
            }
        }

        // Rotate the sphere towards the target waypoint
        Quaternion targetRotation = Quaternion.LookRotation(direction);
        transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
    }
}
