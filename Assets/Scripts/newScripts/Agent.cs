using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Agent : MonoBehaviour
{
    private Controller gameController;

    public float speed = 3f;

    public float minX = -10f;
    public float maxX = 10f;
    public float minZ = -10f;
    public float maxZ = 10f;
//
    void Start()
    {
        gameController = FindObjectOfType<Controller>();
    
    }

    void OnCollisionEnter(Collision collision)
    {
         gameController.HandleCollision(gameObject, collision.gameObject);
    }

    public static Vector3 RandomDirection()
    {
        float x = Random.Range(-1f, 1f);
        float z = Random.Range(-1f, 1f);
        return new Vector3(x, 0, z).normalized;
    }

    private void Update()
    {
        transform.Translate(Vector3.forward * speed * Time.deltaTime);

        if (transform.position.y < -1)
        {
            // Handle trap entry
            Debug.Log("agent enetrs trap");
            Controller.scores[1] -= 3;
            TeleportObjectToRandomLocation(RandomDirection());

        }

    }

    public void TeleportObjectToRandomLocation(Vector3 direction)
    {
        float x = Random.Range(minX, maxX);
        float z = Random.Range(minZ, maxZ);
        transform.position = new Vector3(x, 0.5f, z);
        direction = RandomDirection();
        transform.rotation = Quaternion.LookRotation(direction);
    }
}
