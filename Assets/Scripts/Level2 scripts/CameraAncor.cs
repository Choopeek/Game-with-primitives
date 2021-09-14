using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraAncor : MonoBehaviour
{

    [SerializeField] float cameraSpeed = 1f;
    private Vector3 cameraDefaultPos;
    bool shakeTheCamera;
    
    [SerializeField] float cameraBoundX = 0f;
    [SerializeField] float cameraBoundY = 0f;
    [SerializeField] float cameraBoundZ = 0f;

    void Start()
    {
        
       
        
    }

    
    void Update()
    {
        
    }

    void CalculateNextPos()
    {
        
        //for some fucking reason, RandomRange inside Vector3 gives me INTs. Unless hardcoded floats in the new Vector3.
        Vector3 nextPos = new Vector3(Random.Range(-3f, 0f), Random.Range(2f, 2.5f), Random.Range(-26f, -25f));        
        StartCoroutine(MoveToNewPos(nextPos));

    }

    IEnumerator MoveToNewPos(Vector3 nextPos)
    {
        
        
        bool movecamera = true;
        float elapsedTime = Time.time + 2.0f;
        while(movecamera)
        {
            
            transform.position = Vector3.MoveTowards(transform.position, nextPos, cameraSpeed * Time.deltaTime);
            if(Time.time >= elapsedTime)
            {
                Debug.Log("CameraAtNewPosition");
                movecamera = false;
            }
            yield return null;
        }
        CalculateNextPos();
    }
}
