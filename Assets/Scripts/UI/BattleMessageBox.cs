using TMPro;
using UnityEngine;

namespace UI
{
    public class BattleMessageBox : MonoBehaviour
    {
        private TextMeshProUGUI _textBox;
        
        void Start()
        {
            _textBox = transform.Find("PromptText").GetComponent<TextMeshProUGUI>();
        }

        // Update is called once per frame
        void Update()
        {
        
        }

        public void ChangeBoxText(string text)
        {
            _textBox.text = text;
        }
    }
}
