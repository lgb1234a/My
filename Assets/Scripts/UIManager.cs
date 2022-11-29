using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance {get; private set;}
    public GameObject playOrderPanelGO;
    // Start is called before the first frame update
    void Awake()
    {
        Instance = this;
    }

    // Update is called once per frame
    public void ShowOrHidePlayerOrderPanel(bool state)
    {
        playOrderPanelGO.SetActive(state);
    }

    public void ClickAttackButton()
    {
        GameController.Instance.isPerformingLogic = false;
        GameController.Instance.isPlayerTurn = true;
        GameController.Instance.enterCurrentRound = true;
        ShowOrHidePlayerOrderPanel(false);
    }
}
