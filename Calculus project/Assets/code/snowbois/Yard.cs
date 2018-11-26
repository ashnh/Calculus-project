using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Yard : MonoBehaviour {

	public UnityEngine.UI.Slider width;
	public UnityEngine.UI.Slider height;
	public UnityEngine.UI.Slider length;

	public float wScale;
	public float hScale;
	public float lScale;

	public float zeroWidth;
	public float zeroHeight;
	public float zeroLength;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {

		gameObject.transform.localScale = getTransVector ();

	}

	public Vector3 getTransVector () {
		return new Vector3 ((width.value * wScale) + zeroWidth, (height.value * hScale) + zeroHeight, (length.value * lScale) + zeroLength);
	}
}
