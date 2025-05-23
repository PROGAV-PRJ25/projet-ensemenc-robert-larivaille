using System.Runtime.CompilerServices;
using System.Runtime.InteropServices.Marshalling;

public enum ModeDeJeu
{
    Classique,
    Urgence
}

public enum Terrain
{
    Argile,
    Sable,
    Terre,
    Calcaire,
}
public enum Saison
{
    Printemps,
    Ete,
    Automne,
    Hiver,
}
public enum ChoixOuiNon
{
    Oui,
    Non,
}
public class Simulation
{
    public Potager Pot { get; set; }
    private double Argent { get; set; }
    public int NumeroTour { get; set; }
    public ModeDeJeu mode { get; set; }
    private List<Achats> achatsPossibles = new List<Achats>();
    public List<int> ListeAchats { get; set; } //Nombre de chaque achat qui n'a pas encore été utilisé pour dans l'odre : Arrosage automatique, Bache, Coccinelle, Chien, Epouvantail, Fertilisant, Graine, LampeUV, Pompe, Serre, tuyau d'arrosage, RemedeFusariose, Remede Mildiou, Remede Oidium
    private List<Achats> ObjetsPoses = new List<Achats>();
    public bool PresenceChien { get; set; }
    public bool PresenceEpouvantail { get; set; } // Indique si un epouvantail est présent sur le jeu (acheté et posé)
    public bool PresenceArrosageAutomatique { get; set; }
    public bool PresenceLampeUV { get; set; }
    public bool PresenceSerre { get; set; }
    public bool PresenceBache { get; set; }
    public bool PresencePompe { get; set; }
    public bool Grele { get; set; }
    public bool Inondation { get; set; }
    public bool Secheresse { get; set; }

    private ActionUrgente ActionUrgente { get; set; }
    public Simulation(int hauteur, int largeur)
    {
        Saisons saison = new Saisons(Saison.Printemps);
        saison.ChangerTemperature();
        Pot = new Potager(hauteur, largeur, saison, saison.TemperatureDeSaison(), saison.LuminositeDeSaison()); //Rentrer params
        mode = ModeDeJeu.Classique;
        Argent = 1000;
        NumeroTour = 1;
        ListeAchats = new List<int> { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 };
        PresenceChien = false;
        PresenceEpouvantail = false;
        PresenceArrosageAutomatique = false;
        PresenceLampeUV = false;
        PresenceSerre = false;
        PresenceBache = false;
        PresencePompe = false;
        Grele = false;
        Inondation = false;
        Secheresse = false;
        achatsPossibles.Add(new ArrosageAutomatique());
        achatsPossibles.Add(new Bache());
        achatsPossibles.Add(new AchatCoccinelle());
        achatsPossibles.Add(new AchatChien());
        achatsPossibles.Add(new Epouvantail());
        achatsPossibles.Add(new Fertilisant());
        achatsPossibles.Add(new AchatGraine());
        achatsPossibles.Add(new LampeUV());
        achatsPossibles.Add(new Pompe());
        achatsPossibles.Add(new Serre());
        achatsPossibles.Add(new TuyauArrosage());
        achatsPossibles.Add(new AchatRemedeFusariose());
        achatsPossibles.Add(new AchatRemedeMildiou());
        achatsPossibles.Add(new AchatRemedeOidium());
        ActionUrgente = new ActionUrgente();

    }

    //Animaux :  
    private void CreerAnimal(string nom, Simulation simu)
    {
        Animaux nouveau;
        if (nom == "Abeille") nouveau = new Abeille(Pot, simu);
        else if (nom == "Chat") nouveau = new Chat(Pot, simu);
        else if (nom == "Chien")
        {
            nouveau = new Chien(Pot, simu);
            PresenceChien = true;
        }
        else if (nom == "Coccinelle") nouveau = new Coccinelle(Pot, simu);
        else if (nom == "Escargot") nouveau = new Escargot(Pot, simu);
        else if (nom == "Oiseau") nouveau = new Oiseau(Pot, simu);
        else if (nom == "Pucerons") nouveau = new Pucerons(Pot, simu);
        else if (nom == "Rongeur") nouveau = new Rongeur(Pot, simu);
        else nouveau = new VersDeTerre(Pot, simu);
        nouveau.TourApparition = NumeroTour;
        Pot.ListeAnimaux.Add(nouveau);
        nouveau.EstMange();
    }

    private void ApparaitreHasardAnimal(Simulation simu)
    {
        Random rng = new Random();
        int[] tableauProbabilites = new int[] { 5, 12, 14, 15, 16, 20 }; // Tableau qui contient les valeurs des probabilités d'apparition des Animaux dans l'ordre du tableau ci-dessous.
        string[] tableauAnimaux = new string[] { "Chien", "Escargot", "Coccinelle", "Abeille", "Pucerons", "VersDeTerre" };
        for (int i = 0; i < 6; i++)
        {
            int tirage = rng.Next(0, 101);
            if (tirage < tableauProbabilites[i])
            {
                string ani = tableauAnimaux[i];
                CreerAnimal(ani, simu);
                if (ani == "VersDeTerre") ani = "Vers de terre";
                Console.WriteLine($"Un nouvel animal est apparu : {ani}");
            }
        }
    }

    private void PoserCoccinelle(Simulation simu) //Cas particulier des coccinelles qui peuvent être achetées et posées sur la case souhaitée.
    {
        Console.WriteLine("A quel numéro de ligne voulez-vous poser vos coccinelles ?");
        string reponseX = Console.ReadLine()!;
        int x;
        while (!int.TryParse(reponseX, out x) || (x < 0) || (x >= Pot.Hauteur))
        {
            Console.WriteLine("Vous n'avez pas entré un numéro de ligne valide. Quel est le numéro de la ligne où vous voulez poser vos coccinelles ? ");
            reponseX = Console.ReadLine()!;
        }
        Console.WriteLine("A quel numéro de colonne voulez-vous poser vos coccinelles ? ");
        string reponseY = Console.ReadLine()!;
        int y;
        while (!int.TryParse(reponseY, out y) || (y < 0) || (y >= Pot.Longueur))
        {
            Console.WriteLine("Vous n'avez pas entré un numéro de colonne valide. Quel est le numéro de la colonne où vous voulez poser vos coccinelles ? ");
            reponseY = Console.ReadLine()!;
        }
        Coccinelle c = new Coccinelle(Pot, simu);
        c.X = x;
        c.Y = y;
        Pot.ListeAnimaux.Add(c);
    }

    private void EvolutionAnimaux()
    {
        foreach (Animaux animal in Pot.ListeAnimaux)
        {
            if (NumeroTour - animal.TourApparition > animal.Duree) animal.Disparait();
            if ((animal.Nom != "Pucerons") && (animal.Nom != "VersDeTerre") && (animal.Nom != "Escargot")) animal.SeDeplacer();
            animal.EstMange();
            foreach (Plante plante in Pot.ListePlantes)
            {
                if ((plante.CoorX != -1) && (plante.CoorY != -1) && (plante.CoorY == animal.Y) && (plante.CoorX == animal.X))
                {
                    animal.Effet(plante);
                }
            }
        }
    }

