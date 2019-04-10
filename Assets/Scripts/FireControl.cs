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
    public GameObject laserpre;
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

        var r = Quaternion.Euler(0, 0, ship.transform.rotation.z);
        var b = Instantiate(laserpre, ship.transform.position, r);
        b.GetComponent<Rigidbody2D>().velocity = ProjectileSpeed * b.transform.up;



        r = Quaternion.Euler(0, 0, ship.transform.rotation.z + 45f);
        b = Instantiate(laserpre, ship.transform.position, r);
        b.GetComponent<Rigidbody2D>().velocity = ProjectileSpeed * b.transform.up;


        r = Quaternion.Euler(0, 0, ship.transform.rotation.z - 45f);
        b = Instantiate(laserpre, ship.transform.position, r);
        b.GetComponent<Rigidbody2D>().velocity = ProjectileSpeed * b.transform.up;

        r = Quaternion.Euler(0, 0, ship.transform.rotation.z + 90f);
        b = Instantiate(laserpre, ship.transform.position, r);
        b.GetComponent<Rigidbody2D>().velocity = ProjectileSpeed * b.transform.up;

        r = Quaternion.Euler(0, 0, ship.transform.rotation.z - 90f);
        b = Instantiate(laserpre, ship.transform.position, r);
        b.GetComponent<Rigidbody2D>().velocity = ProjectileSpeed * b.transform.up;

        r = Quaternion.Euler(0, 0, ship.transform.rotation.z - 135f);
        b = Instantiate(laserpre, ship.transform.position, r);
        b.GetComponent<Rigidbody2D>().velocity = ProjectileSpeed * b.transform.up;

        r = Quaternion.Euler(0, 0, ship.transform.rotation.z + 135f);
        b = Instantiate(laserpre, ship.transform.position, r);
        b.GetComponent<Rigidbody2D>().velocity = ProjectileSpeed * b.transform.up;

        r = Quaternion.Euler(0, 0, ship.transform.rotation.z - 180f);
        b = Instantiate(laserpre, ship.transform.position, r);
        b.GetComponent<Rigidbody2D>().velocity = ProjectileSpeed * b.transform.up;



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
                coolTime = 5f;
            }
        }
    } 
}
