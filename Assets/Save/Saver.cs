﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Saver : MonoBehaviour
{
    public delegate void SaveGame();// событие  определения положения
    public static event SaveGame saveGame;// событие  определения положения
    public void Save()
    {
        saveGame?.Invoke();
    }
}