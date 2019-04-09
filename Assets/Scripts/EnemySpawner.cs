using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    
    public SceneController scene;
    public Information info;
    public GameObject prefab;
    public GameObject AsteroidContainer;
    public GameObject SmallAsteroid;
    public GameObject HugeAsteroid;
    public List<Sprite> spritelist = new List<Sprite>();

    public bool spawnstart = false;
    private void SpawnSmall()
    {
        Vector3 pos;
        Vector3 direction;
        int SpritetoUse = Random.Range(0, 3);
        float EnemySpeed = 200f;
        SmallAsteroid = new GameObject();
        SmallAsteroid.name = "SmallAsteroid";
        SmallAsteroid.tag = "SmallAsteroid";

        var SpriteRenderer = SmallAsteroid.AddComponent<SpriteRenderer>();
        SpriteRenderer.sprite = spritelist[SpritetoUse];

        var AsteroidCollider = SmallAsteroid.AddComponent<BoxCollider2D>();
        AsteroidCollider.isTrigger = true;


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
        SmallAsteroid.transform.position = pos;

        SmallAsteroid.transform.SetParent(AsteroidContainer.transform);

        var ascript = SmallAsteroid.AddComponent<SmallAsteroid>();
        ascript.explosionPre = prefab;

        var rb = SmallAsteroid.AddComponent<Rigidbody2D>();
        rb.gravityScale = 0;
        rb.velocity = EnemySpeed * direction;
    }

    private void SpawnHuge()
    {
        Vector3 pos;
        Vector3 direction;
        int SpritetoUse = Random.Range(4, 6);
        float EnemySpeed = 150f;
        HugeAsteroid = new GameObject();
        HugeAsteroid.name = "HugeAsteroid";
        HugeAsteroid.tag = "HugeAsteroid";

        var SpriteRenderer = HugeAsteroid.AddComponent<SpriteRenderer>();
        SpriteRenderer.sprite = spritelist[SpritetoUse];

        var AsteroidCollider = HugeAsteroid.AddComponent<BoxCollider2D>();
        AsteroidCollider.isTrigger = true;
        
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
            direction = new Vector3(Random.Range(-1f, 1f), 1, 0);
        }
        //left
        else if (SpawnSide == 2)
        {
            pos = new Vector3(0f, Random.Range(0f, Screen.height), -1f);
            direction = new Vector3(1, Random.Range(-1f, 1f), 0);
        }
        //right
        else
        {
            pos = new Vector3(Screen.width, Random.Range(0f, Screen.height), -1f);
            direction = new Vector3(-1, Random.Range(-1f, 1f), 0);
        }


        HugeAsteroid.transform.position = pos;

        HugeAsteroid.transform.SetParent(AsteroidContainer.transform);


        var ascript = HugeAsteroid.AddComponent<HugeAsteroid>();
        ascript.explosionPre = prefab;
        ascript.spritelist = spritelist;

        var rb = HugeAsteroid.AddComponent<Rigidbody2D>();
        rb.gravityScale = 0;
        rb.velocity = EnemySpeed * direction;
    }

    public void Awake()
    {
        info = GameObject.FindGameObjectWithTag("UI").GetComponent<Information>();
        scene = GameObject.FindGameObjectWithTag("UI").GetComponent<SceneController>();
        AsteroidContainer = new GameObject();
        AsteroidContainer.name = "AsteroidContainer";
    }

    IEnumerator RestartSpawning()
    {
        yield return new WaitForSeconds(3.0f);
        info.clearscreen = false;
        InvokeRepeating("SpawnSmall", 1f, 1f);
        InvokeRepeating("SpawnHuge", 3f, 3f);
    }

    void Update()
    {
        if (scene.isGameStart)
        {
            if (!spawnstart)
            {
                InvokeRepeating("SpawnSmall", 1f, 1f);
                InvokeRepeating("SpawnHuge", 3f, 3f);
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
