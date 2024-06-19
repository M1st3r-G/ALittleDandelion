using System;
using System.Linq;
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
        
        [Header("Content")]
        [SerializeField] private DialogueSequence[] sequences;
        [SerializeField] private ActorImages[] images;

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
            characterImage.sprite = GetActorSprite(line.actor);
            characterName.text = line.ActorName;
            lineText.text = line.line;
        }
        
        public void NextLine()
        {
            _currentLine++;
            if (_currentLine >= sequences[_currentSequence].lines.Length)
            {
                print("Finished Lines");
                gameObject.SetActive(true);
                TutorialManager.Instance.SetFlag(TutorialManager.TutorialFlag.DialogueFinished);
                return;
            }
            
            DialogueLine line = sequences[_currentSequence].lines[_currentLine];
            DisplayLine(line);
        }

        #endregion

        #region Utils

        private Sprite GetActorSprite(Actor actor)
            => (from aI in images where aI.actor == actor select aI.sprite).FirstOrDefault();
        
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
        
        [Serializable]
        private struct ActorImages
        {
            public string name;
            public Actor actor;
            public Sprite sprite;
        }

        #endregion
    }
}