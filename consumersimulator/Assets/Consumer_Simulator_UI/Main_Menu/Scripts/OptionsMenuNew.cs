using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections.Generic;

namespace SlimUI.ModernMenu{
	public class OptionsMenuNew : MonoBehaviour {

		public enum Platform { Desktop, Mobile };
		public Platform platform;

		[Header( "Scores" )]
		private SQLiteDatabase sqlData;
		private bool isDatarun;
		//public GameObject mobileSFXtext;
		//public GameObject mobileMusictext;
		//public GameObject mobileShadowofftextLINE;
		//public GameObject mobileShadowlowtextLINE;
		//public GameObject mobileShadowhightextLINE;

		//[Header("VIDEO SETTINGS")]
		//public GameObject fullscreentext;
		//public GameObject ambientocclusiontext;
		public GameObject shadowofftextLINE;
		public GameObject shadowlowtextLINE;
		public GameObject shadowhightextLINE;
		//public GameObject aaofftextLINE;
		//public GameObject aa2xtextLINE;
		//public GameObject aa4xtextLINE;
		//public GameObject aa8xtextLINE;
		//public GameObject vsynctext;
		//public GameObject motionblurtext;
		//public GameObject texturelowtextLINE;
		//public GameObject texturemedtextLINE;
		//public GameObject texturehightextLINE;
		//public GameObject cameraeffectstext; 

		[Header( "GAME SETTINGS" )]
		public Dropdown scoreDrp;
		public GameObject timeText;
		public GameObject difficultynormaltext;
		public GameObject difficultynormaltextLINE;
		public GameObject difficultyhardcoretext;
		public GameObject difficultyhardcoretextLINE;

		[Header( "CONTROLS SETTINGS" )]
		public GameObject invertmousetext;
		public TMP_Dropdown perfDropDown;
		// sliders
		public GameObject musicSlider;
		public GameObject leftController;
		public GameObject rightController;
		public GameObject mouseSmoothSlider;

		private float sliderValueSmoothing = 0.0f;

		[Header( "Scriptable Objects" )]

		public FloatData volume;
		public FloatData time;
		public IntData performanceSettings;
		public IntData nDifficulty;
		public IntData hDifficulty;
		public StringData leftHandController;
		public StringData rightHandController;

