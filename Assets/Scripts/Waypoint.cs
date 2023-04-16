using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Waypoint : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (GetComponent<Renderer>().material.color.a == 0)
        {
            transform.position = new Vector3 (transform.position.x + Random.Range(-15, 15), transform.position.y + Random.Range(-15, 15), 0);
            GlobalBehavior globalBehavior = GameObject.Find ("GameManager").GetComponent<GlobalBehavior>();
            Vector2 worldMin = globalBehavior.WorldMin;
            Vector2 worldMax = globalBehavior.WorldMax;
            if (transform.position.x < worldMin.x)
            {
                transform.position = new Vector2 (worldMin.x, transform.position.y);
            }
            if (transform.position.x > worldMax.x)
            {
                transform.position = new Vector2 (worldMax.x, transform.position.y);
            }
            if (transform.position.y < worldMin.y)
            {
                transform.position = new Vector2 (transform.position.x, worldMin.y);
            }
            if (transform.position.y > worldMax.y)
            {
                transform.position = new Vector2 (transform.position.x, worldMax.y);
            }
            Color objectColor = GetComponent<Renderer>().material.color;
			objectColor.a = 1.00f;
			GetComponent<Renderer>().material.color = objectColor;
        }
        if (Input.GetKeyDown("h"))
        {
            gameObject.GetComponent<Renderer>().enabled = !gameObject.GetComponent<Renderer>().enabled;
        }
    }

    void OnTriggerEnter2D(Collider2D col)
    {
		if (col.gameObject.name == "Egg(Clone)")
        {
			Destroy(col.gameObject);
            GlobalBehavior.sTheGlobalBehavior.DecEggCount();
			Color objectColor = GetComponent<Renderer>().material.color;
			objectColor.a -= 0.25f;
			GetComponent<Renderer>().material.color = objectColor;
        };
    }
}
