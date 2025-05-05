public class Mildiou : Maladie
{

    public Mildiou(string nom) : base(nom)
    {
        this.ProbaContamination = 5;
        this.Criticite = 10;
    }
}