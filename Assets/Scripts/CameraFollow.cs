using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public GameObject player;
    private Vector3 offset;

    void Start()
    {
        offset = transform.position - player.transform.position;
    }

    void Update()
    {
        Vector3 pos = player.transform.position + offset;
        transform.position = new Vector3(pos.x, transform.position.y, pos.z);
    }
}

