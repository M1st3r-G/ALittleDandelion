using UnityEngine;
using UnityEngine.InputSystem;

public class GreenHouseController : MonoBehaviour
{
    [SerializeField] private InputActionReference showFlowerAction;
    [SerializeField] private InputActionReference escapeAction;
    [SerializeField] private TableController[] tables;
    [SerializeField] private Camera flowerCam;
    [SerializeField] private Camera overview;

    
    private void Awake()
    {
        flowerCam.gameObject.SetActive(false);
        overview.gameObject.SetActive(true);
    }

    private void OnEnable()
    {
        showFlowerAction.action.Enable();
        showFlowerAction.action.performed += ShowFlowerWrapper;
        escapeAction.action.Enable();
        escapeAction.action.performed += Escape;
        
    }

    private void OnDisable()
    {
        showFlowerAction.action.performed -= ShowFlowerWrapper;
        showFlowerAction.action.Disable();
    }

    private void Escape(InputAction.CallbackContext obj)
    {
        if (flowerCam.gameObject.activeSelf)
        {
            flowerCam.gameObject.SetActive(false);
            overview.gameObject.SetActive(true);
        }
        else
        {
            Debug.Log("Back to Menu");
        }
    }
    
    private void ShowFlowerWrapper(InputAction.CallbackContext ctx)
    {
        ShowFlower((int)ctx.ReadValue<float>() - 1);
    }

    private void ShowFlower(int index)
    {
        flowerCam.transform.position = tables[index].CamPosition;
        flowerCam.gameObject.SetActive(true);
        overview.gameObject.SetActive(false);
    }
}
