using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateObject : MonoBehaviour
{
    [SerializeField] int rotSpeed;
    // Update is called once per frame
    void Update()
    {
        this.transform.Rotate(Vector3.forward * rotSpeed * Time.deltaTime);
    }
}
