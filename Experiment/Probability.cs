namespace Experiment;
public enum Level
{
    L0,
    L1,
    L2,
    L3,
    L4
}

public class Probability
{
    private Random random = new Random();

    public bool ToCenter()
    {
        double randValue = random.NextDouble();
        return randValue >= 0.9;
    }

    public string ToCenterL1()
    {
        double randValue = random.NextDouble();
        if (randValue <= 0.33)
        {
            return "К";
        }
        else if (randValue > 0.33 && randValue <= 0.66)
        {
            return "От";
        }
        else
        {
            return "Остаться";
        }
    }
}