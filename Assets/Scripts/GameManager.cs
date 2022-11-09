using UnityEngine;

public class GameManager : MonoBehaviour
{
    public ProgressBar pb;
    public GameObject playerContainer;
    void Update()
    {

        var zVal = playerContainer.transform.position.z;
        Transform platform = GameObject.FindGameObjectWithTag("Platform").transform;
        var scaleZ = platform.localScale.z;
        var progress = zVal * 100 / scaleZ;
        if (progress * 1.11f <= 100)
        {

            pb.BarValue = progress * 1.11f;
        }
        else
        {
            pb.BarValue = 100;
        }
    }
    public void kaka()
    {

    }
}
