using System.Collections.Generic;
using UnityEngine;

public class Placement1 : MonoBehaviour
{
    List<Collider> collidedObjects1 = new List<Collider>();
    public int numberOfColliders1;
    public GameObject obj;
    void OnCollisionEnter(Collision col)
    {
        if (!collidedObjects1.Contains(col.collider) && col.gameObject.tag == "Objek")
        {
            collidedObjects1.Add(col.collider);
        }
        obj.GetComponent<ObjectPlacement>().place1.text = obj.GetComponent<ObjectPlacement>().randplace1.ToString() + "\n" + numberOfColliders1;
    }
    void OnCollisionExit(Collision col)
    {
        if (collidedObjects1.Contains(col.collider) && col.gameObject.tag == "Objek")
        {
            collidedObjects1.Remove(col.collider);
        }
        obj.GetComponent<ObjectPlacement>().place1.text = obj.GetComponent<ObjectPlacement>().randplace1.ToString() + "\n" + numberOfColliders1;
    }
    void Update()
    {
        numberOfColliders1 = collidedObjects1.Count;
    }
}
