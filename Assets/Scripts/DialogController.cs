using UnityEngine;
using UnityEngine.UI;

public class DialogController : MonoBehaviour {

	public Text top1;
	public Text top2;
	public Text top3;
	public Text top4;
	public Text top5;
	public Text button;

	private bool endGame = true;
	private int top1s = 0;
	private int top2s = 0;
	private int top3s = 0;
	private int top4s = 0;
	private int top5s = 0;

	// Use this for initialization
	void Awake () {
		showScores (-1);
	}

	private void showScores(int score) {
		if (PlayerPrefs.HasKey ("top1")) {
			top1s = PlayerPrefs.GetInt("top1");
			top1.text = ""+top1s;
			if (score == top1s) {
				top1.color = Color.red;
			}
		}
		if (PlayerPrefs.HasKey ("top2")) {
			top2s = PlayerPrefs.GetInt("top2");
			top2.text = ""+top2s;
			if (score == top2s) {
				top2.color = Color.red;
			}
		}
		if (PlayerPrefs.HasKey ("top3")) {
			top3s = PlayerPrefs.GetInt("top3");
			top3.text = ""+top3s;
			if (score == top3s) {
				top3.color = Color.red;
			}
		}
		if (PlayerPrefs.HasKey ("top4")) {
			top4s = PlayerPrefs.GetInt("top4");
			top4.text = ""+top4s;
			if (score == top4s) {
				top4.color = Color.red;
			}
		}
		if (PlayerPrefs.HasKey ("top5")) {
			top5s = PlayerPrefs.GetInt("top5");
			top5.text = ""+top5s;
			if (score == top5s) {
				top5.color = Color.red;
			}
		}
	}

	public void setCurrentScore(int score) {

		if (score > top1s) {
			top5s = top4s;
			PlayerPrefs.SetInt("top5", top5s);
			top4s = top3s;
			PlayerPrefs.SetInt("top4", top4s);
			top3s = top2s;
			PlayerPrefs.SetInt("top3", top3s);
			top2s = top1s;
			PlayerPrefs.SetInt("top2", top2s);
			top1s = score;
			PlayerPrefs.SetInt("top1", top1s);
		}

		if (score > top2s && score < top1s) {
			top5s = top4s;
			PlayerPrefs.SetInt("top5", top5s);
			top4s = top3s;
			PlayerPrefs.SetInt("top4", top4s);
			top3s = top2s;
			PlayerPrefs.SetInt("top3", top3s);
			top2s = score;
			PlayerPrefs.SetInt("top2", top2s);
		}
		if (score > top3s && score < top2s) {
			int tmp = top4s;
			top4s = top3s;
			PlayerPrefs.SetInt("top4", top4s);
			top5s = tmp;
			PlayerPrefs.SetInt("top5", top5s);
			top3s = score;
			PlayerPrefs.SetInt("top3", top3s);
		}
		if (score > top4s && score < top3s) {
			int tmp = top4s;
			top4s = score;
			PlayerPrefs.SetInt("top4", top4s);
			top5s = tmp;
			PlayerPrefs.SetInt("top5", top5s);
		}

		if (score > top5s && score < top4s) {
			top5s = score;
			PlayerPrefs.SetInt("top5", top5s);
		}

//		if (score <= top5s) return;



		showScores (score);
	}

	private PlayerController controller;

	/*
	 *if this called, it's mean that game end. 
	 */
	public void setMode(bool endGame, PlayerController controller) {
		this.endGame = endGame;
		this.controller = controller;

		if (endGame) {
			button.text = "Try Again";
		} else {
			button.text = "Resume";
		}
	}


	public void onQuit() {
		SoundHelper.Instance.MakeButtonSound ();
		Application.Quit ();
	}

	public void onButton() {
		SoundHelper.Instance.MakeButtonSound ();
		//tODO kulebis shenaxva
		controller.isDialogActive = false;
		Time.timeScale = 1f;
		if (endGame) {
			Application.LoadLevel("Game");
		} else {
			Destroy(gameObject);
		}
	}
}
