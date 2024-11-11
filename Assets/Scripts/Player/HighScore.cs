using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class HighScore : MonoBehaviour
{
    private void Start()
    {
        TMP_Text text = GetComponent<TMP_Text>();
        text.text = "Congratulations\r\nYou Unleashed B'Thulu in " + PlayerPrefs.GetInt("seconds");
    }
}
