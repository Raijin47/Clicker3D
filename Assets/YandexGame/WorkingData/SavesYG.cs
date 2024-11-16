namespace YG
{
    [System.Serializable]
    public class SavesYG
    {
        // Технические сохранения
        public int idSave;
        public bool isFirstSession = true;
        public string language = "ru";
        public bool promptDone;

        // Сохранения
        public int CurrentStage;
        public int RecordStage;
        public double CurrentHealth;

        public int[] JobLevel = new int[9];
        public int[] PetLevel = new int[10];
        public int ClickLevel;
        public int ImprovementClickLevel;
        public int CurrentJob;
        public int CurrentPet;

        public int CurrentSkinEquip;
        public int CurrentHairColor;
        public int CurrentEyesColor;
        public int CurrentBodyColor;

        public double Money;
        public double Rebith;
        public double Diamonds;

        public int IslandLevel;
        public int[] RebithLevel = new int[9];
        public int[] DiamondLevel = new int[9];
        public bool[] SkinsPurchased = new bool[10];
        public bool[] ColorPurchased = new bool[30];

        public int[] LastReceiveTime = new int[2];
        public bool[] IsActiveTimer = new bool[2];

        public int[] UpgradePet = new int[10];
        public int[] UpgradeJob = new int[9];

        public int LastLoginTime;
        public bool IsInitOfflineTimer;

        public int AdsLevel;

        public int LastLoginDay;
        public bool IsClaimReward;

        public bool IsShowPanel;

        public int StepTutorial;
        public bool IsTutorialComplated;

        public int DailySpinAdsCount;
        public bool ActiveSpin;

        public SavesYG()
        {
            Diamonds = 0;
            CurrentSkinEquip = 8;
            CurrentHairColor = 0;
            CurrentEyesColor = 10;
            CurrentBodyColor = 20;
            SkinsPurchased[8] = true;
            ColorPurchased[0] = true;
            ColorPurchased[10] = true;
            ColorPurchased[20] = true;
            CurrentStage = 1;
            ClickLevel = 1;
            PetLevel[9] = 1;
        }
    }
}