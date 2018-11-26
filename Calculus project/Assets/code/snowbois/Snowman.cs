using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Snowman : MonoBehaviour {

	public Yard yard;

	public GameObject head;
	public GameObject torso;
	public GameObject legs;

	public bool startMelting;

	public float sizeScale;
	/*
	 	Rs = (3 * v / 4π(7)) ^ (⅓) ft
		Rm = (3 * v / 2π(7)) ^ (⅓) ft
		Rb = (3 *v / 7π) ^ (⅓) ft

		Small: dr/dt = ds/dt / (8πr) = 1ft^2 per second / (8π(3 * 312.5 / 4π(7)) ^ (⅓) ft)
		Middle: dr/dt = ds/dt / (8πr) = 1ft^2 per second / (8π(3 * 312.5 / 2π(7)) ^ (⅓) ft)
		Big: dr/dt = ds/dt / (8πr) = 1ft^2 per second / (8π(3 * 312.5 / 7π) ^ (⅓) ft)

	 
	 */

	// volumes, volume, radius, surface area, radius change, volume change, SA change

	float volume;

	Vector3 v;

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

			float headRadius = Mathf.Pow ((3 * volume / (4 * 7 * Mathf.PI)), 1f / 3f) * sizeScale;
			float torsoRadius = Mathf.Pow ((3 * volume / (2 * 7 * Mathf.PI)), 1f / 3f) * sizeScale;
			float legsRadius = Mathf.Pow ((3 * volume / (7 * Mathf.PI)), 1f / 3f) * sizeScale;

			head.transform.localScale = new Vector3 (headRadius * 2, headRadius * 2, headRadius * 2);
			torso.transform.localScale = new Vector3 (torsoRadius * 2, torsoRadius * 2, torsoRadius * 2);
			legs.transform.localScale = new Vector3 (legsRadius * 2, legsRadius * 2, legsRadius * 2);

			torso.transform.position = new Vector3 (legs.transform.position.x, legs.transform.position.y + legsRadius + torsoRadius, legs.transform.position.z);
			head.transform.position = new Vector3 (torso.transform.position.x, torso.transform.position.y + headRadius + torsoRadius, torso.transform.position.z);
		
		} else {



		}
	}
}
