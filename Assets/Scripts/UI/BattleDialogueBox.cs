using System;
using System.Collections;
using TMPro;
using UnityEngine;

namespace UI
{
    public class BattleDialogueBox : MonoBehaviour
    {
        public bool isPrinting;
        public string targetText;
        
        private TextMeshProUGUI _textBox;

        private const float TimeBetweenCharacters = 8; // in ms
        
        void Start()
        {
            _textBox = transform.Find("PromptText").GetComponent<TextMeshProUGUI>();
        }

        public void InstantChangeBoxText()
        // Use stored string to print immediately what needs to be printed
        {
            _textBox.text = targetText;
        }

        public IEnumerator ProgressiveChangeBoxText(string text)
        {
            targetText = text;
            gameObject.SetActive(true);
            
            isPrinting = true;
            var printed = "";
            for (int i = 0; i < text.Length; i++)
            {
                printed = text[new Range(0, i+1)] + "<color=\"white\">"
                                                + text[new Range(i+1, text.Length)];
                _textBox.text = printed;
                yield return new WaitForSeconds(TimeBetweenCharacters / 1000);
            }

            isPrinting = false;
        }

        public void Close()
        {
            targetText = "";
            gameObject.SetActive(false);
        }
    }
}
