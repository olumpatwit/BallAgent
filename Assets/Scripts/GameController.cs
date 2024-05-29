using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    public GameObject sphere;
    
    private Vector3   randomPosition;

    private Vector3 randomRotation;


    public float minX = -10f;
    public float maxX = 10f;
    public float minZ = -10f;
    public float maxZ = 10f;


    

    int[] scores = { 0, 0, 0, 0 };
    int counter = 0;


    float speed = 3.0f;
    //int ncol = 0;


    // Start is called before the first frame update
    void Start()
    {
        TeleportSphereToRandomLocation();

        randomPosition = GetRandomPosition();

    }

    // Update is called once per frame
    void Update()
    {

        sphere.transform.Translate(randomPosition * speed * Time.deltaTime);

        if (sphere.transform.position.y < 0)
        {
            //UnityEditor.EditorApplication.isPlaying = false;
            // sphere.transform.position = new Vector3(1.0f, 1.0f, 1.0f);
            TeleportSphereToRandomLocation();
        }

       
    }

    void FixedUpdate()
    {
        /* Vector3 direction = new Vector3(Input.GetAxis("Horizontal"), 0f, Input.GetAxis("Vertical")).normalized;
         GetComponent<Rigidbody>().MovePosition(transform.position + direction * speed * Time.fixedDeltaTime);*/
    }

    void OnCollisionEnter(Collision collision)
    {
        // Check if the object colliding is the one you are interested in by tag, name, etc.
        if (collision.collider.tag == "gold")
        {
            Debug.Log("Collision detected with gold!");
            scores[0] += 1;
            counter++;
            // increase associated cube score
            // Add additional logic here for what happens on collision
        }

        if (collision.collider.tag == "poison")
        {
            Debug.Log("Collision detected with poison!");
            Debug.Log("Collision detected with gold!");
            scores[1] -= 1;
            counter += 1;
        }
        //....
        if (counter == 10)
        {
            int totalScore = scores[1] + scores[2];
            Debug.Log("Player scored  " + totalScore);
        }
    }


   



    void TeleportSphereToRandomLocation()
    {
        float x = Random.Range(minX, maxX);
        float z = Random.Range(minZ, maxZ);
        sphere.transform.position = new Vector3(x, 1.0f, z);
        randomRotation = new Vector3(Random.Range(-1f, 1f), 0, Random.Range(-1f, 1f)).normalized;
        sphere.transform.rotation = Quaternion.LookRotation(randomRotation);
    }

    

    Vector3 GetRandomPosition()
    {
        float x = Random.Range(-1f, 1f);
        float z = Random.Range(-1f, 1f);
        return new Vector3(x, 0, z).normalized;
    }
}
