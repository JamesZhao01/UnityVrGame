using UnityEngine;
using System.Collections.Generic;

public class Spawning : MonoBehaviour
{
    private float nextWaveTimer = 0;
    private float nextWaveTime = 5;
    private int enemiesSpawned = 0;
    private int wave = 1;
    private float spawnTimer = 0;
    private float spawnRate = 4f;

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
        
        enemiesList.Remove(obj);
        Destroy(obj);

    }
    void Update()
    {
        spawnTimer = Time.deltaTime + spawnTimer;

        if(Input.GetKeyDown("space"))
        {
            Implode(new Vector3(2, 4, 2), 3);
        }
        spawn();
    }

    public void Implode(Vector3 position, float radius)
    {
        foreach(GameObject obj in enemiesList)
            if ((obj.transform.position - position).magnitude <= radius)
                obj.GetComponent<AIWarrior>().Implode(position);
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
        return (waveNumber) * 1;
    }
}