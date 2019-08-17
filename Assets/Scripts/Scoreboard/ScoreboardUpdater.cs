using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreboardUpdater : MonoBehaviour
{
    public SceneHandler sceneHandler;
    public Text text;
    // Start is called before the first frame update
    void Start()
    {
        text = gameObject.GetComponent<Text>();
        sceneHandler = GameObject.Find("SceneController").GetComponent<SceneHandler>();
    }

    // Update is called once per frame
    void Update()
    {
        string str = "Score: " + sceneHandler.score + "\nWave: " + sceneHandler.waveNumber + "\nEnemies Defeated: " + sceneHandler.enemiesKilled;
        text.text = str;
    }
}
