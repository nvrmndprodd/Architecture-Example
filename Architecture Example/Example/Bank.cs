using System;

public static class Bank // pattern Фасад
{
    public static event Action OnBankInitialized;
    
    public static int coins
    {
        get
        {
            CheckClass();
            return _bankInteractor.coins;
        }
    }

    private static BankInteractor _bankInteractor;
    public static bool IsInitialized { get; private set; }

    public static void Initialize(BankInteractor bankInteractor)
    {
        _bankInteractor = bankInteractor;
        IsInitialized = true;
        OnBankInitialized?.Invoke();
    }

    public static bool IsEnoughCoins(int value)
    {
        CheckClass();
        return coins >= value;
    }


    public static void AddCoins(object sender, int value)
    {
        CheckClass();
        _bankInteractor.AddCoins(sender, value);
    }

    public static void Spend(object sender, int value)
    {
        CheckClass();
        _bankInteractor.Spend(sender, value);
    }

    public static void CheckClass()
    {
        if (!IsInitialized)
        {
            throw new Exception("Bank is not initialized yet");
        }
    }
}