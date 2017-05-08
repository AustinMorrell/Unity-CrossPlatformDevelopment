﻿using System;
using System.Collections.Generic;
using RPGStats;
using ScriptableAssets;
using UnityEngine;
using UnityEngine.Events;

public class PlayerBehaviour : MonoBehaviour
{
    private int modcount;

    public OnStatModify onStatModify;
    public Stats stats;    

    private void Awake()
    {
        var newstats = Instantiate(stats);
        stats = newstats;
    }

    private void Start()
    {
        onStatModify.Invoke();
    }

    public void ModifyStat(string stat, Modifier mod)
    {
        var valids = new List<string>(Enum.GetNames(typeof(StatType)));
        if(!valids.Contains(stat)) return;
        stats.AddModifier(modcount++, mod);
        onStatModify.Invoke();
    }
    public void ModifyRandomStat(string stat)
    {
        var valids = new List<string>(Enum.GetNames(typeof(StatType)));
        if (!valids.Contains(stat)) return;


        var res = stats.AddModifier(modcount++, new Modifier("add", stat, 2));
        var info = string.Format("Stat: {0}, {1} modifier of {2} :: {3}", stat, "add", 2, res);
        Debug.Log(info);
        onStatModify.Invoke();
    }

    [Serializable]
    public class OnStatModify : UnityEvent
    {
    }

    [Serializable]
    public class OnHealthChange : UnityEvent
    {
    }
}