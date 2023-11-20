using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq.Expressions;
using UnityEngine;

[CreateAssetMenu(fileName = "DataManager", menuName = "Slice up/DataManager", order = 0)]
public class DataManager : ScriptableObject
{
    public int currentScore=0;
    public int highestScore=0;
    public int comboScore=0;
    public int missedBlock=0;
    public int maxBlockCamMissed=0;
    public bool endlessMode =false;
    public float blockSpawnSpeed;
    public AudioClip MusicSelected;

    [Range(0,1)]
    public float SFXVolume, musicVolume;

    public void updateCurrentScore()
    {
        currentScore+=comboScore;
    }
    public void setHighestScore()
    {
        if(currentScore> highestScore)
        highestScore = currentScore;
    }

    public void comboReset()
    {
        comboScore = 0;
    }
    public void currentScoreReset()
    {
        currentScore = 0;
    }
    public void missedBlockReset()
    {
        missedBlock = 0;
    }
}
