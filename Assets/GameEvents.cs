using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameEvents : MonoBehaviour
{

    public static GameEvents current;
    public event Action onInteratbleTriggerEnter;

    private void Awake() {
        current = this;
    }

    public void InteratbleTriggerEnter() {
        onInteratbleTriggerEnter();
    }

}
