using System;
using UnityEngine;


[Serializable]

public struct PlayerStruct
{
    [Header("Health data")]
    public float CurrentHP;
    public float MaxHP;
    public float HealRate;

    [Header("Move data")]
    public float CurrentTurnSpeed;
    public float MaxTurnSpeed;
    public float MaxTurnAxisX;
    public float MinTurnAxisX;
    public float MaxTurnAxisY;
    public float MinTurnAxisY;

    [Header("UI data")]
    public Material BloodMaskMaterial;
}