    //Plantes, Graines, Recoltes :
    private void CreerPlante(string espece, int y, int x, Simulation simu)
    {
        Console.WriteLine("Sur quel terrain voulez-vous la planter ? (Argile, Sable, Terre ou Calcaire)");
        string terrain = Console.ReadLine()!;
        Terrain ter;
        while (!Enum.TryParse<Terrain>(terrain, true, out ter) || int.TryParse(terrain, out _))
        {
            Console.WriteLine("Entrée invalide. Veuillez saisir un terrain valide (Argile, Sable, Terre ou Calcaire) :");
            terrain = Console.ReadLine()!;
        }
        if (espece == "Artichaut") Pot.ListePlantes.Add(new Artichaut(y, x, Pot, ter, simu));
        else if (espece == "Aubergine") Pot.ListePlantes.Add(new Aubergine(y, x, Pot, ter, simu));
        else if (espece == "Basilic") Pot.ListePlantes.Add(new Basilic(y, x, Pot, ter, simu));
        else if (espece == "Oignon") Pot.ListePlantes.Add(new Oignon(y, x, Pot, ter, simu));
        else if (espece == "Olivier") Pot.ListePlantes.Add(new Olivier(y, x, Pot, ter, simu));
        else if (espece == "Poivron") Pot.ListePlantes.Add(new Poivron(y, x, Pot, ter, simu));
        else if (espece == "Roquette") Pot.ListePlantes.Add(new Roquette(y, x, Pot, ter, simu));
        else if (espece == "Thym") Pot.ListePlantes.Add(new Thym(y, x, Pot, ter, simu));
        else if (espece == "Tomate") Pot.ListePlantes.Add(new Tomate(y, x, Pot, ter, simu));
    }

    private Recolte AssocierRecoltePlante(Plante plante, Recolte RecAr, Recolte RecAu, Recolte RecB, Recolte RecO, Recolte RecOl, Recolte RecP, Recolte RecR, Recolte RecTh, Recolte RecTo)
    {
        if (plante.Espece == "Artichaut") return RecAr;
        else if (plante.Espece == "Aubergine") return RecAu;
        else if (plante.Espece == "Basilic") return RecB;
        else if (plante.Espece == "Oignon") return RecO;
        else if (plante.Espece == "Olivier") return RecOl;
        else if (plante.Espece == "Poivron") return RecP;
        else if (plante.Espece == "Roquette") return RecR;
        else if (plante.Espece == "Thym") return RecTh;
        else return RecTo;
    }
    private Plante AssocierGrainePlante(Graine graine)
    {
        switch (graine.Espece)
        {
            case "Artichaut": return new Artichaut(); //Instances créés juste pour accéder aux propriétés
            case "Aubergine": return new Aubergine();
            case "Basilic": return new Basilic();
            case "Oignon": return new Oignon();
            case "Olivier": return new Olivier();
            case "Poivron": return new Poivron();
            case "Roquette": return new Roquette();
            case "Thym": return new Thym();
            default: return new Tomate(); ;
        }
    }

    private void Planter(Simulation simu)
    {
        bool presenceGraine = false;
        foreach (Graine graine in Pot.SacDeGraines)
        {
            Plante plante = AssocierGrainePlante(graine);
            if (plante.SaisondeSemis == Pot.Saison.Nom)
            {
                if (graine.Quantite != 0)
                {
                    presenceGraine = true;
                }
            }
            if (plante == null) Console.WriteLine("Plante inconnue, ça c'est fort");
        }
        if (!presenceGraine)
        {
            Console.WriteLine("Vous ne possédez aucune graine que vous pouvez planter en cette saison. ");
        }
        else
        {
            int numero = 0;
            Console.WriteLine("Vous possédez et pouvez planter les graines suivantes en cette saison :");
            foreach (Graine graine in Pot.SacDeGraines)
            {
                if (graine.Quantite != 0 && AssocierGrainePlante(graine).SaisondeSemis == Pot.Saison.Nom)
                {
                    Console.WriteLine($"- {numero}. {graine.Espece} : {graine.Quantite} unités");
                }

                numero++;
            }
            Console.WriteLine("Quel est le numéro de la graine que vous voulez planter ? ");
            string reponse = Console.ReadLine()!;
            int numeroAPlanter = -1;
            bool saisieInt = false;
            bool grainePossible = false;
            while (!saisieInt || !grainePossible)
            {
                if (!int.TryParse(reponse, out numeroAPlanter) || numeroAPlanter < 0 || numeroAPlanter >= Pot.SacDeGraines.Count)
                {
                    Console.WriteLine("Vous n'avez pas entré un nombre valide. Quel est le numéro de la graine que vous voulez planter ? ");
                    reponse = Console.ReadLine()!;
                    saisieInt = false;
                }
                else saisieInt = true;
                if (Pot.SacDeGraines[numeroAPlanter].Quantite == 0)
                {
                    Console.WriteLine("Vous ne possédez pas cette graine, choisissez une graine de la liste :");
                    reponse = Console.ReadLine()!;
                    grainePossible = false;
                }
                else grainePossible = true;

            }
            Console.WriteLine("A quel numéro de colonne voulez-vous la planter ? ");
            string reponseX = Console.ReadLine()!;
            Console.WriteLine("A quel numéro de ligne voulez-vous la planter ? ");
            string reponseY = Console.ReadLine()!;
            int x;
            int y;
            bool espacement;
            bool caseLibre;
            do
            {
                while (!int.TryParse(reponseX, out x) || (x < 0) || (x >= Pot.Longueur))
                {
                    Console.WriteLine("Vous n'avez pas entré un numéro de colonne valide. Quel est le numéro de la colonne où vous voulez planter ?");
                    reponseX = Console.ReadLine()!;
                }
                while (!int.TryParse(reponseY, out y) || (y < 0) || (y >= Pot.Hauteur))
                {
                    Console.WriteLine("Vous n'avez pas entré un numéro de ligne valide. Quel est le numéro de la ligne où vous voulez planter ?");
                    reponseY = Console.ReadLine()!;
                }

                espacement = true;
                caseLibre = true;
                foreach (Plante p in Pot.ListePlantes)
                {
                    if (p.Espece == Pot.SacDeGraines[numeroAPlanter].Espece)
                    {
                        int px = p.CoorX;
                        int py = p.CoorY;
                        int distance = Math.Abs(px - x) + Math.Abs(py - y);

                        if (distance <= p.Espacement)
                        {
                            espacement = false;
                        }
                    }
                    if ((p.CoorX != -1) && (p.CoorY != -1) && (p.CoorX == x) && (p.CoorY == y))
                    {
                        caseLibre = false;
                    }
                }
                if ((!espacement) || (!caseLibre))
                {
                    if (!caseLibre) Console.WriteLine("Votre graine ne peut pas être plantée sur la même case qu'une plante. Veuillez choisir une autre position.");
                    else if (!espacement) Console.WriteLine("Votre graine ne peut pas être plantée aussi proche d'une plante de la même espèce. Veuillez choisir une autre position."); Console.WriteLine("A quel numéro de colonne voulez-vous la planter ?");
                    reponseX = Console.ReadLine()!;
                    Console.WriteLine("A quel numéro de ligne voulez-vous la planter ?");
                    reponseY = Console.ReadLine()!;
                }
            }
            while ((!espacement) || (!caseLibre));
            Pot.SacDeGraines[numeroAPlanter].Quantite--;
            CreerPlante(Pot.SacDeGraines[numeroAPlanter].Espece, y, x, simu);
        }

    }

