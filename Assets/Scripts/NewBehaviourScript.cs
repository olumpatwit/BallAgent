using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewBehaviourScript : MonoBehaviour
{
    int x = 5;
    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("x is currently" + 5); 
    }

    // Update is called once per frame
    void Update()
    {
        x += 1;
        Debug.Log("x is currently " + x);

    }

    void OnCollisionEnter(Collision collision)
    {        
        x+=2;

    }
}
