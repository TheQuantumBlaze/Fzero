using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class ScoreRenderer : MonoBehaviour
{
    public Text score;

    private void Start()
    {
        score = GetComponent<Text>();
    }

    private void Update()
    {
        score.text = "Score - " + Mathf.FloorToInt(ScoreManager.singleton.Score).ToString();
    }
}
