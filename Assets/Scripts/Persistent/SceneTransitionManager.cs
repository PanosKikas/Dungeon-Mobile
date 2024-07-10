using System;
using System.Collections;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using DMT.Battle;
using DMT.Characters;
using UnityEngine;
using UnityEngine.SceneManagement;
using UniRx;

namespace DMT.Persistent
{
    public class SceneTransitionManager : MonoBehaviour
    {
        public static SceneTransitionManager Instance { get; private set; }

        private bool isTransitioning;

        private void Awake()
        {
            if (Instance != null && Instance != this)
            {
                Destroy(this);
            }
            else
            {
                Instance = this;
            }

            DontDestroyOnLoad(this);
        }
        
        [SerializeField] private BattleTransitionAnimation battleSceneTransition;
        [SerializeField] private bool loadSceneOnStart = true;
        private IDisposable transitionCallback;

        private IEnumerable<Character> playerCharacters;
        private IEnumerable<Character> enemyCharacters;


        private GameObject explorationRootObject;

        private async void Start()
        {
            if (!loadSceneOnStart)
            {
                return;
            }

            await SceneManager.LoadSceneAsync(SceneManager.GetActiveScene().buildIndex + 1, LoadSceneMode.Additive);
            explorationRootObject = GameObject.FindGameObjectWithTag("Root");
        }

        public void TransitionToBattleScene(IEnumerable<Character> playerParty, IEnumerable<Character> enemyGroup)
        {
            if (isTransitioning)
            {
                return;
            }

            isTransitioning = true;
            playerCharacters = playerParty;
            enemyCharacters = enemyGroup;
            transitionCallback?.Dispose();
            transitionCallback = battleSceneTransition.OnFinished.Subscribe(_ => TransitionAnimationFinished());
            battleSceneTransition.Show();
            Time.timeScale = 0f;
        }

        public async UniTask TransitionBackFromBattle()
        {
            if (isTransitioning)
            {
                return;
            }

            isTransitioning = true;
            Debug.Log("Battle won");
            await UniTask.WaitForSeconds(1f);
            await SceneManager.UnloadSceneAsync("MainBattle");
            explorationRootObject.SetActive(true);
            // TODO: Add smooth transition fade.
            isTransitioning = false;
        }

        private void TransitionAnimationFinished()
        {
            transitionCallback?.Dispose();
            LoadBattleScene().Forget();
        }

        private async UniTask LoadBattleScene()
        {
            if (explorationRootObject == null)
            {
                explorationRootObject = GameObject.FindGameObjectWithTag("Root");
            }
            
            explorationRootObject.SetActive(false);
            await SceneManager.LoadSceneAsync("MainBattle", LoadSceneMode.Additive).ToUniTask();
            BattleManager.Instance.SetupBattle(playerCharacters, enemyCharacters);
            Time.timeScale = 1f;
            await StartBattle();
        }

        private async UniTask StartBattle()
        {
            battleSceneTransition.Hide();
            isTransitioning = false;
            await BattleManager.Instance.BeginBattle();
        }
    }
}