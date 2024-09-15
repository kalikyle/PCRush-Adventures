using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "GoldDigger1", menuName = "Achievements/GoldDigger1")]
public class GoldDigger : AchievementSO
{
    // Start is called before the first frame update
    public int moneyRequired;
    public override bool CheckCompletion()
    {
        currentValue = GameManager.instance.PlayerTotalMoney; // Keep track of the current total money earned.
        return currentValue >= moneyRequired; // Unlock the achievement if the total money earned is greater than or equal to the required amount.
    }

    private void OnEnable()
    {
        achievementGoal = moneyRequired; // Set the goal for this achievement.
    }
}
