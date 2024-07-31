using UnityEngine;
using UnityEngine.UI;

public class Objek : MonoBehaviour
{
    private GameObject tombol;
    void Awake()
    {
        tombol = GameObject.Find("Ambil");
    }
    public void OnCollisionEnter(Collision coll)
    {
        if(coll.gameObject.name == "Bola")
        {
            tombol.GetComponent<TombolAmbil>().Utuh();
            tombol.GetComponent<Button>().interactable = true;
        }
    }
    public void OnCollisionExit(Collision coll)
    {
        if(coll.gameObject.name == "Bola")
        {
            tombol.GetComponent<TombolAmbil>().Transparan();
            tombol.GetComponent<Button>().interactable = false;                        
        }
    }
}
