using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CanvasGame : MonoBehaviour
{
    public static CanvasGame Instance;
    public Text AnnounceText;
    public Text RankText;
    public float TweeningTime = 5f;
    public float crnTime;
    public CanvasGroup canvasGroup;

    public void Toasting()
    {
        crnTime = TweeningTime;
    }

    public void Awake()
    {
        Instance = this;
    }

    public void Update()
    {
        if (crnTime > 0)
        {
            canvasGroup.alpha += Time.deltaTime;
            crnTime -= Time.deltaTime;
        }
        else
        {
            canvasGroup.alpha -= Time.deltaTime;
        }
    }

    public void FixedUpdate()
    {
        ReNewRank();
    }

    public void ReNewRank()
    {
        (float pos, string name) first = new(float.MinValue,"");
        (float pos, string name) second = new(float.MinValue,"");
        (float pos, string name) third = new(float.MinValue,"");

        foreach (var item in GameManager.Instance.Controllers)
        {
            if (first.pos <= item.gameObject.transform.position.x)
            {
                first.pos = item.gameObject.transform.position.x;
                first.name = item.Status.Name;
            }
            else if (second.pos <= item.gameObject.transform.position.x)
            {
                second.pos = item.gameObject.transform.position.x;
                second.name = item.Status.Name;
            }
            else if (third.pos <= item.gameObject.transform.position.x)
            {
                third.pos = item.gameObject.transform.position.x;
                third.name = item.Status.Name;
            }
        }

        RankText.text = $"1À§ {first.name} \n 2À§ {second.name}\n 3À§ {third.name}";

    }
}