		public void Start()
		{
			sqlData = gameObject.GetComponent<SQLiteDatabase>();
			// check difficulty
			Debug.Log( "allEcartItems " + CartItems.TotalItems );
			if (nDifficulty.RuntimeValue == 1)
			{
				difficultynormaltextLINE.gameObject.SetActive( true );
				difficultyhardcoretextLINE.gameObject.SetActive( false );
			}
			else
			{
				difficultyhardcoretextLINE.gameObject.SetActive( true );
				difficultynormaltextLINE.gameObject.SetActive( false );
			}

			// check slider values
			musicSlider.GetComponent<Slider>().value = volume.RuntimeValue;
			if (leftHandController)
			{
				if (leftHandController.RuntimeValue == "On" || leftHandController.RuntimeValue == "Off")
				{
					leftController.GetComponent<TMP_Text>().text = leftHandController.RuntimeValue;
                }
                else
                {
					leftController.GetComponent<TMP_Text>().text = "Off";

				}
            }
			if (rightController)
			{
				if (rightHandController.RuntimeValue == "On" || rightHandController.RuntimeValue == "Off")
				{
					rightController.GetComponent<TMP_Text>().text = rightHandController.RuntimeValue;
				}
				else
				{
					rightController.GetComponent<TMP_Text>().text = "On";

				}
			}
			if((rightHandController.RuntimeValue != "On" || rightHandController.RuntimeValue != "Off") && (leftHandController.RuntimeValue != "On" || leftHandController.RuntimeValue != "Off"))
            {
				rightHandController.RuntimeValue = "On";
				leftHandController.RuntimeValue = "Off";
			}

			mouseSmoothSlider.GetComponent<Slider>().value = PlayerPrefs.GetFloat( "MouseSmoothing" );

			// check full screen
			//if(Screen.fullScreen == true){
			//	fullscreentext.GetComponent<TMP_Text>().text = "On";
			//}
			//else if(Screen.fullScreen == false){
			//	fullscreentext.GetComponent<TMP_Text>().text = "Off";
			//}

			//check score value
			//sqlData.DropDataTable(); 
			if (sqlData.TableExists())
			{
				Debug.Log( "exists" );
				sqlData.ScoreHandlingStart();
				if (ScoreValues.storeData.Count > 0)
				{
					List<string> m_DopScore = new List<string>();
					m_DopScore.Add( "Round".PadRight( 40 ) + "Score" );
					foreach (var data in ScoreValues.storeData)
					{
						m_DopScore.Add( data.Key.ToString().PadRight( 50 ) + data.Value.ToString() );
					}
					if (m_DopScore.Count > 0)
					{
						scoreDrp.AddOptions( m_DopScore );
					}
				}
			}
			else
			{
				Debug.Log( "Notexists" );
				List<string> m_DropOptions = new List<string> { "Round".PadRight( 40 ) + "Score" };
				scoreDrp.AddOptions( m_DropOptions );
			}

			// check time value
			if (time.RuntimeValue >= 0)
			{
				timeText.GetComponent<TMP_Text>().text = time.RuntimeValue.ToString();
			}
		
			// check shadow distance/enabled
			if (platform == Platform.Desktop)
			{
				if (performanceSettings.RuntimeValue == 0)
				{
					perfDropDown.value = performanceSettings.RuntimeValue;
					QualitySettings.shadowCascades = 0;
					QualitySettings.shadowDistance = 0;
					shadowofftextLINE.gameObject.SetActive( true );
					shadowlowtextLINE.gameObject.SetActive( false );
					shadowhightextLINE.gameObject.SetActive( false );
				}
				else if (performanceSettings.RuntimeValue == 1)
				{
					//QualitySettings.shadowCascades = 2;
					//QualitySettings.shadowDistance = 75;
					//QualitySettings.currentLevel
					perfDropDown.value = performanceSettings.RuntimeValue;
					QualitySettings.SetQualityLevel( 1, true );
					QualitySettings.shadowCascades = 0;
					QualitySettings.shadowDistance = 0;
					shadowofftextLINE.gameObject.SetActive( false );
					shadowlowtextLINE.gameObject.SetActive( true );
					shadowhightextLINE.gameObject.SetActive( false );
				}
				else if (performanceSettings.RuntimeValue == 2)
				{
					//QualitySettings.shadowCascades = 4;
					//QualitySettings.shadowDistance = 500;
					perfDropDown.value = performanceSettings.RuntimeValue;
					QualitySettings.SetQualityLevel( 2, true );
					QualitySettings.shadowCascades = 0;
					QualitySettings.shadowDistance = 0;
					shadowofftextLINE.gameObject.SetActive( false );
					shadowlowtextLINE.gameObject.SetActive( false );
					shadowhightextLINE.gameObject.SetActive( true );
				}
			}
			//else if(platform == Platform.Mobile){
			//	if(PlayerPrefs.GetInt("MobileShadows") == 0){
			//		QualitySettings.shadowCascades = 0;
			//		QualitySettings.shadowDistance = 0;
			//		mobileShadowofftextLINE.gameObject.SetActive(true);
			//		mobileShadowlowtextLINE.gameObject.SetActive(false);
			//		mobileShadowhightextLINE.gameObject.SetActive(false);
			//	}
			//	else if(PlayerPrefs.GetInt("MobileShadows") == 1){
			//		QualitySettings.shadowCascades = 2;
			//		QualitySettings.shadowDistance = 75;
			//		mobileShadowofftextLINE.gameObject.SetActive(false);
			//		mobileShadowlowtextLINE.gameObject.SetActive(true);
			//		mobileShadowhightextLINE.gameObject.SetActive(false);
			//	}
			//	else if(PlayerPrefs.GetInt("MobileShadows") == 2){
			//		QualitySettings.shadowCascades = 4;
			//		QualitySettings.shadowDistance = 100;
			//		mobileShadowofftextLINE.gameObject.SetActive(false);
			//		mobileShadowlowtextLINE.gameObject.SetActive(false);
			//		mobileShadowhightextLINE.gameObject.SetActive(true);
			//	}
			//}


			// check vsync
			//if(QualitySettings.vSyncCount == 0){
			//	vsynctext.GetComponent<TMP_Text>().text = "Off";
			//}
			//else if(QualitySettings.vSyncCount == 1){
			//	vsynctext.GetComponent<TMP_Text>().text = "On";
			//}

			// check mouse inverse
			//if(PlayerPrefs.GetInt("Inverted")==0){
			//	invertmousetext.GetComponent<TMP_Text>().text = "Off";
			//}
			//else if(PlayerPrefs.GetInt("Inverted")==1){
			//	invertmousetext.GetComponent<TMP_Text>().text = "On";
			//}

			// check motion blur
			//if(PlayerPrefs.GetInt("MotionBlur")==0){
			//	motionblurtext.GetComponent<TMP_Text>().text = "Off";
			//}
			//else if(PlayerPrefs.GetInt("MotionBlur")==1){
			//	motionblurtext.GetComponent<TMP_Text>().text = "On";
			//}

			// check ambient occlusion
			//if(PlayerPrefs.GetInt("AmbientOcclusion")==0){
			//	ambientocclusiontext.GetComponent<TMP_Text>().text = "Off";
			//}
			//else if(PlayerPrefs.GetInt("AmbientOcclusion")==1){
			//	ambientocclusiontext.GetComponent<TMP_Text>().text = "On";
			//}

			// check texture quality
			//if(PlayerPrefs.GetInt("Textures") == 0){
			//	QualitySettings.masterTextureLimit = 2;
			//	texturelowtextLINE.gameObject.SetActive(true);
			//	texturemedtextLINE.gameObject.SetActive(false);
			//	texturehightextLINE.gameObject.SetActive(false);
			//}
			//else if(PlayerPrefs.GetInt("Textures") == 1){
			//	QualitySettings.masterTextureLimit = 1;
			//	texturelowtextLINE.gameObject.SetActive(false);
			//	texturemedtextLINE.gameObject.SetActive(true);
			//	texturehightextLINE.gameObject.SetActive(false);
			//}
			//else if(PlayerPrefs.GetInt("Textures") == 2){
			//	QualitySettings.masterTextureLimit = 0;
			//	texturelowtextLINE.gameObject.SetActive(false);
			//	texturemedtextLINE.gameObject.SetActive(false);
			//	texturehightextLINE.gameObject.SetActive(true);
			//}
		}

