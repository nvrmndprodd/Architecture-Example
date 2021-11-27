public class BankInteractor : Interactor
{
    private BankRepository _repository;

    public int coins => _repository.coins;

    public override void OnCreate()
    {
        _repository = Game.GetRepository<BankRepository>();
    }

    public override void Initialize()
    {
        Bank.Initialize(this);
    }

    public bool IsEnoughCoins(int value) => coins >= value;

    public void AddCoins(object sender, int value)
    {
        _repository.coins += value;
        _repository.Save();
    }

    public void Spend(object coins, int value)
    {
        _repository.coins -= value;
        _repository.Save();
    }
}