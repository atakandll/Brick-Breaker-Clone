using Runtime.Interfaces;
using Runtime.Managers;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;
using Logger = Runtime.Extensions.Logger;

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
            var request = Addressables.LoadAssetAsync<GameObject>($"Prefabs/LevelPrefabs/Level {parameter}");
            request.Completed += handle =>
            {
                if (handle.Status == AsyncOperationStatus.Succeeded)
                {
                    var newLevel = Object.Instantiate(handle.Result as GameObject, Vector3.zero, Quaternion.identity);
                    if (newLevel != null)
                    {
                        newLevel.transform.SetParent(_levelManager.levelHolder.transform);
                    }
                    else
                    {
                        Logger.Instance.Log<LevelLoaderCommand>(
                            "Level {parameter} loaded but the asset is not a valid GameObject","black");
                    }
                }
                else
                {
                    
                    Logger.Instance.Log<LevelLoaderCommand>($"Failed to load level {parameter}","red");
                }
            };
        }
    }
}