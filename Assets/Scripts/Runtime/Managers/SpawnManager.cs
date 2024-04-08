using Runtime.Controllers.Ball;
using Runtime.Controllers.Bricks;
using Runtime.Data.UnityObject;
using Runtime.Data.ValueObject;
using Runtime.Managers;
using Runtime.Signals;
using UnityEngine;

namespace Runtime.Managers
{
   
} public class SpawnManager : MonoBehaviour
     {
         #region Self Variables
 
         #region Public Variables
 
         public SpawnData Data { get; set; }
 
         #endregion
 
         #region Private Variables
 
         private BricksSpawnController _bricksSpawnController;
         private BallSpawnController _ballSpawnController;
 
 
         #endregion
 
         #endregion
 
         private void Awake()
         {
             Data = GetData();
             GetReferences();
         }
         private void GetReferences()
         {
             _bricksSpawnController = new BricksSpawnController(this);
             _ballSpawnController = new BallSpawnController(this);
             Debug.Log("SpawnManager GetReferences");
             
         }
         private SpawnData GetData() => Resources.Load<CD_SpawnData>("Data/CD_SpawnData").Data;
 
         private void OnEnable()
         {
             SubscribeEvents();
             ActivatorController();
             Debug.Log("SpawnManager OnEnable");
         }
 
         private void SubscribeEvents()
         {
             CoreGameSignals.Instance.onLevelInitialize += OnLevelInitialize;
             CoreGameSignals.Instance.onPlay += OnPlay;
             CoreGameSignals.Instance.onReset += OnReset;
         }
         
         private void OnDisable()
         {
             UnsubscribeEvents();
             DeactiveController();
         }
 
         private void UnsubscribeEvents()
         {
             CoreGameSignals.Instance.onLevelInitialize -= OnLevelInitialize;
             CoreGameSignals.Instance.onPlay -= OnPlay;
             CoreGameSignals.Instance.onReset -= OnReset;
             
         }
         private void OnPlay()
         {
             Debug.Log("SpawnManager OnPlay Çalıştı");
             TriggerController();
             
         }
         private void OnLevelInitialize(int levelID)
         {
             Debug.Log("SpawnManager OnLevelInitialize");
             ActivatorController();
         }
         private void ActivatorController()
         {
             _bricksSpawnController.IsActivating = true;
             _ballSpawnController.IsActivating = true;
         }
         private void TriggerController()
         {
             Debug.Log("SpawnManager TriggerController");
             _bricksSpawnController.TriggerAction();
             _ballSpawnController.TriggerAction();
         }
         private void OnReset()
         {
             DeactiveController();
             _ballSpawnController.Reset();
             _bricksSpawnController.Reset();
             
         }
         internal void ResetBallSpawnController()
         {
             _ballSpawnController.Reset();
         }
 
         private void DeactiveController()
         {
             _bricksSpawnController.IsActivating = false;
             _ballSpawnController.IsActivating = false;
         }
         
        
     }