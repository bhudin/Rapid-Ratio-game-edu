using System.Collections;
using System.Collections.Generic;
using UnityEngine.Networking;
using UnityEngine;
using UnityEngine.SceneManagement;
using System;

[System.Serializable]
public class KirimDataPemain
{
    public string cond;
    public string time;
}
public class DataToGFrom : MonoBehaviour
{
    public static string cekScene = null;
    private GameObject canvas;
    public static List<ScoreList> dataScore;
    private int percobaan = 1, skorTerakhir;
    private List<string> kirim = new List<string>();
    private string BASE_URL = "https://docs.google.com/forms/u/3/d/e/1FAIpQLSe0IEsj9WEx2pAoMp-L11jYuqEUbcj0F60xMVade2oVORMupg/formResponse";
    public static string kondisiPermainan;
    public static DataToGFrom kirimDataa;
    private void Awake()
    {
        dataScore = new List<ScoreList>();
        if (Application.isPlaying)
        {
            if (kirimDataa == null)
            {
                DontDestroyOnLoad(this);
                kirimDataa = this;
            }
            else
            {
                Destroy(this.gameObject, 0.5f);
            }
        }
    }
    private void Update()
    {
        switch (SceneManager.GetActiveScene().name)
        {
            case "1":
                cekScene = SceneManager.GetActiveScene().name;
                break;
            case "2":
                cekScene = SceneManager.GetActiveScene().name;
                break;
            case "3":
                cekScene = SceneManager.GetActiveScene().name;
                break;
            default:
                cekScene = null;
                break;
        }
    }
    [System.Obsolete]
    public IEnumerator Posting(string kondisiPemain)
    {
        WWWForm form = new WWWForm();
        form.AddField("entry.618357553", PlayerPrefs.GetString("nama"));
        form.AddField("entry.1411345836", cekScene);
        form.AddField("entry.969452388", percobaan.ToString());
        form.AddField("entry.1928167348", skorTerakhir.ToString());
        form.AddField("entry.204748008", kondisiPemain);
        form.AddField("entry.1161947512", PlayerPrefs.GetString("statusPermainan"));
        //Debug.Log(PlayerPrefs.GetString("nama") + " " + scene.name + " " + percobaan.ToString() + " " + PlayerPrefs.GetString("statusPermainan"));


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
    }

    [System.Obsolete]
    public void Send()
    {
        if (PlayerPrefs.GetString("levelTerkini") != cekScene)
        {
            PlayerPrefs.DeleteKey("levelTerkini");
            percobaan = 1;
            SceneManager.MoveGameObjectToScene(this.gameObject, SceneManager.GetActiveScene());
        }
        else
        {
            Debug.Log(percobaan);
            percobaan += 1;
        }
        foreach (DataKondisiPemain data in InputManager.listDataKondisiPemain)
        {
            KirimDataPemain dataPemain = new KirimDataPemain();
            dataPemain.cond = data.kondisiPemain.ToString();
            dataPemain.time = string.Format("{0:0.000}", data.timestamp);
            string pemainToJSON = JsonUtility.ToJson(dataPemain);
            kirim.Add(pemainToJSON);
            if (data.kondisiPemain == KondisiPemain.Menang)
            {
                PlayerPrefs.SetString("statusPermainan", "Menang");
                dataScore.Add(new ScoreList(PlayerPrefs.GetString("nama"), InputManager.score, cekScene));
                if (InputManager.score > PlayerPrefs.GetInt(cekScene))
                {
                    PlayerPrefs.SetInt(cekScene, InputManager.score);
                }
                SceneManager.MoveGameObjectToScene(this.gameObject, SceneManager.GetActiveScene());
            }
            else if (data.kondisiPemain == KondisiPemain.Kalah)
            {
                PlayerPrefs.SetString("statusPermainan", "Kalah");
            }
        }
        skorTerakhir = InputManager.score;
        string dataKirim = String.Join(",", kirim);
        dataKirim = "[" + dataKirim + "]";        
        switch (PlayerPrefs.GetString("statusPermainan"))
        {
            case "Menang" when dataKirim.Contains("Menang"):
                StartCoroutine(Posting(dataKirim));
                kirim.Clear();
                break;
            case "Kalah" when percobaan == 3 && dataKirim.Contains("Kalah"):
                StartCoroutine(Posting(dataKirim));
                AktivasiKuesioner.Tension(cekScene);
                kirim.Clear();
                break;
            case "Kalah" when dataKirim.Contains("Kalah"):
                StartCoroutine(Posting(dataKirim));
                kirim.Clear();
                break;
            default:
                kirim.Clear();
                break;
        }
    }
}
