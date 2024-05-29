using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class Sphere : MonoBehaviour
{
    int[] scores = { 0, 0, 0, 0 };
    int counter = 0;

    
    float speed = 3.0f;
    //int ncol = 0;


    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (transform.position.y < -1)
        {
            UnityEditor.EditorApplication.isPlaying = false;
        }

        //  transform.Translate(Vector3.forward * speed * Time.deltaTime);

        /*   if (Input.GetKeyDown(KeyCode.Space))
           {
               Debug.Log("Space key pressed - Jump action triggered");
           }*/

    }

    void FixedUpdate()
    {
        Vector3 direction = new Vector3(Input.GetAxis("Horizontal"), 0f, Input.GetAxis("Vertical")).normalized;
        GetComponent<Rigidbody>().MovePosition(transform.position + direction * speed * Time.fixedDeltaTime);
    }

    void OnCollisionEnter(Collision collision)
    {
        // Check if the object colliding is the one you are interested in by tag, name, etc.
        if (collision.collider.tag == "gold")
        {
            Debug.Log("Collision detected with cube!");
            scores[0] += 1;
            counter++;
            // increase associated cube score
            // Add additional logic here for what happens on collision
        }

        if (collision.collider.tag == "poison")
        {
            Debug.Log("Collision detected with poison!");
            scores[1] -= 1;
            counter += 1;
        }
        //....
        if (counter == 10) {
            int totalScore = scores[1] + scores[2];
            Debug.Log("you scored  " + totalScore);
        }
    }
}