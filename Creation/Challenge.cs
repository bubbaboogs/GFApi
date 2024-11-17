using GFApi;
using UnityEngine;

namespace GFApi.Creation{
    public static class Challenges{
        public static ChallengeData CreateChallenge(string name, Item itemNeeded, int amountNeeded, ChallengeManager.challengeType type, string text){
            ChallengeData challenge = (ChallengeData)ScriptableObject.CreateInstance("ChallengeData");
            challenge.challengeName = name;
            challenge.itemNeeded = itemNeeded;
            challenge.challengeType = type;
            challenge.challengeText = text;
            challenge.amountNeeded = amountNeeded;
            MainPlugin.gameData.challengeDatabase.challengeDataList.Add(challenge);

            return challenge;
        }
    }
}