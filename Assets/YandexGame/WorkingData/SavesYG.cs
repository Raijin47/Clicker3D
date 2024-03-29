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

        public double Money;
        public double Rebith;
        public double Diamonds;

        public int[] RebithLevel = new int[9];
        public int[] DiamondLevel = new int[9];
        public int[] EnchancementLevel = new int[9];
        public bool[] SkinsPurchased = new bool[10];
        public bool[] ColorPurchased = new bool[20];

        public int[] LastReceiveTime = new int[4];
        public bool[] IsActiveTimer = new bool[4];

        public int LastLoginTime;
        public bool IsInitOfflineTimer;

        public int AdsLevel;

        public int LastLoginDay;
        public bool IsClaimReward;
        public int DailyLoginInRow;

        // ...

        public SavesYG()
        {
            DailyLoginInRow = 1;
            CurrentSkinEquip = 2;
            CurrentHairColor = 0;
            CurrentEyesColor = 9;
            SkinsPurchased[2] = true;
            ColorPurchased[0] = true;
            ColorPurchased[9] = true;
            CurrentStage = 1;

            EnchancementLevel[0] = 1;
        }
    }
}