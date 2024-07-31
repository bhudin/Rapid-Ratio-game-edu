using System.Collections.Generic;
using UnityEngine;

public class Placement3 : MonoBehaviour
{
    List<Collider> collidedObjects3 = new List<Collider>();
    public int numberOfColliders3;
    public GameObject obj;
    void OnCollisionEnter(Collision col)
    {
        if (!collidedObjects3.Contains(col.collider) && col.gameObject.tag == "Objek")
        {
            collidedObjects3.Add(col.collider);
        }
        obj.GetComponent<ObjectPlacement>().place3.text = "Sisa" + "\n" + numberOfColliders3;
    }
    void OnCollisionExit(Collision col)
    {
        if (collidedObjects3.Contains(col.collider) && col.gameObject.tag == "Objek")
        {
            collidedObjects3.Remove(col.collider);
        }
        obj.GetComponent<ObjectPlacement>().place3.text = "Sisa" + "\n" + numberOfColliders3;
    }
    void Update()
    {
        numberOfColliders3 = collidedObjects3.Count;
    }
}
