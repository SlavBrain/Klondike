using System.Collections;
using Agava.YandexGames;
using IJunior.TypedScenes;
using UnityEngine;
using UnityEngine.UI;

public class AsyncLoadScene : MonoBehaviour
    {
        [Header("Objects")]
        [SerializeField] private Saver _saver;

        public static AsyncLoadScene Instance;
        
        private const float MaxAsyncOperationValue = 0.9f;
        private const float FakeAddNotInitProgress = 0.005f;
        private const float FakeAddInitProgress = 0.05f;
        private const float WaitForSeconds = 0.1f;

        private Panel _panel;
        private Slider _progressBar;
        private Coroutine _asyncLoadWork;
        private float _progressValue;

        public void Awake()
        {
            if (Instance == null)
            {
                YandexGamesSdk.CallbackLogging = true;

                transform.parent = null;
                DontDestroyOnLoad(gameObject);
                Instance = this;
                _panel = GetComponentInChildren<Panel>();
                _progressBar = GetComponentInChildren<Slider>();
                _progressBar.value = _progressBar.minValue;
            }
            else
            {
                Destroy(gameObject);
            }
        }
        
        private IEnumerator Start() 
        {
            yield return YandexGamesSdk.Initialize(Init);
        }
        
        private void Init()
        {
            _saver.Initialize();
            _asyncLoadWork = StartCoroutine(LoadMainScene());
        }
        
        private IEnumerator LoadMainScene()
        {
            WaitForSecondsRealtime waitForSecondsRealtime = new WaitForSecondsRealtime(WaitForSeconds);
            _panel.Enable();

            while (_progressValue < _progressBar.maxValue && Saver.IsLoaded == false)
            {
                Debug.Log("saver isLoaded "+Saver.IsLoaded);
                if (Saver.IsLoaded == false)
                    _progressValue += Random.Range(0, FakeAddNotInitProgress);
                else
                    _progressValue = _progressBar.maxValue;
                    //_progressValue += Random.Range(0,FakeAddInitProgress);;

                _progressBar.value = _progressValue;

                yield return waitForSecondsRealtime;
            }
            SetDefaultValues();

            IJunior.TypedScenes.MainMenu.Load();
        }

        private void SetDefaultValues()
        {
            _progressValue = 0;
            _progressBar.value = 0;
            _asyncLoadWork = null;
            _panel.Disable();
        }
    }