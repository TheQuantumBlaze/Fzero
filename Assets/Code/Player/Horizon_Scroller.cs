using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Horizon_Scroller : MonoBehaviour
{
    public GameObject horizonObject;
    public float turnRate = 2.4f;
    public float offset = 180;

    void Update()
    {
        //Rotates the horizon Based on the players rotation
        horizonObject.transform.localPosition = new Vector3(-(turnRate * transform.rotation.eulerAngles.y) + offset, horizonObject.transform.localPosition.y, horizonObject.transform.localPosition.z);
    }
}
