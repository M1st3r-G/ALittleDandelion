using UnityEngine;

public class ButtonMethods : MonoBehaviour
{
    public void TriggerNextDay()
    {
        Debug.Log("Next Day");
    }

    public void Settings()
    {
        Debug.Log("Settings");
    }

    public void Seeds()
    {
        Debug.Log("Seeds");
    }

    public void Book() => Debug.Log("Open Book");
    
    public void OnPotClicked(int index)
    {
        GreenHouseController.Instance.ShowFlower(index);
    }
}
