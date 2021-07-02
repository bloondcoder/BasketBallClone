using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public delegate void FunctionCallInAction(Vector3 pointPosition);

public interface IControllerInterface
{
    ///<summary>
    ///Define the behaviour when start the loading time before start launch of ball
    ///<summary>
    bool StartLoading();

    ///<summary>
    ///Define the behaviour during loading time before start launch of ball
    ///<summary>
    float LoadingLaunch();
    
    ///<summary>
    ///Define the behaviour start launch of ball
    ///<summary>
    Vector3 StartLaunch();

    
    ///<summary>
    ///Define the behaviour to touch the backboard
    ///<summary>
    void SpecialBonus(FunctionCallInAction handler);
}
