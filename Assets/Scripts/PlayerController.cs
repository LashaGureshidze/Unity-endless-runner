using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour {
	public float speed = 10f;
	public Text scoreText;
	public Text hiScoreText;
	public Text levelText;
	public GameObject scriptObject;

	private Rigidbody rb;
	private int score = 0;
	private int hiScore = 0;
	private float time = 0f;
	private int speedCheck = 0;

	// Use this for initialization
	void Start () {
		rb = GetComponent<Rigidbody> ();

		score = 0;
		if (PlayerPrefs.HasKey ("hiscore")) {
			hiScore = PlayerPrefs.GetInt ("hiscore");
		} else {
			hiScore = 0;
			PlayerPrefs.SetInt("hiscore", hiScore);
		}


		updateScores ();
	}

	void updateScores ()
	{

		scoreText.text = "Score : " + score;
		hiScoreText.text = "Hi Score : " + hiScore;
		levelText.text = "Level : " + speed;
	}

	public bool isDialogActive = false;

	void Update() {
		checkScore ();
		checkSpeed ();

		//pause
		if (!isDialogActive && Input.GetMouseButtonDown(0))
		{
			isDialogActive = true;
			Time.timeScale = 0f;
			GameObject dialog = Instantiate(Resources.Load("Prefabs/dialog")) as GameObject;
			dialog.GetComponent<DialogController> ().setMode (false, this);
		}
	}

	void checkSpeed ()
	{
		if (score % 10 == 0 && speedCheck != score) {
			speed += 1f;
			speedCheck = score;

			scriptObject.GetComponent<GameController>().generateColor();
		}

	}

	void checkScore ()
	{
		time += Time.deltaTime;
		if (time >= 1f) {
			time = 0f;
			
			score ++;
			if (score > hiScore) hiScore = score;
			updateScores();
		}
	}	

	void FixedUpdate () {
		float moveHorizontal = 0f;
		if (SystemInfo.supportsAccelerometer) {
			//for mobile
			moveHorizontal = Input.acceleration.x;
		} else {
			//for pc
			moveHorizontal = Input.GetAxis ("Horizontal");
		}
		Vector3 movement = new Vector3 (moveHorizontal, 0.0f, 1.0f);

		rb.velocity =  movement * speed;
	}

	void OnTriggerEnter(Collider collider) {
		endGame ();
	}

	void endGame ()
	{
		SoundHelper.Instance.MakeExplosionSound ();
		Time.timeScale = 0f;

		animateExplotion ();
		saveScore ();
		showDialog ();
		dissaparePlayer ();
	}

	void showDialog ()
	{
		isDialogActive = true;
		GameObject dialog = Instantiate(Resources.Load("Prefabs/dialog")) as GameObject;
		dialog.GetComponent<DialogController> ().setMode (true, this);
		dialog.GetComponent<DialogController> ().setCurrentScore (score);
	}

	void dissaparePlayer ()
	{
		//TODO
	}

	void saveScore ()
	{
		PlayerPrefs.SetInt ("hiscore", hiScore);
	}

	void animateExplotion ()
	{
		//TODO
	}
}
