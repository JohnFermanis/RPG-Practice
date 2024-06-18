using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace RPG.Stats
{
    public class ExpDisplay : MonoBehaviour
    {
        Experience exp;
        [SerializeField] TextMeshProUGUI text;

        private void Awake()
        {
            exp= GameObject.FindWithTag("Player").GetComponent<Experience>();
        }

        private void Update()
        {
            text.text = string.Format("{0:0.0}", exp.GetCurrentExp());
        }
    }
}