		public void Update()
		{
			sliderValueSmoothing = mouseSmoothSlider.GetComponent<Slider>().value;
		
		}
		public void LeftHController()
		{
			if (leftHandController.RuntimeValue == "Off")
			{
				leftHandController.RuntimeValue = "On";
				leftController.GetComponent<TMP_Text>().text = leftHandController.RuntimeValue;
			}
			else
				leftHandController.RuntimeValue = "Off";
			    leftController.GetComponent<TMP_Text>().text = leftHandController.RuntimeValue;
		}
		public void RightHController()
		{
			if (rightHandController.RuntimeValue == "Off")
			{
				rightHandController.RuntimeValue = "On";
				rightController.GetComponent<TMP_Text>().text = rightHandController.RuntimeValue;
			}
			else
				rightHandController.RuntimeValue = "Off";
			    rightController.GetComponent<TMP_Text>().text = rightHandController.RuntimeValue;
		}
		//public void FullScreen (){
		//	Screen.fullScreen = !Screen.fullScreen;

		//	if(Screen.fullScreen == true){
		//		fullscreentext.GetComponent<TMP_Text>().text = "On";
		//	}
		//	else if(Screen.fullScreen == false){
		//		fullscreentext.GetComponent<TMP_Text>().text = "Off";
		//	}
		//}
		public void MusicSlider()
		{
			volume.RuntimeValue = musicSlider.GetComponent<Slider>().value;
		}

		//public void SensitivitySmoothing (){
		//	PlayerPrefs.SetFloat("MouseSmoothing", sliderValueSmoothing);
		//	Debug.Log(PlayerPrefs.GetFloat("MouseSmoothing"));
		//}

		// check score
		public void ShowScore()
		{
			if (ScoreValues.storeData.Count == 0)
			{
				List<string> m_DropOptions = new List<string> { "Round".PadRight( 50 ) + "Score" };
				scoreDrp.AddOptions( m_DropOptions );
			}
			else
			{
				List<string> m_DopScore = new List<string>();
				m_DopScore.Add( "Round".PadRight( 50 ) + "Score" );
				foreach (var data in ScoreValues.storeData)
				{
					m_DopScore.Add( data.Key.ToString().PadRight( 50 ) + data.Value.ToString() );
				}
				if (m_DopScore.Count > 0)
				{
					scoreDrp.AddOptions( m_DopScore );
				}
			}
		}

