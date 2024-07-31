using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;
using System.Collections.Generic;
using System.IO;
using System;

public enum KondisiPemain { A, DU, DD, DL, DR, T, P, F, I, Menang, Kalah };
public class DataKondisiPemain
{
    public KondisiPemain kondisiPemain;
    public float timestamp;
}
public class InputManager : MonoBehaviour

{
    public KondisiPemain kondisiTerakhir = KondisiPemain.A;
    public static List<DataKondisiPemain> listDataKondisiPemain = new List<DataKondisiPemain>();
    public bool ambil = false, deteksiFlick;
    public static bool dapatPeluru, cekPeluru;
    public TextMeshProUGUI scoreText, limitText, timeText, peluruText, levelText, labelKondisiPemain;
    private int time;
    public int limit, energy;
    public static int score;
    private GameObject bola, penembak, musuh;
    private GameObject[] paraMusuh;
    public GameObject prefab;
    static DataToGFrom cekKirimData;
    public Scene scene;
    protected Joystick joystick;
    private Vector3 input;
    private Rigidbody rg;
    private float maxSpeed = 40f;
    public float moveSpeed = 20f, Flick_THRESHOLD = 100f, odometer = 0f;
    private Vector2 fingerDownPos;
    private Vector2 fingerUpPos;
    [SerializeField] private Slider slider;
    [SerializeField] private AudioSource jalan;
    void Start()
    {
        UpdateKondisiPemain(KondisiPemain.A);
        energy = 3000;
        levelText.text = "Level " + scene.name;
    }
    void Awake()
    {
        jalan.Play();
        musuh = GameObject.FindWithTag("Musuh");
        if (cekKirimData == null)
        {
            Instantiate(prefab);
        }
        deteksiFlick = false;
        dapatPeluru = false;
        cekPeluru = false;
        scene = SceneManager.GetActiveScene();
        bola = GameObject.Find("Bola");
        penembak = GameObject.Find("PivotPenembak");
        joystick = FindObjectOfType<Joystick>();
        rg = bola.GetComponent<Rigidbody>();
    }

    public void UpdateKondisiPemain(KondisiPemain kondisiPemain)
    {
        if (kondisiPemain == kondisiTerakhir)
        {
            return;
        }
        kondisiTerakhir = kondisiPemain;
        switch (kondisiPemain)
        {
            case KondisiPemain.A:
                labelKondisiPemain.text = "Awal";
                break;
            case KondisiPemain.DU:
                labelKondisiPemain.text = "Dragging Up";
                break;
            case KondisiPemain.DD:
                labelKondisiPemain.text = "Dragging Down";
                break;
            case KondisiPemain.DL:
                labelKondisiPemain.text = "Dragging Left";
                break;
            case KondisiPemain.DR:
                labelKondisiPemain.text = "Dragging Right";
                break;
            case KondisiPemain.T:
                labelKondisiPemain.text = "Tapping";
                break;
            case KondisiPemain.P:
                labelKondisiPemain.text = "Pointing";
                break;
            case KondisiPemain.F:
                labelKondisiPemain.text = "Flicking";
                break;
            case KondisiPemain.I:
                labelKondisiPemain.text = "Idle";
                break;
            case KondisiPemain.Menang:
                labelKondisiPemain.text = "Menang";
                break;
            case KondisiPemain.Kalah:
                labelKondisiPemain.text = "Kalah";
                break;
            default:
                labelKondisiPemain.text = "Game Start";
                break;
        }
        DataKondisiPemain data = new DataKondisiPemain();
        data.kondisiPemain = kondisiPemain;
        data.timestamp = Round(Time.timeSinceLevelLoad, 3);
        listDataKondisiPemain.Add(data);
    }

    public float Round(float nilai, int digit)
    {
        float multi = Mathf.Pow(10.0f, (float)digit);
        return Mathf.Round(nilai * multi) / multi;
    }

