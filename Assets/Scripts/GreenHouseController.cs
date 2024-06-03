using UnityEngine;
using UnityEngine.InputSystem;

public class GreenHouseController : MonoBehaviour
{
    // Component References
    [SerializeField] private InputActionReference showFlowerAction;
    [SerializeField] private InputActionReference escapeAction;
    
    [SerializeField] private Camera flowerCam;
    [SerializeField] private Camera overview;
    
    public static GreenHouseController Instance { get; private set; }
    
    private void Awake()
    {
        Instance = this;
        flowerCam.gameObject.SetActive(false);
        overview.gameObject.SetActive(true);
    }

    private void OnEnable()
    {
        showFlowerAction.action.Enable();
        showFlowerAction.action.performed += ShowFlowerWrapper;
        
        escapeAction.action.Enable();
        escapeAction.action.performed += Escape;

        showFlowerAction.action.ReadValue<Vector2>();
    }

    
    private void OnDisable()
    {
        showFlowerAction.action.performed -= ShowFlowerWrapper;
        showFlowerAction.action.Disable();
        
        escapeAction.action.Disable();
        escapeAction.action.performed -= Escape;
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

    public void ShowFlower(int index)
    {
        Debug.Log($"Show Flower with index: {index}");
    }
}
