﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boomer_Enemy : MonoBehaviour
{
    public GameObject player;
    public GameObject splatters;
    public Enemy_Spawn en_spawn;

    public int max_splatters;
    public int hp;
    public float x_speed;
    public float x_speed_limit;
    public float y_speed;
    public Vector3 offset;

    private Rigidbody2D rigidBody;


    // Start is called before the first frame update
    void Start()
    {
        rigidBody = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if (gameObject.name == "Boomer")
        {
            //transform.position = new Vector2(player.transform.position.x + 100, player.transform.position.y);
        }
        if (gameObject.name == "Boomer(Clone)")
        {
            if (rigidBody.velocity.x < x_speed_limit)
            {
                rigidBody.AddForce(new Vector2(x_speed, 0), ForceMode2D.Impulse);
            }
            Vector3 desiredPosition = player.transform.position + offset;
            Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, y_speed * Time.deltaTime);
            transform.position = new Vector2(transform.position.x, smoothedPosition.y);
        }
        if (hp <= 0 && (gameObject.name == "Boomer(Clone)"))
        {
            en_spawn.number_of_enemies--;
            Destroy(gameObject);
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player_Bullet")
        {
            for (int i = 0; i < max_splatters; i++)
            {
                Instantiate(splatters, transform.position, Quaternion.identity);
            }
            hp--;
        }
    }
}