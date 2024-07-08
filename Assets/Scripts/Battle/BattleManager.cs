using System;
using UnityEditor;
using UnityEngine;

namespace DMT.Battle
{
    public class BattleManager : MonoBehaviour
    {
        [SerializeField] private BattleTeam playerTeam;
        [SerializeField] private BattleTeam enemyTeam;

        [MenuItem("AssetDatabase/Force Reserialize Assets Example")]
        static void UpdateGroundMaterials()
        {
            /*for (var i = 0; i < 10; i++)
            {
                var matPath = $"Assets/Materials/GroundMaterial{i}.mat";
                var mat = (Material)AssetDatabase.LoadMainAssetAtPath(matPath);
                AssetImporter.GetAtPath(matPath).SetAssetBundleNameAndVariant("GroundBundle", "");
                mat.shader = Shader.Find("Standard");
                mat.color = Color.white;
                mat.mainTexture = (Texture)AssetDatabase.LoadMainAssetAtPath($"Assets/Textures/GroundTexture{i}.jpg");
            }*/
            AssetDatabase.ForceReserializeAssets();

        }

        private void Start()
        {
            BeginBattle();
        }

        private void BeginBattle()
        {
            playerTeam.BeginBattle();
            enemyTeam.BeginBattle();
        }
    }
}