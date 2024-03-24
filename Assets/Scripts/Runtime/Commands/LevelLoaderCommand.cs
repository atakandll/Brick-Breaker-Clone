using Runtime.Interfaces;
using Runtime.Managers;
using UnityEngine;
using UnityEngine.AddressableAssets;

namespace Runtime.Commands
{
    public class LevelLoaderCommand : ICommand
    {
        private readonly LevelManager _levelManager;

        public LevelLoaderCommand(LevelManager levelManager)
        {
            _levelManager = levelManager;
        }

        public void Execute(int parameter)
        {
            //Debug.Log("Execute"); 
            var request = Addressables.LoadAssetAsync<GameObject>($"Prefabs/LevelPrefabs/Level {parameter}");
            request.Completed += handle =>
            {
                var newLevel = Object.Instantiate(request.Result as GameObject, Vector3.zero, Quaternion.identity);
                if (newLevel != null)
                    newLevel.transform.SetParent(_levelManager.levelHolder.transform);
            };
        }
    }
}