using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowCamera : MonoBehaviour
{
    public Transform target;
    Vector3 offset;
    Enemy enmy;

    public bool sigueEnX = true;
    public bool sigueEnY = true;

    public float minXPos;
    public float maxXPos;

    public float minYPos;
    public float maxYPos;

    public float minXDistance;
    public float maxXDistance;

    public float minYDistance;
    public float maxYDistance;

    // Start is called before the first frame update
    void Start()
    {
        offset = transform.position - target.position;
    }

    // Update is called once per frame
    void LateUpdate()
    {
        var newPos = transform.position;

        if (sigueEnX) 
        {
            var distance = target.position.x - transform.position.x;

            if (distance < minXDistance)
            {
                newPos.x += (distance - minXDistance);
            }

            if (distance > maxXDistance)
            {
                newPos.x += (distance - maxXDistance);
            }

            newPos.x = Mathf.Clamp(newPos.x, minXPos, maxXPos);
        }

        if (sigueEnY) 
        {
            var distaceHeight = target.position.y - transform.position.y;

            if (distaceHeight < minYDistance)
            {
                newPos.y += (distaceHeight - minYDistance);
            }

            if (distaceHeight > maxYDistance)
            {
                newPos.y += (distaceHeight - maxYDistance);
            }

            newPos.y = Mathf.Clamp(newPos.y, minYPos, maxYPos);
        }

        transform.position = newPos;
    }
}
