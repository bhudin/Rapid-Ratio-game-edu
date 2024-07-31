using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class FinishLevel : MonoBehaviour
{
    public TextMeshProUGUI score;
    public static string level;
    public Scene scene;
    void Start()
    {
        SoundManager.soundMg.PlaySound(SoundManager.AUDIO_TYPE.MENANG);
        foreach (ScoreList a in DataToGFrom.dataScore)
        {
            score.text = a.nameString + ", skor kamu dalam Level " + a.level + " adalah " + a.scoreInt +
            ". Skor tertinggi di Level " + a.level + " adalah " + PlayerPrefs.GetInt(a.level);
            level = a.level;
        }
    }
}
