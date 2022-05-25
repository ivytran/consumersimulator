using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class SwtichScenes : MonoBehaviour
{
    public InputAction inputMove;
    private float switchValue;
    public FloatData timeData;
    public FloatData volume;
    public IntData performanceSettings;
    public IntData nDifficulty;
    public IntData hDifficulty;
    public StringData leftHandController;
    public StringData rightHandController;
    public GameEvents gameEvents;
    private SQLiteDatabase sqlData;
    private int currentScore;
    private bool isDatarun = false;
    private void FixedUpdate()
    {
        SwtichScene();
    }
    public void SwtichScene()
    {
        inputMove.performed += ctx =>
        {
            sqlData = gameObject.GetComponent<SQLiteDatabase>();
            switchValue = ctx.ReadValue<float>();
            //nDifficulty.RuntimeValue = nDifficulty.RuntimeValue;
            //hDifficulty.RuntimeValue = hDifficulty.RuntimeValue;
            //leftHandController.RuntimeValue = leftHandController.RuntimeValue;
            //rightHandController.RuntimeValue = rightHandController.RuntimeValue;
            //check the performance settings 
            //check thye volume
            if (PlayerPrefs.HasKey( "playerscore" ))
            {
                currentScore = PlayerPrefs.GetInt( "playerscore" ) + 20;
                PlayerPrefs.SetInt( "playerscore" , currentScore );
                ScoreValues.scoreVal = currentScore.ToString();
                if (sqlData)
                {
                    if (!isDatarun)
                    {
                        sqlData.ScoreHandling();
                        //sqlData.DropDataTable();
                        isDatarun = true;
                    }
                }
                StartCoroutine( CallToSwitchScenes() );
            }
           
        };
    }
    private void OnEnable()
    {
        inputMove.Enable();
        SceneManager.sceneLoaded += OnSceneLoaded;
    }
    private void OnDisable()
    {
        inputMove.Disable();
    }
    private void OnSceneLoaded(Scene scene , LoadSceneMode mode)
    {
        Debug.Log( "OnSceneLoadedMain: " + scene.name + " Mode " + mode );
    }
    private IEnumerator CallToSwitchScenes()
    {
        if (gameEvents && timeData)
        {
            timeData.RuntimeValue = 12.75f;
            yield return new WaitForSeconds( 3f );
            gameEvents.Raise();
            yield return new WaitForSeconds( 3f );
            SceneManager.LoadScene( SceneManager.GetActiveScene().buildIndex - 1 );
        }
    }
}
