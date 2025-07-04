using System;
using UnityEngine;
using UnityEngine.InputSystem;
using System.Collections.Generic;


public class Cheats : MonoBehaviour
{
    [SerializeField] InputActionAsset inputActions;

    [SerializeField] List<GameObject> foods;

    [SerializeField] float speedValue;

    InputAction nextLvl;
    InputAction spawnFood;
    InputAction speed;

    private void Awake()
    {
        inputActions.Enable();

        nextLvl = inputActions.FindAction("NextLvl");
        spawnFood = inputActions.FindAction("SpawnFood");
        speed = inputActions.FindAction("Speed");

        nextLvl.started += ChangeLevel;
        spawnFood.started += SpawnFood;
        speed.performed += ChangeSpeed;
    }

    private void OnDestroy()
    {
        inputActions.Disable();

        nextLvl.started -= ChangeLevel;
        spawnFood.started -= SpawnFood;
        speed.performed -= ChangeSpeed;
    }

    private void ChangeSpeed(InputAction.CallbackContext context)
    {
        speedValue = speed.ReadValue<float>();

        Time.timeScale += speedValue;
        Time.timeScale = Math.Clamp(Time.timeScale, 1f, 20f);
    }

    private void SpawnFood(InputAction.CallbackContext context)
    {
        Transform position = GameManager.Instance.GetPlayer().transform;

        if (!position)
        {
            position = GameManager.Instance.GetPlayer().transform;
            return;
        }

        for (int i = 0; i < foods.Count; i++)
        {
            GameObject newfood = Instantiate(foods[i], position.position + position.forward * 2f + position.up * 1.2f, position.rotation);
            newfood.GetComponent<Item>().AddCount(500);
        }
    }

    private void ChangeLevel(InputAction.CallbackContext context)
    {
        if (GameManager.Instance.ActiveScene < 3)
            SceneController.Instance.LoadSceneAsyncSingle(GameManager.Instance.ActiveScene + 1);
    }
}
