using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;


public class MusicController : MonoBehaviour
{
    //this line of code allows this gameObject to transfer through scene's
    private static MusicController instance;
    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
            DontDestroyOnLoad(instance);
        }
        else
        {
            Destroy(gameObject);
        }
    }
    //end of scene transfer code


    public AudioMixerSnapshot menuMusic;
    public AudioMixerSnapshot calmMusic;
    public AudioMixerSnapshot dynamicMusic;

    public bool menuMusicBool = false;
    public bool calmMusicBool = false;
    public bool dynamicMusicBool = false;

    public GameObject menuMusicObject;
    public GameObject calmMusicObject;
    public GameObject dynamicMusicObject;

    private float transitionTime = 1.0f;


    // Start is called before the first frame update
    void Start()
    {
        
    }
    

    // Update is called once per frame
    void Update()
    {
        
    }

    //these functions check the logic and runs the needed music. Please note, that the MusicObjects destroy themselves in their own scripts. To be able to destroy spawned objects from this script you have to wirte additional "GameObject.Find" stuff. Probably. Did not test it, honestly. It just works.
    public void MusicChangeToMenu()
    {
        if(!menuMusicBool)
        {
            Instantiate(menuMusicObject);
        }
        menuMusicBool = true;        
        menuMusic.TransitionTo(transitionTime);        
        calmMusicBool = false;
        dynamicMusicBool = false;

    }
    public void MusicChangeToCalm()
    {
        if(!calmMusicBool)
        {
            Instantiate(calmMusicObject);
        }
        calmMusicBool = true;        
        calmMusic.TransitionTo(transitionTime);
        menuMusicBool = false;
        dynamicMusicBool = false;
        
    }
    public void MusicChangeToDynamic()
    {
        if(!dynamicMusicBool)
        {
            Instantiate(dynamicMusicObject);
        }
        dynamicMusicBool = true;       
        dynamicMusic.TransitionTo(transitionTime);
        menuMusicBool = false;
        calmMusicBool = false;
        
    }
    

}
