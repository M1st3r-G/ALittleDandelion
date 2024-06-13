using System;
using System.Collections.Generic;
using System.Linq;
using Clickable.Shelf;
using Controller;
using Data;
using UI;
using UnityEngine;
using Environment = Data.Environment;

namespace Managers
{
    public class FlowerInstanceLibrary : MonoBehaviour
    {
        #region Fields

        // ComponentReferences
        [SerializeField] private PotButtons potsUIDisplay;
        [SerializeField] private ReplantPot replantPot;
        [SerializeField] private Transform row1;
        [SerializeField] private Transform row2;
        [SerializeField] private GameObject potPrefab;
        
        private List<PotController> _allPots;

        // Publics
        public bool HasSpace => _allPots.Any(p => !p.gameObject.activeSelf);
        public static FlowerInstanceLibrary Instance { get; private set; }
        

        #endregion
        
        #region SetUp

        private void Awake()
        {
            Instance = this;
            _allPots = new List<PotController>();
            
            for (int i = 0; i < 12; i++)
            {
                GameObject tmpObj = Instantiate(potPrefab);
                PotController tmpPot = tmpObj.GetComponent<PotController>();
                tmpObj.SetActive(false);
                
                _allPots.Add(tmpPot);
                ReturnPot(tmpPot);
            }
        }

        private void OnDestroy()
        {
            if (Instance == this) Instance = null;
        }
        
        #endregion

        #region PotManagement

        public void AddReplantFlower(FlowerInstance flower, Environment e)
        {
            PotController pot = GetFirstEmptyPot();
            Debug.Assert(pot is not null, "Fehler Pot sollte zu voller liste hinzugefügt werden.");
            pot.Replant(flower, e);
            
            // Refresh UI
            potsUIDisplay.SetAmountActive(CalculateButtonStates());
            if (!HasSpace) replantPot.gameObject.SetActive(false);
        }

        
        // Set the Position back to the Origin
        public void ReturnPot(PotController returnedPot){
            int idx = _allPots.IndexOf(returnedPot);
            Debug.Assert(idx != -1, $"Fehler Unbekannter Topf {returnedPot.name}");
            
            // Reset Position
            if (idx >= 6)
            {
                returnedPot.transform.parent = row2;
                idx -= 6;
            }
            else returnedPot.transform.parent = row1;
            returnedPot.transform.localPosition = new Vector3(15f - idx * 6f, 2.76f, 0);
        }
        
        // Return the Requested Pot
        public PotController BorrowPot(int index)
        {
            PotController pot = _allPots[index];
            Debug.Assert(pot.gameObject.activeSelf, "Ein Leerer Topf wurde Angefragt");
            return pot;
        }

        #endregion

        #region Utils

        public bool FlowerIsSet(int idx)
            => _allPots[idx].gameObject.activeSelf;
        private PotController GetFirstEmptyPot() 
            => _allPots.FirstOrDefault(pot => !pot.gameObject.activeSelf);
        private bool[] CalculateButtonStates()
            => _allPots.Select(pot => pot.gameObject.activeSelf).ToArray();

        public void GetSaveContent(out FlowerInstance.FlowerSerialization[] flowers, out Environment[] environments, out int map)
        {
            List<FlowerInstance.FlowerSerialization> tmpFlowers = new(); 
            List<Environment> tmpEnvironments = new();
            map = 0;
            int copyMapValue = 1 << 11;
            
            foreach (Tuple<FlowerInstance, Environment> tuple in _allPots.Select(pot => pot.GetSaveContent()))
            {
                if (tuple.Item1 is not null)
                {
                    tmpFlowers.Add(tuple.Item1.Serialization);
                    tmpEnvironments.Add(tuple.Item2);
                    map += copyMapValue;
                }
                
                copyMapValue >>= 1;
            }
            
            flowers = tmpFlowers.ToArray();
            environments = tmpEnvironments.ToArray();
        }
        
        #endregion
    }
}