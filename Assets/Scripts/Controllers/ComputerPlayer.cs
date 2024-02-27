using RPSLS.Controllers;
using RPSLS.Core;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ComputerPlayer
{
    public ElementType GetRandomMove()
    {
        float randomNumber = UnityEngine.Random.Range(1, 6);
        return (ElementType)randomNumber;
    }
}
