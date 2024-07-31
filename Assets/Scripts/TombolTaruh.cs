using UnityEngine;
using UnityEngine.UI;

public class TombolTaruh : MonoBehaviour
{
    private GameObject bola, tombol, Layar;
    void Awake()
    {
        bola = GameObject.Find("Bola");
        tombol = GameObject.Find("Ambil");
        this.gameObject.SetActive(false);
    }
    void Update()
    {
        Layar = GameObject.FindWithTag("Layar");
    }
    public void Taruh()
    {
        SoundManager.soundMg.PlaySound(SoundManager.AUDIO_TYPE.TARUH);
        Layar.GetComponent<InputManager>().ambil = false;
        //Layar.GetComponent<InputManager>().UpdateKondisiPemain(KondisiPemain.Idle);
        Penembak.bolehTembak = true;
        tombol.GetComponent<Image>().enabled = true;
        TombolAmbil.bolehIkut = false;
        bola.GetComponent<BolaGerak>().objek.transform.position = bola.transform.position + new Vector3(1f, -0.4f, 1f);
        this.gameObject.SetActive(false);
        //Debug.Log(ObjectPlacement.numberOfTaggedObjects + ", " + ObjectPlacement.jumlah1 + ", " + ObjectPlacement.jumlah2 + ", " + ObjectPlacement.nilaiSisa);                
    }
}
