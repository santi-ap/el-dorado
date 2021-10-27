using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SWNetwork;
///<summary>
/// This class holds the local game data. This data is synchronized
/// across all players through the netcode. You should not directly change
/// the values of this class, you should instead create a method that 
/// handles the event of room property agent OnValueChange and, 
/// in that method, you should update this class. Check out the methods
/// TestNetCode.OnTestDataChanged() that triggers 
/// TestMultiplayerGame.OnGameDataChanged (int testValue).
///</summary>
public class TestGameData
{
    ///<summary>this is the only value present in gamedata</summary>
    public int testValueData;
    
}
