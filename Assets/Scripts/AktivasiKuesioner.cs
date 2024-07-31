using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;
using UnityEngine.Networking;

public class AktivasiKuesioner : MonoBehaviour
{
    private string BASE_URL = "https://docs.google.com/forms/u/3/d/e/1FAIpQLScEK2ug0YP8xy1bUkygfW3GRwkVL-YDFwraoM-gZ5PsM5r56A/formResponse";
    public TextMeshProUGUI pertanyaanText, skala5Text, skala4Text, skala3Text, skala2Text, skala1Text;
    public static string pertanyaan, skala5, skala4, skala3, skala2, skala1, jenisKuesioner;
    public static bool competenceLv1 = false, competenceLv2 = false, competenceLv3 = false, tensionLv1 = false, tensionLv2 = false, tensionLv3 = false;
    public GameObject aktivasiPilihan;
    IEnumerator Aktivasi()
    {
        yield return new WaitForSeconds(1.5f);
        aktivasiPilihan.SetActive(true);
    }
    private void Awake()
    {
        SoundManager.soundMg.PlaySound(SoundManager.AUDIO_TYPE.KLIK);
        aktivasiPilihan.SetActive(false);
        StartCoroutine(Aktivasi());
    }
    private void Start()
    {
        pertanyaanText.text = pertanyaan;
        skala5Text.text = skala5;
        skala4Text.text = skala4;
        skala3Text.text = skala3;
        skala2Text.text = skala2;
        skala1Text.text = skala1;
        //Debug.Log(competenceLv1.ToString() + " " + competenceLv2.ToString() + " " + competenceLv3.ToString() + " " + tensionLv1.ToString() + " " + tensionLv2.ToString() + " " + tensionLv3.ToString());
    }
    public static void Competence(string scene)
    {
        PlayerPrefs.SetString("sceneTerkini", scene);
        SceneManager.LoadScene("Kuesioner");
        pertanyaan = "Apakah kamu memahami alur permainan dalam level " + PlayerPrefs.GetString("levelTerkini") + " ?";
        skala5 = "Sangat memahami";
        skala4 = "Paham";
        skala3 = "Biasa saja";
        skala2 = "Cukup memahami";
        skala1 = "Tidak begitu paham";
        jenisKuesioner = "Competence";
        switch (PlayerPrefs.GetString("levelTerkini"))
        {
            case "1":
                competenceLv1 = true;
                break;
            case "2":
                competenceLv2 = true;
                break;
            case "3":
                competenceLv3 = true;
                break;
        }
    }
    public static void Flow(string scene)
    {
        PlayerPrefs.SetString("sceneTerkini", scene);
        SceneManager.LoadScene("Kuesioner");
        pertanyaan = "Selama bermain gim ini dalam waktu 1 menit lebih, apa yang kamu rasakan?";
        skala5 = "Sangat terbawa dalam dunia permainan";
        skala4 = "Menikmati permainan";
        skala3 = "Biasa saja";
        skala2 = "Cukup menikmati alur permainannya";
        skala1 = "Tidak menarik gimnya";
        jenisKuesioner = "Flow";
    }
    public static void Tension(string scene)
    {
        PlayerPrefs.SetString("sceneTerkini", scene);
        SceneManager.LoadScene("Kuesioner");
        pertanyaan = "Apakah dalam level " + PlayerPrefs.GetString("levelTerkini") + " kamu merasa kesulitan?";
        skala5 = "Sangat sulit sekali";
        skala4 = "Sulit";
        skala3 = "Biasa saja";
        skala2 = "Cukup sulit";
        skala1 = "Tidak sulit, justru mudah";
        jenisKuesioner = "Tension";
        switch (PlayerPrefs.GetString("levelTerkini"))
        {
            case "1":
                tensionLv1 = true;
                break;
            case "2":
                tensionLv2 = true;
                break;
            case "3":
                tensionLv3 = true;
                break;
        }
    }
    public static void PositiveAffect(string scene)
    {
        PlayerPrefs.SetString("sceneTerkini", scene);
        SceneManager.LoadScene("Kuesioner");
        pertanyaan = "Bagaimana kesan awal kamu terkait bermain gim ini?";
        skala5 = "Semangat untuk menyelesaikan permainan";
        skala4 = "Cukup seru permainannya";
        skala3 = "Biasa saja";
        skala2 = "Tidak begitu berkesan";
        skala1 = "Sangat tidak menarik gimnya";
        jenisKuesioner = "Positive Affect";
    }
    public static void NegativeAffect(string scene)
    {
        PlayerPrefs.SetString("sceneTerkini", scene);
        SceneManager.LoadScene("Kuesioner");
        pertanyaan = "Sebelum kamu keluar dari gim, saya ingin bertanya. Apakah gim ini membosankan untukmu?";
        skala5 = "Sangat membosankan";
        skala4 = "Membosankan";
        skala3 = "Biasa saja";
        skala2 = "Cukup membosankan";
        skala1 = "Tidak kok";
        jenisKuesioner = "Negative Affect";
    }
    public static void Challenge(string scene)
    {
        PlayerPrefs.SetString("sceneTerkini", scene);
        SceneManager.LoadScene("Kuesioner");
        pertanyaan = "Dari keseluruhan permainan Level 1, 2 dan 3, apakah menurutmu tantangan gimnya menarik?";
        skala5 = "Sangat menantang dan menarik";
        skala4 = "Menantang dan menarik";
        skala3 = "Biasa saja";
        skala2 = "Cukup menantang dan menarik";
        skala1 = "Tidak menantang dan menarik";
        jenisKuesioner = "Challenge";
    }
    public static void Immersion(string scene)
    {
        PlayerPrefs.SetString("sceneTerkini", scene);
        SceneManager.LoadScene("Kuesioner");
        pertanyaan = "Setelah mencoba berbagai pengalaman bermain gim Rapid Ratio, apa yang kamu rasakan?";
        skala5 = "Saya terlalu fokus bermain saking serunya";
        skala4 = "Saya bermain dengan senang hati";
        skala3 = "Biasa saja";
        skala2 = "Permainannya kurang menarik bagi saya";
        skala1 = "Permainannya terlalu sulit, membosankan";
        jenisKuesioner = "Immersion";
    }

