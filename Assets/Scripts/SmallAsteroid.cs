using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SmallAsteroid : MonoBehaviour
{
    public AudioSource DestroySound;
    public Information info;
    public SceneController scene;
    public GameObject ship;
    public GameObject explosionPre;
    Vector3 pos;

    void Awake()
    {
        DestroySound = GameObject.FindGameObjectWithTag("destroy").GetComponent<AudioSource>();
        scene = GameObject.FindGameObjectWithTag("UI").GetComponent<SceneController>();
        info = GameObject.FindGameObjectWithTag("UI").GetComponent<Information>();
        ship = GameObject.FindGameObjectWithTag("Ship");
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
        else if(collision.gameObject.tag == "Projectile")
        {
            if (PlayerPrefs.GetInt("isSfxOn") == 1)
            {
                DestroySound.Play();
            }
            Instantiate(explosionPre, transform.position ,Quaternion.identity);
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
        }else if (pos.x>Screen.width || pos.x < 0
            || pos.y>Screen.height|| pos.y<0)
        {
            Destroy(this.gameObject);
        }else if (info.clearscreen == true)
        {
            Instantiate(explosionPre, transform.position, Quaternion.identity);
            Destroy(this.gameObject);
        }
    }
}
