using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraAncor : MonoBehaviour
{
    Level2GameManager gameManager;

    [SerializeField] float cameraSpeed = 50f;
    [SerializeField] float rotateSpeed = -20f;

    public Vector3 currentPos;
   [SerializeField] Vector3 levelStartPos;

    float currentXRotation = 5.552085f;
    float levelStartRotation = 90;

    bool cameraInPosition;
    bool cameraRotated;
    bool cameraPositioned;

    void Start()
    {
        //Debug.Log("rotation" + transform.rotation);
        //Debug.Log("local rotation" + transform.localRotation);
        //Debug.Log("eulerAngles" + transform.eulerAngles);
        //Debug.Log("local eulerAngles " + transform.localEulerAngles);
        //Debug.Log(transform.eulerAngles.x);
        gameManager = GameObject.Find("Level2GameManager").GetComponent<Level2GameManager>();
       
        
    }

    
    void Update()
    {
        
    }

    public void PrepareForStart()
    {
        currentPos = transform.position;
        transform.position = levelStartPos;
        transform.Rotate(levelStartRotation, transform.rotation.y, transform.rotation.z);
        
    }  
    
    public void MoveCameraForStart()
    {
        if (!cameraInPosition)
        {
            StartCoroutine(MoveToStartPos(currentPos));
        }
        if (!cameraRotated)
        {
            StartCoroutine(RotateCameraVer2());
        }
        
        
    }

    IEnumerator RotateCamera()
    {
        bool rotate = true;
        Vector3 rotateTo = new Vector3(currentXRotation, 0, 0);
        Debug.Log("starting rotation");
        while (rotate)
        {           
            
            transform.eulerAngles = Vector3.Lerp(transform.rotation.eulerAngles, rotateTo, Time.deltaTime);

            if (transform.rotation.y == 0)
            {
                rotate = false;
                Debug.Log("finished rotating");                
                StopAllCoroutines();
            }
            yield return null;
        }
        
    } //Im not deleting this metod. Cause using VECTOR3.lerp to rotate camera - gives some funny effect;

    public IEnumerator RotateCameraVer2()
    {        
        bool rotate = true;
        while (rotate)
        {
            transform.Rotate(rotateSpeed * Time.deltaTime, 0, 0);            
            if (transform.eulerAngles.x <= currentXRotation)
            {
                rotate = false;
                cameraRotated = true;
                CameraInPosition();
            }
            yield return null;
            
        }
    }

    public IEnumerator MoveToStartPos(Vector3 nextPos)
    {


        bool movecamera = true;        
        while (movecamera)
        {
            
            transform.position = Vector3.MoveTowards(transform.position, nextPos, cameraSpeed * Time.deltaTime);
            if (transform.position == currentPos)
            {
                                
                movecamera = false;
                cameraPositioned = true;
                CameraInPosition();

            }
            yield return null;
        }
        

    }

    void CameraInPosition()
    {
        if(cameraPositioned & cameraRotated)
        {
            cameraInPosition = true;
            gameManager.StartTheGame();
        }


    }
}
