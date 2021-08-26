using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level2PlayerController : MonoBehaviour
{
    private float playerSpeed = 3f;
    private float playerRightBound = 1.94f;
    private float playerLeftBound = -4.93f;
        
    void Start()
    {
        
    }

    
    void Update()
    {
        if (Input.GetKey(KeyCode.LeftArrow) | Input.GetKey(KeyCode.A))
        {
            transform.Translate(Vector3.left * playerSpeed * Time.deltaTime);
        }

        if (Input.GetKey(KeyCode.RightArrow) | Input.GetKey(KeyCode.D))
        {
            transform.Translate(Vector3.right * playerSpeed * Time.deltaTime);
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            Debug.Log("Player shot a bullet");
        }

        //implement bounds for player so he can't get off screen

        if (transform.position.x > playerRightBound)
        {
            transform.position = new Vector3(playerRightBound, transform.position.y, transform.position.z); 
        }

        if (transform.position.x < playerLeftBound)
        {
            transform.position = new Vector3(playerLeftBound, transform.position.y, transform.position.z);
        }
    }
}