    public void TombolSkala5()
    {
        StartCoroutine(Post(jenisKuesioner, "5"));
        SoundManager.soundMg.PlaySound(SoundManager.AUDIO_TYPE.KLIK);
        if (jenisKuesioner == "Negative Affect")
        {
            PlayerPrefs.DeleteKey("nama");
            PlayerPrefs.DeleteKey("levelTerkini");
            Application.Quit();
        }
    }
    public void TombolSkala4()
    {
        StartCoroutine(Post(jenisKuesioner, "4"));
        SoundManager.soundMg.PlaySound(SoundManager.AUDIO_TYPE.KLIK);
        if (jenisKuesioner == "Negative Affect")
        {
            PlayerPrefs.DeleteKey("nama");
            PlayerPrefs.DeleteKey("levelTerkini");
            Application.Quit();
        }
    }
    public void TombolSkala3()
    {
        StartCoroutine(Post(jenisKuesioner, "3"));
        SoundManager.soundMg.PlaySound(SoundManager.AUDIO_TYPE.KLIK);
        if (jenisKuesioner == "Negative Affect")
        {
            PlayerPrefs.DeleteKey("nama");
            PlayerPrefs.DeleteKey("levelTerkini");
            Application.Quit();
        }
    }
    public void TombolSkala2()
    {
        StartCoroutine(Post(jenisKuesioner, "2"));
        SoundManager.soundMg.PlaySound(SoundManager.AUDIO_TYPE.KLIK);
        if (jenisKuesioner == "Negative Affect")
        {
            PlayerPrefs.DeleteKey("nama");
            PlayerPrefs.DeleteKey("levelTerkini");
            Application.Quit();
        }
    }
    public void TombolSkala1()
    {
        StartCoroutine(Post(jenisKuesioner, "1"));
        SoundManager.soundMg.PlaySound(SoundManager.AUDIO_TYPE.KLIK);
        if (jenisKuesioner == "Negative Affect")
        {
            PlayerPrefs.DeleteKey("nama");
            PlayerPrefs.DeleteKey("levelTerkini");
            Application.Quit();
        }
    }

    [System.Obsolete]
    public IEnumerator Post(string jenisKuesioner, string nilaiSkala)
    {
        WWWForm form = new WWWForm();
        form.AddField("entry.1900092818", PlayerPrefs.GetString("nama"));
        form.AddField("entry.1457566426", jenisKuesioner);
        form.AddField("entry.2050400138", nilaiSkala);
        if (DataToGFrom.cekScene != null)
        {
            form.AddField("entry.426025614", PlayerPrefs.GetString("levelTerkini"));
        }
        else
        {
            form.AddField("entry.426025614", PlayerPrefs.GetString("sceneTerkini"));
        }

        UnityWebRequest www = UnityWebRequest.Post(BASE_URL, form);
        yield return www.SendWebRequest();
        if (www.isNetworkError)
        {
            Debug.Log(www.error);
        }
        else
        {
            Debug.Log("Form upload complete!");
        }
        www.Dispose();
        yield return new WaitForSeconds(0.5f);
        SceneManager.LoadScene(PlayerPrefs.GetString("sceneTerkini"));
    }
}
