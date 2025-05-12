public class Oidium : Maladie
{

    public Oidium(string nom) : base(nom)
    {
        this.ProbaContamination = 3; // Si possible la faire augmenter par humidité >70
        this.Criticite = 5;
    }
}