public class Player
{
    public string Name { get; set; }
    public string Format { get; set; }
    public string Color { get; set; }
    public PlayerAttributes Attributes { get; set; }
    public Player(string name, string format, string color)
    {
        Name = name;
        Format = format;
        Color = color;
        Attributes = new PlayerAttributes(100, 100);
    }
}