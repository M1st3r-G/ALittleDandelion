using System;
using UnityEngine;

namespace Controller
{
    public class DialogController : MonoBehaviour
    {
        [SerializeField] private DialogueSequence[] sequences;
        [SerializeField] private ActorImages[] images;
        private int _currentSequence;
        private int _currentLine;
        
        
        public void StartNextSequence()
        {
            _currentSequence++;
            _currentLine = -1;

            NextLine();
        }

        private void DisplayLine(DialogueLine line)
        {
            Debug.LogWarning($"{line.ActorName}: {line.line}");
        }
        
        public void NextLine()
        {
            _currentLine++;
            if (_currentLine >= sequences[_currentSequence].lines.Length) return;
            
            DialogueLine line = sequences[_currentSequence].lines[_currentLine];
            DisplayLine(line);
        }
        
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
    }
}