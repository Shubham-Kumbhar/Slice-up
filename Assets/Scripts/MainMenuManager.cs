using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System;
using TMPro;
using Unity.VisualScripting;
public class MainMenuManager : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] DataManager dataManager;
    [SerializeField] Slider blockSpeed, SFX , music;
    private void Awake() 
    {
        dataManager.blockSpawnSpeed=blockSpeed.value; 
        SFXSliderValueChanged(); 
        musicSliderValueChanged();
        dataManager.endlessMode =false;

    }
    public void difficultysetter(int a)
    {
        switch (a){
            case 0 :
            dataManager.blockSpawnSpeed = 1.5f;
            dataManager.maxBlockCamMissed = 5;
            return;
            case 1 :
            dataManager.blockSpawnSpeed = 0.8f;
            dataManager.maxBlockCamMissed = 5;
            return;
            case 2 :
            dataManager.blockSpawnSpeed = 0.4f;
            dataManager.maxBlockCamMissed = 5;
            return;

            }
        
    }
    
    public void sceneChanger(int a)
    {
        SceneManager.LoadScene(a);
    }
    public void sliderValueChanged()
    {
        dataManager.blockSpawnSpeed=blockSpeed.value; 
    }

    public void SFXSliderValueChanged()
    {
         dataManager.SFXVolume=SFX.value; 
         SoundManager.instance.SetSFXVolume(SFX.value);
    }
    public void musicSliderValueChanged()
    {
        dataManager.musicVolume=music.value; 
        SoundManager.instance.SetMusicVolume(music.value);
    }
    public void endlessMode()
    {
        dataManager.endlessMode =true;
        dataManager.maxBlockCamMissed = 10;
    }
    //sound Manager

    public void PlaySFX(AudioClip clip)
    {
        SoundManager.instance.PlaySFX(clip);
    }
    public void PlayMusic(AudioClip clip)
    {
        SoundManager.instance.PlayMusic(clip);
    }
    public void StopMusic()
    {
        SoundManager.instance.StopMusic();
    }
    public void SetSFXVolume(float a)
    {
        SoundManager.instance.SetSFXVolume(a);
    }
    public void SetMusicVolume(float a)
    {
        SoundManager.instance.SetMusicVolume(a);
    }
    public void setAudioClip(AudioClip clip)
    {
        SoundManager.instance.setAudioClip(clip);
    }

    public void quit()
    {
        Application.Quit();
    }
}
