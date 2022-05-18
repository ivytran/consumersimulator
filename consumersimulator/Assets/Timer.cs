using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Timer : MonoBehaviour
{
    // Start is called before the first frame update
    public int countdownTime;
    public Text countdownDisplay;

    private int minutes;
    private int seconds;
    private int fraction;

    private float startTime;
    private string timerUIText;
    private float uiTimer;
 

    public Text timerTextField;
    IEnumerator GameStartDelay()
    {

        while (countdownTime > 0)
        {
            countdownDisplay.text = countdownTime.ToString();

            yield return new WaitForSeconds(1f);

            countdownTime--;
            startTime++;
        }

        countdownDisplay.text = "GO!";

        yield return new WaitForSeconds(1f);

        countdownDisplay.gameObject.SetActive(false);

    }

    void Start()
    {
        StartCoroutine(GameStartDelay());

    }

    // Update is called once per frame
    void Update()
    {

        if (countdownTime == 0)
        {
            UpdateTimer();
        }
    }

    private void UpdateTimer()
    {
        uiTimer = Time.time - startTime;
        minutes = (int)uiTimer / 60;
        seconds = (int)uiTimer % 60;
        fraction = (int)(uiTimer * 100) % 100;
        timerUIText = string.Format("{0:00}:{1:00}", minutes, seconds, fraction);

        if (minutes >= 2)
        {
            timerTextField.text = "TIME IS UP";
        }
        else
        {
            timerTextField.text = timerUIText;
        }
    }
}
