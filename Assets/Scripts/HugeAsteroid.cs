using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HugeAsteroid : MonoBehaviour
{
    public AudioSource DestroySound;
    public Information info;
    public SceneController scene;
    public GameObject ship;
    public GameObject explosionPre;
    public GameObject SmallAsteroid;
    public List<Sprite> spritelist;
    Vector3 pos;

    void Awake()
    {
        DestroySound = GameObject.FindGameObjectWithTag("destroy").GetComponent<AudioSource>();
        scene = GameObject.FindGameObjectWithTag("UI").GetComponent<SceneController>();
        info = GameObject.FindGameObjectWithTag("UI").GetComponent<Information>();
        ship = GameObject.FindGameObjectWithTag("Ship");
    }

    public void split()
    {
        int SpritetoUse = Random.Range(0, 3);
        float EnemySpeed = 300f;
        SmallAsteroid = new GameObject();
        SmallAsteroid.name = "SmallAsteroid";
        SmallAsteroid.tag = "SmallAsteroid";

        var SpriteRenderer = SmallAsteroid.AddComponent<SpriteRenderer>();
        SpriteRenderer.sprite = spritelist[SpritetoUse];

        var AsteroidCollider = SmallAsteroid.AddComponent<BoxCollider2D>();
        AsteroidCollider.isTrigger = true;

        SmallAsteroid.transform.position = this.transform.position;
        SmallAsteroid.transform.rotation = this.transform.rotation;
        var ascript = SmallAsteroid.AddComponent<SmallAsteroid>();
        ascript.explosionPre = explosionPre;

        var rb = SmallAsteroid.AddComponent<Rigidbody2D>();
        rb.gravityScale = 0;
        rb.velocity = EnemySpeed * new Vector3(Random.Range(-1f, 1f), Random.Range(-1f, 1f), 0);
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Ship")
        {
            if (PlayerPrefs.GetInt("isSfxOn") == 1)
            {
                DestroySound.Play();
            }
            Instantiate(explosionPre, transform.position, Quaternion.identity);
            info.loselife();
            info.updatelife();
            info.respawning = true;
            info.clearscreen = true;
            Destroy(this.gameObject);
        }
        else if (collision.gameObject.tag == "Projectile")
        {
            if (PlayerPrefs.GetInt("isSfxOn") == 1)
            {
                DestroySound.Play();
            }
            Instantiate(explosionPre, transform.position, Quaternion.identity);
            split();
            split();
            info.addscore();
            info.addscore();
            info.addscore();
            info.updatescore();
            Destroy(this.gameObject);
        }
    }

    // Update is called once per frame
    void Update()
    {
        pos = transform.position;
        if (!scene.isGameStart)
        {
            Instantiate(explosionPre, transform.position, Quaternion.identity);
            Destroy(this.gameObject);
        }
        else if (pos.x > Screen.width || pos.x < 0
           || pos.y > Screen.height || pos.y < 0)
        {
            Destroy(this.gameObject);
        }
        else if (info.clearscreen == true)
        {
            Instantiate(explosionPre, transform.position, Quaternion.identity);
            Destroy(this.gameObject);
        }
    }
}
