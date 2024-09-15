using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "New Achievement", menuName = "Achievements/Achievement")]
public class AchievementSO : ScriptableObject
{
   
    public string achievementName;
    public string description;
    public Sprite icon;
    public bool isUnlocked;
    public int ExpReward;
    public int MoneyReward;

    public int achievementGoal; // The number to reach (e.g., 1000 coins)
    public int currentValue;    // Current progress

    public virtual bool CheckCompletion()
    {
        // Override this method in derived classes to define the condition for unlocking
        return false;
    }

    public bool CheckProgress()
    {
        return currentValue >= achievementGoal;
    }
}
