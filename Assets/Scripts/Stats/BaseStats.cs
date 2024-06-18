using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.Antlr3.Runtime.Misc;
using UnityEngine;

namespace RPG.Stats
{
    public class BaseStats : MonoBehaviour
    {
        [Range(1, 99)]
        [SerializeField] int currentLevel = 1;
        [SerializeField] CharacterClass characterClass;
        [SerializeField] Progression progression = null;

        public float GetStat(Stat stat)
        {
            return progression.GetStat(stat, characterClass, GetLevel());
        }

        public int GetLevel()
        {
            Experience Experience = GetComponent<Experience>();

            //Enemies have NULL experience
            if (Experience == null)
                return currentLevel;

            float currentEXP = Experience.GetCurrentExp();
            int maxLevel = progression.GetLevels(Stat.ExperienceNeededToLevelUp, characterClass);
            
            for (int i = 1; currentEXP >= progression.GetStat(Stat.ExperienceNeededToLevelUp, characterClass, i) && !(i>maxLevel); i++) 
            {
                currentLevel = i;
            }

            return currentLevel;
        }
    }
}
