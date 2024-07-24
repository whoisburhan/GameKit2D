using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class GameOverPanelAnimation : MonoBehaviour
{
    [SerializeField] private Button m_GameOverRestartBtn;
    [SerializeField] private Button m_GameOverHomeButton;

    [SerializeField] private Transform PinkNintendoSwitch;

    public float shakeDuration = 0.5f;
    public float shakeStrength = 10f;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private void OnEnable()
    {
        AudioManager.Instance.PlayAudio(AudioName.Winnig);
        PlayEndScreenAnimation();
        ShakeObject();
    }

    private void PlayEndScreenAnimation()
    {
        Debug.Log("SSSSSSS");
        Sequence scalingSequence = DOTween.Sequence();
        scalingSequence.SetLoops(-1);
        scalingSequence.SetEase(Ease.InOutFlash);
        scalingSequence.Append(m_GameOverRestartBtn.transform.DOScale(Vector3.one * 1.1f, 0.8f));
        scalingSequence.Append(m_GameOverRestartBtn.transform.DOScale(Vector3.one, 0.8f));
        scalingSequence.Append(m_GameOverHomeButton.transform.DOScale(Vector3.one * 1.1f, 0.8f));
        scalingSequence.Append(m_GameOverHomeButton.transform.DOScale(Vector3.one, 0.8f));
    }

    private void ShakeObject()
    {
        // Rotate the object back and forth
        PinkNintendoSwitch.transform.DORotate(new Vector3(0, 0, shakeStrength), shakeDuration)
            .SetEase(Ease.InOutQuad)
            .SetLoops(4, LoopType.Yoyo);
    }
}
