using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

public class BlockDetector : MonoBehaviour
{
    [SerializeField] private InputAction _clickAction;
    [SerializeField] private UnityEvent<BlockScript> _onBlockClicked;
    [SerializeField] private UnityEvent<BlockScript> _onBlockDetected;

    public event UnityAction<BlockScript> OnBlockClicked
    {
        add => _onBlockClicked.AddListener(value);
        remove => _onBlockClicked.RemoveListener(value);
    }

    public event UnityAction<BlockScript> OnBlockDetected
    {
        add => _onBlockDetected.AddListener(value);
        remove => _onBlockDetected.RemoveListener(value);
    }
    
    private void OnEnable()
    {
        _clickAction.Enable();
        _clickAction.performed += Click_performed;
    }

    private void OnDisable()
    {
        _clickAction.performed -= Click_performed;
        _clickAction.Disable();
    }

    private void Click_performed(InputAction.CallbackContext context)
    {
        var clickPosition = Vector2.zero;
        if (Touchscreen.current != null)
            clickPosition = Touchscreen.current.position.ReadValue();
        if (clickPosition == Vector2.zero)
            clickPosition = Mouse.current.position.ReadValue();

        var block = DetectBlockOnPosition(clickPosition);
        if (block != null)
            _onBlockClicked?.Invoke(block);
    }

    private BlockScript DetectBlockOnPosition(Vector2 position)
    {
        var ray = Camera.main.ScreenPointToRay(position);
        if (Physics.Raycast(ray, out var hit) && hit.transform.CompareTag("GrassBlock"))
            return hit.transform.GetComponent<BlockScript>();

        return null;
    }

    private void Update()
    {
        var block = DetectBlockOnPosition(Mouse.current.position.ReadValue());
        if (block != null)
            _onBlockDetected?.Invoke(block);
    }
}
