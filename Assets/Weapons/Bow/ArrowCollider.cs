using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ArrowCollider : MonoBehaviour
{

    public static int score = 0;
    public GameObject arenaFloor;
    // Start is called before the first frame update
    void Start()
    {
        arenaFloor = GameObject.Find("Terrain");
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag.Contains("EnemyHitbox"))
        {
            arenaFloor.GetComponent<Spawning>().DestroyEntity(other.gameObject.GetComponent<MainHitbox>().enemyGameObject.transform.parent.gameObject);
        }
        if (other.tag.Contains("playButton"))
        {
            GameObject.Find("SceneController").GetComponent<SceneHandler>().SwitchScene(SceneHandler.Action.Play);
        }
        if (other.tag.Contains("quitButton"))
        {
            GameObject.Find("SceneController").GetComponent<SceneHandler>().SwitchScene(SceneHandler.Action.Quit);
        }
    }
}
