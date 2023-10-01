using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mng
{
    static public Manager inst
    {
        get { return Manager.Instance; }
    }

    static public SoundManager sound
    {
        get { return SoundManager.Instance; }
    }

/*
    static public PoolManager pool
    {
        get { return PoolManager.Instance; }
    }
*/

    static public PlayManager play
    {
        get { return PlayManager.Instance; }
    }

    static public MainCanvas canvas
    {
        get { return MainCanvas.Instance; }
    }

    static public TableManager table
    {
        get { return TableManager.Instance; }
    }

    static public DataManager data
    {
        get { return DataManager.Instance; }
    }
}
