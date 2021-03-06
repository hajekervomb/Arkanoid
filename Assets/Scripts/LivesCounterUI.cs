using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LivesCounterUI : MonoBehaviour
{
    [SerializeField] private Text livesText;

    private void Start()
    {
        livesText = GetComponent<Text>();
    }

    private void Update()
    {
        livesText.text = "LIVES: " + GameManager.RemainingLives;
    }
}
