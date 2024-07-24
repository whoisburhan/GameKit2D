using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BGChanger : MonoBehaviour
{
    [SerializeField] private Image backgroundImg;

    private void Start()
    {
        backgroundImg.sprite = BGContainer.Instance.GetBGRandom();
    }
}
