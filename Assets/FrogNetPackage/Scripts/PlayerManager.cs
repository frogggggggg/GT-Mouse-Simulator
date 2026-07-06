using System.Collections.Generic;
using UnityEngine;
using PurrNet.Prediction;

public class PlayerManager : PredictedIdentity<PlayerManager.State>
{
    public static List<PlayerManager> AllPlayers { get; private set; } = new List<PlayerManager>();
    public PlayerMovement playerMovement;

    protected override void LateAwake()
    {
        AllPlayers.Add(this);
    }

    public struct State : IPredictedData<State>
    {
        public void Dispose() {}
    }
}
