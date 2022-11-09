using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public static PlayerController Current;
    public float xLimit;
    public float horizontalSpeed;
    public float timeLeft = 0;
    private float _lastTouchedX;
    public float forwardSpeed = 10f;
    public Transform trans;
    private bool isDead = false;
    public GameObject CubePrefab;

    void FixedUpdate()
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

        if (timeLeft > 0)
        {
            timeLeft -= Time.fixedDeltaTime;
            forwardSpeed = 20f;
        }
        else
        {
            if (forwardSpeed > 10f)
                forwardSpeed -= 0.25f;
        }



        Vector3 newPosition = new Vector3(newX, transform.position.y, transform.position.z + forwardSpeed * Time.deltaTime);
        transform.position = newPosition;
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("PickupParent"))
        {
            other.enabled = false;
            int count = other.gameObject.GetComponent<CubeInstantiator>().count;
            Destroy(other.gameObject);
            CreateCube(count);
        }
    }




    public void CreateCube(int count)
    {
        for (int i = 0; i < count; i++)
        {
            GameObject cube = Instantiate(CubePrefab, transform);
            transform.position = new Vector3(transform.position.x, transform.position.y + 1, transform.position.z);
            cube.transform.position = new Vector3(transform.position.x, 1, transform.position.z);

        }

    }
    public void setSpeed(float speed)
    {
        forwardSpeed = speed;
    }
    public float getSpeed()
    {
        return forwardSpeed;
    }
    public void setBoost()
    {
        timeLeft = 1f;
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