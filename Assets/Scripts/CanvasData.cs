using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CanvasData : MonoBehaviour
{
    public Text NameText;
    public Text ChangeText;
    public string Input = "";
    public int crnNum = 0;
    public GameObject GameUI;

    public void Start()
    {
        ChangeText.text = $"{crnNum} / {GameManager.Instance.GameStage}";
    }

    public void ClickButton(int i)
    {
        GameManager.Instance.PlayerName = NameText.text;
        Input += i.ToString();
        crnNum++;
        ChangeText.text = $"{crnNum} / {GameManager.Instance.GameStage}";
        if (crnNum >= GameManager.Instance.GameStage)
        {
            GameManager.Instance.InputValue = Input;
            GameManager.Instance.CreateMyGamer();
            GameUI.SetActive(true);
            GameManager.Instance.StartCoCo();
            this.gameObject.SetActive(false);
        }
    }



}