    private void Arroser()
    {
        Console.WriteLine("---");
        Console.WriteLine("Voici l'état d'humidité de vos plantes : ");
        int numero = 0;
        foreach (Plante plante in Pot.ListePlantes)
        {
            if (plante.CoorX != -1 && plante.CoorY != -1)
            {
                Console.WriteLine($"- {numero}. {plante.Espece} : niveau actuel : {plante.NiveauHumidite} | niveau optimal : {plante.SeuilHumidite} | diminution de l'humidité par tour : {plante.BesoinEau} ");
                numero++;
            }
        }
        Console.WriteLine("---");
        Console.WriteLine("Arroser une plante augmente son niveau d'humidité actuel de 10. ");
        Console.WriteLine("Entrez les numéros des plantes à arroser un par un. Entrez 1000 pour arrêter l'arrosage. ");

        int numeroAArroser = -1;
        bool continuer = true;
        while (continuer)
        {
            string reponse = Console.ReadLine()!;
            if (Int32.TryParse(reponse, out numeroAArroser))
            {
                if (numeroAArroser == 1000)
                    continuer = false;
                else if (numeroAArroser >= 0 && numeroAArroser < Pot.ListePlantes.Count)
                {
                    Pot.ListePlantes[numeroAArroser].NiveauHumidite += 10;
                }
                else
                {
                    Console.WriteLine("Numéro invalide. Essayez encore.");
                }
            }
            else
            {
                Console.WriteLine("Réponse invalide. Entrez un numéro valide ou 1000 pour arrêter.");
            }
        }
    }

    private void MajBesoinEau()
    {
        foreach (Plante plante in Pot.ListePlantes)
        {
            if (plante.NiveauHumidite <= 0)
            {
                plante.NiveauHumidite = 0;
            }
            else
                plante.NiveauHumidite -= plante.BesoinEau;
        }
    }

    private void VerifierEsperanceDeVie(Plante plante)
    {
        if (plante.EsperanceDeVie < NumeroTour - plante.TourPlantation)
            plante.EstMorte();
    }


    // Achats : 
    private void AjouterAchat(int numero, int quantite)
    {
        ListeAchats[numero] += quantite;
    }


    private int DemanderNombreAchats()
    {
        Console.WriteLine("Combien d'unités voulez-vous acheter ?");
        int nombreUnites = -1;
        string reponse = Console.ReadLine()!;
        while ((!Int32.TryParse(reponse, out nombreUnites)) || (nombreUnites < 0))
        {
            Console.WriteLine("Réponse invalide ");
            Console.WriteLine("Entrez le nombre d'unités.");
            reponse = Console.ReadLine()!;
        }
        return nombreUnites;
    }

    private bool PayerAchat(double prixTotal)
    {
        Console.WriteLine($"Le prix pour cet achat est {prixTotal}");
        Console.WriteLine($"Vous avez un solde de {Argent}");
        if (Argent - prixTotal < 0)
        {
            Console.WriteLine("Vous n'avez pas assez d'argent pour effectuer l'achat.");
            return false;
        }
        else
        {
            Console.WriteLine("Confirmez l'achat : Entrez Oui ou Non");
            string reponse = Console.ReadLine()!;
            ChoixOuiNon choix;
            while (!Enum.TryParse<ChoixOuiNon>(reponse, true, out choix) || int.TryParse(reponse, out _))
            {
                Console.WriteLine("Entrée invalide. Veuillez saisir un choix valide : Oui, Non");
                reponse = Console.ReadLine()!;
            }
            if (choix == ChoixOuiNon.Oui)
            {
                Argent -= prixTotal;
                Console.WriteLine($"Vous avez maintenant un solde de {Argent}");
                return true;
            }
            else
            {
                return false;
            }
        }
    }

    private void AcheterGraine(Simulation simu)
    {
        int numero = 0;
        Console.WriteLine("Vous pouvez acheter les graines suivantes :");
        foreach (Graine graine in Pot.SacDeGraines)
        {
            Console.WriteLine($"- {numero}. {graine.Espece} : {graine.Quantite} unités");
            numero++;
        }
        Console.WriteLine($"- {numero}. Retour à la liste des achats");
        Console.WriteLine("Quel est le numéro de la graine que vous voulez acheter ? ");
        string reponse = Console.ReadLine()!;
        int numeroAAcheter;
        while (!Int32.TryParse(reponse, out numeroAAcheter) || (numeroAAcheter < 0) || (numeroAAcheter > Pot.SacDeGraines.Count))
        {
            Console.WriteLine("Vous n'avez pas entré un nombre valide. Quel est le numéro de la graine que vous voulez acheter ? ");
            reponse = Console.ReadLine()!;
        }
        if (numeroAAcheter == numero)
        {
            Acheter(simu);
            return;
        }
        int nombreUnites = DemanderNombreAchats();
        double prixTotal = 0.20 * nombreUnites;
        bool acheter = PayerAchat(prixTotal);
        if (acheter)
        {
            Pot.SacDeGraines[numeroAAcheter].Quantite += nombreUnites;
        }
    }

    private void EffectuerAchat(int numero, Simulation simu)
    {
        Achats achatSouhaite = achatsPossibles[numero];
        if (achatSouhaite.Nature == Natures.Remede)
        {
            Console.WriteLine("Pour information, le remède traite tout le potager lorsqu'on l'utilise");
        }
        double prixUnitaire = 0;
        if (achatSouhaite.PrixVariant)
        {
            prixUnitaire = achatSouhaite.Prix * Pot.Hauteur * Pot.Longueur;
            Console.WriteLine($"Le prix de cet achat dépend de la taille du potager. Pour ce potager, le prix par utilisation est {prixUnitaire}");
        }
        else
        {
            prixUnitaire = achatSouhaite.Prix;
            Console.WriteLine($"Le prix par item est {prixUnitaire}");
        }
        int nombreUnites = 1;

        if (achatSouhaite.Nature != Natures.Graine)
        {
            if (achatSouhaite.Nom != Achat.Chien)
            {
                nombreUnites = DemanderNombreAchats();
            }
            else
            {
                Console.WriteLine("Vous ne pouvez acheter qu'un chien. ");
            }
            double prixTotal = prixUnitaire * nombreUnites;
            bool achatConfirme = PayerAchat(prixTotal);
            if (achatConfirme)
            {
                AjouterAchat(numero, nombreUnites);
                if (achatSouhaite.Nom == Achat.Chien)
                {
                    PresenceChien = true;
                    CreerAnimal("Chien", simu);
                    Console.WriteLine(" Vous possédez maintenant un chien");
                }
            }
        }
        else
        {
            AcheterGraine(simu);
        }
    }

