

using UnityEngine;
using System.Collections;


public class GenericCharacter : MonoBehaviour
{
    public double health, fireRate;
    public float arrowVelocity;
    public Vector3 theta, arrowDir;
	protected double currentTime;
    // Shared projectile object pool for all generic characters
}