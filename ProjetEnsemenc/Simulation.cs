public enum ModeDeJeu
{
    Classique,
    Urgence
}

public class Simulation
{
    public Potager Pot { get; set; }
    public int Argent { get; set; }
    public int NumeroTour { get; set; }
    public ModeDeJeu mode { get; set; }
    public List<int> ListeAchats { get; set; } //Nombre de chaque achat qui n'a pas encore été utilisé pour dans l'odre : 

    public bool PresenceChien { get; set; }
    public bool PresenceEpouvantail { get; set; } // Indique si un epouvantail est présent sur le jeu (acheter et posé)

    public Simulation()
    {
        Pot = new Potager(); //Rentrer params
        mode = ModeDeJeu.Classique;
        Argent = 1000;
        NumeroTour = 0;
        ListeAchats = new List<int>();
        PresenceChien = false;
        PresenceEpouvantail = false;
    }

    public void CreerPlante(string espece, int x, int y)
    {
        if (espece == "Artichaut") Pot.ListePlantes.Add(new Artichaut(x, y));  //ALED
        if (espece == "Aubergine") Pot.ListePlantes.Add(new Aubergine(x, y));
        if (espece == "Basilic") Pot.ListePlantes.Add(new Basilic(x, y));
        if (espece == "Oignon") Pot.ListePlantes.Add(new Oignon(x, y));
        if (espece == "Olivier") Pot.ListePlantes.Add(new Olivier(x, y));
        if (espece == "Poivron") Pot.ListePlantes.Add(new Poivron(x, y));
        if (espece == "Roquette") Pot.ListePlantes.Add(new Roquette(x, y));
        if (espece == "Thym") Pot.ListePlantes.Add(new Thym(x, y));
        if (espece == "Tomate") Pot.ListePlantes.Add(new Tomate(x, y));
    }

    public void Planter()
    {
        if (Pot.SacDeGraines.Count == 0)
        {
            Console.WriteLine("Vous ne possédez aucune graine donc vous ne pouvez rien planter. ");
        }
        else
        {
            int numero = 0;
            Console.WriteLine("Vous possédez les graines suivantes :");
            foreach (Graine graine in Pot.SacDeGraines)
            {
                if (graine.Quantite != 0)
                {
                    Console.WriteLine($"- {numero}. {graine.Espece} : {graine.Quantite} unités");
                }
                numero++;
            }
            Console.WriteLine("Quel est le numéro de la graine que vous voulez planter ? ");
            string reponse = Console.ReadLine();
            int numeroAPlanter;
            while (!Int32.TryParse(reponse, out numeroAPlanter) || (numeroAPlanter < 0) || (numeroAPlanter >= Pot.SacDeGraines.Count))
            {
                Console.WriteLine("Vous n'avez pas entré un nombre valide. Quel est le numéro de la graine que vous voulez planter ? ");
            }
            Console.WriteLine("A quel numéro de ligne voulez-vous la planter ? ");
            string reponseX = Console.ReadLine();
            int x;
            while ((!Int32.TryParse(reponseX, out x)) || (x < 0) || (x >= Pot.Hauteur))
            {
                Console.WriteLine("Vous n'avez pas entré un numéro de ligne valide. Quel est le numéro de la ligne où vous voulez planter ? ");
            }
            Console.WriteLine("A quel numéro de colonne voulez-vous la planter ? ");
            string reponseY = Console.ReadLine();
            int y;
            while ((!Int32.TryParse(reponseY, out y)) || (y < 0) || (x >= Pot.Longueur))
            {
                Console.WriteLine("Vous n'avez pas entré un numéro de colonne valide. Quel est le numéro de la colonne où vous voulez planter ? ");
            }
            Pot.SacDeGraines[numeroAPlanter].Quantite--;
            CreerPlante(Pot.SacDeGraines[numeroAPlanter].Espece, x, y);
        }

    }

    public void Arroser()
    {
        Console.WriteLine("Voici l'état d'humidité de vos plantes : ");
        int numero = 0;
        foreach (Plante plante in Pot.ListePlantes)
        {
            Console.WriteLine($"- {numero}. {plante} : niveau actuel : {plante.NiveauHumidite} | niveau optimal : {plante.SeuilHumidite} | diminution de l'humidité par tour : {plante.BesoinEau} ");
            numero++;
        }
        Console.WriteLine("Arroser une plante augmente son niveau d'humidité actuel de 10. ");
        Console.WriteLine("Entrez les numéros des plantes à arroser un par un. Entrez 1000 pour arreter l'arrosage. ");
        string reponse = Console.ReadLine();
        int numeroAArroser = -1;
        while (numeroAArroser != 1000)
        {
            while ((!Int32.TryParse(reponse, out numeroAArroser)) || (numeroAArroser < 0) || ((numeroAArroser >= Pot.ListePlantes.Count) && (numeroAArroser != 1000)))
            {
                Console.WriteLine("Réponse invalide ");
                Console.WriteLine("Entrez les numéros des plantes à arroser un par un. Entrez 1000 pour arreter l'arrosage. ");
            }
            if (numeroAArroser != 1000)
            {
                Pot.ListePlantes[numeroAArroser].NiveauHumidite += 10;
            }
        }

    }

    public void Simuler()
    {

    }

}