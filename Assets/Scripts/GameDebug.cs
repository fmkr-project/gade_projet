using System;
using UnityEngine;

public class GameDebug : MonoBehaviour
{
    private ConcreteCreatureFactory _factory;

    private void Awake()
    {
        _factory = new ConcreteCreatureFactory();
        _factory.GenerateCreature(1);
        _factory.GenerateCreature(1);
        _factory.GenerateCreature(1);
    }
}