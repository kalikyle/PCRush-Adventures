using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "LevelUp", menuName = "Achievements/LevelUp")]
public class LevelUp : AchievementSO
{
    // Start is called before the first frame update
    public int levelRequired;
    public override bool CheckCompletion()
    {
        currentValue = GameManager.instance.PlayerLevel; // Keep track of the current total money earned.
        return currentValue >= levelRequired; // Unlock the achievement if the total money earned is greater than or equal to the required amount.
    }

    private void OnEnable()
    {
        achievementGoal = levelRequired; // Set the goal for this achievement.
    }
}
