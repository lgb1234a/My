using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set;}
    private float enterFightTimer;
    public bool canEnterFight;
    public GameObject normalModeGO;
    public GameObject fightModeGO;
    // public Transform PlayNormalTransform;
    public AudioSource audioSource;
    public AudioClip normalAudioClip;
    public AudioClip[] fightAudioClips;

    void Awake()
    {
        Instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (canEnterFight)
        {
            JudgeEnterTheFight();
        }
    }

    void JudgeEnterTheFight()
    {
        if (Time.time - enterFightTimer >= 5)
        {
            if (Random.Range(0,5) >= 1)
            {
                // Fight
                SetEnterFightState(false);
                EnterOrExitFightMode(true);
            }else
            {
                // reset timer
                SetEnterFightState(true);
            }
        }
    }

    public void SetEnterFightState(bool state)
    {
        canEnterFight = state;
        enterFightTimer = Time.time;
    }

    public void EnterOrExitFightMode(bool enter)
    {
        normalModeGO.SetActive(!enter);
        fightModeGO.SetActive(enter);
        canEnterFight = !enter;
        Vector3 pos = Camera.main.transform.position;
        pos.z = 0;
        fightModeGO.transform.position = pos;


        if (enter)
        {
            audioSource.clip = fightAudioClips[Random.Range(0, 2)];
        }else
        {
            audioSource.clip = normalAudioClip;
        }
        audioSource.Play();
    }
}