    [System.Obsolete]
    public void PlayerDrag()
    {
        var rigidbody = bola.GetComponent<Rigidbody>();
        rigidbody.velocity = new Vector3(joystick.Horizontal * 10f,
                                        rigidbody.velocity.y,
                                        joystick.Vertical * 10f);
        input = new Vector3(joystick.Horizontal, 0f, joystick.Vertical);
        if (rg.velocity.magnitude < maxSpeed)
        {
            jalan.pitch = rg.velocity.magnitude / 4f;
            jalan.volume = 0.5f;
            odometer += Vector3.Distance(input, new Vector3(0f, 0f, 0f));
            limit = (int)(energy - (odometer / 2));
            //limitText.text = "Energi   = " + limit;
            slider.value = limit;
            if (limit < 1 && (scene.name == "1" || scene.name == "2" || scene.name == "3"))
            {
                SoundManager.soundMg.PlaySound(SoundManager.AUDIO_TYPE.MATI);
                UpdateKondisiPemain(KondisiPemain.Kalah);
                SceneManager.LoadSceneAsync(scene.name);
            }
            else if (limit < 1 && scene.name == "Tutorial")
            {
                energy += 3000;
                bola.transform.position = new Vector3(0f, 1f, 0f);
            }
            rg.AddForce(input * moveSpeed);
        }
        if (bola.transform.position.y < -2f)
        {
            if (scene.name == "1" || scene.name == "2" || scene.name == "3")
            {
                SoundManager.soundMg.PlaySound(SoundManager.AUDIO_TYPE.MATI);
                UpdateKondisiPemain(KondisiPemain.Kalah);
                SceneManager.LoadSceneAsync(scene.name);
            }
            else if (scene.name == "Tutorial")
            {
                bola.transform.position = new Vector3(0f, 1f, 0f);
            }
        }
    }
    [System.Obsolete]
    void OnDisable()
    {
        PengirimanData();
        jalan.Stop();
    }
    private string getPath()
    {
#if UNITY_EDITOR
        return Application.dataPath + "/Data/" + "Saved_Inventory.csv";
#elif UNITY_ANDROID
        return Application.persistentDataPath+"/" + "Saved_Inventory.csv";
#elif UNITY_IPHONE
        return Application.persistentDataPath+"/"+"Saved_Inventory.csv";
#else
        return Application.dataPath + "/" + "Saved_Inventory.csv";
#endif
    }
    // void UpdateData()
    // {
    //     string filePath = getPath();
    //     StreamWriter writer = new StreamWriter(filePath, append: true);
    //     listDataKondisiPemain.ForEach(data => writer.WriteLine(data.kondisiPemain + "," + data.timestamp));
    //     writer.Flush();
    //     writer.Close();
    //     enabled = false;
    // }

    public void PengirimanData()
    {
        if (Round(Time.timeSinceLevelLoad, 3) > 1f)
        {
            DataToGFrom.kirimDataa.Send();
            PlayerPrefs.SetString("levelTerkini", scene.name);
            listDataKondisiPemain.Clear();
        }
    }
    [System.Obsolete]
    void Update()
    {
        if (ambil)
        {
            UpdateKondisiPemain(KondisiPemain.P);
        }
        else if (joystick.Horizontal >= 0 && joystick.Vertical >= 0
        && joystick.Horizontal + joystick.Vertical != 0)
        {
            UpdateKondisiPemain(KondisiPemain.DU);
        }
        else if (joystick.Horizontal < 0 && joystick.Vertical < 0
        && joystick.Horizontal + joystick.Vertical != 0)
        {
            UpdateKondisiPemain(KondisiPemain.DD);
        }
        else if (joystick.Horizontal >= 0 && joystick.Vertical < 0
        && joystick.Horizontal + joystick.Vertical != 0)
        {
            UpdateKondisiPemain(KondisiPemain.DR);
        }
        else if (joystick.Horizontal < 0 && joystick.Vertical >= 0
        && joystick.Horizontal + joystick.Vertical != 0)
        {
            UpdateKondisiPemain(KondisiPemain.DL);
        }
        else
        {
            UpdateKondisiPemain(KondisiPemain.I);
        }
        if (dapatPeluru)
        {
            if (cekPeluru)
            {
                paraMusuh = GameObject.FindGameObjectsWithTag("Musuh");
                peluruText.enabled = true;
                musuh = GameObject.FindWithTag("Musuh");
            }
            CekDapatPeluru();
        }
        else { peluruText.enabled = false; paraMusuh = null; musuh = null; }
        UIText();
        PlayerDrag();
        DeteksiSentuh();
    }

