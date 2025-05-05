public class Hiver : Saisons
{
    public Hiver() : base("Hiver")
    {
        this.Temperature.Add(0);
        this.Temperature.Add(10);
        this.Luminosite.Add(20);
        this.Luminosite.Add(40);
        this.Humidite.Add(80);
        this.Humidite.Add(100);
    }


}