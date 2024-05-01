public abstract record Item
{
    public string Name;
    public string Description;

    public abstract bool Use();
}