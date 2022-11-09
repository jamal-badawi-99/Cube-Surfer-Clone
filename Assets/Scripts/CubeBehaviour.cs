using UnityEngine;

public class CubeBehaviour : MonoBehaviour
{
    public GameObject CubePrefab;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("PickupParent"))
        {
            other.enabled = false;
            int count = other.gameObject.GetComponent<CubeInstantiator>().count;
            Destroy(other.gameObject);
            CreateCube(count);
        }
        if (other.tag == "Obstacle")
        {
            other.tag = "Hit";

            for (int i = 0; i < other.transform.parent.parent.childCount; i++)
            {
                for (int j = 0; j < other.transform.parent.parent.GetChild(i).childCount; j++)
                {
                    other.transform.parent.parent.GetChild(i).GetChild(j).tag = "Hit";
                }
            }

            if (transform.tag == "MainCube")
            {
                Debug.Log("MainCube is dead");
                return;
            }
            Transform destroyedCubes = GameObject.FindGameObjectWithTag("DestroyedCubes").transform;
            if (other.transform.parent.parent.parent.tag == "Cylinder")
            {
                transform.SetParent(other.transform.parent.parent.parent);

            }
            else
            {

                transform.SetParent(destroyedCubes);
            }

        }
        if (other.tag == "Lava")
        {
            if (transform.tag == "MainCube")
            {
                Debug.Log("MainCube is dead");
                return;
            }
            Destroy(gameObject);


        }
        if (other.tag == "SpeedBoost")
        {
            other.isTrigger = false;
            GameObject player = GameObject.FindGameObjectWithTag("PlayerContainer");
            player.GetComponent<PlayerController>().setBoost();


        }


    }
    public void CreateCube(int count)
    {
        PlayerController controller = GameObject.FindGameObjectWithTag("PlayerContainer").GetComponent<PlayerController>();

        float speed = controller.getSpeed();
        controller.setSpeed(0);
        for (int i = 0; i < count; i++)
        {

            // Transform cam = GameObject.FindGameObjectWithTag("MainCamera").transform;
            // cam.position += new Vector3(0, -1, 0);
            Transform parent = GameObject.FindGameObjectWithTag("PlayerContainer").transform;
            GameObject cube = Instantiate(CubePrefab, parent);
            parent.position = new Vector3(parent.position.x, parent.position.y + 1, parent.position.z);
            cube.transform.position = new Vector3(parent.position.x, 1, parent.position.z);

        }
        controller.setSpeed(speed);


    }

}
