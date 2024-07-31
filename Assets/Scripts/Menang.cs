using UnityEngine;
using UnityEngine.SceneManagement;

public class Menang : MonoBehaviour
{
    private GameObject canvas;
    void Awake()
    {
        canvas = GameObject.FindWithTag("Layar");
    }
    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Bola" && GameObject.FindGameObjectsWithTag("Musuh").Length <= 0)
        {
            canvas.GetComponent<InputManager>().UpdateKondisiPemain(KondisiPemain.Menang);
            canvas.GetComponent<InputManager>().PengirimanData();
            AktivasiKuesioner.Competence("Finish Level");
        }
    }
}
