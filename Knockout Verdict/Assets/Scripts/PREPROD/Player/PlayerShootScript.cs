using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShootScript : MonoBehaviour
{
    public GameObject bullet;                                                                               // bullet prefab
    public GameObject Gun;                                                                                  //  gun prefab
    public GameObject player;                                                                               //  player prefab
    public StatSystemScript playerStats;                                                                    //  player stats script
    public Transform gunNozzle;                                                                             //  position from which bullet will be fired
    public Animator anim;                                                                                   //  animator

    private float FiringDelay = 0.25f;                                                                      //  set default firing delay
    private bool isShooting = false;                                                                        //  track if the player is going pew pew

    AudioManagerScript audioManager;                                                                        //  audio manager for pew pew sounds

    private Coroutine shootingCoroutine;                                                                    // coroutine to handle the pew pew

    private void Awake()
    {
        audioManager = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioManagerScript>();         //set audiomanager
    }

    void Start()
    {
        FiringDelay = playerStats.firingDelay;                                                              //set firing delay when script starts
    }

    
    void Update()
    {


        //ensures the player starts and keeps shooting when the key is pressed
        if (Input.GetKeyDown(KeyCode.W) && !isShooting && Time.timeScale==1) 
        {
            isShooting=true;                                                    
            Gun.SetActive(true);                                                                            //  Make gun visible 
            anim.SetBool("isShooting", true);                                                               //  Switch to shooting animation
            shootingCoroutine = StartCoroutine(Shoot());                                                    //  Start shooting coroutine             
            audioManager.PlaySFX(audioManager.GunShot);                                                     //  Play gun shooting audio
        }

        //ensures the player stops shooting when the key is no longer pressed
        if (Input.GetKeyUp(KeyCode.W) && isShooting && Time.timeScale==1) 
        {
            Gun.SetActive(false);                                                                           //  Make gun invisible
            isShooting=false;                                                                               //  Stop the pew pew
            anim.SetBool("isShooting", false);                                                              //  Switch to regular animation    
            StopCoroutine(shootingCoroutine);                                                               //  Stop shooting coroutine
        
        }

    }

    //------------------------------------------------------- THE PEW PEW COROUTINE ----------------------------------------------------------
    IEnumerator Shoot()
    {
        while (true)                                                                                        
        {
            
            {
                GameObject shotBullet = Instantiate(bullet, gunNozzle.position, gunNozzle.rotation);       //   Create new bullet at position of gun nozzle
                BulletScript bulletScript = shotBullet.GetComponent<BulletScript>(); 
                bulletScript.shooter = player;                                                            //    Set the shooter reference for the bullet -- so that we don't get hurt from our own pew pew
                yield return new WaitForSeconds(FiringDelay);                                             //    Time delay before another shot
            }
            
        }
        
    }
}