		// checked to enable mobile sfx while in game
		//public void MobileSFXMute (){
		//	if(PlayerPrefs.GetInt("Mobile_MuteSfx")==0){
		//		PlayerPrefs.SetInt("Mobile_MuteSfx",1);
		//		mobileSFXtext.GetComponent<TMP_Text>().text = "On";
		//	}
		//	else if(PlayerPrefs.GetInt("Mobile_MuteSfx")==1){
		//		PlayerPrefs.SetInt("Mobile_MuteSfx",0);
		//		mobileSFXtext.GetComponent<TMP_Text>().text = "Off";
		//	}
		//}

		//public void MobileMusicMute (){
		//	if(PlayerPrefs.GetInt("Mobile_MuteMusic")==0){
		//		PlayerPrefs.SetInt("Mobile_MuteMusic",1);
		//		mobileMusictext.GetComponent<TMP_Text>().text = "On";
		//	}
		//	else if(PlayerPrefs.GetInt("Mobile_MuteMusic")==1){
		//		PlayerPrefs.SetInt("Mobile_MuteMusic",0);
		//		mobileMusictext.GetComponent<TMP_Text>().text = "Off";
		//	}
		//}

		// show time
		public void ShowTime()
		{
			timeText.GetComponent<TMP_Text>().text = time.RuntimeValue.ToString();
		}

		public void NormalDifficulty()
		{
			difficultyhardcoretextLINE.gameObject.SetActive( false );
			difficultynormaltextLINE.gameObject.SetActive( true ); ;
			nDifficulty.RuntimeValue = 1;
			hDifficulty.RuntimeValue = 0;
		}

		public void HardcoreDifficulty()
		{
			difficultyhardcoretextLINE.gameObject.SetActive( true );
			difficultynormaltextLINE.gameObject.SetActive( false );
			nDifficulty.RuntimeValue = 0;
			hDifficulty.RuntimeValue = 1;
		}

		public void ShadowsOff()
		{
			performanceSettings.RuntimeValue = 0;
			QualitySettings.shadowCascades = 0;
			QualitySettings.shadowDistance = 0;
			shadowofftextLINE.gameObject.SetActive( true );
			shadowlowtextLINE.gameObject.SetActive( false );
			shadowhightextLINE.gameObject.SetActive( false );
		}

		public void ShadowsLow()
		{
			performanceSettings.RuntimeValue = 1;
			QualitySettings.shadowCascades = 2;
			QualitySettings.shadowDistance = 75;
			shadowofftextLINE.gameObject.SetActive( false );
			shadowlowtextLINE.gameObject.SetActive( true );
			shadowhightextLINE.gameObject.SetActive( false );
		}

		public void ShadowsHigh()
		{
			performanceSettings.RuntimeValue = 3;
			QualitySettings.shadowCascades = 4;
			QualitySettings.shadowDistance = 500;
			shadowofftextLINE.gameObject.SetActive( false );
			shadowlowtextLINE.gameObject.SetActive( false );
			shadowhightextLINE.gameObject.SetActive( true );
		}
		public void PerformanceSetting(TMP_Dropdown drp)
        {
            if (perfDropDown)
            {
				performanceSettings.RuntimeValue = drp.value;
            }
        }
		//public void MobileShadowsOff (){
		//	PlayerPrefs.SetInt("MobileShadows",0);
		//	QualitySettings.shadowCascades = 0;
		//	QualitySettings.shadowDistance = 0;
		//	mobileShadowofftextLINE.gameObject.SetActive(true);
		//	mobileShadowlowtextLINE.gameObject.SetActive(false);
		//	mobileShadowhightextLINE.gameObject.SetActive(false);
		//}

		//public void MobileShadowsLow (){
		//	PlayerPrefs.SetInt("MobileShadows",1);
		//	QualitySettings.shadowCascades = 2;
		//	QualitySettings.shadowDistance = 75;
		//	mobileShadowofftextLINE.gameObject.SetActive(false);
		//	mobileShadowlowtextLINE.gameObject.SetActive(true);
		//	mobileShadowhightextLINE.gameObject.SetActive(false);
		//}

