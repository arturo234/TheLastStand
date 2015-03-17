using UnityEngine;
using System.Collections;

public class BasicProjectile : MonoBehaviour
{
    private double timeSpawned;
    private double selfDestructTime;
    private double currentTime;

	// Use this for initialization
	void Start () 
    {
        selfDestructTime = 5.0f;
	}
	
	// Update is called once per frame
	void Update () 
    {
        currentTime += Time.deltaTime;
        if (currentTime + timeSpawned >= timeSpawned + selfDestructTime)
        {
			currentTime = 0;
			RemoveArrow();
        }
	}

    public void RemoveArrow()
    {
		this.gameObject.tag = "";
		ObjectPool.instance.PoolObject(this.gameObject);
    }
	public void OnTriggerEnter2D(Collider2D other)
	{
		if (other.tag.Equals ("Obstacle"))
			RemoveArrow ();
	}
}
