using UnityEngine;
using TMPro;

public class AmbilPeluru : MonoBehaviour
{
    public decimal nilaiPeluru;
    public GameObject musuh;
    public TextMeshProUGUI teksAmbilPeluru;
    void Update()
    {
        if(nilaiPeluru > 0)
        {
            teksAmbilPeluru.text = "+" + nilaiPeluru.ToString();
        }
        else
        {
            teksAmbilPeluru.text = nilaiPeluru.ToString();
        }        
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Bola")
        {
            SoundManager.soundMg.PlaySound(SoundManager.AUDIO_TYPE.PELURU);
            musuh.GetComponent<Musuh>().selisih = nilaiPeluru;
            InputManager.dapatPeluru = true;
            InputManager.cekPeluru = true;
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Bola")
        {
            InputManager.cekPeluru = false;
        }
    }
}
