﻿using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;
using System.Collections.Generic;
using System.Linq;

namespace SlimUI.ModernMenu{
	public class MainMenuNew : MonoBehaviour {
		Animator CameraObject;

		[Header( "Loaded Scene" )]
		[Tooltip( "The name of the scene in the build settings that will load" )]
		private string sceneName;

		public enum Theme { custom1, custom2, custom3 };

		[Header( "TopMenu" )]

		public TMP_Text scoreValue;
		public TMP_Text itemValue;
		[Header( "Theme Settings" )]
		public Theme theme;
		int themeIndex;
		public FlexibleUIData themeController;

		[Header( "Panels" )]
		[Tooltip( "The UI Panel parenting all sub menus" )]
		public GameObject mainCanvas;
		[Tooltip( "The UI Panel that holds the CONTROLS window tab" )]
		public GameObject PanelControls;
		[Tooltip( "The UI Panel that holds the VIDEO window tab" )]
		public GameObject PanelVideo;
		[Tooltip( "The UI Panel that holds the GAME window tab" )]
		public GameObject PanelGame;
		[Tooltip( "The UI Panel that holds the KEY BINDINGS window tab" )]
		public GameObject PanelKeyBindings;
		[Tooltip( "The UI Sub-Panel under KEY BINDINGS for MOVEMENT" )]
		public GameObject PanelMovement;
		[Tooltip( "The UI Sub-Panel under KEY BINDINGS for COMBAT" )]
		public GameObject PanelCombat;
		[Tooltip( "The UI Sub-Panel under KEY BINDINGS for GENERAL" )]
		public GameObject PanelGeneral;

		[Header( "SFX" )]
		[Tooltip( "The GameObject holding the Audio Source component for the HOVER SOUND" )]
		public AudioSource hoverSound;
		[Tooltip( "The GameObject holding the Audio Source component for the AUDIO SLIDER" )]
		public AudioSource sliderSound;
		[Tooltip( "The GameObject holding the Audio Source component for the SWOOSH SOUND when switching to the Settings Screen" )]
		public AudioSource swooshSound;

		// campaign button sub menu
		[Header( "Menus" )]
		[Tooltip( "The Menu for when the MAIN menu buttons" )]
		public GameObject mainMenu;
		[Tooltip( "THe first list of buttons" )]
		public GameObject firstMenu;
		[Tooltip( "The Menu for when the PLAY button is clicked" )]
		public GameObject playMenu;
		[Tooltip( "The Menu for when the EXIT button is clicked" )]
		public GameObject exitMenu;
		[Tooltip( "Optional 4th Menu" )]
		public GameObject extrasMenu;

		// highlights
		[Header( "Highlight Effects" )]
		[Tooltip( "Highlight Image for when GAME Tab is selected in Settings" )]
		public GameObject lineGame;
		[Tooltip( "Highlight Image for when VIDEO Tab is selected in Settings" )]
		public GameObject lineVideo;
		[Tooltip( "Highlight Image for when CONTROLS Tab is selected in Settings" )]
		public GameObject lineControls;
		[Tooltip( "Highlight Image for when KEY BINDINGS Tab is selected in Settings" )]
		public GameObject lineKeyBindings;
		[Tooltip( "Highlight Image for when MOVEMENT Sub-Tab is selected in KEY BINDINGS" )]
		public GameObject lineMovement;
		[Tooltip( "Highlight Image for when COMBAT Sub-Tab is selected in KEY BINDINGS" )]
		public GameObject lineCombat;
		[Tooltip( "Highlight Image for when GENERAL Sub-Tab is selected in KEY BINDINGS" )]
		public GameObject lineGeneral;
		private List<string> allScenesName = new List<string>();
		public GameObject optionCanvas;
		void Start()
		{
			CameraObject = gameObject.GetComponent<Animator>();
			if (scoreValue)
			{
				if (PlayerPrefs.HasKey( "playerscore" ))
				{
					scoreValue.text = PlayerPrefs.GetInt( "playerscore" ).ToString();
				}
			}
            if (itemValue)
            {
				if (CartItems.TotalItems > 0)
				{
					itemValue.text = CartItems.TotalItems.ToString();
				}
			}
			playMenu.SetActive( false );
			exitMenu.SetActive( false );
			if (extrasMenu) extrasMenu.SetActive( false );
			firstMenu.SetActive( true );
			mainMenu.SetActive( true );

			SetThemeColors();
		}

		void SetThemeColors()
		{
			if (theme == Theme.custom1)
			{
				themeController.currentColor = themeController.custom1.graphic1;
				themeController.textColor = themeController.custom1.text1;
				themeIndex = 0;
			}
			else if (theme == Theme.custom2)
			{
				themeController.currentColor = themeController.custom2.graphic2;
				themeController.textColor = themeController.custom2.text2;
				themeIndex = 1;
			}
			else if (theme == Theme.custom3)
			{
				themeController.currentColor = themeController.custom3.graphic3;
				themeController.textColor = themeController.custom3.text3;
				themeIndex = 2;
			}
		}

		public void PlayCampaign()
		{
			exitMenu.SetActive( false );
			if (extrasMenu) extrasMenu.SetActive( false );
			playMenu.SetActive( true );
		}

		public void PlayCampaignMobile()
		{
			exitMenu.SetActive( false );
			if (extrasMenu) extrasMenu.SetActive( false );
			playMenu.SetActive( true );
			mainMenu.SetActive( false );
		}

