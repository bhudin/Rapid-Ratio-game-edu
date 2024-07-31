using UnityEngine;
using TMPro;

public class TeksTutorial : MonoBehaviour
{
    public TextMeshProUGUI nama;
    void Start()
    {
        nama.text = "Selamat datang di sesi tutorial Rapid Ratio, " + PlayerPrefs.GetString("nama") + "!";        
    }
}
