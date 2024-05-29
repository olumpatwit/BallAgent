using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Controller : MonoBehaviour
{
    public static int[] scores = { 0, 0, 0, 0 };
    public static int counter = 0;


    // Start is called before the first frame update
    public GameObject player;
    public GameObject agent;
    public GameObject wall;

    //public int score = 0;


    private Vector3 randomDirection;

    void Start()
    {
    }

    void Update()
    {
        
    } 

    public void HandleCollision(GameObject obj, GameObject collidedObj)
    {
        if (obj == agent)
        {
            if (collidedObj.CompareTag("poison"))
            {
                scores[1] -= 2;
                counter++;
                Destroy(collidedObj);
                Debug.Log("Sphere collides with poison!");
             //   Agent.TeleportObjectToRandomLocation(RandomDirection());

            }
            else if (collidedObj.CompareTag("gold"))
            {
                scores[1] += 2;
                counter++;
                Destroy(collidedObj);
                Debug.Log("Sphere collides with gold!");
               // Agent.TeleportObjectToRandomLocation(Agent.RandomDirection());

            }
        }
 


        if (obj == player)
        {
            if (collidedObj.CompareTag("poison"))
            {
                scores[0] -= 2;
                counter++;
                Destroy(collidedObj);
                Debug.Log("Sphere collides with poison!");
              //  Player.TeleportObjectToRandomLocation(RandomDirection());

            }
            else if (collidedObj.CompareTag("gold"))
            {
                scores[0] += 2;
                counter++;
                Destroy(collidedObj);
                Debug.Log("Sphere collides with gold!");
               // Player.TeleportObjectToRandomLocation(RandomDirection());

            }
        }
      

      

    }

    
}
