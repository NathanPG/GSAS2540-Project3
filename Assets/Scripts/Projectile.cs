using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public AudioSource HitSound;
    public Information info;
    public SceneController scene;
    Vector3 pos;

    void Start()
    {
        scene = GameObject.FindGameObjectWithTag("UI").GetComponent<SceneController>();
        scene = GameObject.FindGameObjectWithTag("UI").GetComponent<SceneController>();
        info = GameObject.FindGameObjectWithTag("UI").GetComponent<Information>();
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "SmallAsteroid" 
            || collision.gameObject.tag == "HugeAsteroid")
        {
            Destroy(this.gameObject);
        }
    }

    // Update is called once per frame
    void Update()
    {
        pos = transform.position;
        if (!scene.isGameStart)
        {
            Destroy(this.gameObject);
        }
        else if (pos.x > Screen.width || pos.x < 0
           || pos.y > Screen.height || pos.y < 0)
        {
            Destroy(this.gameObject);
        }
        else if (info.clearscreen == true)
        {
            Destroy(this.gameObject);
        }
    }
}
