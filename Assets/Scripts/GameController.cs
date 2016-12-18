using UnityEngine;
using System.Collections;

public class GameController : MonoBehaviour {

	public GameObject player;
	public float horizontZ = 30f;
	public float spawnDistance = 5f;
	public int screenWidth = 100; //TODO

	private float playerDist = 0f; 
	private float playerZ;
	private Color color;

	// Use this for initialization
	void Start () {
		generateColor ();
		Debug.Log (color);
	}

	public void generateColor ()
	{
		color = new Color (Random.Range(0f, 1f), Random.Range(0f, 1f), Random.Range(0f, 1f));
	}
	
	// Update is called once per frame
	void Update () {
		float curZ = player.transform.position.z;
		playerDist += curZ - playerZ;
		playerZ = curZ;
	}

	void FixedUpdate() 
	{
		if (playerDist > spawnDistance) 
		{
			playerDist = 0f;
			spawnCubes();
		}
	}

	void spawnCubes ()
	{
		for (int i = 0; i < 10; i ++) {
			spawnCube();
		}
	}

	void spawnCube ()
	{
		float playerX = player.transform.position.x;
		float randomX = Random.Range (playerX - (screenWidth / 2), playerX + (screenWidth / 2));
		
		GameObject cube = Instantiate(Resources.Load("Prefabs/Cube")) as GameObject;
		cube.GetComponent<Renderer> ().material.color = color;

		cube.transform.position = new Vector3 (randomX, 0.0f, playerZ + horizontZ);
	}
}
