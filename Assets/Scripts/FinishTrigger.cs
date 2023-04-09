using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinishTrigger : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        StartCoroutine(ResetCo(other.GetComponentInParent<UnitController>().Status.Name));
    }   

    public IEnumerator ResetCo(string Name)
    {
        CanvasGame.Instance.AnnounceText.text = Name + " ½Â¸®!!";
        CanvasGame.Instance.Toasting();
        
        foreach (var item in GameManager.Instance.Controllers)
        {
            item.gameObject.SetActive(false);
        }

        yield return new WaitForSeconds(5f);
        GameManager.Instance.ResetGame();

    }
}
