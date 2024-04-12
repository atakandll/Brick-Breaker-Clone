<h1 align="center"> Brick Breaker Clone</h1>

## Overview
- This repository contains the source code for an enhanced version of the classic Brick Breaker game. Developed as a final week project for `Masomo Gaming`'s internship program in a 2.5 weeks. This game adheres to principles of SOLID, KISS, YAGNI and utilizes Dependency Injection for a robust and maintainable codebase. Inspired by the "Juice it or Lose it" talk by Martin Jonasson & Petri Purho ([Watch here](https://www.youtube.com/watch?v=Fy0aCDmgnxg)), this game aims to deliver a rich, dynamic experience through the addition of juiciness and tweening, enhancing the core gameplay with more engaging and satisfying interactions.

## Table of Contents
1. [Overview](#overview)
2. [Key Features](#key-features)
3. [Design Patterns](#design-patterns)
4. [Architecture](#architecture)



**Watch the `gameplay` of the project:  [Gameplay youtube video](https://youtu.be/_1Hp7TDg5TM).**


![ezgif com-video-to-gif-converter](https://github.com/atakandll/Brick-Breaker-Clone/assets/130579265/2f23f128-d36c-4d6c-9ff1-6f603a1cedcb)

## Key Features
 ## Object Pooling:
- Utilizes a custom `PoolManager` class to manage object instantiation efficiently, reducing overhead and improving performance, particularly for the dynamic spawning of `Brick` and `Ball` objects.

![Screenshot_334](https://github.com/atakandll/Brick-Breaker-Clone/assets/130579265/0ff23ef8-0835-4311-bedc-eeac9fc4d23a) 

![Screenshot_335](https://github.com/atakandll/Brick-Breaker-Clone/assets/130579265/90ddbcf6-2fed-4f94-bf47-8e55511df00e)

  
 ## Event-Driven Architecture:
- Leverages a signal-based system to facilitate communication between various game managers, promoting loose coupling and event-driven interactions.
  
  ## Example for signals.
  
```csharp
public class InputSignals : MonoSingleton<InputSignals>
{
    public UnityAction onFirstTimeTouchTaken = delegate { };
    public UnityAction onInputTaken = delegate { };
    public UnityAction<HorizontalInputParams> onInputDragged = delegate { };
    public UnityAction onInputReleased = delegate { };
    public UnityAction<bool> onChangeInputState = delegate { };
}
```


  
 ## Juiced-Up Interactions:
- Incorporates polished game feel techniques such as tweening, particle effects, and responsive sound design to elevate the player experience.

  ![simsizvideoClipchampileyapld1-ezgif com-video-to-gif-converter](https://github.com/atakandll/Brick-Breaker-Clone/assets/130579265/93124583-5f5c-4120-8b2d-1358f9bbc434)


## Design Patterns
- `Singleton Pattern:` Ensures that game managers such as **SpawnManager** and **PoolManager** are globally accessible without multiple instances.
- `Command Pattern:` Encapsulates actions as objects, allowing for flexible command execution functionality. Used for  level created in this project
- `Object Pooling Pattern:` Manages a pool of initialized objects, recycled to avoid frequent allocations/deallocations.
- `Strategy Pattern:` Defines a family of algorithms, encapsulates each one and makes them interchangeable within each **Controller** class.
- `Observer Pattern:` Utilizes events and signals to subscribe and broadcast changes to multiple listeners without creating tight dependencies.

## Architecture
The game is built upon a robust system of controllers and managers: For example;

- **Paddle Management:** Through `PaddleManager`, `PaddlePhysicsController`, and `PaddleSpriteController`, the game manages the paddle's behavior, appearance, and physical interactions within the game world.
- **Ball Dynamics:** `BallManager`, `BallPhysicsController`, `BallSpawnController`, `BallSpriteController`, and `BallMovementController` collaborate to govern the physics, spawning, and visual representation of the balls in play.

  ## PaddleMovementController class for example of controllers system:
  
  ```csharp
  #region Self Variables

        #region Serialized Variables

        [SerializeField] private new Rigidbody2D rigidbody;

        #endregion

        #region Private Variables

        [Header("Data")][ShowInInspector] private PaddleData _data;
        [ShowInInspector] private bool _isReadyToMove, _isReadyToPlay;
        [ShowInInspector] private float _inputValue;
        [ShowInInspector] private Vector2 _clampValues;

        #endregion

        #endregion
        internal void SetMovementData(PaddleData data) => _data = data;
        private void OnEnable() => SubscribeEvents();
        private void SubscribeEvents()
        {
            PaddleSignals.Instance.onPlayConditionChanged += OnPlayConditionChanged;
            PaddleSignals.Instance.onMoveConditionChanged += OnMoveConditionChanged;
        }

        private void OnMoveConditionChanged(bool condition) => _isReadyToMove = condition;
        private void OnPlayConditionChanged(bool condition) => _isReadyToPlay = condition;
        private void OnDisable() => UnsubscribeEvents();
        private void UnsubscribeEvents()
        {
            PaddleSignals.Instance.onPlayConditionChanged -= OnPlayConditionChanged;
            PaddleSignals.Instance.onMoveConditionChanged -= OnMoveConditionChanged;
        }
        public void UpdateInputValue(HorizontalInputParams inputParams)
        {
            _inputValue = inputParams.HorizontalInputValue;
            _clampValues = inputParams.HorizontalInputClampSides;
        }
        private void FixedUpdate()
        {
            if (_isReadyToPlay)
            {
                if (_isReadyToMove)
                {
                    Move();
                }
                else
                {
                    StopSideways();
                }
            }
            else
                Stop();
        }
        
        private void Move()
        {
            var velocity = rigidbody.velocity;
            velocity = new Vector3(_inputValue * _data.SideWaysSpeed, velocity.y);
            rigidbody.velocity = velocity;

            Vector2 position;
            position = new Vector2(Mathf.Clamp(rigidbody.position.x, _clampValues.x, _clampValues.y),
                rigidbody.position.y);
            rigidbody.position = position;

        }
        private void StopSideways()
        {
            var velocity = rigidbody.velocity;
            velocity = new Vector3(0, velocity.y);
            rigidbody.velocity = velocity;
        }

        private void Stop() => rigidbody.velocity = Vector3.zero;
        public void OnReset()
        {
            Stop();
            _isReadyToPlay = false;
            _isReadyToMove = false;
        }
    }
  ```
  
 **Pooling and Spawning:** `PoolManager` and `SpawnManager` work in tandem to efficiently handle the lifecycle and behavior of game objects using the pooling technique.
  ## Pool system in this project;
  
  ```csharp
  public class PoolManager : MonoBehaviour
    {
        #region Self Variables

        #region Serialized Variables
        
        [SerializeField] private SerializedDictionary<PoolObjectType, Queue<GameObject>> objectPool;
        [SerializeField] private GameObject poolholder;
        [SerializeField] private CD_ObjectPoolData data;

        #endregion

        #region Private Variables

        private ObjectPoolData _data;
        private GameObject _getObjectFromPool;
        private readonly int _loadPoolCount = Enum.GetNames(typeof(PoolObjectType)).Length;

        #endregion

        #endregion

        private int poolCount = 0;

        private void Awake() => PoolGenerator();
        private void PoolGenerator()
        {
            objectPool = new SerializedDictionary<PoolObjectType, Queue<GameObject>>();

            for (; poolCount < _loadPoolCount; poolCount++)
            {
                objectPool.Add(data.ObjectData[poolCount].PoolObjectType, new Queue<GameObject>());

                for (int j = 0; j < data.ObjectData[poolCount].PoolSize; j++)
                {
                    var poolObj = Instantiate(data.ObjectData[poolCount].PoolObject, poolholder.transform);

                    poolObj.SetActive(false);

                    objectPool[data.ObjectData[poolCount].PoolObjectType].Enqueue(poolObj);
                }
            }
        }

        private void OnEnable() => SubscribeEvents();

        private void SubscribeEvents()
        {
            PoolSignals.Instance.onGetPoolObject += OnGetPoolObject;
            PoolSignals.Instance.onReleasePoolObject += OnReleasePoolObject;
        }

        private void UnsubscribeEvents()
        {
            PoolSignals.Instance.onGetPoolObject -= OnGetPoolObject;
            PoolSignals.Instance.onReleasePoolObject -= OnReleasePoolObject;
        }

        private void OnDisable() => UnsubscribeEvents();
        

        private GameObject OnGetPoolObject(PoolObjectType type)
        {
            if (objectPool[type].Count == 0 && data.ObjectData[(int)type].PoolType == PoolType.Dynamic)
            {
                _getObjectFromPool = Instantiate(data.ObjectData[(int)type].PoolObject, poolholder.transform);
            }
            else
            {
                if (objectPool[type].Count != 0)
                {
                    _getObjectFromPool = objectPool[type].Peek();

                    _getObjectFromPool.SetActive(true);

                    objectPool[type].Dequeue();
                }
            }

            return _getObjectFromPool;
        }

        private void OnReleasePoolObject(PoolObjectType type, GameObject obj)
        {
            obj.SetActive(false);

            objectPool[type].Enqueue(obj);
        }
    }
  

