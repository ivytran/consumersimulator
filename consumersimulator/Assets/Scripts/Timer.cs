using System.Collections;
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
    private bool inFirst = false;
    public GameObject mainItemsUi;
    private int currentScore;
    public float remainingTime;
    public IEnumerator GameStartDelay()
    {
        timerTextField.text = "Timer";
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
        inFirst = true;

    }
    // Update is called once per frame
    void Update()
    {

        if (countdownTime == 0)
        {
            UpdateTimer();
        }
        if (inFirst && mainItemsUi && mainItemsUi.activeSelf)
        {
            mainItemsUi.SetActive( false );
        }
    }

    private void UpdateTimer()
    {
        uiTimer = Time.time - startTime;
        minutes = (int)uiTimer / 60;
        seconds = (int)uiTimer % 60;
        fraction = (int)(uiTimer * 100) % 100;
        timerUIText = string.Format("{0:00}:{1:00}", minutes, seconds, fraction);
        string thisTime = string.Format( "{0}.{1}" , minutes , seconds );
        remainingTime = float.Parse( thisTime );

        if (minutes >= 2)
        {
            timerTextField.text = "TIME IS UP";
            WinningConditions();
        }
        else
        {
            timerTextField.text = timerUIText;
            //finish game under circumstances earlier or quit the game
        }
    }
    //winning conditions
    private void WinningConditions()
    {
        switch (CartItems.TotalItems)
        {
            case 5:
                {
                    //stop timer
                    //display winning UI
                    //display winning audio
                    //add 100 scores
                    if (PlayerPrefs.HasKey( "playerscore" ))
                    {
                        currentScore = PlayerPrefs.GetInt( "playerscore" ) + 100;
                        PlayerPrefs.SetInt( "playerscore" , currentScore );
                    }
                }
                break;
            case 3:
                {
                    //stop timer
                    //display added scores UI
                    //display Ending audio
                    //add 60 scores
                    if (PlayerPrefs.HasKey( "playerscore" ))
                    {
                        currentScore = PlayerPrefs.GetInt( "playerscore" ) + 60;
                        PlayerPrefs.SetInt( "playerscore" , currentScore );
                    }
                }
                break;
            case 2:
                {

                    //stop timer
                    //display Ending UI
                    //display Ending audio
                    //add 40 scores
                    if (PlayerPrefs.HasKey( "playerscore" ))
                    {
                        currentScore = PlayerPrefs.GetInt( "playerscore" ) + 40;
                        PlayerPrefs.SetInt( "playerscore" , currentScore );
                    }

                }
                break;
            case 1:
            case 0:
                {

                    //stop timer
                    //display Loosing UI
                    //display Loosing audio
                    //update score to 0
                    if (PlayerPrefs.HasKey( "playerscore" ))
                    {
                        currentScore = 0;
                        PlayerPrefs.SetInt( "playerscore" , currentScore );
                    }
                }
                break;
            default:
                break;
        }
    }
}
