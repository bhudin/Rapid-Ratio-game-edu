using UnityEngine;
using UnityEngine.SceneManagement;

public class TombolRestart : MonoBehaviour
{
    public void Restart()
    {
        SceneManager.LoadSceneAsync(FinishLevel.level);
        SoundManager.soundMg.PlaySound(SoundManager.AUDIO_TYPE.KLIK);
    }
    public void TombolMainMenu()
    {
        SceneManager.LoadScene("Main Menu");
        SoundManager.soundMg.PlaySound(SoundManager.AUDIO_TYPE.KLIK);
    }
}
