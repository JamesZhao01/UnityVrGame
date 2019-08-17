using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class ScoreScript : MonoBehaviour
{
    public SceneHandler sceneHandler;
    public Text text;

    public void SetText(int score, int wave, int enemiesKilled, float timeSurvived)
    {
        text.text = "Score " + score + "\nWave " + wave + "\nEnemies Killed" + enemiesKilled + "\nTime Survived" + timeSurvived;
    }

    void Start()
    {
        if (sceneHandler.firstRound == false)
        {
            SetText(sceneHandler.score, sceneHandler.waveNumber, sceneHandler.enemiesKilled, sceneHandler.timeSurvived);
        }
    }
}
