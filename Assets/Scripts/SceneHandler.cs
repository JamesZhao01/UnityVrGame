using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Rendering.PostProcessing;
using UnityEngine.UI;

public class SceneHandler : MonoBehaviour
{
    public static bool isInitiated = false;
    private AssetBundle loadedAsset;
    private string[] scenePaths;

    public Image image;
    private bool switchScene = false;
    public int waveNumber { get; set; } = 1;
    public int enemiesKilled { get; set; }
    public int score { get; set; }
    public float timeSurvived { get; set; }
    public bool firstRound { get; set; } = true;

    public enum Action
    {
        Play = 0, Quit = 1
    }
    public void SwitchScene(Action action)
    {
        switch(action)
        {
            case Action.Play:
                //switchScene = true;
                SceneManager.LoadScene("GameScene", LoadSceneMode.Single);
                break;
            case Action.Quit:
                Application.Quit();
                break;
        }

    }

    private void SceneSwitch()
    {

    }

    void Start()
    {

            

        if (!isInitiated) {
            isInitiated = true;
            DontDestroyOnLoad(gameObject);
        } else
        {

            Destroy(gameObject);
        }
    }

    public void Update()
    {
        if (Input.GetKeyDown("space"))
        {
            SwitchScene(Action.Play);
            Debug.Log("play");
        }
        //if(switchScene)
        //{
        //GameObject pp = GameObject.Find("PP");
        //PostProcessVolume ppv = pp.GetComponent<PostProcessVolume>();
        //ColorGrading cg = new ColorGrading();
        //ppv.profile.TryGetSettings(out cg);
        //Debug.Log(cg.lift.value);
        //}
    }
    public void Dead()
    {
        firstRound = false;
        SceneManager.LoadScene("StartScene", LoadSceneMode.Single);
    }

}
