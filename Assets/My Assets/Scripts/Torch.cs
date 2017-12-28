using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Torch : MonoBehaviour {

    public Camera mainCamera;
    public GameObject self;

    private Vector3 offset = new Vector3(-0.1f, 0, 0);

    // Use this for initialization
    void Start () {
        transform.Rotate(new Vector3(1, 0, 0), 30);
    }
	
	// Update is called once per frame
	void Update () {
        Vector3 forward = Camera.main.transform.forward;
        transform.position = Camera.main.transform.position + forward * 0.2f;
    }
}
