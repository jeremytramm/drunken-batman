﻿using UnityEngine;
using System.Collections;

public class EnemyBulletAI : MonoBehaviour {

	public GameBehavior globals;
	public bool entered = false; // You need this flag or the game will try to read EVERY instance of the collison!

	// Use this for initialization
	void Start () {

		Vector2 newVelocity = Vector2.zero;
		newVelocity.y = -3.0f;
		rigidbody2D.velocity = newVelocity;
		globals = Camera.main.GetComponent<GameBehavior>();
	
	}
	
	void OnTriggerEnter2D(Collider2D other){

		int levelOneGrazeBenchmark = 15;
		int levelTwoGrazeBenchmark = 31;
		int levelThreeGrazeBenchmark = 63;

		if (other.tag == "graze_trigger") {
			if (!entered) {
				print ("in graze trigger");
				globals.grazeCounter++;
				if(globals.grazeCounter > levelOneGrazeBenchmark && globals.grazeCounter <= levelTwoGrazeBenchmark){
					print ("graze level = 2");
					globals.grazeMultiplier = 2;
				}
				if(globals.grazeCounter > levelTwoGrazeBenchmark && globals.grazeCounter <= levelThreeGrazeBenchmark){
					print ("graze level = 4");
					globals.grazeMultiplier = 4;
				}
				if(globals.grazeCounter > levelThreeGrazeBenchmark){
					print ("graze level = 8");
					globals.grazeMultiplier = 8;
				}
				entered = true;
			}
		} else {
			Destroy (gameObject);
		}
	}
}
