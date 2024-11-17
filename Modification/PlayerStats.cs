namespace GFApi.Modification{
    public static class PlayerStats{
        public enum stat{
            glim,
            starRank,
            clickGrowStrength,
            xp
        }
        public static void ModifyStats(stat statToModify, int newStat){
            PlayerData playerData = new PlayerData();
            PlayerData.PlayedBiomeData playedBiomeData = new PlayerData.PlayedBiomeData();
            switch(statToModify){
                case stat.clickGrowStrength:
                    playerData.clickGrowStrength = newStat;
                    break;
                case stat.glim:
                    playedBiomeData.glimAmount = newStat;
                    break;
                case stat.starRank:
                    playerData.starRank = newStat;
                    break;
                case stat.xp:
                    playedBiomeData.xp = newStat;
                    break;
            }
        }
    }
}