using UnityEngine;

public class KeluarGame : MonoBehaviour
{
    public void KlikKeluarGame()
    {
        Time.timeScale = 1;
        AktivasiKuesioner.NegativeAffect("Main Menu");
    }
}
