using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class GunDirectionScript : MonoBehaviour
{
    void Update()
    {
        //Might change from arrow keys to WASD or smthn so that gun direction is separate from movement
        CheckDirection();
        
    }

    //------------------------ FUNCTION TO CHECK AND SET DIRECTION BASED ON KEY PRESS -----------------------------------------------------------------
    void CheckDirection()
    {

        
        float xInput = UnityEngine.Input.GetAxis("Horizontal");          // checks if player is moving horizontally or not

        if (Input.GetKey(KeyCode.UpArrow)  && xInput == 0)
        {
            //aiming up
            transform.rotation = Quaternion.Euler(transform.rotation.eulerAngles.x, transform.rotation.eulerAngles.y, 90);
        }
        else if (Input.GetKey(KeyCode.UpArrow) && xInput != 0)
        {
            //aiming up diagonally
            transform.rotation = Quaternion.Euler(transform.rotation.eulerAngles.x, transform.rotation.eulerAngles.y, 45);
        }
        else if (Input.GetKey(KeyCode.DownArrow) && xInput != 0) 
        {
            //aiming down diagonally
            transform.rotation = Quaternion.Euler(transform.rotation.eulerAngles.x, transform.rotation.eulerAngles.y, -45);
        }
        else if (Input.GetKey(KeyCode.DownArrow) && xInput == 0)
        {
            //aiming down
            transform.rotation = Quaternion.Euler(transform.rotation.eulerAngles.x, transform.rotation.eulerAngles.y, -90);
        }
        
        else
        {
            //aiming straight
            transform.rotation = Quaternion.Euler(transform.rotation.eulerAngles.x, transform.rotation.eulerAngles.y, 0);
        }
    }
}
