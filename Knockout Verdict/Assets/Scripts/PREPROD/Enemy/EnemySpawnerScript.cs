using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawnerScript : MonoBehaviour
{
    public GameObject toSpawnEnemy;             //enemy gameobject

    private void OnBecameVisible()
    {
        Debug.Log("Enemy Instantiated");
        Instantiate(toSpawnEnemy,gameObject.transform.position, gameObject.transform.rotation);   
    }

}