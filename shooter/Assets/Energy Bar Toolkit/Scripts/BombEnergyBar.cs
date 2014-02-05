/*
* Energy Bar Toolkit by Mad Pixel Machine
* http://www.madpixelmachine.com
*/

using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class EnergyBar : MonoBehaviour {
	
	// ===========================================================
	// Constants
	// ===========================================================
	
	// ===========================================================
	// Fields
	// ===========================================================
	
	public GameBehavior globals;
	public int valueCurrent = 50;
	public int valueMin = 0;
	public int valueMax = 100;
	
	void Start() { 
		valueCurrent = globals.numOfLives;
	}
	
	
	public float ValueF {
		get {
			if (!animationEnabled) {
				return Mathf.Clamp((valueCurrent - valueMin) / (float) (valueMax - valueMin), 0, 1);
			} else {
				return Mathf.Clamp(animValueF, 0, 1);
			}
		}
		
		set {
			valueCurrent = Mathf.RoundToInt(value * (valueMax - valueMin) + valueMin);
		}
	}
	
	[HideInInspector]
	public bool animationEnabled;
	[HideInInspector]
	public float animValueF;
	
	// ===========================================================
	// Constructors (Including Static Constructors)
	// ===========================================================
	
	// ===========================================================
	// Getters / Setters
	// ===========================================================
	
	// ===========================================================
	// Methods for/from SuperClass/Interfaces
	// ===========================================================
	
	protected void Update() {
		//valueCurrent = globals.numOfLives;
		valueCurrent = Mathf.Clamp(globals.numOfLives, valueMin, valueMax);
		
		if (animationEnabled) {
			valueCurrent = valueMin + (int) (animValueF * (valueMax - valueMin));
		}
	}
	
	// ===========================================================
	// Methods
	// ===========================================================
	
	public void SetValueCurrent(int valueCurrent) {
		this.valueCurrent = valueCurrent;
	}
	
	public void SetValueMin(int valueMin) {
		this.valueMin = valueMin;
	}
	
	public void SetValueMax(int valueMax) {
		this.valueMax = valueMax;
	}
	
	public void SetValueF(float valueF) {
		ValueF = valueF;
	}
	
	// ===========================================================
	// Static Methods
	// ===========================================================
	
	// ===========================================================
	// Inner and Anonymous Classes
	// ===========================================================
	
}
