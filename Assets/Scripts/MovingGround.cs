using UnityEngine;
using System.Collections;

public class MovingGround : MonoBehaviour
{

    public GameObject groundObj;
    public Transform[] points; 
    public Transform pointToGo;
    public int pointIndex;
    public float smoothing;
    // Use this for initialization
    void Start()
    {

        pointIndex = 0;
        pointToGo = points[pointIndex];

    }

    // Update is called once per frame
    void Update()
    {

        groundObj.transform.position = Vector3.MoveTowards(groundObj.transform.position, pointToGo.position, smoothing * Time.deltaTime);

        if (groundObj.transform.position == pointToGo.position)
        {
            pointIndex++;

            if (pointIndex == points.Length)
            {
                pointIndex = 0;
            }

            pointToGo = points[pointIndex];
        }

    }       // END of Update();

    // method for drawing a path from point to point
    public void OnDrawGizmos()
    {

        // atleast 2 points are neede to draw a line if not the return i.e do nothing
        // since points obj is not instantiated first i.e null we cannot acces the length property which cause error
        // so we need to check the points == null also
        if (points == null || points.Length < 2)
        {
            return;
        }

        for (int i = 1; i < points.Length; i++)
        {

            Gizmos.DrawLine(points[i - 1].position, points[i].position);
        }
    }
}
