using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeScene : MonoBehaviour
{
    public void Change()
    {
        GameObject.Find("GameManager").GetComponent<GameManager>().ChangeScene();
    }
}
