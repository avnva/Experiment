
namespace Experiment;

public class Game
{
    private Probability probability = new Probability();
    private int numberOfSteps;
    private int numberOfGames;
    private double stepCounter = 0;
    private List<bool> answers = new List<bool>
    {
        true, true, true, true, true,
        false, false, false, false, false, false,
        true, true,
        false, false, false, false,
        true, true, true, true,
        false,
        true,
        false, false
    };

    private Level level = Level.L0;

    public Game(int steps, int games)
    {
        this.numberOfGames = games;
        this.numberOfSteps = steps;
    }

    public bool GameModeration()
    {
        for (int i = 0; i < numberOfSteps; i++)
        {
            switch (level)
            {
                case Level.L0:
                    level = Level.L1;
                    break;

                case Level.L1:
                    string choice = probability.ToCenterL1();
                    switch (choice)
                    {
                        case "От":
                            if (answers[i] == true) level = Level.L2;
                            break;
                        case "К":
                            if (answers[i] == true) level = Level.L0;
                            else level = Level.L2;
                            break;
                        case "Остаться":
                            if (answers[i] == false) level = Level.L1;
                            break;
                    }
                    break;
                case Level.L2:
                    bool toCenterS2 = probability.ToCenter();
                    if (toCenterS2)
                    {
                        if (answers[i]) level = Level.L1;
                        else level = Level.L3;
                    }
                    else
                    {
                        if (answers[i]) level = Level.L3;
                        else level = Level.L1;
                    }
                    break;
                case Level.L3:
                    bool toCenterS3 = probability.ToCenter();
                    if (toCenterS3)
                    {
                        if (answers[i]) level = Level.L2;
                        else level = Level.L4;
                    }
                    else
                    {
                        if (answers[i]) level = Level.L4;
                        else level = Level.L2;
                    }
                    break;
                case Level.L4:
                    bool toCenterS4 = probability.ToCenter();
                    if (toCenterS4)
                    {
                        if (answers[i]) level = Level.L3;
                        else
                        {
                            stepCounter = i + 1;
                            return true;
                        }
                    }
                    else
                    {
                        if (answers[i])
                        {
                            stepCounter = i + 1;
                            return true;
                        }
                        else level = Level.L3;
                    }
                    break;
            }
        }
        stepCounter = numberOfSteps;
        return false;
    }

    public void Results()
    {
        List<bool> gamesResults = new List<bool>();
        List<double> gamesSteps = new List<double>();
        double d = 0;

        for (int i = 0; i < numberOfGames; i++)
        {
            gamesResults.Add(this.GameModeration());
            gamesSteps.Add(stepCounter);
            stepCounter = 0;
        }

        foreach (double dd in gamesSteps)
        {
            d += dd;
        }

        Console.WriteLine("Кол-во игр - " + numberOfGames);
        Console.WriteLine("Длительность игры - " + numberOfSteps + " ходов");
        Console.WriteLine("Среднее кол-во шагов - " + d / numberOfGames + "\n");

        int robotWins = 0;

        foreach (bool result in gamesResults)
        {
            if (result) robotWins++;
        }

        int otherWins = numberOfGames - robotWins;
        double winPercentage = (double)robotWins / numberOfGames * 100;
        double otherWinPercentage = (double)otherWins / numberOfGames * 100;
        Console.WriteLine("Алгоритм выиграл " + robotWins + " раз");
        Console.WriteLine("Ведущий(случайный выбор) выиграл " + otherWins + " раз\n");
        Console.WriteLine("Процент побед алгоритма: " + winPercentage + " %");
        Console.WriteLine("Процент побед ведущего: " + otherWinPercentage + " %");
    }
}
