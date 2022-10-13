using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IKSolve : MonoBehaviour
{
    //Initialize variables
    public LayerMask terrainLayer;
    public IKSolve otherLeg;
    public float stepDistance, stepHeight, stepLength, footSpacing, speed;
    public Transform body;
    public Vector3 footOffset;
    //Initialize private variables
    Vector3 oldPos, newPos, currentPos;
    Vector3 oldNorm, currentNorm, newNorm;
    float lerp;

    // Start is called before the first frame update
    void Start()
    {
        //Set all variables to origin
        footSpacing = transform.localPosition.x;
        oldPos = newPos = currentPos = transform.position;
        oldNorm = currentNorm = newNorm = transform.up;
        lerp = 1;
    }

    // Update is called once per frame
    void Update()
    {
        //Update transforms and normals
        transform.position = currentPos;
        transform.up = currentNorm;
        print(transform.localPosition.x);

        //Define ground detecting raw
        Ray ray = new Ray(body.position + (new Vector3(1,0,0) * footSpacing), Vector3.down);

        //Debug ray
        Debug.DrawRay(body.position+(body.right * footSpacing), Vector3.down*10,Color.green/*body.position + (body.right * footSpacing), Vector3.down*10, Color.green*/);

        //If raycast hits terrain
        if (Physics.Raycast(ray, out RaycastHit hit, 10, terrainLayer.value))
        {
            //If distance required to take a step is exceeded && other leg is stationary
            if (Vector3.Distance(newPos, hit.point)> stepDistance && !otherLeg.IsMoving() && lerp >= 1)
            {
                lerp = 0;
                //Set direction based on whether destination is forwards or backwards
                int direction=body.InverseTransformPoint(hit.point).z>body.InverseTransformPoint(newPos).z ? 1:-1;
                //Set new destination for foot
                newPos = hit.point + (body.forward * stepLength * direction) + footOffset;
                //Set target rotation for foot
                newNorm = hit.normal;
            }
        }
        //If foot is not yet at position
        if (lerp < 1)
        {
            //Linear interpolate between current/target position/rotation
            Vector3 tempPos = Vector3.Lerp(oldPos, newPos, lerp);
            tempPos.y += Mathf.Sin(lerp * Mathf.PI) * stepHeight;
            currentPos = tempPos;
            currentNorm = Vector3.Lerp(oldNorm, newNorm, lerp);
            lerp += Time.deltaTime * speed;
        }
        //When foot is ~at target destination
        else
        {
            //Set position and rotation as destination target
            oldPos = newPos;
            oldNorm = newNorm;
        }
    }

    //Function determining whether to animate leg
    public bool IsMoving()
    {
        return lerp < 1;
    }
}
