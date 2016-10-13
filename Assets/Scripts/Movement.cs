using UnityEngine;
using System.Collections;

public class Movement : MonoBehaviour {
    public GameObject obj;
    public float moveSpeed = 10.0f;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        ObjMovement();
	}
    void ObjMovement() {
        Rigidbody rb = obj.GetComponent<Rigidbody>();

        if (Input.GetKeyDown(KeyCode.A))
        {
            rb.velocity = Vector3.left * moveSpeed;
        }
        else if (Input.GetKeyDown(KeyCode.D))
        {
            rb.velocity = Vector3.right * moveSpeed;
        }
        else if (Input.GetKeyDown(KeyCode.W))
        {
            rb.velocity = Vector3.forward * moveSpeed;
        }
        else if (Input.GetKeyDown(KeyCode.S))
        {
            rb.velocity = Vector3.back * moveSpeed;
        }
    }
}
