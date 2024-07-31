using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using Unity.VisualScripting;

public class Mulai : MonoBehaviour
{
    public TMP_InputField isianNama;
    private string isian, cek;
    public Button tombolOK;
    public GameObject aktifkan, check;
    void Awake()
    {
        cek = PlayerPrefs.GetString("nama");
        if (cek == null || cek == "")
        {
            aktifkan.SetActive(false);
        }
        else if (cek != null || cek != "")
        {
            aktifkan.SetActive(true);
            isianNama.gameObject.SetActive(false);
            tombolOK.gameObject.SetActive(false);
        }
    }
    void Update()
    {
        isian = isianNama.text;
        if (isian == null || isian == "")
        {
            tombolOK.gameObject.SetActive(false);
        }
        else if (isianNama.isActiveAndEnabled)
        {
            tombolOK.gameObject.SetActive(true);
        }
    }
    public void KlikOK()
    {
        SoundManager.soundMg.PlaySound(SoundManager.AUDIO_TYPE.KLIK);
        PlayerPrefs.SetString("nama", isian);
        SudahMulai.sekali = true;
        Destroy(isianNama.gameObject);
        Destroy(tombolOK.gameObject);
        Destroy(this);
        aktifkan.SetActive(true);
    }
}
