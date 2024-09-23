using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "FinishStory", menuName = "Achievements/FinishStory")]
public class FinishStory : AchievementSO
{
    // Start is called before the first frame update
    public override bool CheckCompletion()
    {
        // Check if the player has built their first PC
        return AchievementManager.instance.FinishStory;
    }
}
