using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireControl : MonoBehaviour
{
    public Sprite mysprite;
    //public List<Sprite> spritelist = new List<Sprite>();
    public SceneController scene;
    public float ProjectileSpeed;
    private float cdirection;
    public bool spawnstart = false;
    //public int SpritetoUse;
    public Information info;
    public GameObject ship;
    public GameObject ProjectileContainer;
    public AudioSource firesound;
    public float coolTime;
    Vector3 pos;
    Vector3 direction;

    private void Fire()
    {
        var NewProjectile = new GameObject();
        NewProjectile.tag = "Projectile";
        var SpriteRenderer = NewProjectile.AddComponent<SpriteRenderer>();
        //SpritetoUse = Random.Range(0, 7);
        SpriteRenderer.sprite = mysprite;
        NewProjectile.transform.localScale = new Vector3(200, 200, 1);
        var ProjectileCollider = NewProjectile.AddComponent<BoxCollider2D>();
        ProjectileCollider.isTrigger = true;
        pos.z = -10;
        NewProjectile.transform.position = pos;
        NewProjectile.transform.SetParent(ProjectileContainer.transform);
        direction = ship.transform.up;
        //TO BE FIXED
        var r = Quaternion.Euler(ship.transform.rotation.x, ship.transform.rotation.y, -ship.transform.rotation.z);
        NewProjectile.transform.rotation = r;
        NewProjectile.AddComponent<Projectile>();
        var rb = NewProjectile.AddComponent<Rigidbody2D>();
        rb.isKinematic = true;
        rb.velocity = ProjectileSpeed * direction;
        
    }

    public void Awake()
    {
        firesound = GameObject.FindGameObjectWithTag("fire").GetComponent<AudioSource>();
        info = GameObject.FindGameObjectWithTag("UI").GetComponent<Information>();
        scene = GameObject.FindGameObjectWithTag("UI").GetComponent<SceneController>();
        ship = GameObject.FindGameObjectWithTag("Ship");
        ProjectileContainer = new GameObject();
    }

    void Update()
    {
        pos = ship.transform.position;
        direction = ship.transform.up;
        if (coolTime > 0)
        {
            coolTime -= Time.deltaTime;
        }
        if (Input.GetKey(KeyCode.Space) && coolTime <= 0f)
        {
            if (PlayerPrefs.GetInt("isSfxOn") == 1)
            {
                firesound.Play();
            }
            Fire();
            coolTime = 0.5f;
        }
    } 
}