		//public void MobileShadowsHigh (){
		//	PlayerPrefs.SetInt("MobileShadows",2);
		//	QualitySettings.shadowCascades = 4;
		//	QualitySettings.shadowDistance = 500;
		//	mobileShadowofftextLINE.gameObject.SetActive(false);
		//	mobileShadowlowtextLINE.gameObject.SetActive(false);
		//	mobileShadowhightextLINE.gameObject.SetActive(true);
		//}

		//public void vsync (){
		//	if(QualitySettings.vSyncCount == 0){
		//		QualitySettings.vSyncCount = 1;
		//		vsynctext.GetComponent<TMP_Text>().text = "On";
		//	}
		//	else if(QualitySettings.vSyncCount == 1){
		//		QualitySettings.vSyncCount = 0;
		//		vsynctext.GetComponent<TMP_Text>().text = "Off";
		//	}
		//}

		//public void InvertMouse (){
		//	if(PlayerPrefs.GetInt("Inverted")==0){
		//		PlayerPrefs.SetInt("Inverted",1);
		//		invertmousetext.GetComponent<TMP_Text>().text = "On";
		//	}
		//	else if(PlayerPrefs.GetInt("Inverted")==1){
		//		PlayerPrefs.SetInt("Inverted",0);
		//		invertmousetext.GetComponent<TMP_Text>().text = "Off";
		//	}
		//}

		//public void MotionBlur (){
		//	if(PlayerPrefs.GetInt("MotionBlur")==0){
		//		PlayerPrefs.SetInt("MotionBlur",1);
		//		motionblurtext.GetComponent<TMP_Text>().text = "On";
		//	}
		//	else if(PlayerPrefs.GetInt("MotionBlur")==1){
		//		PlayerPrefs.SetInt("MotionBlur",0);
		//		motionblurtext.GetComponent<TMP_Text>().text = "Off";
		//	}
		//}

		//public void AmbientOcclusion (){
		//	if(PlayerPrefs.GetInt("AmbientOcclusion")==0){
		//		PlayerPrefs.SetInt("AmbientOcclusion",1);
		//		ambientocclusiontext.GetComponent<TMP_Text>().text = "On";
		//	}
		//	else if(PlayerPrefs.GetInt("AmbientOcclusion")==1){
		//		PlayerPrefs.SetInt("AmbientOcclusion",0);
		//		ambientocclusiontext.GetComponent<TMP_Text>().text = "Off";
		//	}
		//}

		//public void CameraEffects (){
		//	if(PlayerPrefs.GetInt("CameraEffects")==0){
		//		PlayerPrefs.SetInt("CameraEffects",1);
		//		cameraeffectstext.GetComponent<TMP_Text>().text = "On";
		//	}
		//	else if(PlayerPrefs.GetInt("CameraEffects")==1){
		//		PlayerPrefs.SetInt("CameraEffects",0);
		//		cameraeffectstext.GetComponent<TMP_Text>().text = "Off";
		//	}
		//}

		//	public void TexturesLow (){
		//		PlayerPrefs.SetInt("Textures",0);
		//		QualitySettings.masterTextureLimit = 2;
		//		texturelowtextLINE.gameObject.SetActive(true);
		//		texturemedtextLINE.gameObject.SetActive(false);
		//		texturehightextLINE.gameObject.SetActive(false);
		//	}

		//	public void TexturesMed (){
		//		PlayerPrefs.SetInt("Textures",1);
		//		QualitySettings.masterTextureLimit = 1;
		//		texturelowtextLINE.gameObject.SetActive(false);
		//		texturemedtextLINE.gameObject.SetActive(true);
		//		texturehightextLINE.gameObject.SetActive(false);
		//	}

		//	public void TexturesHigh (){
		//		PlayerPrefs.SetInt("Textures",2);
		//		QualitySettings.masterTextureLimit = 0;
		//		texturelowtextLINE.gameObject.SetActive(false);
		//		texturemedtextLINE.gameObject.SetActive(false);
		//		texturehightextLINE.gameObject.SetActive(true);
		//	}
	}
}