using UnityEngine;
using UnityEngine.UI;

public class Penembak : MonoBehaviour
{
    protected Joystick joystick;
    public static Vector3 dir;
    public Transform target, saya, penembak;
    public GameObject peluru;
    public Quaternion arah;
    public Button ambil, taruh;
    public static bool bolehTembak, bolehAmbil;
    void Start()
    {
        bolehTembak = true;
        bolehAmbil = false;
        joystick = FindObjectOfType<Joystick>();
    }
    void Awake()
    {
        ambil.onClick.AddListener(KondisiPencetAmbil);
        taruh.onClick.AddListener(KondisiPencetTaruh);
    }
    private void KondisiPencetAmbil()
    {
        bolehTembak = false;
        bolehAmbil = true;
    }
    private void KondisiPencetTaruh()
    {
        bolehTembak = true;
        bolehAmbil = false;
    }
    void Update()
    {
        saya.position = new Vector3(target.position.x, target.position.y + 0.05f, target.position.z);
        // if (joystick.Horizontal + joystick.Vertical == 0 && Input.touchCount > 0 &&
        // Input.GetTouch(0).phase == TouchPhase.Began && bolehTembak)
        // {
        //     //Tembak();            
        // }
        // if (Input.GetMouseButtonDown(0))
        // TembakMouse();
    }
    public void Tembak()
    {
        SoundManager.soundMg.PlaySound(SoundManager.AUDIO_TYPE.TEMBAK);
        Ray ray = Camera.main.ScreenPointToRay(Input.GetTouch(0).position);
        Vector3 hasilPutaran = Vector3.RotateTowards(this.transform.position, dir, 1000f * Time.deltaTime, 0f);
        if (Physics.Raycast(ray, out RaycastHit hit))
        {
            dir = hit.point - saya.transform.position;
            GameObject shot = Instantiate(peluru, this.transform.position, Quaternion.LookRotation(hasilPutaran));
        }
    }
    // public void TembakMouse()
    // {
    //     Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
    //     Vector3 hasilPutaran = Vector3.RotateTowards(this.transform.position, dir, 1000f * Time.deltaTime, 0f);
    //     if (Physics.Raycast(ray, out RaycastHit hit))
    //     {
    //         if (hit.collider != null)
    //         {
    //             dir = hit.point - saya.transform.position;
    //             GameObject shot = Instantiate(peluru, this.transform.position, Quaternion.LookRotation(hasilPutaran));
    //         }
    //     }
    // }
}
