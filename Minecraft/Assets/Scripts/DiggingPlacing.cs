using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class DiggingPlacing : MonoBehaviour
{
    public Transform cubePrefab;
    private Transform mainCamera;
    private float maxDist = 3.9f; //maxDistance to Digging and Placing
    private float minDiggingTime = 0.2f;


    private void Start()
    {
        mainCamera = transform.GetChild(0);
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Mouse0))
        {
            Invoke("digging", minDiggingTime);
        }
        else if(Input.GetKeyDown(KeyCode.Mouse1))
            placing();
    }

    private void digging()
    {
        if(Input.GetKey(KeyCode.Mouse0)) //Still(after minDiggingTime) Mouse0 is down
        {
            RaycastHit hit;
            if (Physics.Raycast(mainCamera.transform.position, mainCamera.transform.TransformDirection(Vector3.forward), out hit, maxDist))
            {
                Destroy(hit.transform.gameObject);
            }
        }
    }

    private void placing()
    {
        RaycastHit hit;
        if (Physics.Raycast(mainCamera.transform.position, mainCamera.transform.TransformDirection(Vector3.forward), out hit, maxDist))
        {
            Vector3 pos = hit.transform.position + hit.normal;
            Transform newCube = Instantiate(cubePrefab, pos, Quaternion.identity);
        }
    }
}