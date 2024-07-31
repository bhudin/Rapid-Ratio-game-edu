using UnityEngine;

public class CameraIkut : MonoBehaviour
{
    public Quaternion targetRotation;
    private Transform PlayerTransform;
    private GameObject player;
    private Vector3 cameraOffset;
    [Range(0.01f, 1.0f)]
    public float SmoothFactor = 0.7f;
    void Awake()
    {
        player = GameObject.FindWithTag("Bola");
        PlayerTransform = player.transform;
    }
    void Start()
    {
        cameraOffset = transform.position - PlayerTransform.position;
    }
    void Update()
    {
        Plane playerPlane = new Plane(Vector3.up, transform.position);
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        float hitdist = 0.0f;
        if (playerPlane.Raycast(ray, out hitdist))
        {
            Vector3 targetPoint = ray.GetPoint(hitdist);
            targetRotation = Quaternion.LookRotation(targetPoint - transform.position);
            if (transform.rotation.y < 0.1f && transform.rotation.y > -0.1f)
            {
                transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, 1f * Time.deltaTime);
            }
            else if (transform.rotation.y > 0.1f)
            {
                transform.Rotate(new Vector3(0f, -0.1f, 0f), Space.World);
            }
            else if (transform.rotation.y < -0.1f)
            {
                transform.Rotate(new Vector3(0f, 0.1f, 0f), Space.World);
            }
        }
        Vector3 newPos = PlayerTransform.position + cameraOffset;
        transform.position = Vector3.Slerp(transform.position, newPos, SmoothFactor);
    }
}
