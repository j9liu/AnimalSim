using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ground : MonoBehaviour
{
    private Vector3 minimumCorner;
    private Vector3 maximumCorner;

    private Vector3 minimumPaddedCorner;
    private Vector3 maximumPaddedCorner;

    // private GameObject[] trees;

    // Start is called before the first frame update
    void Start()
    {
        minimumCorner = gameObject.transform.position - (gameObject.transform.localScale / 2.0f);
        maximumCorner = minimumCorner + gameObject.transform.localScale;

        minimumPaddedCorner = new Vector3(minimumCorner.x + 0.5f, minimumCorner.y, minimumCorner.z + 0.5f);
        maximumPaddedCorner = new Vector3(maximumCorner.x - 0.5f, maximumCorner.y, maximumCorner.z - 0.5f);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    // A position is valid if it is within the bounds of the ground plane.
    public bool IsValidPosition(Vector3 position) {
        return position.x >= minimumPaddedCorner.x && position.z >= minimumPaddedCorner.z
            && position.x <= maximumPaddedCorner.x && position.z <= maximumPaddedCorner.z;
    }

    public Vector3 ClampPosition(Vector3 position) {
        return new Vector3(Mathf.Max(position.x, minimumPaddedCorner.x),
                           position.y,
                           Mathf.Max(position.z, minimumPaddedCorner.z));

    }

    /*public void GetRandomPosition() {

    }*/

    // This will change once trees are added to the environment such that acorns only
    // fall near trees.
    public Vector3 GetRandomPositionForFood(GameObject food) {
        return new Vector3(Random.Range(minimumPaddedCorner.x, maximumPaddedCorner.x),
                           maximumCorner.y + food.transform.localScale.y,
                           Random.Range(minimumPaddedCorner.z, maximumPaddedCorner.z));
    }
}
