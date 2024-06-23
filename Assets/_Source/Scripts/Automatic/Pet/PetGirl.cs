using YG;
using System;

public class PetGirl : Pet
{
    protected override void SaveLevel()
    {
        YandexGame.savesData.PetLevel[_id] = Level;
        // активация бафа для силы клика
    }

    public override void Activate(int level)
    {
        Level = level;
    }

    public override void Deactivate()
    {
        Level = 1;
    }
}