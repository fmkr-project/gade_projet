using System;

public enum Stats
{
    Attack,
    Defense,
    Speed,
    Critical,
    Accuracy,
    Evasion
}

public static class StatDialogue
{
    public static string StatChangeVerb(int alteration)
    {
        return alteration switch
        {
            -3 => "diminue fortement",
            -2 => "diminue beaucoup",
            -1 => "diminue",
            1 => "augmente",
            2 => "augmente beaucoup",
            3 => "augmente fortement",
            _ => throw new ArgumentException("This should not be called when the alteration is 0.")
        };
    }
}