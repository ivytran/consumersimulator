using UnityEngine;
using System.Collections;

namespace SlimUI.ModernMenu{

	public class CheckMusicVolume : MonoBehaviour {
		public FloatData musicVolume;
		public void  Start (){
			GetComponent<AudioSource>().volume = musicVolume.RuntimeValue ;
		}

		public void UpdateVolume (){
			GetComponent<AudioSource>().volume = musicVolume.RuntimeValue;
		}
	}
}