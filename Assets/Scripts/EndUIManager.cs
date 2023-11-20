using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class EndUIManager : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI highestScore, currentScore;
    private void OnEnable() 
    {
        highestScore.text = "HIGHEST SCORE : "+GameManager.gameManager.dataManager.highestScore;
        currentScore.text = "SCORE :"+GameManager.gameManager.dataManager.currentScore;
    }
    public void PlaySFXAuido(AudioClip clip)
    {
        SoundManager.instance.PlaySFX(clip);
    }
    public void mainMenuChanger(int a)
    {
        SceneManager.LoadScene(a);
    }
}
