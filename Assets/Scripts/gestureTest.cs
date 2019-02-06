using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class gestureTest : MonoBehaviour {

	int count = 0;
	public Text c;
	public AudioSource[] instruments;
	public GameObject tracker;

	bool startOrchestra = true;

	void Start(){
		Invoke ("Orchestra", 10);
	}

	void Update () {
 		DetectWhenUserIsHolding(ManomotionManager.Instance.Hand_infos[0].hand_info.gesture_info);

		if (startOrchestra) {
			if (tracker.transform.position.y > 2.4f) {
				startOrchestra = false;
			}
			foreach (AudioSource source in instruments) {
				source.volume = (tracker.transform.position.y-0.6f) / 2;
			}
		}
	}

	void Orchestra(){
		startOrchestra = false;
	}

	void DetectWhenUserIsHolding(GestureInfo gesture)
	{
		if (gesture.mano_gesture_trigger == ManoGestureTrigger.CLICK_GESTURE) {
			count++;
			c.text = count%2 + "";
		}
	}
}



