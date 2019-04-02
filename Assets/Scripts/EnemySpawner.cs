using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public List<Sprite> spritelist = new List<Sprite>();
    public SceneController scene;
    public float AsteroidSpeed;
    private float cdirection;
    public bool spawnstart = false;
    public int SpritetoUse;
    public Information info;
    public GameObject prefab;
    
    public GameObject AsteroidContainer;

    private void InsAsteroid()
    {
        var NewAsteroid = new GameObject();
        NewAsteroid.tag = "Asteroid";
        var SpriteRenderer = NewAsteroid.AddComponent<SpriteRenderer>();
        SpritetoUse = Random.Range(0, 7);
        SpriteRenderer.sprite = spritelist[SpritetoUse];
        var AsteroidCollider = NewAsteroid.AddComponent<BoxCollider2D>();
        AsteroidCollider.isTrigger = true;
        Vector3 pos;
        Vector3 direction;
        int SpawnSide = Random.Range(0, 4);
        //top
        if (SpawnSide == 0)
        {
            pos = new Vector3(Random.Range(0f, Screen.width), Screen.height, -1f);
            direction = new Vector3(Random.Range(-1f, 1f), -1, 0);
        }
        //bottom
        else if (SpawnSide == 1)
        {
            pos = new Vector3(Random.Range(0f, Screen.width), 0f, -1f);
            direction = new Vector3(Random.Range(-1f, 1f),1,0);
        }
        //left
        else if (SpawnSide == 2)
        {
            pos = new Vector3(0f, Random.Range(0f, Screen.height), -1f);
            direction = new Vector3(1, Random.Range(-1f, 1f),0);
        }
        //right
        else
        {
            pos = new Vector3(Screen.width, Random.Range(0f, Screen.height), -1f);
            direction = new Vector3(-1, Random.Range(-1f, 1f),0);
        }
        NewAsteroid.transform.position = pos;
        NewAsteroid.transform.SetParent(AsteroidContainer.transform);
        var ascript = NewAsteroid.AddComponent<Asteroid>();
        ascript.explosionPre = prefab;
        var rb = NewAsteroid.AddComponent<Rigidbody2D>();
        rb.gravityScale = 0;
        rb.velocity = AsteroidSpeed * direction;
    }
   
    public void Awake()
    {
        info = GameObject.FindGameObjectWithTag("UI").GetComponent<Information>();
        scene = GameObject.FindGameObjectWithTag("UI").GetComponent<SceneController>();
        AsteroidContainer = new GameObject();
    }

    IEnumerator RestartSpawning()
    {
        yield return new WaitForSeconds(3.0f);
        info.clearscreen = false;
        InvokeRepeating("InsAsteroid", 0.5f, 0.5f);
    }

    void Update()
    {
        if (scene.isGameStart)
        {
            if (!spawnstart)
            {
                InvokeRepeating("InsAsteroid", 0.5f, 0.5f);
                spawnstart = true;
            }
            else
            {
                if (info.spawnenemyagain)
                {
                    CancelInvoke();
                    StartCoroutine(RestartSpawning());
                    info.spawnenemyagain = false;
                }
            }
        }
        else
        {
            if (spawnstart)
            {
                spawnstart = false;
            }
            CancelInvoke();
        }
    }
}
