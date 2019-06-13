﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityStandardAssets.CrossPlatformInput;


public class Player_shoot : MonoBehaviour
{
    //public GameObject player;
    public GameObject bulletPrefab;
    public GameObject player;
    public GameObject flashSpawnPoint;
    public GameObject playerFlash;
    public Sprite gun0;
    public Sprite gun1;
    public Sprite gun2;
    private Sprite curGun;
    public float fireRate = 0.5f;
    public int gunType;
    private int ammoCount; //-1 = infinite bullets
    private float nextFire;
    private int totalBulletTypes = 3; //assume at least 2
    public int[] ammo;
    // Use this for initialization
    void Start()
    {
        //        transform.position = player.transform.position;
        //transform.position += new Vector3(1f, 0, 0);
        nextFire = fireRate;
        ammo = new int[totalBulletTypes];
        ammo[0] = -1;
        ammo[1] = 120; //machine gun
        ammo[2] = 30; //shotgun
        //ammo[3] = 10; //railgun
        gunType = 0;
        curGun = gun0;
        this.GetComponent<SpriteRenderer>().sprite = curGun;
    }

    // Update is called once per frame
    void Update()
    {
        //       transform.position = player.transform.position;
        //transform.position += new Vector3(1f, 0, 0);
        faceMouse();
        nextFire += Time.deltaTime;
        changeGun();
        shootGun();
        if (ammo[gunType] == 0)
            gunType = 0;
    }

    void faceMouse()
    {
#if UNITY_IOS || UNITY_ANDROID
	if(EventSystem.current.IsPointerOverGameObject())
	{;}
#endif
	
        //Vector3 mouseWorldPos = camera.ScreenToWorldPoint(new Vector2(Input.mousePosition.x, Input.mousePosition.y));
        var mousePos = Input.mousePosition;
        mousePos.z = 12;
        Vector3 mousWorldPos = Camera.main.ScreenToWorldPoint(mousePos);
        //Vector2 mousePos = new Vector2(Camera.main.ScreenToWorldPoint (Input.mousePosition).x, Camera.main.ScreenToWorldPoint(Input.mousePosition).y);        //Set the rotation of the sprite so it always faces the player
        //Vector3 vector = new Vector3(Input.mousePosition.x, Input.mousePosition.y, Camera.main.nearClipPlane);
        Vector3 difference = mousWorldPos - transform.position;
        difference.Normalize();
        float rot = Mathf.Atan2(difference.y, difference.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0f, 0f, rot);
        //    mousePos.y - transform.position.y); // Get the relative position from player to this object

        //transform.right = relPos; // Change the direction of right to be the relative position's x and y coordinates
    }
    void shootGun()
    {
        switch (gunType)
        {
            case 0:
                regularGun();
                break;
            case 1:
                machineGun();
                break;
            case 2:
                shotGun();
                break;
            //    case 3:
            //      homingGun();
            //    break;
            /*case 4:
                if (wrapInput.Fire() && nextFire >= fireRate)
                {
                    GameObject bullet = Instantiate(bulletPrefab, transform.position, transform.rotation);
                    Bullet_Script bulletScript = bullet.GetComponent<Bullet_Script>();
                    bulletScript.bulletType = 2;
                    //bull.transform.localScale = new Vector3(10f, 10f, bull.transform.localScale.z);
                    nextFire = 0;
                }
                break;*/
            default:
                //Debug.Log("Default bullet");
                break;
        }
        if (ammo[gunType] == 0)
        {
            curGun = gun0;
            gunType = 0;
        }
    }
    
