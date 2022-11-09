using System.Collections.Generic;
using UnityEngine;

public class ss : MonoBehaviour
{
    public static PlayerController Current;
    public float xLimit;
    public float horizontalSpeed, runningSpeed;

    private float _lastTouchedX;
    private float forwardSpeed = 5f;
    public List<GameObject> cubes;
    public GameObject CubePrefab;
    public Transform trans;
    private bool isDead = false;

    void Update()
    {
        if (isDead)
            return;
        Movement();
    }

    private void Movement()
    {
        float newX;
        float touchDeltaX = 0;
        if (Input.touchCount > 0)
        {
            if (Input.GetTouch(0).phase == TouchPhase.Began)
            {
                _lastTouchedX = Input.GetTouch(0).position.x;
            }
            else if (Input.GetTouch(0).phase == TouchPhase.Moved)
            {
                touchDeltaX = 5 * (Input.GetTouch(0).position.x - _lastTouchedX) / Screen.width;
                _lastTouchedX = Input.GetTouch(0).position.x;
            }
        }
        else if (Input.GetMouseButton(0))
        {
            touchDeltaX = Input.GetAxis("Mouse X");
        }
        newX = transform.position.x + horizontalSpeed * touchDeltaX * Time.deltaTime;
        newX = Mathf.Clamp(newX, -xLimit, xLimit);

        Vector3 newPosition = new Vector3(newX, transform.position.y, transform.position.z + forwardSpeed * Time.deltaTime);
        transform.position = newPosition;
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "PickupParent")
        {
            other.enabled = false;
            int count = other.gameObject.GetComponent<CubeInstantiator>().count;
            Destroy(other.gameObject);
            CreateCube(count);
        }
        else if (other.CompareTag("ObstacleCollection"))

        {
            RemoveCubes(other);

        }
        else if (other.CompareTag("Lava"))

        {
            if (cubes.Count > 0)
            {

                DestroyWithLava();
            }
            else
            {
                KillPlayer("Lava");

            }

        }

    }

    private void RemoveCubes(Collider other)
    {
        int count = other.gameObject.GetComponent<CubeInstantiator>().count;
        if (cubes.Count > count - 1)
        {

            other.tag = "Hit";
            for (int i = 0; i < 5; i++)
            {
                other.transform.parent.GetChild(i).tag = "Hit";
            }

            DestroyCube(count);
        }
        else
        {
            KillPlayer("Obstacle");

        }
    }



    public void CreateCube(int count)
    {
        for (int i = 0; i < count; i++)
        {
            GameObject cube = Instantiate(CubePrefab, transform);
            cubes.Add(cube);
            transform.position = new Vector3(transform.position.x, transform.position.y + 1, transform.position.z);
            cube.transform.position = new Vector3(transform.position.x, 1, transform.position.z);

        }

    }
    public void DestroyWithLava()
    {
        Destroy(cubes[cubes.Count - 1]);
        cubes.Remove(cubes[cubes.Count - 1]);
    }
    public void DestroyCube(int count)
    {
        for (int i = 0; i < count; i++)
        {

            RemoveCube(cubes[cubes.Count - 1]);
        }

    }

    public void RemoveCube(GameObject cube)
    {
        cubes.Remove(cube);
        Transform destroyedCubes = GameObject.FindGameObjectWithTag("DestroyedCubes").transform;
        cube.transform.SetParent(destroyedCubes);

    }

    private void KillPlayer(string type)
    {

        isDead = true;
        forwardSpeed = 0;

        GameObject player = GameObject.FindGameObjectWithTag("Player");

        player.transform.SetParent(null);
        player.GetComponent<Collider>().enabled = true;
        player.AddComponent<Rigidbody>();
        player.GetComponent<Rigidbody>().useGravity = true;

        GameObject playerContainer = GameObject.FindGameObjectWithTag("PlayerContainer");
        playerContainer.transform.SetParent(null);
        playerContainer.GetComponent<Collider>().enabled = true;
        playerContainer.GetComponent<Collider>().isTrigger = false;
        if (type == "Lava")
        {

            GameObject mainCube = GameObject.FindGameObjectWithTag("MainCube");
            Destroy(mainCube);
        }
    }

}