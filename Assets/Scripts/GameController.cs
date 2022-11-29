using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    public static GameController Instance {get; private set;}
    public bool enterCurrentRound;  // 判断是否进入当前回合
    public bool isPlayerTurn;       // 当前的回合是
    public bool isPerformingLogic;
    public CharacterFightAI playerAI;
    public CharacterFightAI enemyAI;
    // Start is called before the first frame update
    void Awake()
    {
        Instance = this;
    }

    // Update is called once per frame
    void Update()
    {
        if (enterCurrentRound)
        {
            if (!isPerformingLogic)
            {
                isPerformingLogic = true;
                if (isPlayerTurn)
                {
                    playerAI.PerformLogic(ActCode.Attack, enemyAI.attackerTransform.position);
                }else
                {
                    // 敌人逻辑
                    enemyAI.PerformLogic(ActCode.Attack, playerAI.attackerTransform.position);
                }
            }
        }
    }
}
