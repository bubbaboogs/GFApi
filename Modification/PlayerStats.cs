namespace GFApi.Modification{
    public static class PlayerStats{
        public enum stat{
            glim,
            starRank,
            clickGrowStrength,
            xp
        }
        public static void ModifyStats(stat statToModify, int newStat){
            switch(statToModify){
                case stat.clickGrowStrength:
                    PlayerData.playerData.clickGrowStrength = newStat;
                    MainPlugin.Logger.LogInfo($"Changed clickGrowStrength to {newStat}");
                    break;
                case stat.glim:
                    PlayerData.playerData.playedBiomeData.glimAmount = newStat;
                    MainPlugin.Logger.LogInfo($"Changed glim to {newStat}");
                    break;
                case stat.starRank:
                    PlayerData.playerData.starRank = newStat;
                    MainPlugin.handCursor.gameHUD.GetComponentInChildren<HUDTracker>().SetTracker();
                    MainPlugin.Logger.LogInfo($"Changed starRank to {newStat}");
                    break;
                case stat.xp:
                    PlayerData.playerData.playedBiomeData.xp = newStat;
                    MainPlugin.Logger.LogInfo($"Changed xp to {newStat}");
                    break;
            }
            //SaveSystem.SavePlayer(PlayerData.playerData, GameData.gameData);
        }
    }
}