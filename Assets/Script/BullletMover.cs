using System.Net;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class BullletMover : MonoBehaviour
{
    public float speed = 100;
    Vector3 endPoint;
    Vector3 direction;
    bool ended = false;


    public void Initialise(Vector3 endPoint)
    {
        this.endPoint = endPoint;
        direction = endPoint - transform.position;
        direction.Normalize();
    }


    // Update is called once per frame
    void Update()
    {
        Vector3 nextMove = direction * speed * Time.deltaTime;
        if (nextMove.magnitude > Vector3.Distance(transform.position, endPoint))
        {
            ended = true;
            transform.position = endPoint;
        }
        else
        {
            transform.Translate(nextMove);
        }

    }
}
