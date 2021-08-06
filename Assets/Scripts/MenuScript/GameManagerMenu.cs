using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManagerMenu : MonoBehaviour

{
    public Canvas wholeMenu;
    public Button startButton;
    public Button controlsButton;
    public GameObject controlsText;
    public GameObject controlsTextBackground;
    public GameObject studioLogoImage;
    private bool controlsDisplayed;
    public MusicController musicController;

    public Animator transition;
    public float sceneTransitionTime = 1f;


    // Start is called before the first frame update
    void Start()
    {
        controlsDisplayed = false;
        musicController = GameObject.Find("Music").GetComponent<MusicController>();
        musicController.MusicChangeToMenu();
    }

    // Update is called once per frame
    void Update()
    {
                
    }

    public void ShowControls()
    {

        if (controlsDisplayed == false)
        {
            controlsText.gameObject.SetActive(true);
            controlsTextBackground.gameObject.SetActive(true);
            studioLogoImage.gameObject.SetActive(true);
            controlsDisplayed = true;
        }

        else if (controlsDisplayed == true)
        {
            controlsText.gameObject.SetActive(false);
            controlsTextBackground.gameObject.SetActive(false);
            studioLogoImage.gameObject.SetActive(false);
            controlsDisplayed = false;
        }



    }

    

    public void StartGame()
    {
        StartCoroutine(LevelFadeAndLoad());
        
    }

    IEnumerator LevelFadeAndLoad()
    {
        transition.SetTrigger("Start");
        yield return new WaitForSeconds(sceneTransitionTime);
        SceneManager.LoadScene("My Game");
    }

    
   

}
