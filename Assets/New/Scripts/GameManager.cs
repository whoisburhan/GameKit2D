using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.Threading.Tasks;

namespace FantasyRealm.AA
{
	public class GameManager : MonoBehaviour
	{
		public static GameManager Instance;

		private bool gameHasEnded = false;

		public Rotator rotator;
		public Spawner spawner;

		public Animator animator;

		[Space]
		[Header("Aduio")]
		[SerializeField] private AudioSource audioSource;
		[Space]
		[SerializeField] private AudioClip victoryClip;
		[SerializeField] private AudioClip gameOverClip;
		[SerializeField] private AudioClip pinFireClip;
		[SerializeField] private AudioClip clickClip;
		[Header("Particles")]
		[SerializeField] private GameObject victoryParticle;

        private const string FILE_KEY = "CURRENT_LEVEL";
		private const string FILE_KEY_LAST_RANDOM_LEVEL = "LAST_RANDOM_LEVEL";

		private int pinLeftToCompleteLevel;

		public int PinLeftToCompleteLevel 
		{
			get { return pinLeftToCompleteLevel; }
			set 
			{
				pinLeftToCompleteLevel = value;
				//UIManager.Instance.UpdatePinLeftCounterInUI(value);
				if(pinLeftToCompleteLevel == 0) 
				{
					LevelCompleted();
				}

            }
		}
        public int CurrentLevel
        {
            get
            {
                return PlayerPrefs.GetInt(FILE_KEY, 1);
            }
            set
            {
                PlayerPrefs.SetInt(FILE_KEY, value);
            }
        }


        public int LastRandomLevel
        {
            get
            {
                return PlayerPrefs.GetInt(FILE_KEY_LAST_RANDOM_LEVEL, -1);
            }
            set
            {
                PlayerPrefs.SetInt(FILE_KEY_LAST_RANDOM_LEVEL, value);
            }
        }
        private void Awake()
        {
            if(Instance == null) 
			{
				Instance = this;
			}
			else 
			{
				Destroy(gameObject);
			}
        }

        private void Start()
        {
			// Update Level In UI
			LevelGenerator.Instacne.RequestForLoadLevel(CurrentLevel);
        }

        public void EndGame()
		{
			if (gameHasEnded)
				return;

			AudioManager.Instance.PlayAudio(AudioName.Losing);

			rotator.enabled = false;
			spawner.enabled = false;

			animator.SetTrigger("EndGame");

			gameHasEnded = true;
		}


		public void LevelCompleted() 
		{
           // await Task.Delay(200);
            if (gameHasEnded)
                return;
            CurrentLevel++;
			LastRandomLevel = -1;
			UIManager.Instance.ActivateVictoryPanel();

			AudioManager.Instance.PlayAudio(AudioName.Winnig);
			victoryParticle.SetActive(true);
		}

		public void TriggerPinFireSound() 
		{
            AudioManager.Instance.PlayAudio(AudioName.Bubble);
        }

		public void ClickSound() 
		{
            AudioManager.Instance.PlayAudio(AudioName.ButtonClick);
        }

		public void RestartLevel()
		{
			UIManager.Instance.ActivateGameOverPanel();
			//SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
		}

	}
}