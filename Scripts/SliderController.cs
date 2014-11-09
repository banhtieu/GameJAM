using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class SliderController : MonoBehaviour {

	GameObject toggleOff;
	Toggle toggle;

	// Use this for initialization
	void Start () {
		toggleOff = GetComponent<RectTransform>().Find("Off").gameObject;
		toggle = GetComponent<Toggle>();
	}

	void UpdateState() {
		if (toggle.isOn) {
			toggleOff.SetActive(false);
		} else {
			toggleOff.SetActive(true);
		}
	}
	
	// Update is called once per frame
	void Update () {
		UpdateState();
	}
}
