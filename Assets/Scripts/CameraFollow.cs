using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public GameObject player;
    private Vector3 offset;
    private bool MultiplierHitStatus = false;

    void Start()
    {
        offset = transform.position - player.transform.position;
    }

    void Update()
    {
        if (MultiplierHitStatus)
        {
            MultiplierHitStatus = false;
            Vector3 pos = player.transform.position + offset;
            transform.position = new Vector3(pos.x, transform.position.y + 1, pos.z);
        }
        else
        {

            Vector3 pos = player.transform.position + offset;
            transform.position = new Vector3(pos.x, transform.position.y, pos.z);
        }

    }
    public void MultiplierHit()
    {
        MultiplierHitStatus = true;
    }

}

