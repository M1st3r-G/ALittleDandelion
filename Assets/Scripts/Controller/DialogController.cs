using System;
using Managers;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Controller
{
    public class DialogController : MonoBehaviour
    {
        #region Fields

        [Header("Components")]
        [SerializeField] private Image characterImage;
        [SerializeField] private TextMeshProUGUI characterName;
        [SerializeField] private TextMeshProUGUI lineText;
        [SerializeField] private Animator anim;
        
        [Header("Content")]
        [SerializeField] private DialogueSequence[] sequences;
        
        private int _currentSequence = -1;
        private int _currentLine;

        #endregion
        
        #region LineManagement

        private void Awake()
        {
            gameObject.SetActive(false);
        }

        public void StartNextSequence()
        {
            gameObject.SetActive(true);
            _currentSequence++;
            _currentLine = -1;

            NextLine();
        }

        private void DisplayLine(DialogueLine line)
        {
            anim.Play(line.actor == Actor.Mutter ? "Mutter" : "Tochter" );
            
            characterName.text = line.ActorName;
            lineText.text = line.line;
        }
        
        public void NextLine()
        {
            _currentLine++;
            AudioManager.Instance.PlayEffect(AudioManager.AudioEffect.Click);
            
            if (_currentLine >= sequences[_currentSequence].lines.Length)
            {
                print("Finished Lines");
                anim.Play("Idle");
                gameObject.SetActive(false);
                TutorialManager.Instance.SetFlag(TutorialManager.TutorialFlag.DialogueFinished);
                return;
            }
            
            DialogueLine line = sequences[_currentSequence].lines[_currentLine];
            DisplayLine(line);
        }

        #endregion

        #region Utils

        private enum Actor
        {
            Mutter, Tochter
        }

        [Serializable]
        private struct DialogueLine
        {
            public string ActorName =>
                actor switch
                {
                    Actor.Mutter => "Anna-Lena",
                    Actor.Tochter => "Sophie",
                    _ => "Unknown"
                };

            public Actor actor;
            public string line;
        }

        [Serializable]
        private struct DialogueSequence
        {
            public string name;
            public DialogueLine[] lines;
        }
        
        #endregion
    }
}