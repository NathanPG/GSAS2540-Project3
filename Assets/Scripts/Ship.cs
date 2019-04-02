using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ship : MonoBehaviour
{
    public float speed;
    public float rotationSpeed;
    public SceneController scene;
    public Rigidbody2D rb;
    public Information info;
    
    public void Awake()
    {
        scene = GameObject.FindGameObjectWithTag("UI").GetComponent<SceneController>();
        info = GameObject.FindGameObjectWithTag("UI").GetComponent<Information>();
        transform.position = new Vector3(960f, 540f, -10f);
        transform.localEulerAngles = new Vector3(0, 0, 0);
    }
    
    void Update()
    {
        if (scene.isGameStart)
        {
            Vector3 pos = transform.position;
            if (Input.GetKey(KeyCode.W) && pos.x < Screen.width && pos.x > 0
            && pos.y < Screen.height && pos.y > 0)
            {
                GetComponent<ParticleSystem>().Play();
                rb.AddForce(transform.up * speed);
            }
            float rotation = -Input.GetAxis("Horizontal") * rotationSpeed * Time.deltaTime;
            transform.Rotate(0, 0, rotation);
        }
    }
}
