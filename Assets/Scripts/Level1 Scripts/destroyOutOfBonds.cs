using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class destroyOutOfBonds : MonoBehaviour
{
    private float leftBound = -13.5f;
    private float rightBound = 13.5f;
    private float upBound = 8.5f;
    private float downBound = -6.5f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (transform.position.y > upBound)
        {
            Destroy(gameObject);
        }
        if (transform.position.y < downBound)
        {
            Destroy(gameObject);
        }
        if (transform.position.x > rightBound)
        {
            Destroy(gameObject);
        }
        if (transform.position.x < leftBound)
        {
            Destroy(gameObject);
        }
    }
}
