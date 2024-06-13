
using Palmmedia.ReportGenerator.Core.Parser.Analysis;
using System;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;
namespace RPG.Stats
{
    [CreateAssetMenu(fileName = "Progression", menuName = "Stats/New Progression", order = 0)]
    public class Progression : ScriptableObject
    {
        [SerializeField] ProgressionCharacterClass[] characterClasses = null;

        Dictionary<CharacterClass, Dictionary<Stat, float[]>> lookupTable = null;
        public float GetStat(Stat stat, CharacterClass characterClass, int level)
        {

            BuildLookUp();

            float[] levels = lookupTable[characterClass][stat];

            if (levels.Length >= level)
            return levels[level];

            /*
              LEGACY CODE

              foreach (ProgressionCharacterClass ProgressionClass in characterClasses)
            {

                if (ProgressionClass.characterClass != characterClass) continue;
                
                foreach(ProgressionStat progressionStat in ProgressionClass.stats)
                {
                    if (progressionStat.stat != stat) continue;
                    
                    if (progressionStat.levels.Length < level) continue;
                    
                    return progressionStat.levels[level];
                }

            }*/

            Debug.LogError(this.GameObject().name + " did not set Health level correctly");
            return 0;
        }

        private void BuildLookUp()
        {
            if (lookupTable != null) return;

            lookupTable = new Dictionary<CharacterClass, Dictionary<Stat, float[]>>();

            foreach (ProgressionCharacterClass ProgressionClass in characterClasses)
            {
                var statLookupTable = new Dictionary<Stat, float[]>();

                foreach (ProgressionStat progressionStat in ProgressionClass.stats)
                {
                    statLookupTable[progressionStat.stat] = progressionStat.levels;

                }


                lookupTable[ProgressionClass.characterClass] = statLookupTable;
            }

        }

        [System.Serializable]
        class ProgressionCharacterClass
        {
            public CharacterClass characterClass;
            public ProgressionStat[] stats;
            //public float[] HealthProgression;

        }

        [System.Serializable]
        class ProgressionStat
        {
            public Stat stat;
            public float[] levels;
        }
    }
}