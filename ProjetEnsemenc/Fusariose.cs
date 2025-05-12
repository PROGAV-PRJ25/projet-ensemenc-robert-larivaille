public class Fusariose : Maladie
{

    public Fusariose(string nom) : base(nom)
    {
        this.ProbaContamination = 5; // Si possible la faire augmenter par humidité >70
        this.Criticite = 10;
    }
}