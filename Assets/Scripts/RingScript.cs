using UnityEngine;
using System.Collections;

public class RingScript : MonoBehaviour {
    public GameObject confetti;
    public MeshRenderer mr;
    public AudioSource collectSound;
    void OnTriggerEnter(Collider other)
    {
        Debug.Log(other.name.ToString());
        if (other.name.ToString() == "GenericMotorBike") {
            //this.gameObject.SetActive(false);
            mr.enabled = false;
            confetti.SetActive(true);
            collectSound.Play();

        }
    }
    // Use this for initialization
    void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
