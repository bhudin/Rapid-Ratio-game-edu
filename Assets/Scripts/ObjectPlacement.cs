using UnityEngine;
using TMPro;

public class ObjectPlacement : MonoBehaviour
{
    public TextMeshProUGUI place1, place2, place3;
    public GameObject tempat1, tempat2, sisa, activateTanah, kumpulanObjek;
    private Placement1 plc1;
    private Placement2 plc2;
    private Placement3 plc3;
    private GameObject UI;
    public int randplace1, randplace2, total, nilaiSisa,
    numberOfTaggedObjects, jumlah1, jumlah2, jumlahSisa, nilaiMax1 = 0, nilaiMax2 = 0;
    void Start()
    {
        if (plc1 == null)
            plc1 = FindObjectOfType<Placement1>();
        if (plc2 == null)
            plc2 = FindObjectOfType<Placement2>();
        if (plc3 == null)
            plc3 = FindObjectOfType<Placement3>();
        UI = GameObject.FindWithTag("Layar");
        activateTanah.SetActive(false);
        randplace1 = Random.Range(1, 4);
        randplace2 = Random.Range(1, 4);
        do
        {
            randplace2 = Random.Range(1, 4);
        }
        while (randplace2 == randplace1);
        place1.text = randplace1.ToString() + "\n" + jumlah1;
        place2.text = randplace2.ToString() + "\n" + jumlah2;
        place3.text = "Sisa" + "\n" + "0";
        total = randplace1 + randplace2;
        numberOfTaggedObjects = GameObject.FindGameObjectsWithTag("Objek").Length;
        nilaiSisa = Mod(numberOfTaggedObjects, total);
        do
        {
            nilaiMax1 += randplace1;
            nilaiMax2 += randplace2;
        }
        while (nilaiMax1 + nilaiMax2 < numberOfTaggedObjects - nilaiSisa);
    }
    int Mod(int a, int n) => (a % n + n) % n;
    void Update()
    {
        numberOfTaggedObjects = GameObject.FindGameObjectsWithTag("Objek").Length;
        jumlah1 = tempat1.GetComponent<Placement1>().numberOfColliders1;
        jumlah2 = tempat2.GetComponent<Placement2>().numberOfColliders2;
        jumlahSisa = sisa.GetComponent<Placement3>().numberOfColliders3;
        if (jumlah1 == nilaiMax1 && jumlah2 == nilaiMax2 && jumlahSisa == nilaiSisa)
        {
            activateTanah.SetActive(true);
            SoundManager.soundMg.PlaySound(SoundManager.AUDIO_TYPE.MENANG);
            place1.text = randplace1.ToString() + "\n" + jumlah1;
            place2.text = randplace2.ToString() + "\n" + jumlah2;
            place3.text = "Sisa";
            Destroy(kumpulanObjek);
            Destroy(this);
            Destroy(plc1);
            Destroy(plc2);
            Destroy(plc3);
            UI.GetComponent<InputManager>().energy += 3000;
        }
        else if (jumlah1 == nilaiMax1 - randplace1 && jumlah2 == nilaiMax2 - randplace2 &&
        jumlahSisa == nilaiSisa + randplace1 + randplace2)
        {
            activateTanah.SetActive(true);
            SoundManager.soundMg.PlaySound(SoundManager.AUDIO_TYPE.MENANG);
            place1.text = randplace1.ToString() + "\n" + jumlah1;
            place2.text = randplace2.ToString() + "\n" + jumlah2;
            place3.text = "Sisa";
            Destroy(kumpulanObjek);
            Destroy(this);
            Destroy(plc1);
            Destroy(plc2);
            Destroy(plc3);
            UI.GetComponent<InputManager>().energy += 1500;
        }
        else if (jumlah1 == randplace1 && jumlah2 == randplace2 &&
        jumlahSisa == numberOfTaggedObjects - randplace1 - randplace2)
        {
            activateTanah.SetActive(true);
            SoundManager.soundMg.PlaySound(SoundManager.AUDIO_TYPE.MENANG);
            place1.text = randplace1.ToString() + "\n" + jumlah1;
            place2.text = randplace2.ToString() + "\n" + jumlah2;
            place3.text = "Sisa";
            Destroy(kumpulanObjek);
            Destroy(this);
            Destroy(plc1);
            Destroy(plc2);
            Destroy(plc3);
            UI.GetComponent<InputManager>().energy += 500;
        }
    }
}
