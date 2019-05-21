 using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "PlayerModel", menuName ="Models/Player", order =0)]
public class PlayerModel : CharacterModel
{
    public float jumpImpulse;
    public float jumpSpeedFactor;
    public float horizontalForce;
    public float groundRadius;
    public float speedClimbingLadders;
    public int lastCheckpointChecked; //Se usa en el sistema de guardado
    public float speedMaxInNeutro;
}
