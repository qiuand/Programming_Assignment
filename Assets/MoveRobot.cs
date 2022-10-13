using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveRobot : MonoBehaviour
{
    public LayerMask terrainLayer;
    Rigidbody rb;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        Debug.DrawRay(transform.position, -Vector3.up * 10, Color.green);
        if (Physics.Raycast(transform.position, Vector3.up * -10, out RaycastHit hit, 10, terrainLayer.value))
        {
            transform.position = new Vector3(transform.position.x, hit.point.y + 5, transform.position.z);
        }
        rb.velocity = new Vector3(0, 0, 5);
    }
}
