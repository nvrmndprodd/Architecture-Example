using UnityEngine;

public class BankRepository : Repository
{
    private const string KEY = "BANK_KEY";
    
    public int coins { get; set; }

    public override void OnCreate()
    {
        
    }

    public override void Initialize()
    {
        coins = PlayerPrefs.GetInt(KEY, 0);
    }

    public override void OnStart()
    {
        
    }

    public override void Save()
    {
        PlayerPrefs.SetInt(KEY, coins);
    }
}