using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class EndGame : MonoBehaviour
{
    public static EndGame Instance{get;private set;}
    public Text winnerName;
    public Button restart;
    public Button quitGame;
    void Awake() {
        if(Instance!= null && Instance==this)
        {
            Destroy(this);
        }
        Instance = this;
        restart.onClick.AddListener(Restart);
        quitGame.onClick.AddListener(QuitGame);
    }
    void Restart()
    {
        SceneManager.LoadScene("SampleScene");
    }
    
    void QuitGame()
    {
        Application.Quit();
    }
}
