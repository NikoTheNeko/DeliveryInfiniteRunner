using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;

public class AudioSlider : MonoBehaviour{

    #region Public Variables
    [Header("Slider")]
    [Tooltip("The volume slider of game")]
    public Slider VolumeSlider;
    
    [Header("Audio Mixer")]
    [Tooltip("The mixer for all of the audio")]
    public AudioMixer VolumeMix;
    #endregion

    #region Private Variables
    #endregion

    // Start is called before the first frame update
    void Start(){
        //Grabs the player pref if there is one
        VolumeSlider.value = PlayerPrefs.GetFloat("Volume", 0.5f);
        
        //Sets it to the volume mixer
        VolumeMix.SetFloat("MasterVolume", ConvertToCorrectValue(VolumeSlider.value));

    }

    // Update is called once per frame
    void Update(){
        //Updates the volume based on the slider
        VolumeMix.SetFloat("MasterVolume", ConvertToCorrectValue(VolumeSlider.value));

        //Updates player prefs
        VolumeSlider.value = PlayerPrefs.GetFloat("Volume", VolumeSlider.value);
    }

    /**
        Because of how the way the things are logarithmic, just do this thing for now.
    **/
    private float ConvertToCorrectValue(float value){
        return Mathf.Log10(value) * 20;
    }
}
