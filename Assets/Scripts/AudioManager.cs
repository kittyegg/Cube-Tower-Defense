using UnityEngine;

public class AudioManager : MonoBehaviour
{
    [SerializeField] private AudioSource[] _grassBlockDestroySound;
    private int _grassSoundIndex = 0;

    public void PlayBlockDestroySound(BlockScript block)
    {
        switch (block.Type)
        {
            case BlockScript.BlockType.Grass:
            case BlockScript.BlockType.PathFinish:
                if (_grassBlockDestroySound == null)
                    return;

                _grassBlockDestroySound[_grassSoundIndex].Play();
                _grassSoundIndex = (_grassSoundIndex + 1) % _grassBlockDestroySound.Length;
                break;
        }
    }
}
