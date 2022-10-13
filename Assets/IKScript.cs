using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IKScript : MonoBehaviour
{
    public LayerMask terrainLayer;
    public GameObject body;
    float spacing;
    // Start is called before the first frame update
    void Start()
    {
        spacing = transform.localPosition.x;
    }

    // Update is called once per frame
    void Update()
    {
        print(1);
        Ray raycast = new Ray(body.transform.position + (-body.transform.right * spacing), Vector3.down);
        if (Physics.Raycast(raycast, out RaycastHit info, 10, terrainLayer.value))
        {
            transform.position = info.point;
        }
    }
}
