﻿using UnityEngine;
using System.Collections;

public class BackgroundMusic : MonoBehaviour {

	// Use this for initialization
	void Awake(){
		DontDestroyOnLoad (transform.gameObject);
	}
}
