using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class CubeAwake : MonoBehaviour
{
    private GameObject parent;
    void Awake()
    {
        transform.name = transform.name.Replace("(Clone)","").Trim();
        parent = GameObject.Find("Parent" + transform.name);
        transform.parent = parent.transform;

        transform.gameObject.isStatic = true;
    }
}