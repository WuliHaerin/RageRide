using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class InGameGui : MonoBehaviour {

    public static InGameGui instance;

    [SerializeField] private Text score;
    [SerializeField] private Text scoreGameOver;
    [SerializeField] private Text hiScore;
    [SerializeField] GameObject gameOverPanel;
    [SerializeField] private string playScene;
    [SerializeField] private string menuScene;

    public bool isCancelAd;
    public bool isMiss;
    public GameObject CountDownObj;
    public GameObject AdPanel;


    public void SetAdPanel(bool a)
    {
        AdPanel.SetActive(a);
        Time.timeScale = a == true ? 0 : 1;
    }

    public void CancelAd()
    {
        isCancelAd = true;
        InGameGui.instance.SetAdPanel(false);
        PlayerCOntroller player = GameObject.FindFirstObjectByType<PlayerCOntroller>().GetComponent<PlayerCOntroller>();
        player.Die();

    }

    public IEnumerator Miss()
    {
        CountDownObj.SetActive(true);
        isMiss = true;

        yield return new WaitForSeconds(3);
        CountDownObj.SetActive(false);
        isMiss = false;

    }

    public void Revive()
    {
        AdManager.ShowVideoAd("192if3b93qo6991ed0",
            (bol) => {
                if (bol)
                {
                    SetAdPanel(false);
                    StartCoroutine(Miss());
                    PlayerCOntroller player = GameObject.FindFirstObjectByType<PlayerCOntroller>().GetComponent<PlayerCOntroller>();
                    Debug.Log("Revive");


                    AdManager.clickid = "";
                    AdManager.getClickid();
                    AdManager.apiSend("game_addiction", AdManager.clickid);
                    AdManager.apiSend("lt_roi", AdManager.clickid);


                }
                else
                {
                    StarkSDKSpace.AndroidUIManager.ShowToast("观看完整视频才能获取奖励哦！");
                }
            },
            (it, str) => {
                Debug.LogError("Error->" + str);
                //AndroidUIManager.ShowToast("广告加载异常，请重新看广告！");
            });
    }

    void Awake()
    {
        if (instance == null)
            instance = this;
    }

    // Use this for initialization
    void Start ()
    {
        if (gameOverPanel.activeSelf)
            gameOverPanel.SetActive(false);

        score.text = "" + GameController.instance.currentScore;

        if (GameController.instance.isMusicOn)
        {
            AudioListener.volume = 1;
        }
        else
        {
            AudioListener.volume = 0;
        }
    }
	
	// Update is called once per frame
	void Update ()
    {
        HiScore();
        score.text = "" + GameController.instance.currentScore;
        if (GameController.instance.isGameOver)
        {
            StartCoroutine(WaitForExplosion());
        }
    }

    void HiScore()
    {
        if (GameController.instance.hiScore < GameController.instance.currentScore)
            GameController.instance.hiScore = GameController.instance.currentScore;
    }

    public void MainMenuButton()
    {
        GameController.instance.isGameOver = false;
#if UNITY_5_3
        SceneManager.LoadScene(menuScene);
#else
        SceneManager.LoadScene(menuScene);
#endif
    }

    public void PlayButton()
    {
        GameController.instance.isGameOver = false;
        GameController.instance.currentScore=0;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    IEnumerator WaitForExplosion()
    {
        yield return new WaitForSeconds(0.5f);
        score.gameObject.SetActive(false);
        scoreGameOver.text = "" + GameController.instance.currentScore;
        hiScore.text = "" + GameController.instance.hiScore;
        gameOverPanel.SetActive(true);

        AdManager.ShowInterstitialAd("1lcaf5895d5l1293dc",
           () => {
               Debug.Log("--插屏广告完成--");

           },
           (it, str) => {
               Debug.LogError("Error->" + str);
           });
    }

}
