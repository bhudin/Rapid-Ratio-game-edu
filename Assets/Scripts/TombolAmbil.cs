using UnityEngine;
using UnityEngine.UI;

public class TombolAmbil : MonoBehaviour
{
    private Image gambar;
    private GameObject bola, Layar;
    public GameObject tombol, taruh;
    public static bool bolehIkut;
    void Awake()
    {     
        Transparan();
        bola = GameObject.Find("Bola");
        //Layar = GameObject.FindGameObjectsWithTag("Layar");
        bolehIkut = false;
        tombol.GetComponent<Button>().interactable = false;
    }
    public void Transparan()
    {
        gambar = tombol.GetComponent<Image>();
        Color transparan = gambar.color;
        transparan.a = 0.5f;
        gambar.color = transparan;
    }
    public void Utuh()
    {
        gambar = tombol.GetComponent<Image>();
        Color warnaasli = gambar.color;
        warnaasli.a = 1f;
        gambar.color = warnaasli;
    }
    public void Ambil()
    {
        SoundManager.soundMg.PlaySound(SoundManager.AUDIO_TYPE.AMBIL);
        Layar.GetComponent<InputManager>().ambil = true;
        bolehIkut = true;
        Penembak.bolehTembak = false;
        taruh.SetActive(true);
        tombol.GetComponent<Image>().enabled = false;
    }
    void Update()
    {
        Layar = GameObject.FindWithTag("Layar");
        if(bolehIkut)
        {
            //Layar.GetComponent<InputManager>().UpdateKondisiPemain(KondisiPemain.Pointing);
            bola.GetComponent<BolaGerak>().objek.transform.position = bola.transform.position + new Vector3(0f, 1.2f, 0f);
        }    
    }
}
