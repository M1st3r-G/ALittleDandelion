using System.Collections;
using Clickable.Shelf;
using Controller;
using UnityEngine;

namespace Managers
{
    public class TutorialManager : MonoBehaviour
    {
        [SerializeField] private DialogController dialogueSystem;

        [Header("ContentToLimit")] 
        [SerializeField] private ShelfSeedsItem[] seeds;
        [SerializeField] private ShelfSoilItem[] soils;
        [SerializeField] private ShelfFertilizerItem[] fertilizers;
        
        
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
            DecidedForPlant,
            BookOpened
        }

        private void Awake()
        {
            Instance = this;
        }

        public void StartTutorial()
        {
            if (_isInTutorial) return;
            
            _isInTutorial = true;
            UnlockContent(false);
            StartCoroutine(TutorialRoutine());
        }

        private void UnlockContent(bool state)
        {
            foreach (ShelfSeedsItem seed in seeds)
                seed.gameObject.SetActive(state);

            foreach (ShelfSoilItem soil in soils)
                soil.gameObject.SetActive(state);

            foreach (ShelfFertilizerItem fertilizer in fertilizers)
                fertilizer.gameObject.SetActive(state);
        }
        
        public void SetFlag(TutorialFlag type)
        {
            if (_isInTutorial) _lastFlag = type;
        }

        private IEnumerator TutorialRoutine()
        {
            dialogueSystem.StartNextSequence();
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
            CameraManager.Instance.ToHub();
            
            dialogueSystem.StartNextSequence();
            yield return new WaitUntil(() => _lastFlag == TutorialFlag.DialogueFinished);
            yield return new WaitUntil(() => _lastFlag == TutorialFlag.EnterGreenhouse); 
            dialogueSystem.StartNextSequence();
            yield return new WaitUntil(() => _lastFlag == TutorialFlag.DialogueFinished);
            yield return new WaitUntil(() => _lastFlag == TutorialFlag.Watered);
            dialogueSystem.StartNextSequence();
            yield return new WaitUntil(() => _lastFlag == TutorialFlag.DialogueFinished);
            yield return new WaitUntil(() => _lastFlag == TutorialFlag.NextDay);
            CameraManager.Instance.ToHub();
            
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
            CameraManager.Instance.ToHub();
            
            dialogueSystem.StartNextSequence();
            yield return new WaitUntil(() => _lastFlag == TutorialFlag.DialogueFinished);
            yield return new WaitUntil(() => _lastFlag == TutorialFlag.EnterGreenhouse);
            dialogueSystem.StartNextSequence();
            yield return new WaitUntil(() => _lastFlag == TutorialFlag.DialogueFinished);
            yield return new WaitUntil(() => _lastFlag == TutorialFlag.DecidedForPlant);
            dialogueSystem.StartNextSequence();
            yield return new WaitUntil(() => _lastFlag == TutorialFlag.DialogueFinished);
            yield return new WaitUntil(() => _lastFlag == TutorialFlag.BookOpened);
            dialogueSystem.StartNextSequence();            
            yield return new WaitUntil(() => _lastFlag == TutorialFlag.DialogueFinished);
            
            UnlockContent(true);
            dialogueSystem.StartNextSequence();
            _lastFlag = TutorialFlag.None;
            
            yield return new WaitUntil(() => _lastFlag == TutorialFlag.DialogueFinished);
            _isInTutorial = false;
        }
    }
}
