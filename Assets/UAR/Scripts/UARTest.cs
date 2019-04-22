using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UAR;

public class UARTest : MonoBehaviour
{
    public GameObject objPrefab;
    public GameObject obj;

    void Start()
    {
        UAR.UAR.IAnchorAdded += (IAnchor anchor) =>
        {
            obj = Instantiate(objPrefab, anchor.pose.position, anchor.pose.rotation);
        };

        UAR.UAR.IAnchorUpdated += (IAnchor anchor) =>
        {
            if (anchor.tracking)
            {
                obj.transform.SetPositionAndRotation(anchor.pose.position, anchor.pose.rotation);
            }
            
        };
    }

    
}