    private void Acheter(Simulation simu)
    {
        Console.WriteLine($" Vous possédez {Argent} ");
        Console.WriteLine(" Vous pouvez achetez : \n1. Un arrosage automatique \n2. Une bache \n3. Des coccinelles \n4. Un chien \n5. Un epouvantail \n6. Du fertilisant \n7. Des graines \n8. Des lampes UV \n9. Une pompe \n10. Une serre\n11. Un tuyau d'arrosage \n12. Du remede anti Fusariose \n13. Du remede anti Mildiou \n14. Du remede anti Oidium ");
        Console.WriteLine("Entrez les numéros des achats que vous souhaitez faire un par un. Entrez 1000 pour arreter les achats. ");
        string reponse = Console.ReadLine()!;
        int numeroAAcheter = -1;
        while (numeroAAcheter != 1000)
        {
            while ((!Int32.TryParse(reponse, out numeroAAcheter)) || (numeroAAcheter < 0) || ((numeroAAcheter >= 15) && (numeroAAcheter != 1000)))
            {
                Console.WriteLine("Réponse invalide ");
                Console.WriteLine("Entrez les numéros des achats que vous souhaitez faire un par un. Entrez 1000 pour arreter les achats. ");
                reponse = Console.ReadLine()!;
            }
            if (numeroAAcheter != 1000)
            {
                if (PresenceChien && (numeroAAcheter - 1 == 3))
                {
                    Console.WriteLine("Vous ne pouvez acheter qu'un chien. Effectuez un autre achat.");
                    return;
                }
                else
                {
                    EffectuerAchat(numeroAAcheter - 1, simu);
                    break;
                }
            }
        }
        if (ListeAchats[4] != 0) { PresenceEpouvantail = true; }
    }

    private void PoserAchat(Simulation simu)
    {
        bool presenceAchat = false;
        for (int i = 0; i < ListeAchats.Count(); i++)
        {
            //On ignore les items que l'on ne peut pas poser
            if ((i != 1) && (i != 3) && (i != 6) && (i != 8) && (i != 10))
            {
                if (ListeAchats[i] > 0)
                {
                    presenceAchat = true;
                }
            }
        }
        if (!presenceAchat)
        {
            Console.WriteLine("Vous n'avez aucun item à poser.");
        }
        else
        {
            int numero = 0;
            Console.WriteLine("Vous possédez les items suivants :");
            foreach (int nombreAchat in ListeAchats)
            {
                if ((nombreAchat != 0) && (numero != 3) && (numero != 6) && (numero != 1) && (numero != 8) && (numero != 10)) //On ne peut pas poser : chien (3), graine (6), bache (1), pompe (8), tuyau d'arrosage (10)
                {
                    Console.WriteLine($"- {numero}. {achatsPossibles[numero].Nom} : {nombreAchat} unités");
                }
                numero++;
            }
            Console.WriteLine("Vous ne pouvez utiliser les baches, pompes et tuyau d'arrosage qu'en cas d'intempéries ; ils n'apparaissent pas dans la liste ci-dessus.");
            Console.WriteLine("Même chose pour le chien et l'épouvantail qui ne sont utilisable qu'en cas d'animaux urgent à faire fuir");
            Console.WriteLine("Pour planter une graine, référez vous à l'action (1) du menu principal");
            Console.WriteLine("Quel est le numéro de l'achat que vous voulez utiliser ? ");
            string reponse = Console.ReadLine()!;
            int numeroAPoser;
            bool achatPermis = false;
            do
            {
                while (!Int32.TryParse(reponse, out numeroAPoser) || (numeroAPoser < 0) || (numeroAPoser >= ListeAchats.Count))
                {
                    Console.WriteLine("Vous n'avez pas entré un nombre valide. Quel est le numéro de l'achat que vous voulez utiliser ? ");
                    reponse = Console.ReadLine()!;
                }
                if (ListeAchats[numeroAPoser] > 0) achatPermis = true;
                if (!achatPermis)
                {
                    Console.WriteLine("Vous pouvez poser uniquement ce que vous possédez. Quel est le numéro de l'achat que vous voulez utiliser ? ");
                    reponse = Console.ReadLine()!;
                }
            } while (!achatPermis);
            if (numeroAPoser == 0)
            {
                Pot.EffetArrosageAutomatique();
                Console.WriteLine("Vous avez installé un arrosage automatique.");
            }
            else if (numeroAPoser == 2)
            {
                PoserCoccinelle(simu);
                Console.WriteLine("Vous avez posé des coccinelles.");
            }
            else if (numeroAPoser == 4)
            {
                PresenceEpouvantail = true;
                Console.WriteLine("Vous avez posé un épouvantail.");
            }
            else if (numeroAPoser == 5)
            {
                Pot.EffetFertilisant();
                Console.WriteLine("Vous avez choisi le fertilisant, il a amélioré la production maximum de toutes les plantes du potager.");
            }
            else if (numeroAPoser == 7)
            {
                PresenceLampeUV = true;
                Console.WriteLine("Vous avez posé des lampes UV.");
            }
            else if (numeroAPoser == 9)
            {
                PresenceSerre = true;
                Console.WriteLine("Vous avez posé une serre.");
            }
            else if ((numeroAPoser == 11) || (numeroAPoser == 12) || (numeroAPoser == 13))
            {
                Console.WriteLine("Vous avez posé un  remède.");
                Pot.EffetPoserRemede(numeroAPoser); //Le Console.WriteLine pour dire qu'on a utilisé un remède est dans la méthode Pot.EffetPoserRemede.
            }
            ObjetsPoses.Add(achatsPossibles[numeroAPoser]);
        }
    }

    private void ImpactAchatPose() //Effectue l'impact pour les achats déjà posé
    {
        if (PresenceLampeUV) Pot.EffetLampeUV();
        if (PresenceArrosageAutomatique) Pot.EffetArrosageAutomatique();
        if (PresenceSerre) Pot.EffetSerre();
    }


    // Initialisation, actions et affichage : 
    private void InitialisationPotager(Potager Pot, string[,] grille)
    {
        for (int i = 0; i < Pot.Hauteur; i++)
        {
            for (int j = 0; j < Pot.Longueur; j++)
            {
                grille[i, j] = " 🔳 ";
            }
        }
    }

    private void MajConditionsPotager()
    {
        Console.WriteLine("---");
        Console.WriteLine("-- Statuts du potager --");
        Console.WriteLine($"Saison : {Pot.Saison.Nom}, Température : {Pot.Temperature}, Luminosité : {Pot.Luminosite}");
        Console.WriteLine("---");

    }


