using UnityEngine;

public class PeluruMusuh : MonoBehaviour
{
    public float Timer = 1f;
    public Transform projectile;
    void Update()
    {
        Timer -= 0.5f * Time.deltaTime;
        if (Timer <= 0)
        {
            Destroy(projectile.gameObject);
        }
    }
    public void OnCollisionEnter(Collision coll){
        if (coll.gameObject.tag == "Tanah")
        {
            Destroy(projectile.gameObject);
        }
    }
}
