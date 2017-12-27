using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaypointSpawner : MonoBehaviour {

	public GameObject waypoint;

	// Use this for initialization
	void Start () {
		for (int i = 0; i <= 20; i++) {
			for (int j = 0; j <= 20; j++) {
				Instantiate (waypoint, new Vector3 (-84 + (9 * i), 5, -89 + (9 * j)), Quaternion.Euler (0, 0, 0));
			}
		}
	
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
