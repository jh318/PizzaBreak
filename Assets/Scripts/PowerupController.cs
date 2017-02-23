using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerupController : MonoBehaviour {

	public enum PowerupType {
		SpeedBall,
		SlowBall,
		ExtraLife,
		BigPaddle
	}

	public PowerupType powerupType = PowerupType.ExtraLife;
}