    private void UIText()
    {
        time = (int)Time.timeSinceLevelLoad;
        timeText.text = "Time = " + time.ToString() + " sec";
        if (time > 0)
            score = (int)(limit / time);
        scoreText.text = "Score    = " + score;
    }

    private void CekDapatPeluru()
    {
        if (musuh.GetComponent<Musuh>().selisih > 0)
        {
            foreach (GameObject m in paraMusuh)
            {
                if (m.GetComponent<Musuh>().selisih != 0)
                {
                    peluruText.text = "Peluru = +" + m.GetComponent<Musuh>().selisih;
                }
            }
        }
        else if (musuh.GetComponent<Musuh>().selisih < 0)
        {
            foreach (GameObject m in paraMusuh)
            {
                if (m.GetComponent<Musuh>().selisih != 0)
                    peluruText.text = "Peluru = " + musuh.GetComponent<Musuh>().selisih;
            }
        }
    }

    public void DeteksiSentuh()
    {
        foreach (Touch touch in Input.touches)
        {
            switch (touch.phase)
            {
                case TouchPhase.Moved:
                    deteksiFlick = true;
                    if (joystick.Horizontal + joystick.Vertical == 0)
                    {
                        fingerDownPos = touch.position;
                        DetectFlick();
                    }
                    break;
                case TouchPhase.Began:
                    if (Penembak.bolehTembak && !Penembak.bolehAmbil)
                    {
                        deteksiFlick = false;
                    }
                    if (joystick.Horizontal + joystick.Vertical == 0)
                    {
                        fingerUpPos = touch.position;
                        fingerDownPos = touch.position;
                    }
                    break;
                case TouchPhase.Ended:
                    if (!deteksiFlick && Penembak.bolehTembak)
                    {
                        if (dapatPeluru)
                        {
                            UpdateKondisiPemain(KondisiPemain.T);
                            energy -= 10;
                            penembak.GetComponent<Penembak>().Tembak();
                        }
                    }
                    if (joystick.Horizontal + joystick.Vertical != 0)
                    {
                        fingerDownPos = touch.position;
                        DetectFlick();
                    }
                    break;
            }
        }
    }
    void DetectFlick()
    {
        if (VerticalMoveValue() > Flick_THRESHOLD && VerticalMoveValue() > HorizontalMoveValue())
        {
            if (fingerDownPos.y - fingerUpPos.y > 0 && Penembak.bolehTembak &&
            bola.GetComponent<BolaGerak>().hitungLompat == 1)
            {
                OnFlickUp();
            }
            else if (fingerDownPos.y - fingerUpPos.y < 0 && Penembak.bolehTembak)
            {
                OnFlickDown();
            }
            fingerUpPos = fingerDownPos;

        }
        else if (HorizontalMoveValue() > Flick_THRESHOLD && HorizontalMoveValue() > VerticalMoveValue())
        {
            if (fingerDownPos.x - fingerUpPos.x > 0 && Penembak.bolehTembak)
            {
                OnFlickRight();
            }
            else if (fingerDownPos.x - fingerUpPos.x < 0 && Penembak.bolehTembak)
            {
                OnFlickLeft();
            }
            fingerUpPos = fingerDownPos;

        }
    }

    float VerticalMoveValue()
    {
        return Mathf.Abs(fingerDownPos.y - fingerUpPos.y);
    }

    float HorizontalMoveValue()
    {
        return Mathf.Abs(fingerDownPos.x - fingerUpPos.x);
    }

    void OnFlickUp()
    {
        SoundManager.soundMg.PlaySound(SoundManager.AUDIO_TYPE.LOMPAT);
        UpdateKondisiPemain(KondisiPemain.F);
        Vector3 atas = new Vector3(0, 25, 0);
        rg.AddForce(atas * 20f);
        bola.GetComponent<BolaGerak>().hitungLompat -= 1;
    }

    void OnFlickDown()
    {
        //Debug.Log ("Flick Bawah");
    }

    void OnFlickLeft()
    {
        //Debug.Log ("Flick Kiri");
    }

    void OnFlickRight()
    {
        //Debug.Log ("Flick Kanan");
    }

}
