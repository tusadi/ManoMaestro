using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class handGestureController : MonoBehaviour {

	public Text debugger;
	public GameObject tracker;
	public Light activeLight;
	public GameObject activeInstrument;

	bool isSelected = false;
	AudioSource selectedSource;

	// Update is called once per frame
	void Update () {
		transform.LookAt (tracker.transform);
		DetectWhenUserIsHolding(ManomotionManager.Instance.Hand_infos[0].hand_info.gesture_info);
		if (isSelected) {
			selectedSource.volume =  (tracker.transform.position.y-0.6f) / 2;;
		}
	}


	void DetectWhenUserIsHolding(GestureInfo gesture)
	{
		if (gesture.mano_gesture_trigger == ManoGestureTrigger.CLICK_GESTURE)
		{
			
			if (activeInstrument != null) {
				if (!isSelected) {
					isSelected = true;
					selectedSource = activeInstrument.GetComponent<AudioSource> ();
					debugger.text = "Selected: " + selectedSource.gameObject.tag;

				} else {
					debugger.text = "";
					isSelected = false;
					activeInstrument = null;
					selectedSource = null;
				}
			}


		}
	}

	void OnTriggerEnter(Collider col){
		if (!isSelected) {
			if (activeLight != null) {
				activeLight.enabled = false;
			}
			activeLight = col.GetComponentInChildren<Light> ();
			activeLight.enabled = true;
			activeInstrument = col.gameObject;
		}
	}
}
