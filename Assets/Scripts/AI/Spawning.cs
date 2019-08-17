using UnityEngine;
using System.Collections.Generic;

public class Spawning : MonoBehaviour
{
    private float nextWaveTimer = 0;
    private float nextWaveTime = 5;
    private int enemiesSpawned = 0;
    private int wave = 1;
    private float spawnTimer = 0;
    private float spawnRate = 0.1f;

    public List<GameObject> enemiesList = new List<GameObject>();
    public List<Vector2> spawnList = new List<Vector2>();
    public float[] spawnWeights;
    public GameObject[] enemyTypes;

    public float spawnHeight = 0.75f;

    private void Start()
    {
    }
    public void DestroyEntity(GameObject obj)
    {
        SceneHandler handler = GameObject.Find("SceneController").GetComponent<SceneHandler>();
        handler.enemiesKilled++;
        handler.score++;
        obj.GetComponent<Shatterer>().Shatter();
        enemiesList.Remove(obj);

    }
    void Update()
    {
        SceneHandler handler = GameObject.Find("SceneController").GetComponent<SceneHandler>();
        handler.timeSurvived += Time.deltaTime;
        spawnTimer = Time.deltaTime + spawnTimer;

        if (Input.GetKeyDown(KeyCode.W))
        {
            foreach(GameObject enemy in enemiesList)
            {
                enemy.GetComponent<AIController>().Root(1.5f);
            }
        }
        spawn();
    }

    public void Implode(Vector3 position, float radius)
    {
        foreach(GameObject obj in enemiesList)
            if ((obj.transform.position - position).magnitude <= radius)
                obj.GetComponent<AI>().Implode(position);
    }
    private GameObject chooseEnemy()
    {
        float choice = Random.Range(0f, 1f);
        float summation = 0;
        for (int i = 0; i < spawnWeights.Length; i++)
        {
            summation += spawnWeights[i];
            if (choice < summation)
                return enemyTypes[i];
        }
        return null;
    }
    private void spawn()
    {
        if(enemiesSpawned < numberEnemies(wave))
        {
            if (spawnTimer > spawnRate)
            {
                spawnTimer = 0;
                GameObject enemy = chooseEnemy();
                GameObject obj = Instantiate(enemy, generatePosition(), Quaternion.identity);
                enemiesList.Add(obj);
                enemiesSpawned++;
            }
        }
        else if(enemiesList.Count == 0)
        {
            nextWaveTimer += Time.deltaTime;
            if (nextWaveTimer > nextWaveTime)
            {
                SceneHandler handler = GameObject.Find("SceneController").GetComponent<SceneHandler>();
                handler.waveNumber++;
                wave++;
                spawnRate *= .75f;
                Debug.Log(wave);
                nextWaveTimer = 0;
                enemiesSpawned = 0;
                spawnTimer = 10;
            }

        }

    }

    private Vector3 generatePosition()
    {
        int rIndex = Random.Range(0, spawnList.Count);
        return new Vector3(spawnList[rIndex].x, spawnHeight, spawnList[rIndex].y);
        //float rCord = Random.Range(13f, 15f);
        //float thetaCord = Random.Range(0f, 2 * Mathf.PI);
        //float xCord = rCord * Mathf.Cos(thetaCord);
        //float zCord = rCord * Mathf.Sin(thetaCord);
        //return new Vector3(xCord, spawnHeight, zCord);
    }

    private int numberEnemies(int waveNumber)
    {
        return (waveNumber) * 24;
    }
}