using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace RPG.Combat
{
    public class EnemyHealthDisplay : MonoBehaviour
    {
        Fighter playerFighter;
        [SerializeField] TextMeshProUGUI text;

        private void Awake()
        {
            playerFighter = GameObject.FindWithTag("Player").GetComponent<Fighter>();
        }

        private void Update()
        {
            if (playerFighter.ReturnTargetHealth() == null)
            {
                text.text = string.Format("None");
                return;
            }

            text.text = string.Format("{0:0.0}% ", playerFighter.ReturnTargetHealth().GetPercentage());
        }
    }
}
