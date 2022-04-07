using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum CameraType
{
    ObjectFront,
    Reset,
    FadeOut,
    FadeIn,
    FlashOut,
    FlashIn,
    ShowCutScene,
    HideCutScene,
    MoveToTarget,
    CameraReset,
}

[System.Serializable]
public class Dialogue
{
    [Header("카메라가 타겟팅할 대상")]
    public CameraType cameraType;
    public Transform tf_Target;

    //[Tooltip("대사 치는 캐릭터 이름")]
    [HideInInspector] public string name;

    //[Tooltip("대사 내용")]
    [HideInInspector] public string[] contexts;

    [HideInInspector]
    public string[] spriteName;
}

[System.Serializable]
public class DialogueEvent
{
    public string name;

    public Vector2 line;
    public Dialogue[] dialogues;
}

