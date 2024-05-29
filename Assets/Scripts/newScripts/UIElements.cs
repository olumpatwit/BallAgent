using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIElements : MonoBehaviour
{

    public Text AgentScore;
    public Text PlayerScore;


    public Button endGame;
    // Start is called before the first frame update
    void Start()
    {
        //endGame.onClick.AddListener(EndGame);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void updateScore()
    {
        PlayerScore.text = "Player scores " + Controller.scores[0]; 
    }


}
