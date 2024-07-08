using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.Antlr3.Runtime.Misc;
using UnityEngine;

namespace RPG.Stats
{
    public class BaseStats : MonoBehaviour
    {
        [Range(1, 99)]
        int startingLevel;
        private Experience Experience = null;
        [SerializeField] CharacterClass characterClass;
        [SerializeField] Progression progression = null;
        [SerializeField] GameObject levelUpParticleEffect = null;

        int currentLevel = 0;
        private void Start()
        {
            Experience = GetComponent<Experience>();

            //Enemies have NULL experience
            if (Experience != null)
            {
                Experience.onExperienceGained += UpdateLevel;
            }
        }
        private void UpdateLevel()
        {
            int newLevel = CalculateLevel();
            if (newLevel > currentLevel)
            {
                currentLevel = newLevel;
                LevelupEffect();
                print("You leveled Up!");
            }
        }

        private void LevelupEffect()
        {
            Instantiate(levelUpParticleEffect, transform);
        }

        public float GetStat(Stat stat)
        {
            return progression.GetStat(stat, characterClass, GetLevel());
        }

        public int GetLevel()
        {
            if (currentLevel > 1)
            {
                currentLevel = CalculateLevel();
            }
            return currentLevel;
        }

        public int CalculateLevel()
        {
            if (Experience == null)
            {
                return startingLevel;
            }

            float currentEXP = Experience.GetCurrentExp();
            int maxLevel = progression.GetLevels(Stat.ExperienceNeededToLevelUp, characterClass);
            int currentlvl=1;

            for (int i = 1; currentEXP >= progression.GetStat(Stat.ExperienceNeededToLevelUp, characterClass, i) && !(i > maxLevel); i++)
            {
                currentlvl = i;
            }

            return currentlvl;

        }
    }
}
