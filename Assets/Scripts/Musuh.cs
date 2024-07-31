using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System.Linq;
using Unity.VisualScripting;

public class Musuh : MonoBehaviour
{
    private List<decimal> nyawaMusuh = new List<decimal>();
    private List<decimal> acakPeluru = new List<decimal>();
    private decimal life;
    public decimal peluru1, peluru2, peluru3, peluru4, selisih;
    public TextMeshProUGUI teksNyawaMusuh;
    public Rigidbody projectile;
    private Transform shotPos, targeted;
    public GameObject kumpulanMusuh;
    private GameObject target, tambahSkor;
    public GameObject[] ambilPeluru;
    public static float shotForce = 800f;
    public static float Timer = 0.5f, rotateAroundSpeed = 400f, rotationSpeed = 100f;
    public Transform[] patrolPoints, ambilPelurupos, acakPelurupos;
    private int currentPoint;
    [SerializeField]
    private float moveSpeed = 3f;
    void Awake()
    {
        shotPos = this.transform;
        target = GameObject.FindWithTag("Bola");
        tambahSkor = GameObject.FindWithTag("Layar");
        targeted = target.transform;
    }
    void Start()
    {
        PenentuanMusuh();
        transform.position = patrolPoints[0].position;
        currentPoint = 0;
    }
    void Update()
    {
        if (life == 0)
        {
            tambahSkor.GetComponent<InputManager>().energy += 1500;
            SoundManager.soundMg.PlaySound(SoundManager.AUDIO_TYPE.MENANG);
            Destroy(kumpulanMusuh);
            InputManager.dapatPeluru = false;
            InputManager.cekPeluru = false;
        }
        Shoot();
        if (transform.position == patrolPoints[currentPoint].position)
        {
            currentPoint++;
        }
        if (currentPoint >= patrolPoints.Length)
        {
            currentPoint = 0;
        }
        transform.position = Vector3.MoveTowards(transform.position, patrolPoints[currentPoint].position, moveSpeed * Time.deltaTime);
    }
    public void Shoot()
    {
        Timer -= 1 * Time.deltaTime;
        if (Timer <= 0)
        {
            var curRot = shotPos.rotation;
            shotPos.RotateAround(shotPos.position, Vector3.up, rotateAroundSpeed * Time.deltaTime);
            shotPos.rotation = Quaternion.Slerp(curRot, Quaternion.LookRotation(
                targeted.position - shotPos.position), rotationSpeed * Time.deltaTime);
            Rigidbody shot = Instantiate(projectile, shotPos.position + new Vector3(-0.2f, 0.5f, -0.2f), shotPos.rotation) as Rigidbody;
            shot.AddForce(shotPos.forward * shotForce);
            Timer = 0.5f;
        }
    }
    public void PenentuanMusuh()
    {
        AcakPosisiAmbilPeluru(Random.Range(0, 5));
        nyawaMusuh.Add((decimal)0.6);
        nyawaMusuh.Add((decimal)0.12);
        nyawaMusuh.Add((decimal)0.36);
        nyawaMusuh.Add((decimal)0.48);
        life = nyawaMusuh[Random.Range(0, nyawaMusuh.Count)];
        teksNyawaMusuh.text = FractionConverter.Convertt(life);
        peluru1 = life / 3;
        peluru2 = life / 2 + peluru1;
        peluru3 = -(life / 3);
        peluru4 = -(life / 2) + peluru3;
        acakPeluru.Add(peluru1);
        acakPeluru.Add(peluru2);
        acakPeluru.Add(peluru3);
        acakPeluru.Add(peluru4);
        int index = 0;
        foreach (GameObject g in ambilPeluru)
        {
            g.GetComponent<AmbilPeluru>().nilaiPeluru = acakPeluru[index];
            index++;
        }
    }
    void AcakPosisiAmbilPeluru(int input)
    {
        if (input == 0)
        {
            ambilPelurupos[0].position = acakPelurupos[1].position;
            ambilPelurupos[1].position = acakPelurupos[3].position;
            ambilPelurupos[2].position = acakPelurupos[2].position;
            ambilPelurupos[3].position = acakPelurupos[0].position;
        }
        else if (input == 1)
        {
            ambilPelurupos[0].position = acakPelurupos[2].position;
            ambilPelurupos[1].position = acakPelurupos[1].position;
            ambilPelurupos[2].position = acakPelurupos[0].position;
            ambilPelurupos[3].position = acakPelurupos[3].position;
        }
        else if (input == 2)
        {
            ambilPelurupos[0].position = acakPelurupos[3].position;
            ambilPelurupos[1].position = acakPelurupos[2].position;
            ambilPelurupos[2].position = acakPelurupos[1].position;
            ambilPelurupos[3].position = acakPelurupos[0].position;
        }
        else if (input == 3)
        {
            ambilPelurupos[0].position = acakPelurupos[0].position;
            ambilPelurupos[1].position = acakPelurupos[3].position;
            ambilPelurupos[2].position = acakPelurupos[1].position;
            ambilPelurupos[3].position = acakPelurupos[2].position;
        }
        else if (input == 4)
        {
            ambilPelurupos[0].position = acakPelurupos[2].position;
            ambilPelurupos[1].position = acakPelurupos[3].position;
            ambilPelurupos[2].position = acakPelurupos[1].position;
            ambilPelurupos[3].position = acakPelurupos[0].position;
        }
    }
    void OnCollisionEnter(Collision col)
    {
        if (col.gameObject.tag == "Tembak")
        {
            life += selisih;
            teksNyawaMusuh.text = FractionConverter.Convertt(life);
            SoundManager.soundMg.PlaySound(SoundManager.AUDIO_TYPE.TERTEMBAK);
        }
    }
}
