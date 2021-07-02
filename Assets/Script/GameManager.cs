using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    [SerializeField] private float _gameTime = 60f;
    //this variable is used to change physics of game
    [SerializeField] private float _timeToBasket = 1;
    [SerializeField] private float _distY;
    [SerializeField] private float _maxXDistance;
    [SerializeField] private float _minXDist;
    public float TimeToBasket {get {return _timeToBasket;}}
    public float DistY {get {return _distY;}}
    public float MinVx {get {return new Vector2(_minXDist, _distY).magnitude / _timeToBasket;}}
    public float MaxVx {get {return new Vector2(_maxXDistance, _distY).magnitude / _timeToBasket;}}
    private float _initialGameTime;
    public delegate void GameEventHandler(bool startEvent);
    public event GameEventHandler GameEvent;
    public delegate void TimeEventHandler(float time);
    public event TimeEventHandler TimeEvent;
    public delegate void StartTimeHandler(int time);
    public event StartTimeHandler StartTimeEvent;

    public int FinalScore {get {return _finalScore;} set{_finalScore = value;}}
    private int _finalScore;
    public int EnemyFinalScore {get {return _enemyFinalScore;} set{_enemyFinalScore = value;}}
    private int _enemyFinalScore;
    private int _lastLevel;
    [SerializeField] private int _winnerScore = 10;
    [SerializeField] private Animator fade;

    public IControllerInterface Input{get {return _input;}}
    private IControllerInterface _input;

    void Awake()
    {
        if(instance == null)
        {
            instance = this;
            DontDestroyOnLoad(transform.root.gameObject);
            if(Application.platform == RuntimePlatform.WindowsPlayer || Application.platform == RuntimePlatform.WindowsEditor)
            {
                _input = new MouseController();
            }
            if(Application.platform == RuntimePlatform.Android)
            {
                _input = new MobileController();
            }
        }else
        {
            Destroy(transform.root.gameObject);
            return;
        }
    }

    void Start()
    {
        GameEvent?.Invoke(false);
    }

    // Function used to start the all game
    public void GameStart()
    {
        StartCoroutine("StartTime");
    }

    IEnumerator StartTime()
    {
        int _residualTime = 3;
        
        while(_residualTime >= 0)
        {   
            StartTimeEvent?.Invoke(_residualTime);

            _residualTime -= 1;
            yield return new WaitForSeconds(1);
        }
        
        GameEvent?.Invoke(true);
        float currentGameTime = _gameTime;
        while(currentGameTime >= 0)
        {
            TimeEvent?.Invoke(currentGameTime);
            currentGameTime -= 1;
            yield return new WaitForSeconds(_gameTime/_gameTime);
        }
        GameEnd();
    }

    // Function used to end the all game
    void GameEnd()
    {
        GameEvent?.Invoke(false);
        _initialGameTime = 0f;
        //CallFinishUI
        ChangeScene(2);
    }

    public void RegisterGameEvent(GameEventHandler handler)
    {
        GameEvent += handler;
    }

    public void RegisterTimeEvent(TimeEventHandler handler)
    {
        TimeEvent += handler;
    }
    public void RegisterStartTimeEvent(StartTimeHandler handler)
    {
        StartTimeEvent += handler;
    }

    public void UnRegisterGameEvent(GameEventHandler handler)
    {
        GameEvent -= handler;
    }

    public void UnRegisterTimeEvent(TimeEventHandler handler)
    {
        TimeEvent -= handler;
    }

    public void UnRegisterStartTimeEvent(StartTimeHandler handler)
    {
        StartTimeEvent -= handler;
    }

    public void RestartLevel()
    {
        StartCoroutine(LoadYourAsyncScene(_lastLevel));
    }

    public void ChangeScene(int scene)
    {
        StartCoroutine(LoadYourAsyncScene(scene));
    }

    IEnumerator LoadYourAsyncScene(int scene)
    {
        fade.SetTrigger("CrossFadeEnter");
        yield return new WaitForSeconds(1f);
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync( scene, LoadSceneMode.Single);
            
        while(!asyncLoad.isDone)
        {
            yield return null;
        }

        if(asyncLoad.isDone)
        {
            
            if(scene == 1 || scene == 3)
            {    
                _lastLevel = scene;
                GameStart();
            }
            if(scene == 2)
            {
                if(_lastLevel == 1)
                {
                    GameObject.Find("ResultText").GetComponent<Text>().text = _finalScore > _winnerScore ? "WIN":"LOSER";
                    GameObject.Find("ResultScore").GetComponent<Text>().text = "Score: " + _finalScore;
                }else if(_lastLevel == 3)
                {
                    GameObject.Find("ResultText").GetComponent<Text>().text = _finalScore > _enemyFinalScore ? "WIN":"LOSER";
                    GameObject.Find("ResultScore").GetComponent<Text>().text =  _finalScore + "/" + _enemyFinalScore;
                }


            }
            fade.SetTrigger("CrossFadeExit");
            yield return new WaitForSeconds(1f);
        }



    }

    public void QuitGame()
    {
        Application.Quit();
        //AndroidJavaObject activity = new AndroidJavaClass("com.unity3d.player.UnityPlayer").GetStatic<AndroidJavaObject>("currentActivity");
        //activity.Call("finish");
    }

}
