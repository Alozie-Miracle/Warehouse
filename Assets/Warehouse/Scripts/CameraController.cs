using TMPro;
using Unity.Cinemachine;
using UnityEngine;
using StarterAssets;

public class CameraController : MonoBehaviour
{
    [Header("Cinemachine Cameras")]
    [SerializeField] private CinemachineCamera thirdPresonCamera;
    [SerializeField] private CinemachineCamera firstPersonCamera;

    [Header("Mesh Rendering")]
    [SerializeField] private SkinnedMeshRenderer playerMesh;

    [Header("UI Indicator")]
    [SerializeField] private TextMeshProUGUI viewModePromptText;

    private StarterAssetsInputs starterInputs;

    private bool isFirstPerson = false;
    private ThirdPersonController thirdPersonController;
    private FirstPersonController firstPersonController;

    void Start()
    {
        // Cache input reference if not assigned manually
        if (starterInputs == null)
        {
            starterInputs = GetComponentInParent<StarterAssetsInputs>();
        }

        if (thirdPersonController == null)
            thirdPersonController = GetComponentInParent<ThirdPersonController>();

        if (firstPersonController == null)
            firstPersonController = GetComponent<FirstPersonController>();
        

        SetFirstPersonView(isFirstPerson);
    }

    void Update()
    {
        if (UnityEngine.InputSystem.Keyboard.current.vKey.wasPressedThisFrame)
        {
            isFirstPerson = !isFirstPerson;
            SetFirstPersonView(isFirstPerson);
        }
    }


    void SetFirstPersonView(bool enableFPV)
    {
        if (enableFPV)
        {
            thirdPresonCamera.Priority = 9;
            firstPersonCamera.Priority = 10;

            if (playerMesh != null) playerMesh.enabled = false;
            // Toggle Movement Scripts
            if (thirdPersonController != null) thirdPersonController.enabled = false;
            if (firstPersonController != null) firstPersonController.enabled = true;

            if (viewModePromptText != null) viewModePromptText.text = "Press [V] for Thrid Person View";
        } else
        {
            thirdPresonCamera.Priority = 10;
            firstPersonCamera.Priority = 9;
            // Toggle Movement Scripts
            if (thirdPersonController != null) thirdPersonController.enabled = true;
            if (firstPersonController != null) firstPersonController.enabled = false;

            if (playerMesh != null) playerMesh.enabled = true;
            
            if (viewModePromptText != null)
                viewModePromptText.text = "Press [V] for First Person View";
        }
    }
}
