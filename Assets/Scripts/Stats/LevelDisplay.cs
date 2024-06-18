using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace RPG.Stats
{
    public class LevelDisplay : MonoBehaviour
    {
        BaseStats basestats;
        [SerializeField] TextMeshProUGUI text;

        private void Awake()
        {
            basestats= GameObject.FindWithTag("Player").GetComponent<BaseStats>();
        }

        private void Update()
        {
            text.text = string.Format("{0:0}", basestats.GetLevel());
        }
    }
}