    public void MajAffichagePlantes(string[,] grille)

    {
        InitialisationPotager(Pot, grille);
        foreach (Plante plante in Pot.ListePlantes)
        {
            if (plante.CoorX != -1 && plante.CoorY != -1)
            {
                if ((plante.Espece == "Artichaut") && (plante.Taille == 1)) grille[plante.CoorY, plante.CoorX] = " atc";
                if ((plante.Espece == "Artichaut") && (plante.Taille == 2)) grille[plante.CoorY, plante.CoorX] = " ATC";
                if ((plante.Espece == "Artichaut") && (plante.Taille == 3)) grille[plante.CoorY, plante.CoorX] = " 🌲 ";
                if ((plante.Espece == "Artichaut") && (plante.Taille == plante.TailleMax)) grille[plante.CoorY, plante.CoorX] = " 🥦 ";

                if ((plante.Espece == "Aubergine") && (plante.Taille == 1)) grille[plante.CoorY, plante.CoorX] = " aub";
                if ((plante.Espece == "Aubergine") && (plante.Taille == 2)) grille[plante.CoorY, plante.CoorX] = " AUB";
                if ((plante.Espece == "Aubergine") && (plante.Taille == 3)) grille[plante.CoorY, plante.CoorX] = "🌾";
                if ((plante.Espece == "Aubergine") && (plante.Taille == plante.TailleMax)) grille[plante.CoorY, plante.CoorX] = " 🍆 ";

                if ((plante.Espece == "Basilic") && (plante.Taille == 1)) grille[plante.CoorY, plante.CoorX] = " bsl";
                if ((plante.Espece == "Basilic") && (plante.Taille == plante.TailleMax)) grille[plante.CoorY, plante.CoorX] = " 🪴 ";

                if ((plante.Espece == "Oignon") && (plante.Taille == 1)) grille[plante.CoorY, plante.CoorX] = " ogn";
                if ((plante.Espece == "Oignon") && (plante.Taille == plante.TailleMax)) grille[plante.CoorY, plante.CoorX] = " 🧅 ";

                if ((plante.Espece == "Olivier") && (plante.Taille == 1)) grille[plante.CoorY, plante.CoorX] = " olv";
                if ((plante.Espece == "Olivier") && (plante.Taille == 2)) grille[plante.CoorY, plante.CoorX] = " OLV";
                if ((plante.Espece == "Olivier") && (plante.Taille == 3)) grille[plante.CoorY, plante.CoorX] = " 🌿 ";
                if ((plante.Espece == "Olivier") && (plante.Taille == 4)) grille[plante.CoorY, plante.CoorX] = " 🌳 ";
                if ((plante.Espece == "Olivier") && (plante.Taille == plante.TailleMax)) grille[plante.CoorY, plante.CoorX] = " 🫒 ";

                if ((plante.Espece == "Poivron") && (plante.Taille == 1)) grille[plante.CoorY, plante.CoorX] = " pvr";
                if ((plante.Espece == "Poivron") && (plante.Taille == 2)) grille[plante.CoorY, plante.CoorX] = " PVR";
                if ((plante.Espece == "Poivron") && (plante.Taille == plante.TailleMax)) grille[plante.CoorY, plante.CoorX] = " 🫑 ";

                if ((plante.Espece == "Roquette") && (plante.Taille == 1)) grille[plante.CoorY, plante.CoorX] = " rqt";
                if ((plante.Espece == "Roquette") && (plante.Taille == 2)) grille[plante.CoorY, plante.CoorX] = " RQT";
                if ((plante.Espece == "Roquette") && (plante.Taille == plante.TailleMax)) grille[plante.CoorY, plante.CoorX] = " 🥬 ";

                if ((plante.Espece == "Thym") && (plante.Taille == 1)) grille[plante.CoorY, plante.CoorX] = " thy";
                if ((plante.Espece == "Thym") && (plante.Taille == plante.TailleMax)) grille[plante.CoorY, plante.CoorX] = " 🌱 ";

                if ((plante.Espece == "Tomate") && (plante.Taille == 1)) grille[plante.CoorY, plante.CoorX] = " tmt";
                if ((plante.Espece == "Tomate") && (plante.Taille == 2)) grille[plante.CoorY, plante.CoorX] = " TMT";
                if ((plante.Espece == "Tomate") && (plante.Taille == plante.TailleMax)) grille[plante.CoorY, plante.CoorX] = " 🍅 ";
            }
        }
    }

    public List<string> MajAffichageAnimaux(string[,] grille)
    {
        List<string> listeAnimauxAfficher = new List<string>();
        listeAnimauxAfficher.Add("Certains animaux ne sont pas visibles sur la grille car ils sont sur des plantes ou d'autres animaux : ");
        string emoji = "";
        foreach (Animaux animal in Pot.ListeAnimaux)
        {
            if ((animal.X != -1) && (animal.Y != -1))
            {
                if (animal.Nom == "Chien") emoji = "🐕";
                if (animal.Nom == "Abeille") emoji = "🐝";
                if (animal.Nom == "Chat") emoji = "🐈";
                if (animal.Nom == "Coccinelle") emoji = "🐞";
                if (animal.Nom == "Escargot") emoji = "🐌";
                if (animal.Nom == "Oiseau") emoji = "‍🐦";
                if (animal.Nom == "Pucerons") emoji = "🦗";
                if (animal.Nom == "Rongeur") emoji = "🐀";
                if (animal.Nom == "VersDeTerre") emoji = "🪱 ";
                if (grille[animal.X, animal.Y] == " 🔳 ")
                {
                    grille[animal.X, animal.Y] = " " + emoji + " ";
                }
                else // Cette partie permet de ne pas surcharger l'affichage.
                {
                    listeAnimauxAfficher.Add($"- {emoji} | ligne : {animal.X}, colonne : {animal.Y}");
                }
            }
        }
        return listeAnimauxAfficher;
    }


    private void ChoisirActionsTour(ref bool jeuEnCours, ref string[,] grille, Simulation simu)
    {
        int reponse;
        do
        {
            ApparaitreHasardAnimal(simu);
            MajAffichagePlantes(grille);
            List<string> ajoutAffichage = MajAffichageAnimaux(grille);
            AffichageComplet(grille);
            if (ajoutAffichage.Count >= 2)
            {
                foreach (string message in ajoutAffichage)
                {
                    Console.WriteLine(message);
                }
            }
            Console.WriteLine("Choisissez une action du menu principal :");
            string rep = Console.ReadLine()!;
            while (!int.TryParse(rep, out reponse) || reponse > 7 || reponse <= 0)
            {
                Console.WriteLine("Vous n'avez pas entré un nombre valide. Que voulez-vous faire ? ");
                rep = Console.ReadLine()!;
            }
            if (reponse == 1) Planter(simu);
            if (reponse == 2) Acheter(simu);
            if (reponse == 3) Arroser();
            if (reponse == 4) PoserAchat(simu);
            if (reponse == 5) AfficherWiki();
        }
        while (reponse != 6 && reponse != 7);
        if (reponse == 6)
        {
            NumeroTour += 1;
            Argent += 50;
        }
        if (reponse == 7) jeuEnCours = false;
    }

