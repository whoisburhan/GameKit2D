using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BGContainer : MonoBehaviour
{
    public static BGContainer Instance { get; private set; }

    [Space]
    [SerializeField] private List<Sprite> bgs;
    int lastBgsIndex = 1;
    [HideInInspector] public bool SameLevelLoad = false;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public Sprite GetBGRandom()
    {
        if (SameLevelLoad) return bgs[lastBgsIndex];

        int index = Random.Range(0, bgs.Count);

        while (index == lastBgsIndex) index = Random.Range(0, bgs.Count);

        lastBgsIndex = index;

        return bgs[index];
    }
}
