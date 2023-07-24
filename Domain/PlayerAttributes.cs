public class PlayerAttributes
{
    public int Speed { get; set; }
    public int Strength { get; set; }

    public PlayerAttributes(int speed, int strength)
    {
        Speed = speed;
        Strength = strength;
    }
}