using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class BillBoardManager : MonoBehaviour
{

    public Dictionary<Tuple<int, int>, List<Billboard>> BillboardManagement;


    private void Awake()
    {
        BillboardManagement = new Dictionary<Tuple<int, int>, List<Billboard>>();
    }

    private void Update()
    {
        
    }
}
