using UnityEngine;

public class Quest : SingletonBehavior<Quest>
{
    public string questName;
    public int requiredKills; 
    private int currentKills = 0;
    public GameObject questUI;

    private bool isActive = false;

    private void Start()
    {
        questUI.SetActive(false); 
    }
    public void ActivateQuest()
    {
        isActive = true;
        currentKills = 0;
        questUI.SetActive(true);
        UpdateQuestUI();
    }

    public void EnemyKilled()
    {
        if (!isActive) return;

        currentKills++;
        UpdateQuestUI();

        if (currentKills >= requiredKills)
        {
            CompleteQuest();
        }
    }

    private void UpdateQuestUI()
    {
        questUI.GetComponent<QuestUI>().UpdateQuestText(questName, currentKills, requiredKills);
    }

    private void CompleteQuest()
    {
        isActive = false;
        Debug.Log("Nhiệm vụ hoàn thành!");
        questUI.SetActive(false);
    }
}
