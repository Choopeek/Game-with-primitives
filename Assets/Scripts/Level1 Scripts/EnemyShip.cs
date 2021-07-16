using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyShip : MonoBehaviour
{
    private float leftBound = -16.0f;
    private float enemyShipSpeed = 20.0f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //ship constantly flies to the left and than vanishes
        transform.Translate(Vector3.left * enemyShipSpeed * Time.deltaTime);
        if (transform.position.x < leftBound)
        {
            Destroy(gameObject);
        }
    }
}