		public void ReturnMenu()
		{
			playMenu.SetActive( false );
			if (extrasMenu) extrasMenu.SetActive( false );
			exitMenu.SetActive( false );
			mainMenu.SetActive( true );
		}

		public void StartGame()
		{

			int sceneCount = SceneManager.sceneCountInBuildSettings;
			string[] scenes = new string[sceneCount];
			IEnumerable<string> saveName;
			for (int i = 0; i < sceneCount; i++)
			{
				scenes = SceneUtility.GetScenePathByBuildIndex( i ).Split( '/' );
				allScenesName.Add( scenes[scenes.Length - 1].Split( '.' )[0] );
			}
			if (allScenesName.Count > 0)
			{
				saveName = allScenesName.Where( x =>
					 x != SceneManager.GetActiveScene().name
					);
				if (saveName.ToList().Count > 0)
				{
					sceneName = saveName.ToList()[0];
					StartCoroutine( LoadAsynchronously( sceneName ) );
				}
			}
		}
		public void ReloadGame()
		{
			int sceneCount = SceneManager.sceneCountInBuildSettings;
			string[] scenes = new string[sceneCount];
			IEnumerable<string> saveName;
			for (int i = 0; i < sceneCount; i++)
			{
				scenes = SceneUtility.GetScenePathByBuildIndex( i ).Split( '/' );
				allScenesName.Add( scenes[scenes.Length - 1].Split( '.' )[0] );
			}
			if (allScenesName.Count > 0)
			{
				saveName = allScenesName.Where( x =>
					 x == SceneManager.GetActiveScene().name
					);
				if (saveName.ToList().Count > 0)
				{
					sceneName = saveName.ToList()[0];
					StartCoroutine( LoadAsynchronously( sceneName ) );
				}
			}
		}
		public void DisablePlayCampaign()
		{
			playMenu.SetActive( false );
		}

		public void Position2()
		{
			DisablePlayCampaign();
			CameraObject.SetFloat( "Animate" , 1 );
		}

		public void Position1()
		{
			CameraObject.SetFloat( "Animate" , 0 );
		}

		void DisablePanels()
		{
			PanelControls.SetActive( false );
			PanelVideo.SetActive( false );
			PanelGame.SetActive( false );
			PanelKeyBindings.SetActive( false );

			lineGame.SetActive( false );
			lineControls.SetActive( false );
			lineVideo.SetActive( false );
			lineKeyBindings.SetActive( false );

			PanelMovement.SetActive( false );
			lineMovement.SetActive( false );
			PanelCombat.SetActive( false );
			lineCombat.SetActive( false );
			PanelGeneral.SetActive( false );
			lineGeneral.SetActive( false );
		}

		public void GamePanel()
		{
			DisablePanels();
			PanelGame.SetActive( true );
			lineGame.SetActive( true );
		}

		public void VideoPanel()
		{
			DisablePanels();
			PanelVideo.SetActive( true );
			lineVideo.SetActive( true );
		}

		public void ControlsPanel()
		{
			DisablePanels();
			PanelControls.SetActive( true );
			lineControls.SetActive( true );
		}

		public void KeyBindingsPanel()
		{
			DisablePanels();
			MovementPanel();
			PanelKeyBindings.SetActive( true );
			lineKeyBindings.SetActive( true );
		}

		public void MovementPanel()
		{
			DisablePanels();
			PanelKeyBindings.SetActive( true );
			PanelMovement.SetActive( true );
			lineMovement.SetActive( true );
		}

		public void CombatPanel()
		{
			DisablePanels();
			PanelKeyBindings.SetActive( true );
			PanelCombat.SetActive( true );
			lineCombat.SetActive( true );
		}

		public void GeneralPanel()
		{
			DisablePanels();
			PanelKeyBindings.SetActive( true );
			PanelGeneral.SetActive( true );
			lineGeneral.SetActive( true );
		}

		public void PlayHover()
		{
			hoverSound.Play();
		}

		public void PlaySFXHover()
		{
			sliderSound.Play();
		}

		public void PlaySwoosh()
		{
			swooshSound.Play();
		}

		// Are You Sure - Quit Panel Pop Up
		public void AreYouSure()
		{
			exitMenu.SetActive( true );
			if (extrasMenu) extrasMenu.SetActive( false );
			DisablePlayCampaign();
		}

		public void AreYouSureMobile()
		{
			exitMenu.SetActive( true );
			if (extrasMenu) extrasMenu.SetActive( false );
			mainMenu.SetActive( false );
			DisablePlayCampaign();
		}

		public void ExtrasMenu()
		{
			playMenu.SetActive( false );
			if (extrasMenu) extrasMenu.SetActive( true );
			exitMenu.SetActive( false );
		}

		public void QuitGame()
		{
			#if UNITY_EDITOR
						UnityEditor.EditorApplication.isPlaying = false;
			#else
							Application.Quit();
			#endif
		}

		IEnumerator LoadAsynchronously(string sceneName)
		{
			optionCanvas.SetActive( false );
			mainCanvas.SetActive( false );
			AsyncOperation operation = SceneManager.LoadSceneAsync( sceneName );
			operation.allowSceneActivation = false;
			mainCanvas.SetActive( false );
			while (!operation.isDone)
			{
					if (operation.progress >= 0.9f)
					{
						operation.allowSceneActivation = true;
					}
				
				yield return null;
			}
		}
	}
}