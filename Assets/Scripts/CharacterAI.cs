using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class CharacterAI : MonoBehaviour
{
    public NavMeshAgent meshAgent;
    private Vector3 targetPos;
    public CharacterAnimatorController cac;
    public bool willStoping;
    public GameObject testTarget;
    public GameObject clickEffect;
    private float followMouseTimer;
    private int clickCount;
    private bool inFollowMouse;
    private float createEffectTimer;
    // Start is called before the first frame update
    void Start()
    {
        meshAgent.updateRotation = false;
        meshAgent.updateUpAxis = false;
        targetPos = transform.position;
        meshAgent.updatePosition = false;
    }

    // Update is called once per frame
    void Update()
    {
        cac.SwitchAnimationState(meshAgent.nextPosition - transform.position, Vector3.Distance(transform.position, targetPos));
        if (Input.GetMouseButtonDown(0))
        {
            clickCount ++;
            inFollowMouse = false;
            ClickMouse();
        }

        GetNotWalkableAreaMovePoint();
        DoubleClickMouse();

        testTarget.transform.position = targetPos;
        transform.position = meshAgent.nextPosition;

    }

    void ClickMouse()
    {
        targetPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        targetPos.z = transform.position.z;
        meshAgent.SetDestination(targetPos);
        if (Time.time - createEffectTimer >= 0.05f) {
            createEffectTimer = Time.time;
            Instantiate(clickEffect, targetPos, Quaternion.identity);
        }
    }

    void GetNotWalkableAreaMovePoint()
    {
        if (willStoping)
        {
            var ray = new Ray2D(transform.position, targetPos - transform.position);
            var rh = Physics2D.Raycast(ray.origin, ray.direction);
            if (rh)
            {
                targetPos = rh.point;
                targetPos -= 0.1f * (targetPos - transform.position);
            }
            willStoping = false;
            targetPos.z = transform.position.z;
            meshAgent.SetDestination(targetPos);
        }
    }

    void DoubleClickMouse()
    {
        if (inFollowMouse) 
        {
            ClickMouse();
        }else 
        {
            if (Time.time - followMouseTimer >= 0.6f)
            {
                followMouseTimer = Time.time;
                clickCount = 0;
            }else
            {
                if (clickCount > 1)
                {
                    inFollowMouse = true;
                }
            }
        }
    }
}
