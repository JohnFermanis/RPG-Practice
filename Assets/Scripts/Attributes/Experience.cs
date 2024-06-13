using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPG.Attributes
{
    public class Experience : MonoBehaviour
    {
        [SerializeField] float experiencePoints = 0.0f;

        public void GainExperience(float experience)
        {
            experiencePoints += experience;
        }

        public float GetCurrentExp()
        {
            return experiencePoints;
        }
    }
}