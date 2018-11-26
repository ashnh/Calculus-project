using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Snowman : MonoBehaviour {

	public Yard yard;

	public GameObject head;
	public GameObject torso;
	public GameObject legs;

	public UnityEngine.UI.Text t;

	public bool startMelting;

	public float sizeScale;
	/*
	 	Rs = (3 * v / 4π(7)) ^ (⅓) ft
		Rm = (3 * v / 2π(7)) ^ (⅓) ft
		Rb = (3 *v / 7π) ^ (⅓) ft

		Small: dr/dt = ds/dt / (8πr) = 1ft^2 per second / (8π(3 * 312.5 / 4π(7)) ^ (⅓) ft)
		Middle: dr/dt = ds/dt / (8πr) = 1ft^2 per second / (8π(3 * 312.5 / 2π(7)) ^ (⅓) ft)
		Big: dr/dt = ds/dt / (8πr) = 1ft^2 per second / (8π(3 * 312.5 / 7π) ^ (⅓) ft)

		s/v = 3/r

		ds/dt / dv/dt = 3 / dr/dt
	 
	 */

	// volumes, volume, radius, surface area, radius change, volume change, SA change

	float volume;

	Vector3 v;

	float time;

	float headRadius;
	float torsoRadius;
	float legsRadius;

	float volDiv;
	float surfaceArea;

	// Use this for initialization
	void Start () {
		v = yard.getTransVector ();
		volume = v.x * v.y * v.z; 
		
	}

	// Update is called once per frame
	void Update () {
		
		if (!startMelting) {
		
			v = yard.getTransVector ();
			volume = v.x * v.y * v.z; 



			headRadius = Mathf.Pow ((3 * volume / (4 * 7 * Mathf.PI)), 1f / 3f) * sizeScale;
			torsoRadius = Mathf.Pow ((3 * volume / (2 * 7 * Mathf.PI)), 1f / 3f) * sizeScale;
			legsRadius = Mathf.Pow ((3 * volume / (7 * Mathf.PI)), 1f / 3f) * sizeScale;

			time = Time.timeSinceLevelLoad;


			volDiv = 4* Mathf.PI * (Mathf.Pow ((3 * volume * 1000f / (4 * 7 * Mathf.PI)), 2f / 3f)) * (1 / (8 * Mathf.PI * (Mathf.Pow ((3 * volume * 1000f / (4 * 7 * Mathf.PI)), 1f / 3f)))) +
				4* Mathf.PI * (Mathf.Pow ((3 * volume * 1000f / (4 * 7 * Mathf.PI)), 2f / 3f)) * (1 / (8 * Mathf.PI * (Mathf.Pow ((3 * volume * 1000f / (4 * 7 * Mathf.PI)), 1f / 3f)))) +
				4* Mathf.PI * (Mathf.Pow ((3 * volume * 1000f / (4 * 7 * Mathf.PI)), 2f / 3f)) * (1 / (8 * Mathf.PI * (Mathf.Pow ((3 * volume * 1000f / (4 * 7 * Mathf.PI)), 1f / 3f))));

		} else {


			float elapseTime = Time.timeSinceLevelLoad - time;

			float headDiv = 1.5f * Mathf.Pow (Mathf.Abs((4 * Mathf.PI * Mathf.Pow(Mathf.Pow ((3 * volume / (7 * Mathf.PI)), 1f / 3f) * sizeScale, 2f)) - (3 * elapseTime)), 0.5f) / 1000f;
			float torsoDiv = 1.5f * Mathf.Pow (Mathf.Abs((4 * Mathf.PI * Mathf.Pow(Mathf.Pow ((3 * volume / (2 * 7 * Mathf.PI)), 1f / 3f) * sizeScale, 2f)) - (3 * elapseTime)), 0.5f) / 1000f;
			float legsDiv = 1.5f * Mathf.Pow (Mathf.Abs((4 * Mathf.PI * Mathf.Pow(Mathf.Pow ((3 * volume / (4 * 7 * Mathf.PI)), 1f / 3f) * sizeScale, 2f)) - (3 * elapseTime)), 0.5f) / 1000f;

			headRadius -= headDiv;
			torsoRadius -= torsoDiv;
			legsRadius -= legsDiv;

			Debug.Log (legsDiv);

			headRadius = (headRadius > 0f) ? headRadius : 0f;
			torsoRadius = (torsoRadius > 0f) ? torsoRadius : 0f;
			legsRadius = (legsRadius > 0f) ? legsRadius : 0f;
		}

		t.text = "Stats:\tstarting volume = " + v.x * 10f + "x" + v.z * 10f + "x" + v.y * 10f + " = " + (v.x * v.y * v.z * 10f * 10f * 10f) + "\n" +
		"change in surface area = 1 \n" +
			"head radius = " + headRadius * 10f +
			"\ntorso radius = " + torsoRadius * 10f +
			"\nlegs radius = " + legsRadius * 10f /*+
			"\nInitial Change in Volume = " + volDiv*/;
		
		head.transform.localScale = new Vector3 (headRadius * 2, headRadius * 2, headRadius * 2);
		torso.transform.localScale = new Vector3 (torsoRadius * 2, torsoRadius * 2, torsoRadius * 2);
		legs.transform.localScale = new Vector3 (legsRadius * 2, legsRadius * 2, legsRadius * 2);

		torso.transform.position = new Vector3 (legs.transform.position.x, legs.transform.position.y + legsRadius + torsoRadius, legs.transform.position.z);
		head.transform.position = new Vector3 (torso.transform.position.x, torso.transform.position.y + headRadius + torsoRadius, torso.transform.position.z);
	}
}
