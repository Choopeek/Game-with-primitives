using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wallpaper : MonoBehaviour
{
    private float moveSpeed = 0.3f;
    private bool wallpaperAtTop;
    private bool wallpaperAtBottom;

    private float wallpaperPosTop = 3.22f;
    private float wallpaperPosBottom = -1.30f;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(MovingUp());
    }

    private IEnumerator MovingUp()
    {
        wallpaperAtBottom = false;
        while (!wallpaperAtTop)
        {
            transform.position = Vector3.MoveTowards(transform.position, new Vector3(transform.position.x, wallpaperPosTop, transform.position.z), moveSpeed * Time.deltaTime);
            if (transform.position.y == wallpaperPosTop)
            {
                wallpaperAtTop = true;
            }
            yield return null;
        }
        StartCoroutine(MovingDown());
        StopCoroutine(MovingUp());
    }

    private IEnumerator MovingDown()
    {
        wallpaperAtTop = false;
        while (!wallpaperAtBottom)
        {
            transform.position = Vector3.MoveTowards(transform.position, new Vector3(transform.position.x, wallpaperPosBottom, transform.position.z), moveSpeed * Time.deltaTime);
            if (transform.position.y == wallpaperPosBottom)
            {
                wallpaperAtBottom = true;
            }
            yield return null;
        }
        StartCoroutine(MovingUp());
        StopCoroutine(MovingDown());
    }
         
    
}
