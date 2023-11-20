using UnityEngine;
using TMPro;
using System;
using System.Threading;

public class GameManager : MonoBehaviour
{

    static public GameManager gameManager;

    [SerializeField] GameObject player, playerUI;
    public DataManager dataManager;
    public GameObject endUI;
    public TextMeshProUGUI comboScoreText,currentScoreText;
    // Start is called before the first frame update
    private void Awake() {
        gameManager = GetComponent<GameManager>();
        dataManager.comboReset();
        dataManager.currentScoreReset();
        dataManager.missedBlockReset();
        endUI.SetActive(false);
        player.SetActive(true);
        playerUI.SetActive(false);
        Time.timeScale = 1f;
        SoundManager.instance.PlayMusic(dataManager.MusicSelected);
    }

    private void Update() 
    {
        Timer();
        if(dataManager.endlessMode) {
            gameEndEndLess();
        }
        else gameEnd(); 
    }

     public void gameEndEndLess()
    {
        if (!SoundManager.instance.musicSource.isPlaying)
        {
            PlayNextMusic();
        }
        if (dataManager.missedBlock > dataManager.maxBlockCamMissed)
        {
            //game end 
            dataManager.updateCurrentScore();
            dataManager.comboReset();
            dataManager.setHighestScore();
            SoundManager.instance.StopMusic();
            //game Pause 
            Time.timeScale = 0f;
           
            //player disable UI player enable 
            player.SetActive(false);
            playerUI.SetActive(true);
            
            //end pannal apper
            endUI.SetActive(true);
            GameObject[] obstracles = GameObject.FindGameObjectsWithTag("obstracle");

            foreach(GameObject obstracle in obstracles)
            {
                Destroy(obstracle);
            }

        }
    }
    public void gameEnd()
    {
        
        if (dataManager.missedBlock > dataManager.maxBlockCamMissed||time>dataManager.MusicSelected.length)
        {
            //game end 
            dataManager.updateCurrentScore();
            dataManager.comboReset();
            dataManager.setHighestScore();
            //game Pause 
            SoundManager.instance.StopMusic();
            Time.timeScale = 0f;
           
            //player disable UI player enable 
            player.SetActive(false);
            playerUI.SetActive(true);
            
            //end pannal apper
            endUI.SetActive(true);
            GameObject[] obstracles = GameObject.FindGameObjectsWithTag("obstracle");

            foreach(GameObject obstracle in obstracles)
            {
                Destroy(obstracle);
            }

        }
    }
//-------------------------------- Score --------------------------

    private void LateUpdate() {

        if(comboStarted) comboScoreText.gameObject.SetActive(true); 
        else comboScoreText.gameObject.SetActive(false);
        
        comboScoreText.text = "COMBO SCORE : " +" X "+ dataManager.comboScore;

        currentScoreText.text = "CURRENT SCORE : " + dataManager.currentScore;
    }
    [HideInInspector] 
    public float comboTime;
    [HideInInspector]
    public bool comboStarted;
    public void combo()
    {
        comboTime = 0;
        comboStarted = true;
        dataManager.comboScore+=1;
    }

    public void commboEnd() 
    {
        dataManager.updateCurrentScore();
        dataManager.comboReset();
        comboStarted = false;
    }
    public float time;
    void Timer()
    {
        time+=Time.deltaTime;
    }

    //idk
    [SerializeField] AudioClip[] musicPlaylist;
    int currentMusicIndex;

    void PlayNextMusic()
    {
        if (musicPlaylist.Length > 0)
        {
            SoundManager.instance.PlayMusic(musicPlaylist[currentMusicIndex]);
            currentMusicIndex = (currentMusicIndex + 1) % musicPlaylist.Length;
        }
    }

}

