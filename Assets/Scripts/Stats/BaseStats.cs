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
        [SerializeField] bool IgnoreModifiers = true;

        int currentLevel = 0;

        public event Action onLevelUp;
        private void Start()
        {
            Experience = GetComponent<Experience>();

            onLevelUp += LevelupEffect;

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
                onLevelUp();
                print("You leveled Up!");
            }
        }

        private void LevelupEffect()
        {
            Instantiate(levelUpParticleEffect, transform);
        }

        public float GetStat(Stat stat)
        {
            //This is true only for enemies
            if (IgnoreModifiers)
                return progression.GetStat(stat, characterClass, GetLevel());

            return (progression.GetStat(stat, characterClass, GetLevel()) + GetStatAdder(stat)) * (1.0f + GetStatMultiplier(stat)/100.0f);
        }




        public int GetLevel()
        {
            if (currentLevel > 1)
            {
                currentLevel = CalculateLevel();
            }
            return currentLevel;
        }

        private int CalculateLevel()
        {
            if (Experience == null)
            {
                return startingLevel;
            }

            float currentEXP = Experience.GetCurrentExp();
            int maxLevel = progression.GetLevels(Stat.ExperienceNeededToLevelUp, characterClass);
            int currentlvl = 1;

            for (int i = 1; currentEXP >= progression.GetStat(Stat.ExperienceNeededToLevelUp, characterClass, i) && !(i > maxLevel); i++)
            {
                currentlvl = i;
            }

            return currentlvl;

        }

        private float GetStatAdder(Stat stat)
        {

            float total = 0.0f;
            foreach (IStatModifier provider in GetComponents<IStatModifier>())
            {
                foreach (float modifier in provider.GetStatAdders(stat))
                {
                    total += modifier;
                }
            }
            return total;
        }

        private float GetStatMultiplier(Stat stat)
        {
            float total = 0.0f;
            foreach (IStatModifier provider in GetComponents<IStatModifier>())
            {
                foreach (float modifier in provider.GetStatMultipliers(stat))
                {
                    total += modifier;
                }
            }
            return total;
        }
    }
}
