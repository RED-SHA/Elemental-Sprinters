using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    public GameObject Prefeb;

    public List<UnitController> Controllers = new();
    public Transform[] SpawnPoint;
    public int GamerCount = 4;
    public int GameStage = 10;
    public int NowStage = 0;
    public bool IsCamOnMe= false;
    public string Answer = "";

    [Header("PlayerSetting")]
    public string PlayerName;
    public string InputValue;

    public string[] Names = { "김", "남", "소", "한" };

    public void Awake()
    {
        Instance = this;
        CreateGamer();
    }

    public void Start()
    {
        //StartGame();
    }

    public void ResetGame()
    {
        SceneManager.LoadScene(0);
    }

    public void CreateGamer()
    {
        for (int i = 0; i < GamerCount; i++)
        {
            var Go = Instantiate(Prefeb);
            Controllers.Add(Go.GetComponent<UnitController>());
            Go.transform.position = SpawnPoint[i].position;
            Go.GetComponent<UnitController>().Status.SetUnitStatus(2f, 0f, Names[Random.Range(0, Names.Length)], 5f, StageValue(), false, 1, 1f);
        }

        int rndCount = Random.Range(0, GamerCount);
        Controllers[rndCount].Cam.ActiveCamera(CamStyle.OribitalCam);
    }

    public void ChangeCamMode()
    {
        IsCamOnMe = !IsCamOnMe;

        if (IsCamOnMe)
        {
            int rndCount0 = Random.Range(0, 2);
            Controllers[GamerCount].Cam.ActiveCamera((CamStyle)rndCount0);
            return;
        }
        int rndCount = Random.Range(0, GamerCount);
        int rndCount2 = Random.Range(0, 2);
        Controllers[rndCount].Cam.ActiveCamera((CamStyle)rndCount2);
    }

    public void CreateMyGamer()
    {
        var Go = Instantiate(Prefeb);
        Controllers.Add(Go.GetComponent<UnitController>());
        Go.transform.position = SpawnPoint[GamerCount].position;
        Go.GetComponent<UnitController>().Status.SetUnitStatus(2f, 0f, PlayerName, 5f, InputValue, false, 1, 1f);
        Controllers[GamerCount].Cam.ActiveCamera(CamStyle.OribitalCam);
        IsCamOnMe = true;
    }

    public string StageValue()
    {
        string value = "";
        for (int i = 0; i <= GameStage; i++)
        {
            value += Random.Range(0, 3).ToString();
        }
        return value;
    }

    public void StartGame()
    {
        Answer = StageValue();

        foreach (var controller in Controllers)
        {
            controller.StartStart();
        }
    }

    public void BattlePhase(int stage, char AnswerChar)
    {
        int Answer = AnswerChar - '0';
        string AppendText = $"{NowStage} / {GameStage}번째 가위바위보!\n";

        foreach (var controller in Controllers)
        {
            int controllerChoice = controller.Status.preInputValue[stage] - '0';

            if ((Answer == 0 && controllerChoice == 2) 
                || (Answer == 1 && controllerChoice == 0) 
                || (Answer == 2 && controllerChoice == 1))
            {
                //win
                controller.StartBuff();
                AppendText += $"{controller.Status.Name} 승리\n";
                print($"{Answer} / {controllerChoice} {controller.Status.Name} 는 승리했다!\n");
            }
            else if (Answer == controllerChoice)
            {
                //tie
                controller.StartRoll_Front();
                AppendText += $"{controller.Status.Name} 비김\n";
                print($"{Answer} / {controllerChoice}  {controller.Status.Name} 는 비겼다!\n");
            }
            else
            {
                //lose
                controller.StartDeath();
                AppendText += $"{controller.Status.Name} 패배\n";
                print($"{Answer} / {controllerChoice}  {controller.Status.Name} 는 패배했다!\n");
            }
        }

        CanvasGame.Instance.AnnounceText.text = AppendText;
        CanvasGame.Instance.Toasting();
    }

    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            if (NowStage >= GameStage)
            {
                print("남은 기회가 없습니다");
                return;
            }
            BattlePhase(NowStage, Answer[NowStage++]);
        }
    }
    public void GamingButton()
    {
        if (NowStage >= GameStage)
        {
            CanvasGame.Instance.AnnounceText.text = "남은 기회가 없습니다";
            CanvasGame.Instance.Toasting();
            print("남은 기회가 없습니다");
            return;
        }
        if (Answer == "")
        {
            CanvasGame.Instance.AnnounceText.text = "아직 달리기전입니다!";
            CanvasGame.Instance.Toasting();
            return;
        }
        BattlePhase(NowStage, Answer[NowStage++]);

    }


    public void StartCoCo()
    {
        StartCoroutine(startCo());
    }

    public IEnumerator startCo()
    {
        yield return new WaitForSeconds(3f);
        GameManager.Instance.StartGame();
    }
}
