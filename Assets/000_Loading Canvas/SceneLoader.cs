using System.Collections;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SceneLoader : MonoBehaviour
{
    public static SceneLoader Instance;

    [SerializeField] private GameObject m_LoadingCanvas;
    [SerializeField] private Image m_ProgressBar;
    [SerializeField] private Text m_ProgressBarText;
    [SerializeField] private Button startButton;

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

    private void Start()
    {
        //startButton.onClick.AddListener(() => { LoadScene("Game"); });
       // startButton.onClick.AddListener(() => { LoadScene(SceneName.GameScene); });
        LoadScene("GD");
    }


    public void LoadScene(string sceneName)
    {
        StartCoroutine(LoadSceneCoroutine(sceneName));
    }


    private IEnumerator LoadSceneCoroutine(string sceneName)
    {
        AsyncOperation operation = SceneManager.LoadSceneAsync(sceneName);
        operation.allowSceneActivation = false;

        m_LoadingCanvas.SetActive(true);

        float progress = 0f;
        float minLoadTime = 1f; // Minimum time you want the loading to take
        float timeElapsed = 0f;

        // Use this to keep track of whether the actual loading is complete
        bool loadComplete = false;

        while (!operation.isDone || timeElapsed < minLoadTime)
        {
            timeElapsed += Time.deltaTime;
            progress = Mathf.Clamp01(operation.progress / 0.9f);

            // Update the slider value based on the actual progress or the time elapsed, whichever is smaller
            m_ProgressBar.fillAmount = Mathf.Min(progress, timeElapsed / minLoadTime);
            m_ProgressBarText.text = (m_ProgressBar.fillAmount * 100f).ToString("F0") + "%";

            // Check if the actual load is complete
            if (operation.progress >= 0.9f)
            {
                loadComplete = true;
            }

            // If the minimum load time has passed and the actual load is complete, activate the scene
            if (timeElapsed >= minLoadTime && loadComplete)
            {
                operation.allowSceneActivation = true;
                yield return new WaitForSeconds(0.1f);
                m_LoadingCanvas.SetActive(false);
            }

            yield return null;
        }

    }
    public void HideLoadingTransition()
    {
        m_LoadingCanvas.SetActive(false);
        startButton.gameObject.SetActive(true);
    }
}

public enum SceneName
{
    StartScene, GameScene
}
