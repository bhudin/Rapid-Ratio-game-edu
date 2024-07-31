using UnityEngine;
using UnityEngine.SceneManagement;

public class BolaGerak : MonoBehaviour
{
    private Scene scene;
    public GameObject objek, inputManager;
    public int hitungLompat = 0;

    void Awake()
    {
        //objek = GameObject.FindWithTag("Objek");
        scene = SceneManager.GetActiveScene();
        inputManager = GameObject.FindWithTag("Layar");
    }

    [System.Obsolete]
    public void OnCollisionEnter(Collision coll)
    {
        if (coll.gameObject.tag == "Objek" && !Penembak.bolehAmbil)
        {
            objek = coll.gameObject;
            Penembak.bolehTembak = false;
        }
        if (coll.gameObject.tag == "Tanah")
        {
            hitungLompat = 1;
        }
        if (coll.gameObject.tag == "Peluru")
        {
            if (scene.name == "1" || scene.name == "2" || scene.name == "3")
            {
                SoundManager.soundMg.PlaySound(SoundManager.AUDIO_TYPE.MATI);
                inputManager.GetComponent<InputManager>().UpdateKondisiPemain(KondisiPemain.Kalah);
                SceneManager.LoadSceneAsync(scene.name);                
            }
            else
            { 
                this.transform.position = new Vector3(0f,1f,0f);
            }
        }
    }
    public void OnCollisionExit(Collision coll)
    {
        if (coll.gameObject.tag == "Objek" && !Penembak.bolehAmbil)
        {
            Penembak.bolehTembak = true;
        }
    }
}
