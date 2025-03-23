using System.Collections;
using UnityEngine;

public class EnemyShootScript : MonoBehaviour
{
    public GameObject bullet;                                                           //  bullet gameobject ref
    public Transform gunNozzle;                                                         //  bullet spawn position ref

    public StatSystemScript enemy1Stat;                                                //   enemy stats
    private bool isAttacking = false;                                                  //   is enemy attacking player or not
    private Coroutine shootingCoroutine;                                               //   enemy's pew pew coroutine


    public float detectionRange = 10f;                                                 // distance within which enemy scans for player
    

    private Transform player;                                                          // player position ref 
    
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        enemy1Stat = gameObject.GetComponent<StatSystemScript>();
        enemy1Stat.entityName = "Stationary terrorist";
    }

    void Update()
    {
        if (player)
        {
            float distanceToPlayer = Vector2.Distance(transform.position, player.position);

            Flip();

            if (distanceToPlayer <= detectionRange)
            {

                Aim();

                if (!isAttacking)
                {
                    isAttacking = true;
                    shootingCoroutine = StartCoroutine(Attacking());

                }
            }
            else
            {
                if (isAttacking)
                {
                    isAttacking = false;
                    StopCoroutine(shootingCoroutine);
                }
            }
        }
    }

    void Aim() 
    {
        if (player)
        {
            Vector3 direction = player.position - transform.position;
            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
            gunNozzle.rotation = Quaternion.Euler(new Vector3(0, 0, angle));
        }
    }

    //enemy still can't flip
    void Flip() 
    {
        if (player)
        {
            Vector3 distance = player.position - transform.position;
            if (distance.x < 0)
            {
                transform.rotation = Quaternion.Euler(0f, 180f, 0f);
            }
            else
            {
                transform.rotation = Quaternion.Euler(0f, 0f, 0f);
            }
        }
        
    }

    IEnumerator Attacking()
    {

        while (isAttacking && player)
        {
            yield return null;
            
            GameObject shotBullet = Instantiate(bullet,gunNozzle.position, gunNozzle.rotation);
            BulletScript bulletScript = shotBullet.GetComponent<BulletScript>();
            bulletScript.shooter = gameObject; // Set the shooter reference for the bullet.
            yield return new WaitForSeconds(0.2f);
            GameObject shotBullet2 = Instantiate(bullet, gunNozzle.position, gunNozzle.rotation);
            BulletScript bulletScript2 = shotBullet2.GetComponent<BulletScript>();
            bulletScript2.shooter = gameObject; // Set the shooter reference for the bullet.

            yield return new WaitForSeconds(enemy1Stat.firingDelay);
        }
    }
}
