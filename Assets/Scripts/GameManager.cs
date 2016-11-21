using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System;
public class GameManager : MonoBehaviour {
	public GameObject playerPrefab;
	public Text continueText;
    public Text scoreText;
    private float blinkTime = 0f;
    private float timeElapsed = 0f;
    private float bestTime = 0f;
	private bool blink;
    //public bool entered = false;
    private GameObject floor;
	private Spawner spawner;
	private GameObject player;
    private Enter checker;
	private TimeManager tmanager;
    private bool gameStarted = false;
    private bool beatBestTime = false;
    private bool prepared;
	void Awake () {
		floor = GameObject.Find ("Foreground");
		spawner = GameObject.Find ("Spawner").GetComponent<Spawner> ();
		tmanager = GetComponent<TimeManager> ();
        checker = GameObject.Find("CentreCheck").GetComponent<Enter>();
   }
    // Use this for initialization
    void Start () {
		var floorHeight = floor.transform.localScale.y;
		var pos = floor.transform.position;
		pos.x = 0;
		pos.y = -((Screen.height / PixelPerfectCamera.pixelsToUnit) / 2) + (floorHeight / 2);
		floor.transform.position = pos;
		spawner.active = false;
		Time.timeScale = 0;
        continueText.text = "PRESS ANY KEY TO CONTINUE";
        bestTime = PlayerPrefs.GetFloat("BestTime");
	}

    // Update is called once per frame
    void Update()
    {
        if (!gameStarted && Time.timeScale == 0)
        {
            if (Input.anyKeyDown)
            {
                tmanager.ManipulateTime(1, 1);
                prepared = true;
            }
        }
        if (!gameStarted)
        {
            blinkTime++;
            if (blinkTime % 40 == 0)
            {
                blink = !blink;
            }
            if (prepared == true)
            {
                if (!checker.clearOn)
                {
                    ResetGame();
                    prepared = false;
                }
            }
            var textColor = beatBestTime ? "#FF0" : "#FFF";
            continueText.canvasRenderer.SetAlpha(blink ? 0 : 1);
            scoreText.text = "TIME: " + ScoreFormat(timeElapsed) + "\n<color="+textColor+">BEST :" + ScoreFormat(bestTime)+"</color>";
        }
        else
        {
            timeElapsed += Time.deltaTime;
            scoreText.text = "TIME :" + ScoreFormat(timeElapsed);
        }
    }
	void onPlayerKill() {
		gameStarted = false;
		spawner.active = false;
		var playerDestroyScript = player.GetComponent<DestroyOffScreen> ();
		playerDestroyScript.DestroyCallback -= onPlayerKill;
		player.GetComponent<Rigidbody2D> ().velocity = Vector2.zero;
		tmanager.ManipulateTime (0, 5.5f);
        continueText.text = "PRESS ANY BUTTON TO RESTART";
        if(timeElapsed > bestTime)
        {
            beatBestTime = true;
            bestTime = timeElapsed;
            PlayerPrefs.SetFloat("BestTime", bestTime);
        }
	}

	void ResetGame() {
		gameStarted = true;
		spawner.active = true;
		player = GameObjectUtil.Instantiate(playerPrefab, new Vector3(0, Screen.height/PixelPerfectCamera.pixelsToUnit/20+100f));
		var playerDestroyScript = player.GetComponent<DestroyOffScreen> ();
		playerDestroyScript.DestroyCallback += onPlayerKill;
        continueText.canvasRenderer.SetAlpha(0);
		continueText.text = "";
        timeElapsed = 0;
        beatBestTime = false;

    }
    string ScoreFormat(float s)
    {
        TimeSpan t = TimeSpan.FromSeconds(s);
        return string.Format("{0:D2}:{1:D2}", t.Minutes, t.Seconds);
    }
    /*void onTriggerEnter (BoxCollider2D checker)
    {
        entered = true;
    }
    void onTriggerExit (BoxCollider2D checker)
    {
        entered = false;
    }*/
}
