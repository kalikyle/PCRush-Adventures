using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

[CreateAssetMenu(fileName ="QuestInfoSO", menuName ="ScriptableObject/QuestInfoSO", order =1)]
public class QuestInfoSO : ScriptableObject
{

    [field: SerializeField]
    public string id { get;  set; }

    [Header("General")]

    public string displayName;

    [Header("Requirements")]

    public int levelRequirement;

    public QuestInfoSO[] questPrerequisites;


    [Header("Steps")]

    public GameObject[] questStepsPrefab;


    [Header("Rewards")]
    public int goldReward;
    public int experiencereward;


    private void OnValidate()
    {
#if UNITY_EDITOR
        id = this.name;
        UnityEditor.EditorUtility.SetDirty(this);
#endif
    }

}
