using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FireControl : MonoBehaviour
{
    public Sprite plasma;
    public Sprite laser;
    public SceneController scene;
    private float cdirection;
    public bool spawnstart = false;
    public Information info;
    public GameObject ship;
    public GameObject ProjectileContainer;
    public AudioSource firesound;
    public Text weaponname;
    public float coolTime;
    public int weaponnum;
    Vector3 pos;
    Vector3 direction;

    private void FireBullet()
    {
        float ProjectileSpeed = 400f;
        var NewProjectile = new GameObject();
        NewProjectile.tag = "Projectile";
        var SpriteRenderer = NewProjectile.AddComponent<SpriteRenderer>();
        //SpritetoUse = Random.Range(0, 7);
        SpriteRenderer.sprite = plasma;
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
    private void FireLaser()
    {
        float ProjectileSpeed = 300f;
        var Laser1 = new GameObject();
        var Laser2 = new GameObject();
        var Laser3 = new GameObject();
        Laser1.tag = "Projectile";
        Laser2.tag = "Projectile";
        Laser3.tag = "Projectile";
        var SpriteRenderer = Laser1.AddComponent<SpriteRenderer>();
        SpriteRenderer.sprite = laser;
        SpriteRenderer = Laser2.AddComponent<SpriteRenderer>();
        SpriteRenderer.sprite = laser;
        SpriteRenderer = Laser3.AddComponent<SpriteRenderer>();
        SpriteRenderer.sprite = laser;


        //NewProjectile.transform.localScale = new Vector3(200, 200, 1);
        var ProjectileCollider = Laser1.AddComponent<BoxCollider2D>();
        ProjectileCollider.isTrigger = true;
        ProjectileCollider = Laser2.AddComponent<BoxCollider2D>();
        ProjectileCollider.isTrigger = true;
        ProjectileCollider = Laser3.AddComponent<BoxCollider2D>();
        ProjectileCollider.isTrigger = true;
        pos.z = -10;
        Laser1.transform.position = pos;
        Laser1.transform.SetParent(ProjectileContainer.transform);
        Laser2.transform.position = pos;
        Laser2.transform.SetParent(ProjectileContainer.transform);
        Laser3.transform.position = pos;
        Laser3.transform.SetParent(ProjectileContainer.transform);
        
        var r = Quaternion.Euler(ship.transform.rotation.x, ship.transform.rotation.y, -ship.transform.rotation.z);
        Laser1.transform.rotation = r;
        Laser1.AddComponent<Projectile>();
        var rb = Laser1.AddComponent<Rigidbody2D>();
        rb.isKinematic = true;
        direction = Laser1.transform.up;
        rb.velocity = ProjectileSpeed * direction;

        var r2 = Quaternion.Euler(ship.transform.rotation.x, ship.transform.rotation.y, -ship.transform.rotation.z-30f);
        Laser2.transform.rotation = r2;
        Laser2.AddComponent<Projectile>();
        var rb2 = Laser2.AddComponent<Rigidbody2D>();
        rb2.isKinematic = true;
        direction = Laser2.transform.up;
        rb2.velocity = ProjectileSpeed * direction;

        var r3 = Quaternion.Euler(ship.transform.rotation.x, ship.transform.rotation.y, -ship.transform.rotation.z+30f);
        Laser3.transform.rotation = r3;
        Laser3.AddComponent<Projectile>();
        var rb3 = Laser3.AddComponent<Rigidbody2D>();
        rb3.isKinematic = true;
        direction = Laser3.transform.up;
        rb3.velocity = ProjectileSpeed * direction;
    }

    public void Awake()
    {
        firesound = GameObject.FindGameObjectWithTag("fire").GetComponent<AudioSource>();
        info = GameObject.FindGameObjectWithTag("UI").GetComponent<Information>();
        scene = GameObject.FindGameObjectWithTag("UI").GetComponent<SceneController>();
        ship = GameObject.FindGameObjectWithTag("Ship");
        ProjectileContainer = new GameObject();
        weaponnum = 1;
    }

    void Update()
    {
        pos = ship.transform.position;
        direction = ship.transform.up;
        if (coolTime > 0)
        {
            coolTime -= Time.deltaTime;
        }
        if (Input.GetKey(KeyCode.Alpha1))
        {
            weaponname.text = "Plasma";
            weaponnum = 1;
        }
        if (Input.GetKey(KeyCode.Alpha2))
        {
            weaponname.text = "Spread Laser";
            weaponnum = 2;
        }
        if (Input.GetKey(KeyCode.Space) && coolTime <= 0f)
        {
            if (PlayerPrefs.GetInt("isSfxOn") == 1)
            {
                firesound.Play();
            }
            if(weaponnum == 1)
            {
                FireBullet();
                coolTime = 1f;
            }else if(weaponnum == 2)
            {
                FireLaser();
                coolTime = 2f;
            }
        }
    } 
}
