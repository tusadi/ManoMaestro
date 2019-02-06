using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class testing : MonoBehaviour {

	public Text t;
	Vector3 offset, startPos;
	public GameObject tracker;
//	public bool hasGameEnded = false;

 	// Use this for initialization
	void Start () {
		startPos = new Vector3 (3.53f, 1.55f, -0.22f);
 	}
	
	// Update is called once per frame
	void Update () {
		//if (!hasGameEnded) {
			//t.text = tracker.transform.position.ToString();
			CheckIfUserIsPointing (ManomotionManager.Instance.Hand_infos [0].hand_info.tracking_info);
		///}
	}

	void CheckIfUserIsPointing(TrackingInfo trackingInfo){

		Vector3 normPalmCenter = trackingInfo.palm_center;
		//float depth = trackingInfo.relative_depth;

		Vector3 relativePalmPos;

		//Vector3 relativePalmPos = ManoUtils.Instance.CalculateNewPosition (normPalmCenter, depth) ;
		//relativePalmPos = new Vector3 (relativePalmPos.x * 2, relativePalmPos.y * 2, relativePalmPos.z + 10);
		if (normPalmCenter == Vector3.zero) {
			relativePalmPos = startPos;
		} else {
			relativePalmPos = startPos + new Vector3 (0, normPalmCenter.y*4-2 , -normPalmCenter.x * 10 + 5); 
		}
		tracker.transform.position = Vector3.Lerp (tracker.transform.position, relativePalmPos, Time.deltaTime * 10);
		//t.text = tracker.transform.position.ToString ();
	}

	public void calibrate(){
		ManomotionManager.Instance.Calibrate ();
	}

	
}
