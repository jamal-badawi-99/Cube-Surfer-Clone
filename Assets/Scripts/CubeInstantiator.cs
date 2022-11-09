using UnityEngine;

public class CubeInstantiator : MonoBehaviour
{
    public int count = 1;

    public GameObject ObstacleBox;

    private void Start()
    {
        for (int i = 0; i < count; i++)
        {
            var pos = transform.position;
            var boxPreFabYPos = ObstacleBox.GetComponent<Renderer>().bounds.size.y;

            GameObject go = Instantiate(ObstacleBox, new Vector3(pos.x, pos.y + boxPreFabYPos * (i), pos.z), Quaternion.identity);

            go.transform.SetParent(transform);
        }
    }


}
