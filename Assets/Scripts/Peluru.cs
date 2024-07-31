using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Peluru : MonoBehaviour
{
    public float Timer = 1f;
    public Transform peluru;
    void Awake()
    {
        peluru.transform.Rotate(40.0f, 0.0f, 0.0f, Space.Self);        
    }
    void Update()
    {
        Timer -= 1 * Time.deltaTime;
        if (Timer <= 0)
        {
            Destroy(peluru.gameObject);
        }
        peluru.GetComponent<Rigidbody>().AddForceAtPosition(Penembak.dir * 20f, transform.position);     
    }

    private void OnCollisionEnter(Collision other) {
        if(other.gameObject.tag == "Musuh")
        {
            Destroy(peluru.gameObject);
        }
    }
}
