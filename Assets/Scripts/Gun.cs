using System;
using UnityEngine;

[Serializable]
public class Gun
{
    [HideInInspector] public string name = "Gun";
    public GunProperties gunProperties;
    public Animator gunAnimator;
}
