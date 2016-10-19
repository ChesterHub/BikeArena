using UnityEngine;
using System.Collections;

public class BikeMovement : MonoBehaviour {

	// Use this for initialization

	public float relZAngle;
	public WheelMovement[] wheels;
	public Transform reference;
	public float maxSpeed=15;
	public float rotationCoef=1/50;
	public float accel=0.05f;
	public float deCel=0.08f;
	public UnityEngine.UI.Image ImageSpedometer;
	public UnityEngine.UI.Text textTimer;



	Transform head;
	Rigidbody bikeBody;
	GvrViewer cardboard;
	public bool checkTrigger;
	float bikeSpeed;
	float lastClick;
	float Yangle=90;
	float speed=0;
	float lastTrigger;
	bool gameOver=false;

	int sec,min;
	float elapsed;

	void Start () 
	{


		// carboard head contains the rotation of the player (where the player is looking at)
		head=GameObject.FindGameObjectWithTag("player").transform.GetChild(0).transform;
		bikeBody=GetComponent<Rigidbody>();
		cardboard=GameObject.FindGameObjectWithTag("player").GetComponent<GvrViewer>();
		checkTrigger=false;

		// dissable gaze input:
		GameObject.Find("Gaze Pointer Cursor").GetComponent<CapsuleCollider>().enabled=false;
		relZAngle=90;
		lastTrigger=Time.fixedTime;
		gameOver=false;

		sec=0;
		min=0;
	}


	
	// Update is called once per fixed frame
	void FixedUpdate () 
	{

		// obtain speed of the car

		bikeSpeed=bikeBody.velocity.magnitude;

		elapsed+=Time.fixedDeltaTime;


		getTimer();



		if(cardboard.Triggered==true && Time.fixedTime>lastClick+0.5f && gameOver==false)
		{
			lastClick=Time.fixedTime;
			if(	checkTrigger==true)
			{
				checkTrigger=false;
			}
			else
			{
				checkTrigger=true;
			}
		}



		if(Time.fixedTime>lastTrigger+0.6f && gameOver==false)
		{
			checkTrigger=false;
			Invoke("restart",6);
			gameOver=true;
		}


		if(checkTrigger==true)
		{
			speed+=accel;
			if(speed>maxSpeed)
			{
				speed=maxSpeed;
			}


		}
		else
		{
			speed-=deCel;

			if(speed<0)
			{
				speed=0;
			}
		}

		// change arrow in spedometer
		ImageSpedometer.transform.rotation = ImageSpedometer.transform.parent.transform.rotation* Quaternion.Euler(0,0,-speed*268/maxSpeed);


		relZAngle=head.transform.rotation.eulerAngles[2];

		if(relZAngle>180)
		{
			relZAngle=-360+relZAngle;
		}

		/*if(Mathf.Abs(Yangle)>10)
		{
			
		}*/


		Yangle-=relZAngle*rotationCoef;


		// bike rotation and camera orientation
		cardboard.transform.position=Vector3.Lerp(cardboard.transform.position,reference.position,0.95f);
		cardboard.transform.forward=Vector3.Lerp(cardboard.transform.forward,reference.forward,0.95f);
		transform.rotation=Quaternion.Lerp(transform.rotation,Quaternion.Euler(-head.transform.rotation.eulerAngles[2],Yangle,0),0.1f);
		bikeBody.velocity=-transform.right*speed;




		moveWheels();
//		speedMet.speed=carSpeed;


	}




	void moveWheels()
	{
		for(int ii=0;ii<2;ii++)
		{
			wheels[ii].speed=bikeSpeed;
		}
	}

	void getTimer()
	{
		min=(int) elapsed/60;

		sec=(int)(elapsed-min*60);

		if(sec<10 )
		{
			textTimer.text=min+":0"+sec;
		}
		else
		{
			textTimer.text=min+":"+sec;
		}

		if(gameOver==true)
		{
			textTimer.text="Game\nOver";
		}
	}


	void restart()
	{
		Application.LoadLevel(Application.loadedLevel);
	}


	void OnTriggerStay(Collider col)
	{
		if(col.gameObject.tag=="road")
		{
			lastTrigger=Time.fixedTime;
		}
		else if(col.gameObject.tag=="finishLine")
		{
			elapsed=0;
		}
	}


}
