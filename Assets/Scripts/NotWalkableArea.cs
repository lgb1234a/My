using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NotWalkableArea : MonoBehaviour
{
    public CharacterAI characterAI;

    void OnMouseDown()
    {
        characterAI.willStoping = true;
    }
}
