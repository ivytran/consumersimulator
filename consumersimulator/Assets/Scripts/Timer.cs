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
    public GameObject winObj;
    public GameObject lostObj;
    public GameObject progObj;
    public GameObject progAdvObj;
    private bool isWinResult = false;
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
            if (!isWinResult)
            {
                WinningConditions();
            }
        }
        else
        {
            timerTextField.text = timerUIText;
            //finish game under circumstances earlier or quit the game
        }
    }
    //winning conditions
    private bool WinningConditions()
    {
        isWinResult = true;
        if (CartItems.MatchedCount != null)
        {
            switch (CartItems.MatchedCount)
            {
                case 5:
                    if (PlayerPrefs.HasKey( "playerscore" ))
                    {
                        PlayerPrefs.SetInt( "playerscore" , 100 );
                        Debug.Log( "scoreIs " + 100 );
                        winObj.SetActive( true );
                        return true;
                    }
                    return false ;
                case 4:
                    if (PlayerPrefs.HasKey( "playerscore" ))
                    {
                        PlayerPrefs.SetInt( "playerscore" , 60 );
                        Debug.Log( "scoreIs " + 60 );
                        progAdvObj.SetActive( true );
                        return true;
                    }
                    return false;
                case 2:
                case 3:
                    if (PlayerPrefs.HasKey( "playerscore" ))
                    {
                        PlayerPrefs.SetInt( "playerscore" , 40 );
                        Debug.Log( "scoreIs " + 40 );
                        progObj.SetActive( true );
                        return true;
                    }
                    return false;
                case 1:
                case 0:
                    if (PlayerPrefs.HasKey( "playerscore" ))
                    {
                        currentScore = 0;
                        PlayerPrefs.SetInt( "playerscore" , currentScore );
                        lostObj.SetActive( true );
                        return true;
                    }
                    return false;
                default:
                    break;
            }
        }
        return false;
    }
}
