using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class CharacterFightAI : MonoBehaviour
{
    public NavMeshAgent meshAgent;
    private bool isMoving;
    private Vector3 attackTargetPos;
    public Transform attackerTransform;
    public CharacterAnimatorController cac;
    public int hp;
    public int mp;
    public bool isPlayer;
    public Vector3 initPos;
    private bool isReturning;
    private bool isDead;
    // Start is called before the first frame update
    void Start()
    {
        // 关闭导航z轴旋转
        meshAgent.updateRotation = false;
        meshAgent.updateUpAxis = false;
        meshAgent.SetDestination(transform.position);
        initPos = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        if (isDead)
        {
            return;
        }
        if (isMoving)
        {
            if (isReturning)
            {
                ReturnBehaviour();
            }
            else
            {
                MoveBehaviour();
            }
        }
    }

    public void PerformLogic(ActCode code, Vector3 attackPos)
    {
        switch (code)
        {
            case ActCode.Attack:
                SetMoveAction(attackPos, true);
                break;
            default:
                break;
        }
    }

    public void SetMoveAction(Vector3 attackPos = default, bool moveAct = false)
    {
        isMoving = true;
        if (moveAct)
        {
            isReturning = false;
            attackTargetPos = attackPos;
            meshAgent.SetDestination(attackTargetPos);
            // 设置移动动画
            cac.PlayMoveAnimation(-1);
        }
        else
        {
            isReturning = true;
            meshAgent.SetDestination(initPos);
            // 设置移动动画
            cac.PlayMoveAnimation(1);
        }
    }

    public void MoveBehaviour()
    {
        if (Vector3.Distance(transform.position, attackTargetPos) <= 0.2f)
        {
            isMoving = false;
            // 攻击
            AttackBehaviour();
        }
    }

    public void ReturnBehaviour()
    {
        if (Vector3.Distance(transform.position, initPos) <= 0.2f)
        {
            isMoving = false;
            cac.PlayIdleAnimation();
            // 结束自身回合
            GameController.Instance.isPerformingLogic = false;
            if (GameController.Instance.isPlayerTurn)
            {
                GameController.Instance.isPlayerTurn = false;
            }
            else
            {
                GameController.Instance.enterCurrentRound = false;
                UIManager.Instance.ShowOrHidePlayerOrderPanel(true);
            }
        }
    }

    public void AttackBehaviour()
    {
        cac.PlayAttackAnimation();
    }

    public void AttackTarget()
    {
        if (isPlayer)
        {
            GameController.Instance.enemyAI.HitBehaviour();
        }else
        {
            GameController.Instance.playerAI.HitBehaviour();
        }
    }

    public void HitBehaviour()
    {
        cac.PlayHitAnimation();
        hp -= 1;
        if (hp <= 0)
        {
            cac.PlayDieAnimation();
            Time.timeScale = 0.3f;
        }
    }

    public void DieBehaviour()
    {
        Time.timeScale = 1;
        isDead = true;
    }
}

public enum ActCode
{
    Attack
}
