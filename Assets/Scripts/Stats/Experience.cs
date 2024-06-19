using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPG.Stats
{
    public class Experience : MonoBehaviour
    {
        [SerializeField] float experiencePoints = 0.0f;

        //public delegate void ExperienceGainDelegate(); = Action
        public event Action onExperienceGained;

        public void GainExperience(float experience)
        {
            experiencePoints += experience;
            Debug.Log("Called 1");
            onExperienceGained();
        }

        public float GetCurrentExp()
        {
            return experiencePoints;
        }
    }
}