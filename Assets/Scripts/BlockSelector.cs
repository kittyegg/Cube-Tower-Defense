using UnityEngine;

public class BlockSelector : MonoBehaviour
{
    [SerializeField] private BlockDetector _blockDetector;
    [SerializeField] private Material _selectedMaterial;

    private Renderer _selectedBlockRenderer;
    private Material _oldMaterial;

    private void OnEnable()
    {
        _blockDetector.OnBlockDetected += OnBlockDetected;
    }

    private void OnDisable()
    {
        _blockDetector.OnBlockDetected -= OnBlockDetected;

        if (_selectedBlockRenderer != null)
        {
            _selectedBlockRenderer.material = _oldMaterial;
            _selectedBlockRenderer = null;
        }
    }

    private void OnBlockDetected(BlockScript block)
    {
       if (_selectedBlockRenderer != null)
            _selectedBlockRenderer.material = _oldMaterial;

       _selectedBlockRenderer = block.GetComponent<Renderer>();
        _oldMaterial = _selectedBlockRenderer.material;
        _selectedBlockRenderer.material = _selectedMaterial;
    }
}
