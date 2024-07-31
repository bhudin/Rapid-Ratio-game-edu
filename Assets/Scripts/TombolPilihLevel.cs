using UnityEngine;
using UnityEngine.SceneManagement;

public class TombolPilihLevel : MonoBehaviour
{
    public static bool sudahPilih1 = false, sudahPilih2 = false, sudahPilih3 = false;
    public void PilihLevel1()
    {
        SceneManager.LoadScene("1");
        sudahPilih1 = true;
        //PlayerPrefs.SetString("levelTerkini", "1");
        SoundManager.soundMg.PlaySound(SoundManager.AUDIO_TYPE.KLIK);
    }
    public void PilihLevel2()
    {
        SceneManager.LoadScene("2");
        sudahPilih2 = true;
        //PlayerPrefs.SetString("levelTerkini", "2");
        SoundManager.soundMg.PlaySound(SoundManager.AUDIO_TYPE.KLIK);
    }
    public void PilihLevel3()
    {
        SceneManager.LoadScene("3");
        sudahPilih3 = true;
        //PlayerPrefs.SetString("levelTerkini", "3");
        SoundManager.soundMg.PlaySound(SoundManager.AUDIO_TYPE.KLIK);
    }
}
