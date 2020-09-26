using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flashing : MonoBehaviour
{
    private float timer = 0f;
    private SpriteRenderer render;

    public float flashtime = 2f;
    public float lastFlash = 0f;
    public float flashlength = 0.2f;

    public bool isFlashing = false;

    private void Start()
    {
        render = GetComponent<SpriteRenderer>();
        timer = flashtime;
        isFlashing = true;
        render.enabled = false;
        lastFlash = timer;
    }

    private void Update()
    {
        timer -= Time.deltaTime;

        if(isFlashing)
        {
            if((lastFlash - flashlength) > timer)
            {
                isFlashing = false;
                render.enabled = true;
                lastFlash = timer;
            }
        }
        else if(!isFlashing)
        {
            if((lastFlash - flashlength) > timer)
            {
                isFlashing = true;
                render.enabled = false;
                lastFlash = timer;
            }
        }

        if(timer <= 0)
        {
            render.enabled = true;
            Destroy(this);
        }
    }
}
