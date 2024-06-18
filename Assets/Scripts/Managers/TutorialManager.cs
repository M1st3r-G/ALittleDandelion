using System.Collections;
using Controller;
using UnityEngine;

namespace Managers
{
    public class TutorialManager : MonoBehaviour
    {
        [SerializeField] private DialogController dialogueSystem;
        
        private TutorialFlag _lastFlag;
        private bool _isInTutorial;
        
        public static TutorialManager Instance { get; private set; }
        
        public enum TutorialFlag
        {
            None,
            DialogueFinished,
            EnterGreenhouse,
            FocusedSeed,
            PlantedSeed,
            NextDay,
            Watered,
            Replant,
            AddedFertilizer,
            FlowerBlooms,
            DecidedForPlant
        }

        private void Awake()
        {
            Instance = this;
        }

        public void StartTutorial()
        {
            if (_isInTutorial) return;
            
            _isInTutorial = true;
            
            //Limit Seeds, Soil and Fertilizer
            
            StartCoroutine(TutorialRoutine());
        }

        public void SetFlag(TutorialFlag type)
        {
            if (!_isInTutorial) return;

            _lastFlag = type;
        }

        private IEnumerator TutorialRoutine()
        {
            yield return new WaitUntil(() => _lastFlag == TutorialFlag.DialogueFinished);
            yield return new WaitUntil(() => _lastFlag == TutorialFlag.EnterGreenhouse);
            dialogueSystem.StartNextSequence();
            yield return new WaitUntil(() => _lastFlag == TutorialFlag.DialogueFinished);
            yield return new WaitUntil(() => _lastFlag == TutorialFlag.FocusedSeed);
            dialogueSystem.StartNextSequence();
            yield return new WaitUntil(() => _lastFlag == TutorialFlag.DialogueFinished);
            yield return new WaitUntil(() => _lastFlag == TutorialFlag.PlantedSeed);
            dialogueSystem.StartNextSequence();
            yield return new WaitUntil(() => _lastFlag == TutorialFlag.DialogueFinished);
            yield return new WaitUntil(() => _lastFlag == TutorialFlag.NextDay); 
            dialogueSystem.StartNextSequence();
            yield return new WaitUntil(() => _lastFlag == TutorialFlag.DialogueFinished);
            yield return new WaitUntil(() => _lastFlag == TutorialFlag.EnterGreenhouse); 
            dialogueSystem.StartNextSequence();
            yield return new WaitUntil(() => _lastFlag == TutorialFlag.DialogueFinished);
            yield return new WaitUntil(() => _lastFlag == TutorialFlag.Watered);
            dialogueSystem.StartNextSequence();
            yield return new WaitUntil(() => _lastFlag == TutorialFlag.DialogueFinished);
            yield return new WaitUntil(() => _lastFlag == TutorialFlag.NextDay);
            dialogueSystem.StartNextSequence();
            yield return new WaitUntil(() => _lastFlag == TutorialFlag.DialogueFinished);
            yield return new WaitUntil(() => _lastFlag == TutorialFlag.EnterGreenhouse);
            dialogueSystem.StartNextSequence();
            yield return new WaitUntil(() => _lastFlag == TutorialFlag.DialogueFinished);
            yield return new WaitUntil(() => _lastFlag == TutorialFlag.Replant);
            dialogueSystem.StartNextSequence();
            yield return new WaitUntil(() => _lastFlag == TutorialFlag.DialogueFinished);
            yield return new WaitUntil(() => _lastFlag == TutorialFlag.AddedFertilizer);
            dialogueSystem.StartNextSequence();
            yield return new WaitUntil(() => _lastFlag == TutorialFlag.DialogueFinished);
            yield return new WaitUntil(() => _lastFlag == TutorialFlag.FlowerBlooms);
            dialogueSystem.StartNextSequence();
            yield return new WaitUntil(() => _lastFlag == TutorialFlag.DialogueFinished);
            yield return new WaitUntil(() => _lastFlag == TutorialFlag.EnterGreenhouse);
            dialogueSystem.StartNextSequence();
            yield return new WaitUntil(() => _lastFlag == TutorialFlag.DialogueFinished);
            yield return new WaitUntil(() => _lastFlag == TutorialFlag.DecidedForPlant);
            dialogueSystem.StartNextSequence();
            
            //UnlockAll
            //Destroy()
        }
    }
}