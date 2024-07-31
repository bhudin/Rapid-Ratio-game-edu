using System.Collections.Generic;
using UnityEngine;

public class Placement2 : MonoBehaviour
{
    List<Collider> collidedObjects2 = new List<Collider>();
    public int numberOfColliders2;
    public GameObject obj;
    void OnCollisionEnter(Collision col)
    {
        if (!collidedObjects2.Contains(col.collider) && col.gameObject.tag == "Objek")
        {
            collidedObjects2.Add(col.collider);
        }
        obj.GetComponent<ObjectPlacement>().place2.text = obj.GetComponent<ObjectPlacement>().randplace2.ToString() + "\n" + numberOfColliders2;
    }
    void OnCollisionExit(Collision col)
    {
        if (collidedObjects2.Contains(col.collider) && col.gameObject.tag == "Objek")
        {
            collidedObjects2.Remove(col.collider);
        }
        obj.GetComponent<ObjectPlacement>().place2.text = obj.GetComponent<ObjectPlacement>().randplace2.ToString() + "\n" + numberOfColliders2;
    }
    void Update()
    {
        numberOfColliders2 = collidedObjects2.Count;
    }
}
