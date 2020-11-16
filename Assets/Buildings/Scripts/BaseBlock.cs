﻿using UnityEngine;

public sealed class BaseBlock : MonoBehaviour
{
    public bool IsBlock = true;
    [Range(0, 6)] public int Type;
    private Renderer myRend;
    internal Rigidbody MyRb { get; private set; }
    internal ObjectDown ObDown { get; set; }

    public void OnEnable() => myRend = GetComponent<Renderer>();
    private void Start()
    {
        if (Type != 2)// if block not is glass
            myRend.sharedMaterial.color = Color.white;
        else
            myRend.sharedMaterial.color = new TransparentColors(0).color;


        if (IsBlock)
        {
            ObDown.Objects.Add(this);//добавление в список ВзрывОбъектов

            gameObject.AddComponent<SaveObject>().enabled = true;
        }
    }
    public void Destroy(float power)
    {
        if (!IsBlock)
            return;

        power *= Random.Range(10, 16);
        int multiply = Random.Range(1, 3);
        power = multiply == 1 ? power : power * -1;

        //в зависимости от типа блока нужно определять сопротивление
        if (Type == 0)//кирпич
            power *= 0.74f;
        else if (Type == 1)//дерево
            power *= 1;
        else if (Type == 2)//стекло
            power *= 1.43f;
        else if (Type == 6)//камень
            power *= 0.55f;

        if (!MyRb)// если объект не был взорван
        {
            MyRb = gameObject.AddComponent<Rigidbody>();
            ObDown.ExplosivedObjects.Add(this);
            gameObject.AddComponent<RetentionObject>();
        }
        MyRb.velocity = new Vector3(power, power, power);
    }

    public void ChangeColor(byte type)
    {
        switch (type)
        {
            case 0:
                if (Type != 2)// if block not is glass
                    myRend.material.color = Color.white;
                else
                    myRend.material.color = new TransparentColors(0).color;
                break;
            case 1:
                myRend.material.color = new TransparentColors(1).color;
                break;
            case 2:
                myRend.material.color = new TransparentColors(2).color;
                break;
            case 3:
                myRend.material.color = new TransparentColors(3).color;
                break;

            default:
                break;
        }
    }
    private void OnDestroy()
    {
        if (!ObDown)
            return;
        if (IsBlock)
        {
            ObDown.Objects.Remove(this);
            ObDown.ExplosivedObjects.Remove(this);
        }
    }
}