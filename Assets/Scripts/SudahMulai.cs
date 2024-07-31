using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using UnityEngine.UI;

public class SudahMulai : MonoBehaviour
{
    public static bool bolehPlay = false, sekali = false;
    public string namaPemain;
    public TextMeshProUGUI nama;
    public GameObject tombolStart;
    private Image gambar;
    void Awake()
    {
        namaPemain = PlayerPrefs.GetString("nama");
        nama.text = "Hai, " + namaPemain + "!";
        if (!bolehPlay)
            Transparan();
    }
    void Update()
    {
        if (this.isActiveAndEnabled)
        {
            if (sekali)
            {
                namaPemain = PlayerPrefs.GetString("nama");
                nama.text = "Hai, " + namaPemain + "!";
            }
            sekali = false;
        }
    }
    void Transparan()
    {
        gambar = tombolStart.GetComponent<Image>();
        Color transparan = gambar.color;
        transparan.a = 0.5f;
        gambar.color = transparan;
    }
    void Utuh()
    {
        gambar = tombolStart.GetComponent<Image>();
        Color warnaasli = gambar.color;
        warnaasli.a = 1f;
        gambar.color = warnaasli;
    }
    public void TombolPlay()
    {
        if (bolehPlay)
        {
            SoundManager.soundMg.PlaySound(SoundManager.AUDIO_TYPE.KLIK);
            SceneManager.LoadSceneAsync("Pilih Level");
        }
    }
    public void TombolTutorial()
    {
        Utuh();
        bolehPlay = true;
        SoundManager.soundMg.PlaySound(SoundManager.AUDIO_TYPE.KLIK);
        SceneManager.LoadSceneAsync("Tutorial");
    }
}
