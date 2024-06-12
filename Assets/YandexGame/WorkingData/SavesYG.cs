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
        public int[] PetLevel = new int[9];
        public int CurrentJob;
        public int CurrentPet;

        public int CurrentSkinEquip;
        public int CurrentHairColor;
        public int CurrentEyesColor;
        public int CurrentBodyColor;

        public double Money;
        public double Rebith;
        public double Diamonds;

        public int[] RebithLevel = new int[9];
        public int[] DiamondLevel = new int[9];
        public int[] EnchancementLevel = new int[9];
        public bool[] SkinsPurchased = new bool[10];
        public bool[] ColorPurchased = new bool[30];

        public int[] LastReceiveTime = new int[2];
        public bool[] IsActiveTimer = new bool[2];

        public int[] UpgradeAutomatic = new int[18];

        public int LastLoginTime;
        public bool IsInitOfflineTimer;

        public int AdsLevel;

        public int LastLoginDay;
        public bool IsClaimReward;
        public int DailyLoginInRow;

        public bool IsTutorialComplated;

        // ...

        public SavesYG()
        {
            DailyLoginInRow = 1;
            CurrentSkinEquip = 8;
            CurrentHairColor = 0;
            CurrentEyesColor = 10;
            CurrentBodyColor = 20;
            SkinsPurchased[8] = true;
            ColorPurchased[0] = true;
            ColorPurchased[10] = true;
            ColorPurchased[20] = true;
            CurrentStage = 1;
            EnchancementLevel[0] = 1;
#if UNITY_EDITOR
            Diamonds = 5000;
            Rebith = 100000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000d;
#endif
        }
    }
}