    void changeGun()
    {
	int temp = WrapInput.switchGun();
	if(temp != -1)
	{
		gunType = temp;
	}

        if (gunType == 0)
        {
            curGun = gun0;
/*            Color tmp = transform.GetChild(0).GetComponent<SpriteRenderer>().color;
            tmp.a = 0f;
            transform.GetChild(0).GetComponent<SpriteRenderer>().color = tmp;

            tmp = transform.GetChild(1).GetComponent<SpriteRenderer>().color;
            tmp.a = 0f;
            transform.GetChild(1).GetComponent<SpriteRenderer>().color = tmp;

            tmp = transform.GetChild(2).GetComponent<SpriteRenderer>().color;
            tmp.a = 0f;
            transform.GetChild(2).GetComponent<SpriteRenderer>().color = tmp;*/
        }
        //if (Input.GetKeyDown(KeyCode.Alpha4) && ammo[3] != 0)
        //  gunType = 3;
        if (gunType == 2 && ammo[2] != 0)
        {
            curGun = gun2;
        }
        if (gunType == 1 && ammo[1] != 0)
        {
            curGun = gun1;
        }
        //Debug.Log("Changing type");
        this.GetComponent<SpriteRenderer>().sprite = curGun;
    }
    void regularGun()
    {
        if (WrapInput.Fire() && nextFire >= fireRate)
        {
            flashSpawnPoint.transform.localPosition = new Vector3(0.53f, 0.02f, 0f);
            Instantiate(playerFlash, flashSpawnPoint.transform.position, flashSpawnPoint.transform.rotation, flashSpawnPoint.transform);
            GameObject bullet = Instantiate(bulletPrefab, flashSpawnPoint.transform.position, transform.rotation);
            bullet.GetComponent<Rigidbody2D>().velocity = new Vector2(player.GetComponent<Rigidbody2D>().velocity.x, 0);
            SoundManagerScript.PlaySound("pistol");
            nextFire = 0;
        }
    }
    void machineGun()
    {
        float fasterFireRate = fireRate / 2;
        if (WrapInput.Fire() && nextFire >= fasterFireRate && ammo[gunType] != 0)
        {
            flashSpawnPoint.transform.localPosition = new Vector3(0.82f, 0.06f, 0f);
            Instantiate(playerFlash, flashSpawnPoint.transform.position, flashSpawnPoint.transform.rotation, flashSpawnPoint.transform);
            GameObject bullet = Instantiate(bulletPrefab, flashSpawnPoint.transform.position, transform.rotation);
            bullet.GetComponent<Rigidbody2D>().velocity = new Vector2(player.GetComponent<Rigidbody2D>().velocity.x, 0);
            nextFire = 0;
            SoundManagerScript.PlaySound("machine gun");
            if (ammo[gunType] != -1)
                ammo[gunType]--;
        }
    }
    void shotGun()
    {
        float shootAngle = 30; //total angle between 2 end bullets
        int bulletCount = 3; //minimum 2
        if (WrapInput.Fire() && nextFire >= fireRate && ammo[gunType] != 0)
        {
            //Debug.Log("Shotgun");

            flashSpawnPoint.transform.localPosition = new Vector3(0.95f, 0.05f, 0f);
            Instantiate(playerFlash, flashSpawnPoint.transform.position, flashSpawnPoint.transform.rotation, flashSpawnPoint.transform);
            for (int a = 0; a < bulletCount; a++)
            {
                //Quaternion x = transform.rotation;
                if (bulletCount != 1)
                {
                    //Debug.Log("rotation value: " + (-shootAngle / 2 + shootAngle * a / (b - 1)));
                    //x = new Quaternion(0, 0, (-shootAngle / 2 + shootAngle * a / (b - 1)), 0 );
                    //bullet.GetComponent<Rigidbody2D>().rotation += -shootAngle / 2 + shootAngle * a / (b - 1);
                    /*x = Quaternion.Euler(/*-shootAngle / 2 + shootAngle * a / (b - 1) + transform.rotation.x
                        , transform.rotation.y, transform.rotation.z);*/
                }
                GameObject bullet = Instantiate(bulletPrefab, flashSpawnPoint.transform.position, transform.rotation);
                bullet.GetComponent<Rigidbody2D>().velocity = new Vector2(player.GetComponent<Rigidbody2D>().velocity.x, 0);
                bullet.transform.Rotate(0, 0, -shootAngle / 2 + shootAngle * a / (bulletCount - 1));
                //Debug.Log(bullet.GetComponent<Rigidbody2D>().rotation);
            }
            nextFire = 0;
            SoundManagerScript.PlaySound("shot gun");
            if (ammo[gunType] != -1)
                ammo[gunType] -= 1;
        }
    }
    void homingGun()
    {
        if (WrapInput.Fire() && nextFire >= fireRate && ammo[gunType] != 0)
        {
            GameObject bullet = Instantiate(bulletPrefab, transform.position, transform.rotation);
            bullet.GetComponent<Rigidbody2D>().velocity = new Vector2(player.GetComponent<Rigidbody2D>().velocity.x, 0);
            Bullet bulletScript = bullet.GetComponent<Bullet>();
            bulletScript.bulletType = 1;
            nextFire = 0;
            if (ammo[gunType] != -1)
                ammo[gunType]--;
        }
    }

}
