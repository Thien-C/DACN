using UnityEngine;
using TMPro;

public class QuestUI : MonoBehaviour
{
    public TextMeshProUGUI questText;

    public void UpdateQuestText(string questName, int currentKills, int requiredKills)
    {
        questText.text = $"{questName}: {currentKills}/{requiredKills} quái đã tiêu diệt";
    }
}
