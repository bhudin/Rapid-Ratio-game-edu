using UnityEngine;
using UnityEngine.SceneManagement;

public class SoundManager : MonoBehaviour
{
    public static SoundManager soundMg;
    bool sekali = false, duakali = false, tigakali = false, empatkali = false;
    [Header("Audio Source")]
    [SerializeField] private AudioSource _source;
    [SerializeField] private AudioSource _backgroundmusic;

    [Header("Audio Clip")]
    [SerializeField] private AudioClip _bgm;
    [SerializeField] private AudioClip _tembak;
    [SerializeField] private AudioClip _mati;
    [SerializeField] private AudioClip _lompat;
    [SerializeField] private AudioClip _ambil;
    [SerializeField] private AudioClip _taruh;
    [SerializeField] private AudioClip _menang;
    [SerializeField] private AudioClip _klik;
    [SerializeField] private AudioClip _peluru;
    [SerializeField] private AudioClip _tertembak;
    void Awake()
    {
        if (Application.isPlaying)
        {
            if (soundMg == null)
            {
                soundMg = this;
                DontDestroyOnLoad(this);
                Time.timeScale = 0;
                _backgroundmusic.clip = _bgm;
                _backgroundmusic.volume = 0.15f;
                _backgroundmusic.Play();
            }
            else
            {
                Destroy(this.gameObject);
            }
        }
    }
    void Update() => CekAktivasiKuesioner();

    public enum AUDIO_TYPE
    {
        TEMBAK,
        MATI,
        LOMPAT,
        AMBIL,
        TARUH,
        MENANG,
        KLIK,
        PELURU,
        TERTEMBAK
    }
    public void PlaySound(AUDIO_TYPE type)
    {
        _source.clip = type switch
        {
            AUDIO_TYPE.TEMBAK => _tembak,
            AUDIO_TYPE.MATI => _mati,
            AUDIO_TYPE.LOMPAT => _lompat,
            AUDIO_TYPE.AMBIL => _ambil,
            AUDIO_TYPE.TARUH => _taruh,
            AUDIO_TYPE.MENANG => _menang,
            AUDIO_TYPE.KLIK => _klik,
            AUDIO_TYPE.PELURU => _peluru,
            AUDIO_TYPE.TERTEMBAK => _tertembak,
            _ => _klik,
        };

        _source.Play();
    }
    private void CekAktivasiKuesioner()
    {
        if (SudahMulai.bolehPlay)
            Time.timeScale = 1;
        switch (Time.time)
        {
            case >= 1f when !tigakali && TombolPilihLevel.sudahPilih1 && TombolPilihLevel.sudahPilih2 && TombolPilihLevel.sudahPilih3:
                if (SceneManager.GetActiveScene().name == "Main Menu")
                {
                    AktivasiKuesioner.Challenge("Main Menu");
                    tigakali = true;
                }
                break;
            case >= 1f when !empatkali && AktivasiKuesioner.competenceLv1 && AktivasiKuesioner.competenceLv2 && AktivasiKuesioner.competenceLv3
&& AktivasiKuesioner.tensionLv1 && AktivasiKuesioner.tensionLv2 && AktivasiKuesioner.tensionLv3:
                if (SceneManager.GetActiveScene().name == "Main Menu")
                {
                    AktivasiKuesioner.Immersion("Main Menu");
                    empatkali = true;
                }
                break;
            case >= 30f when !sekali:
                if (SceneManager.GetActiveScene().name == "Main Menu" || SceneManager.GetActiveScene().name == "Pilih Level")
                {
                    AktivasiKuesioner.PositiveAffect(SceneManager.GetActiveScene().name);
                    sekali = true;
                }
                break;
            case >= 100f when !duakali:
                if (TombolPilihLevel.sudahPilih1 || TombolPilihLevel.sudahPilih2 || TombolPilihLevel.sudahPilih3)
                {
                    if (SceneManager.GetActiveScene().name == "Main Menu")
                    {
                        AktivasiKuesioner.Flow(SceneManager.GetActiveScene().name);
                        duakali = true;
                    }
                }
                break;
            default:
                break;
        }
    }
}
