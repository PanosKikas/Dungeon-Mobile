using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BattleTransistor : MonoBehaviour
{
    #region Singletton
    public static BattleTransistor Instance { get; private set; }


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
    #endregion

    [SerializeField]
    public List<CharacterStatsSO> enemyGroupStats;

    BattleSetup setup;

    public void EnterBattleScene(GameObject enemy)
    {
        FindEnemyGroupFromEnemy(enemy);
        StartCoroutine(LoadBattleScene());
    }

    IEnumerator LoadBattleScene()
    {
        var asyncOp = SceneManager.LoadSceneAsync(SceneManager.GetActiveScene().buildIndex + 1, LoadSceneMode.Single);
        while (!asyncOp.isDone)
        {
            yield return null;
        }
    }

    void FindEnemyGroupFromEnemy(GameObject enemy)
    {
        enemyGroupStats.Clear();
        GameObject parentGroup = enemy.transform.parent.gameObject;
        var stats = parentGroup.GetComponentsInChildren<Character>();
        foreach (var stat in stats)
        {
            enemyGroupStats.Add(stat.Data);
        }
    }

    public void ExitBattleScene()
    {
        // LOL
    }

    
}