    private void AfficherWiki()
    {
        Console.WriteLine("Bienvenue dans le Wiki, tu trouveras ici toutes les informations nécessaires pour prendre soin de ton super potager !");
        Console.WriteLine(@"
╔═════════════════════════════════════════════════════════╦════════════╦════════════╦════════════╦════════════╗
║ Plantes Vivaces                                         ║ Artichaut  ║ Aubergine  ║  Olivier   ║ Thym       ║
╠═════════════════════════════════════════════════════════╬════════════╬════════════╬════════════╬════════════╣
║ Terrain préféré                                         ║ Terre      ║ Terre      ║ Terre      ║ Calcaire   ║
║ Saison de semis                                         ║ Printemps  ║ Printemps  ║ Automne    ║ Printemps  ║
║ Saison de récolte                                       ║ Automne    ║ Été        ║ Automne    ║ Été        ║
║ Espacement (1 = 50 cm)                                  ║ 3          ║ 1          ║ 14         ║ 1          ║
║ Quota Croissance (somme taille max des plantes autour)  ║ 10         ║ 20         ║ 8          ║ 30         ║
║ Taille maximale de la plante                            ║ 4          ║ 4          ║ 5          ║ 2          ║
║ Temps de croissance (tours)                             ║ 3          ║ 3          ║ 12         ║ 3          ║
║ Humidité préférée                                       ║ 60%        ║ 50%        ║ 40%        ║ 50%        ║
║ Luminosité préférée                                     ║ 90%        ║ 80%        ║ 85%        ║ 90%        ║
║ Température préférée                                    ║ 15-25°C    ║ 20-28°C    ║ 20-30°C    ║ 15-25°C    ║
╠═════════════════════════════════════════════════════════╬════════════╬════════════╬════════════╬════════════╣
║ Maladies que la plante peut attraper                    ║ Mildiou    ║ Mildiou    ║ Mildiou    ║ Oidium     ║
║ Probabilité d’attraper ces maladies                     ║ 15%        ║ 30%        ║ 40%        ║ 15%        ║
╠═════════════════════════════════════════════════════════╬════════════╬════════════╬════════════╬════════════╣
║ Espérance de vie (nb de tours)                          ║ 55         ║ 40         ║ 200        ║ 44         ║
║ Quantité produite par plant                             ║ 5          ║ 2          ║ 5000       ║ 20         ║
║ Récoltes possibles par saison                           ║ 2          ║ 3          ║ 1          ║ 2          ║
╚═════════════════════════════════════════════════════════╩════════════╩════════════╩════════════╩════════════╝
");

        Console.WriteLine(@"
╔═════════════════════════════════════════════════════════╦════════════╦═════════╦══════════╦══════════╦══════════╗
║ Plantes Annuelles                                       ║ Basilic    ║ Oignon  ║ Poivron  ║ Roquette ║ Tomate   ║
╠═════════════════════════════════════════════════════════╬════════════╬═════════╬══════════╬══════════╬══════════╣
║ Terrain préféré                                         ║ Terre      ║ Terre   ║ Terre    ║ Terre    ║ Terre    ║
║ Saison de semis                                         ║ Printemps  ║ Automne ║ Hiver    ║ Été      ║ Printemps║
║ Saison de récolte                                       ║ Été        ║ Été     ║ été      ║ Automne  ║ Été      ║
║ Espacement (1 = 50 cm)                                  ║ 0          ║ 0       ║ 1        ║ 0        ║ 1        ║
║ Quota Croissance (somme taille max des plantes autour)  ║ 18         ║ 20      ║ 15       ║ 25       ║ 30       ║
║ Taille maximale de la plante                            ║ 2          ║ 2       ║ 3        ║ 3        ║ 3        ║
║ Temps de croissance (tours)                             ║ 3          ║ 3       ║ 2        ║ 2        ║ 4        ║
║ Besoins en eau                                          ║ 5          ║ 5       ║ 10       ║ 5        ║ 10       ║
║ Humidité préférée                                       ║ 60%        ║ 70%     ║ 80%      ║ 65%      ║ 80%      ║
║ Luminosité préférée                                     ║ 85%        ║ 90%     ║ 90%      ║ 70%      ║ 90%      ║
║ Zone de température préférée                            ║ 20-25°C    ║ 5-38°C  ║ 20-28°C  ║ 10-20°C  ║ 15-30°C  ║
╠═════════════════════════════════════════════════════════╬════════════╬═════════╬══════════╬══════════╬══════════╣
║ Maladies que la plante peut attraper                    ║ Fusariose, ║ Mildiou ║ Mildiou, ║ Mildiou  ║ Mildiou, ║
║                                                         ║ Mildiou,   ║         ║ Oidium   ║          ║ Oidium   ║
║                                                         ║ Oidium     ║         ║          ║          ║          ║
╠═════════════════════════════════════════════════════════╬════════════╬═════════╬══════════╬══════════╬══════════╣
║ Probabilités d’attraper ces maladies                    ║ 35%,10%,20%║ 20%     ║ 35%,25%  ║ 30%      ║ 50%,20%  ║
╠═════════════════════════════════════════════════════════╬════════════╬═════════╬══════════╬══════════╬══════════╣
║ Espérance de vie (nb de tours)                          ║ 12         ║ 12      ║ 12       ║ 12       ║ 12       ║
║ Quantité produite par plant                             ║ 20         ║ 1       ║ 6        ║ 10       ║ 30       ║
║ Récoltes possibles par saison                           ║ 3          ║ 1       ║ 3        ║ 3        ║ 3        ║
╚═════════════════════════════════════════════════════════╩════════════╩═════════╩══════════╩══════════╩══════════╝
");

        Console.WriteLine(@"
╔════════════════════╦═══════════════════╦══════════════════════╦═════════════════════╦═══════════════════╗
║ Animaux bééfiques  ║ Abeille           ║ Chien                ║ Coccinelle          ║ Vers de terre     ║
╠════════════════════╬═══════════════════╬══════════════════════╬═════════════════════╬═══════════════════╣
║ Probabilité        ║ 15%               ║ 5%                   ║ 14%                 ║ 20%               ║
║ Durée (nb tours)   ║ Infini            ║ Infini               ║ 3                   ║ Infini            ║
║ Urgence            ║ Non               ║ Non                  ║ Non                 ║ Non               ║
║ Prédateurs         ║ Aucun             ║ Aucun                ║ Aucun               ║ Oiseau            ║
╠════════════════════╬═══════════════════╬══════════════════════╬═════════════════════╬═══════════════════╣
║ Effet              ║ Augmente la santé ║ Empêche l’apparition ║ Chasse les pucerons ║ Augmente le score ║
║                    ║ de la plante de 5 ║ de chats et de       ║                     ║ de terrain de la  ║
║                    ║                   ║ rongeurs             ║                     ║ plante de 5       ║
╚════════════════════╩═══════════════════╩══════════════════════╩═════════════════════╩═══════════════════╝
");

        Console.WriteLine(@"
╔════════════════════╦══════════╦════════╦════════════╦══════════╦════════════════════════════════╗
║ Animaux Néfastes   ║ Escargot ║ Oiseau ║ Pucerons   ║ Rongeur  ║ Chat                           ║
╠════════════════════╬══════════╬════════╬════════════╬══════════╬════════════════════════════════╣
║ Probabilité        ║ 12%      ║ 20%    ║ 16%        ║ 10%      ║ 7%                             ║
║ Durée (nb tours)   ║ 8        ║ /      ║ 3          ║ /        ║ /                              ║
║ Urgence            ║ Non      ║ Oui    ║ Non        ║ Oui      ║ Oui                            ║
║ Prédateurs         ║ Aucun    ║ Chat   ║ Coccinelle ║ Chat     ║ Aucun                          ║
╠════════════════════╬══════════╩════════╩════════════╩══════════╬════════════════════════════════╣
║ Effet              ║ Mange la plante sur laquelle il se trouve ║ Ecrase les plantes de taille 1 ║
╚════════════════════╩═══════════════════════════════════════════╩════════════════════════════════╝
");




        Console.WriteLine("");
        Console.WriteLine("Entre 1000 pour revenir au menu principal.");

        bool continuer = true;
        int saisie;
        while (continuer)
        {
            string reponse = Console.ReadLine()!;
            if (Int32.TryParse(reponse, out saisie))
            {
                if (saisie == 1000)
                    continuer = false;
                else
                    Console.WriteLine("Tape 1000 pour revenir au menu principal.");
            }
            else
            {
                Console.WriteLine("Réponse invalide. Entrez un numéro valide ou 1000 pour arrêter.");
            }
        }

    }

    private void AffichageComplet(string[,] grille)
    {
        Console.WriteLine("--- Statuts du potager ---");
        Console.WriteLine($"Saison : {Pot.Saison.Nom}, Température : {Pot.Temperature}, Luminosité : {Pot.Luminosite}");
        Console.WriteLine();

        //Affichage Potager 

        List<string> lignesPotager = new List<string>();
        //Ajout numéros de colonne
        string numColonnes = "    "; // Espace pour l'alignement avec les numéros de ligne
        for (int j = 0; j < Pot.Longueur; j++)
        {
            numColonnes += $"{j,3} "; //Affiche les nums sur 3 caractères pour alignement
        }
        lignesPotager.Add(numColonnes);
        lignesPotager.Add("");

        for (int i = 0; i < Pot.Hauteur; i++)
        {
            string ligne = $"{i,2}  ";
            for (int j = 0; j < Pot.Longueur; j++)
            {
                ligne += grille[i, j];
            }
            lignesPotager.Add(ligne);
            lignesPotager.Add("");
        }
        lignesPotager.Add("");
        lignesPotager.Add("-- Menu Principal --");
        lignesPotager.Add("(1) Planter une graine");
        lignesPotager.Add("(2) Faire un Achat");
        lignesPotager.Add("(3) Arroser");
        lignesPotager.Add("(4) Poser un item");
        lignesPotager.Add("(5) Afficher le Wiki");
        lignesPotager.Add("(6) Avancer dans le temps");
        lignesPotager.Add("(7) Quitter le jeu");

        // status, récoltes
        List<string> lignesDroite = new List<string>();
        lignesDroite.Add("-- Récoltes : --");
        foreach (Recolte recolte in Pot.Inventaire)
        {
            if (recolte.Quantite != 0)
                lignesDroite.Add($"- {recolte.Espece} : {recolte.Quantite}");
        }
        lignesDroite.Add("");
        lignesDroite.Add("-- Statuts des plantes : --");
        foreach (Plante plante in Pot.ListePlantes)
        {
            if (plante.CoorX != -1 && plante.CoorY != -1)
            {
                plante.NiveauTemperature = Pot.Temperature;
                plante.NiveauLuminosite = Pot.Luminosite;
                lignesDroite.Add($"Statuts {plante.Espece} : Taille :{plante.Taille}, Santé {plante.Sante} ");
                lignesDroite.Add($"  | Humidité {plante.NiveauHumidite}, Luminosité {plante.NiveauLuminosite}, Température : {plante.NiveauTemperature}");
            }
        }


        int largeurAffichage = Pot.Longueur * 5;
        int maxLignes = Math.Max(lignesPotager.Count, lignesDroite.Count);

        for (int i = 0; i < maxLignes; i++)
        {
            string gauche = i < lignesPotager.Count ? lignesPotager[i] : "";
            string droite = i < lignesDroite.Count ? lignesDroite[i] : "";
            Console.WriteLine(string.Format("{0,-" + largeurAffichage + "}   {1}", gauche, droite));
        }

        Console.WriteLine("");
        Console.WriteLine("Objets posés :");
        foreach (Achats obj in ObjetsPoses)
        {
            Console.WriteLine($"     - {obj.Nom} ");
        }
        Console.WriteLine("");
    }

    private void TirerAuSortIntemperie(Simulation simu, Grele grele, Inondation inondation, Secheresse secheresse, Saison saison, string[,] grille)
    {
        Random rng = new Random();
        int probaGrele = 10;
        int probaInondation = 10;
        int probaSecheresse = 0;
        if (saison == Saison.Hiver)
            probaGrele = 20;
        if (saison == Saison.Ete)
            probaSecheresse = 25;
        if (saison == Saison.Automne || saison == Saison.Printemps)
            probaInondation = 20;
        if (simu.PresenceArrosageAutomatique)
            probaSecheresse = 0;  //Les plantes ne sont plus affectées => plus d'urgence
        int tirageGrele = rng.Next(0, 101);
        if (tirageGrele < probaGrele) // Inférieur stricte pour si proba de 0
        {
            Grele = true;
            simu.mode = ModeDeJeu.Urgence;
            AffichageUrgence(ref grille, grele, simu.ActionUrgente, simu.Pot, simu);
        }
        int tirageInondation = rng.Next(0, 101);
        if (tirageInondation < probaInondation)
        {
            Inondation = true;
            simu.mode = ModeDeJeu.Urgence;
            AffichageUrgence(ref grille, inondation, simu.ActionUrgente, simu.Pot, simu);
        }
        int tirageSecheresse = rng.Next(0, 101);
        if (tirageSecheresse < probaSecheresse)
        {
            Secheresse = true;
            simu.mode = ModeDeJeu.Urgence;
            AffichageUrgence(ref grille, secheresse, simu.ActionUrgente, simu.Pot, simu);
        }
    }

    private void TirerAuSortAnimauxUrgents(Simulation simu, ref Oiseau oiseau, ref Chat chat, ref Rongeur rongeur, string[,] grille)
    {
        Random rng = new Random();
        int probaOiseau = 20;
        int probaChat = 7;
        int probaRongeur = 10;
        if (simu.PresenceEpouvantail)
            probaOiseau = 0;
        if (simu.PresenceChien)
            probaRongeur = 0;
        int tirageOiseau = rng.Next(0, 101);
        if (tirageOiseau < probaOiseau)
        {
            oiseau.X = rng.Next(0, Pot.Hauteur);
            oiseau.Y = rng.Next(0, Pot.Longueur);
            simu.mode = ModeDeJeu.Urgence;
            AffichageUrgence(ref grille, oiseau, simu.ActionUrgente, simu.Pot, simu);
        }
        int tirageChat = rng.Next(0, 101);
        if (tirageChat < probaChat)
        {
            chat.X = rng.Next(0, Pot.Hauteur);
            chat.Y = rng.Next(0, Pot.Longueur);
            simu.mode = ModeDeJeu.Urgence;
            AffichageUrgence(ref grille, chat, simu.ActionUrgente, simu.Pot, simu);
        }
        int tirageRongeur = rng.Next(0, 101);
        if (tirageRongeur <= probaRongeur)
        {
            rongeur.X = rng.Next(0, Pot.Hauteur);
            rongeur.Y = rng.Next(0, Pot.Longueur);
            simu.mode = ModeDeJeu.Urgence;
            AffichageUrgence(ref grille, rongeur, simu.ActionUrgente, simu.Pot, simu);
        }
    }

    private void AffichageUrgence(ref string[,] grille, object pb, ActionUrgente actionUrgente, Potager pot, Simulation simu)
    {
        int timeoutMs = 3000;
        int tickMs = 500;
        actionUrgente.GererUrgenceAvecTimeout(pb, pot, simu, grille, timeoutMs, tickMs);
    }

    public void Simuler(Potager pot, Simulation simu)
    {
        bool jeuEnCours = true;
        // Création des récoltes
        Recolte RecArtichaut = new Recolte("Artichaut", 0);
        Recolte RecAubergine = new Recolte("Aubergine", 0);
        Recolte RecBasilic = new Recolte("Basilic", 0);
        Recolte RecOignon = new Recolte("Oignon", 0);
        Recolte RecOlivier = new Recolte("Olive", 0);
        Recolte RecPoivron = new Recolte("Poivron", 0);
        Recolte RecRoquette = new Recolte("Roquette", 0);
        Recolte RecThym = new Recolte("Thym", 0);
        Recolte RecTomate = new Recolte("Tomate", 0);

        //Ajout à l'inventaire
        Pot.Inventaire.Add(RecArtichaut);
        Pot.Inventaire.Add(RecAubergine);
        Pot.Inventaire.Add(RecBasilic);
        Pot.Inventaire.Add(RecOignon);
        Pot.Inventaire.Add(RecOlivier);
        Pot.Inventaire.Add(RecPoivron);
        Pot.Inventaire.Add(RecRoquette);
        Pot.Inventaire.Add(RecThym);
        Pot.Inventaire.Add(RecTomate);

        // Création des intempéries et animaux pour le mode urgence
        Grele grele = new Grele(Pot);
        Inondation inondation = new Inondation(Pot);
        Secheresse secheresse = new Secheresse(Pot);
        ActionUrgente = new ActionUrgente();
        Oiseau oiseau = new Oiseau(Pot, simu);
        oiseau.X = -1;
        oiseau.Y = -1;
        Pot.ListeAnimaux.Add(oiseau);
        Chat chat = new Chat(Pot, simu);
        chat.X = -1;
        chat.Y = -1;
        Pot.ListeAnimaux.Add(chat);
        Rongeur rongeur = new Rongeur(Pot, simu);
        rongeur.X = -1;
        rongeur.Y = -1;
        Pot.ListeAnimaux.Add(rongeur);

        string[,] GrillePotager = new string[pot.Hauteur, pot.Longueur];
        InitialisationPotager(pot, GrillePotager);

        while (jeuEnCours)
        {
            if (simu.mode == ModeDeJeu.Classique)
            {
                EvolutionAnimaux();
                MajAffichagePlantes(GrillePotager);
                List<string> ajoutAffichage = MajAffichageAnimaux(GrillePotager);
                MajBesoinEau();

                foreach (Plante plante in pot.ListePlantes)
                {
                    if ((plante.CoorX != -1) && (plante.CoorY != -1))
                    {
                        plante.MettreAJourPlantesAutour();
                        VerifierEsperanceDeVie(plante);
                        ImpactAchatPose();
                        plante.ImpactConditions();
                        plante.ProbabiliteTomberMalade();
                        plante.Contamination();
                        if (NumeroTour % plante.TempsCroissance == 0)
                            plante.Grandir();
                        if (pot.Saison.Nom == plante.SaisondeRecolte && plante.NbRecolte < plante.NbRecoltePossible)
                            plante.DonnerRecolte(pot, AssocierRecoltePlante(plante, RecArtichaut, RecAubergine, RecBasilic, RecOignon, RecOlivier, RecPoivron, RecRoquette, RecThym, RecTomate));
                        Console.WriteLine(plante);
                    }
                }
                if (NumeroTour % 12 == 1)
                {
                    pot.Saison.Nom = Saison.Printemps;
                    Pot.Saison.ChangerBesoinEau();
                    Pot.Saison.ChangerTemperature();
                }
                if (NumeroTour % 12 == 4)
                {
                    pot.Saison.Nom = Saison.Ete;
                    Pot.Saison.ChangerBesoinEau();
                    Pot.Saison.ChangerTemperature();
                }
                if (NumeroTour % 12 == 7)
                {
                    pot.Saison.Nom = Saison.Automne;
                    Pot.Saison.ChangerBesoinEau();
                    Pot.Saison.ChangerTemperature();
                }
                if (NumeroTour % 12 == 10)
                {
                    pot.Saison.Nom = Saison.Hiver;
                    Pot.Saison.ChangerBesoinEau();
                    Pot.Saison.ChangerTemperature();
                }
                Pot.Temperature = Pot.Saison.TemperatureDeSaison();
                Pot.Luminosite = Pot.Saison.LuminositeDeSaison();

                ChoisirActionsTour(ref jeuEnCours, ref GrillePotager, simu);
                if (jeuEnCours)
                {
                    Random rng = new Random();
                    if (rng.Next(1, 3) == 1)  //Pour éviter d'avoir les 2 urgences.
                        TirerAuSortAnimauxUrgents(simu, ref oiseau, ref chat, ref rongeur, GrillePotager);
                    else
                        TirerAuSortIntemperie(simu, grele, inondation, secheresse, Pot.Saison.Nom, GrillePotager);
                }
            }

        }
        Console.WriteLine("-- FIN DE LA PARTIE --");
        Console.WriteLine(" Merci d'avoir joué ! A très vite ;)");
    }


}