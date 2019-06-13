﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SoundManagerScript : MonoBehaviour
{
    public static AudioClip playerHitSound, playerDieSound, playerJumpSound, slowDownSound, speedUpSound,
                            pistolSound, shotgunSound, railgunSound, machinegunSound,
                            enemyDestroySound, enemyShootSound, boomerShootSound, boomerHitSound, boomerBombSound, boomerDieSound,
                            heartPickupSound, reloadSound, alarmSound, glassBreakSound, meteorSound, meteorDropSound;
    static AudioSource audioSrc;
    public float pitch = 1.0f;

    private void Awake()
    {
        DontDestroyOnLoad(gameObject);
    }

    // Start is called before the first frame update
    void Start()
    {
        playerHitSound = Resources.Load<AudioClip>("Sound/hurt sound");
        playerDieSound = Resources.Load<AudioClip>("Sound/player die");
        speedUpSound = Resources.Load<AudioClip>("Sound/slow down");
        slowDownSound = Resources.Load<AudioClip>("Sound/speed up");
        playerJumpSound = Resources.Load<AudioClip>("Sound/jump");
        pistolSound = Resources.Load<AudioClip>("Sound/pistol");
        shotgunSound = Resources.Load<AudioClip>("Sound/shot gun");
        railgunSound = Resources.Load<AudioClip>("Sound/rail gun");
        machinegunSound = Resources.Load<AudioClip>("Sound/machine gun");
        heartPickupSound = Resources.Load<AudioClip>("Sound/heart pickup");
        reloadSound = Resources.Load<AudioClip>("Sound/reload");

        enemyDestroySound = Resources.Load<AudioClip>("Sound/Explosion");
        boomerHitSound = Resources.Load<AudioClip>("Sound/boomer hit");
        boomerBombSound = Resources.Load<AudioClip>("Sound/boomer bomb");
        boomerDieSound = Resources.Load<AudioClip>("Sound/boomer die");
        meteorSound = Resources.Load<AudioClip>("Sound/meteor");
        enemyShootSound = Resources.Load<AudioClip>("Sound/super pew");
        boomerShootSound = Resources.Load<AudioClip>("Sound/super pew 2");
        glassBreakSound = Resources.Load<AudioClip>("Sound/glass break");
        alarmSound = Resources.Load<AudioClip>("Sound/alarm");
        meteorDropSound = Resources.Load<AudioClip>("Sound/meteor drop");

        audioSrc = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        audioSrc.pitch = pitch;
        Scene scene = SceneManager.GetActiveScene();
        if (scene.name == "GameOver")
            Destroy(gameObject);
    }

    public static void PlaySound(string clip)
    {
        audioSrc.volume = 1.0f;
        switch (clip)
        {
            case "meteor drop":
                audioSrc.volume = 0.3f;
                audioSrc.PlayOneShot(meteorDropSound);
                break;
            case "hurt sound":
                audioSrc.PlayOneShot(playerHitSound);
                break;
            case "boomer hit":
                audioSrc.PlayOneShot(boomerHitSound);
                break;
            case "boomer bomb":
                audioSrc.PlayOneShot(boomerBombSound);
                break;
            case "boomer die":
                audioSrc.PlayOneShot(boomerDieSound);
                break;
            case "meteor":
                audioSrc.volume = 0.08f;
                audioSrc.PlayOneShot(meteorSound);
                break;
            case "speed up":
                audioSrc.PlayOneShot(speedUpSound);
                break;
            case "slow down":
                audioSrc.PlayOneShot(slowDownSound);
                break;
            case "reload":
                audioSrc.volume = 0.05f;
                audioSrc.PlayOneShot(reloadSound);
                break;
            case "jump":
                audioSrc.PlayOneShot(playerJumpSound);
                break;
            case "super pew 2":
                audioSrc.PlayOneShot(boomerShootSound);
                break;
            case "pistol":
                audioSrc.PlayOneShot(pistolSound);
                break;
            case "shot gun":
                audioSrc.PlayOneShot(shotgunSound);
                break;
            case "rail gun":
                audioSrc.PlayOneShot(railgunSound);
                break;
            case "machine gun":
                audioSrc.PlayOneShot(machinegunSound);
                break;
            case "Explosion":
                audioSrc.PlayOneShot(enemyDestroySound);
                break;
            case "super pew":
                audioSrc.PlayOneShot(enemyShootSound);
                break;
            case "player die":
                audioSrc.PlayOneShot(playerDieSound);
                break;
            case "glass break":
                audioSrc.volume = 0.06f;
                audioSrc.PlayOneShot(glassBreakSound);
                break;
            case "heart pickup":
                audioSrc.volume = 0.3f;
                audioSrc.PlayOneShot(heartPickupSound);
                break;
        }
    }
}







