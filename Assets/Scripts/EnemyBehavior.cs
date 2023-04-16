using UnityEngine;
using System.Collections;

public class EnemyBehavior : MonoBehaviour {
	
	public float mSpeed = 20f;
	public float mTurnRate = 0.5f;
	public float health = 4f;
	private bool isQuitting = false;
	public GameObject MyTarget = null;
	private bool sequential = true;
	private int waypoint;
		
	// Use this for initialization
	void Start () {
		waypoint = Random.Range(1, 7);
		switch(waypoint)
		{
			case 1:
				MyTarget = GameObject.Find("WaypointA");
				break;
			case 2:
				MyTarget = GameObject.Find("WaypointB");
				break;
			case 3:
				MyTarget = GameObject.Find("WaypointC");
				break;
			case 4:
				MyTarget = GameObject.Find("WaypointD");
				break;
			case 5:
				MyTarget = GameObject.Find("WaypointE");
				break;
			case 6:
				MyTarget = GameObject.Find("WaypointF");
				break;
		}
	}
	
	// Update is called once per frame
	void Update () {
		PointAtPosition(MyTarget.transform.position, mTurnRate * Time.smoothDeltaTime);
        transform.position += mSpeed * Time.smoothDeltaTime * transform.up;

		GlobalBehavior globalBehavior = GameObject.Find ("GameManager").GetComponent<GlobalBehavior>();
		
		GlobalBehavior.WorldBoundStatus status =
			globalBehavior.ObjectCollideWorldBound(GetComponent<Renderer>().bounds);
			
		if (status != GlobalBehavior.WorldBoundStatus.Inside) {
			Debug.Log("collided position: " + this.transform.position);
		}	
		if (health == 0)
		{
			Destroy(gameObject);
		}
		if (Input.GetKeyDown("j"))
		{
			sequential = !sequential;
		}
	}

	private void PointAtPosition(Vector3 p, float r)
    {
        Vector3 v = p - transform.position;
        transform.up = Vector3.LerpUnclamped(transform.up, v, r);
    }


	void OnTriggerEnter2D(Collider2D col)
    {
        // Check if the collision involves the other game object you're interested in
        if (col.gameObject.name == "Hero")
        {
            Destroy(gameObject);  // this.gameObject, this is destroying the game object
			GlobalBehavior.sTheGlobalBehavior.IncTouched();
        };
		if (col.gameObject.name == "Egg(Clone)")
        {
            health -= 1f;
			Destroy(col.gameObject);
            GlobalBehavior.sTheGlobalBehavior.DecEggCount();
			Color objectColor = GetComponent<Renderer>().material.color;
			objectColor.a *= 0.8f;
			GetComponent<Renderer>().material.color = objectColor;
        };
		if (sequential == true)
		{
			if (col.gameObject.name == "WaypointA")
			{
				MyTarget = GameObject.Find("WaypointB");
			}
			if (col.gameObject.name == "WaypointB")
			{
				MyTarget = GameObject.Find("WaypointC");
			}
			if (col.gameObject.name == "WaypointC")
			{
				MyTarget = GameObject.Find("WaypointD");
			}
			if (col.gameObject.name == "WaypointD")
			{
				MyTarget = GameObject.Find("WaypointE");
			}
			if (col.gameObject.name == "WaypointE")
			{
				MyTarget = GameObject.Find("WaypointF");
			}
			if (col.gameObject.name == "WaypointF")
			{
				MyTarget = GameObject.Find("WaypointA");
			}
		} else {
			if (col.gameObject.name == "WaypointA" || col.gameObject.name == "WaypointB" || col.gameObject.name == "WaypointC" || col.gameObject.name == "WaypointD" || col.gameObject.name == "WaypointE" || col.gameObject.name == "WaypointF")
			{
				int random = Random.Range(1, 7);
				if (random == waypoint)
				{
					random++;
				}
				waypoint = random;
				switch(waypoint)
				{
					case 1:
						MyTarget = GameObject.Find("WaypointA");
						break;
					case 2:
						MyTarget = GameObject.Find("WaypointB");
						break;
					case 3:
						MyTarget = GameObject.Find("WaypointC");
						break;
					case 4:
						MyTarget = GameObject.Find("WaypointD");
						break;
					case 5:
						MyTarget = GameObject.Find("WaypointE");
						break;
					case 6:
						MyTarget = GameObject.Find("WaypointF");
						break;
					default:
						MyTarget = GameObject.Find("WaypointA");
						break;
				}
			}
		}
		
    }
	void OnApplicationQuit()
	{
		isQuitting = true;
	}
	private void OnDestroy()
    {
		if (!isQuitting)
		{
        // Get a reference to the EnemySpawner script
        EnemySpawner enemySpawner = GameObject.Find("EnemySpawner").GetComponent<EnemySpawner>();
        
        // Spawn a new enemy
        enemySpawner.spawnEnemy();
		GlobalBehavior.sTheGlobalBehavior.IncDestroyed();
		}
    }
}
