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
    public double Argent { get; set; }
    public int NumeroTour { get; set; }
    public ModeDeJeu mode { get; set; }
    public List<Achats> achatsPossibles = new List<Achats>();
    public List<int> ListeAchats { get; set; } //Nombre de chaque achat qui n'a pas encore été utilisé pour dans l'odre : Arrosage automatique, Bache, Coccinelle, Chien, Epouvantail, Fertilisant, Graine, LampeUV, Pompe, Serre, tuyau d'arrosage, RemedeFusariose, Remede Mildiou, Remede Oidium

    public bool PresenceChien { get; set; }
    public bool PresenceEpouvantail { get; set; } // Indique si un epouvantail est présent sur le jeu (acheté et posé)
    public bool PresenceArrosageAutomatique { get; set; }
    public bool PresenceLampeUV { get; set; }
    public bool PresenceSerre { get; set; }



    public Simulation(int hauteur, int largeur)
    {
        Saisons saison = new Saisons(Saison.Printemps);
        Pot = new Potager(hauteur, largeur, saison, saison.TemperatureDeSaison()); //Rentrer params
        mode = ModeDeJeu.Classique;
        Argent = 1000;
        NumeroTour = 1;
        ListeAchats = new List<int> { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 };
        PresenceChien = false;
        PresenceEpouvantail = false;
        PresenceArrosageAutomatique = false;
        PresenceLampeUV = false;
        PresenceSerre = false;
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
    }

    public void CreerPlante(string espece, int x, int y, Simulation simu)
    {
        Console.WriteLine("Sur quel terrain voulez-vous la planter ? (Argile, Sable, Terre ou Calcaire)");
        string terrain = Console.ReadLine()!;
        Terrain ter;
        while (!Enum.TryParse<Terrain>(terrain, true, out ter))
        {
            Console.WriteLine("Entrée invalide. Veuillez saisir un terrain valide (Argile, Sable, Terre ou Calcaire) :");
            terrain = Console.ReadLine()!;
        }
        if (espece == "Artichaut") Pot.ListePlantes.Add(new Artichaut(x, y, Pot, ter, simu));
        if (espece == "Aubergine") Pot.ListePlantes.Add(new Aubergine(x, y, Pot, ter, simu));
        if (espece == "Basilic") Pot.ListePlantes.Add(new Basilic(x, y, Pot, ter, simu));
        if (espece == "Oignon") Pot.ListePlantes.Add(new Oignon(x, y, Pot, ter, simu));
        if (espece == "Olivier") Pot.ListePlantes.Add(new Olivier(x, y, Pot, ter, simu));
        if (espece == "Poivron") Pot.ListePlantes.Add(new Poivron(x, y, Pot, ter, simu));
        if (espece == "Roquette") Pot.ListePlantes.Add(new Roquette(x, y, Pot, ter, simu));
        if (espece == "Thym") Pot.ListePlantes.Add(new Thym(x, y, Pot, ter, simu));
        if (espece == "Tomate") Pot.ListePlantes.Add(new Tomate(x, y, Pot, ter, simu));
    }

    public Recolte AssocierRecoltePlante(Plante plante, Recolte RecAr, Recolte RecAu, Recolte RecB, Recolte RecO, Recolte RecOl, Recolte RecP, Recolte RecR, Recolte RecTh, Recolte RecTo)
    {
        if (plante.Espece == "Artichaut") return RecAr;
        if (plante.Espece == "Aubergine") return RecAu;
        if (plante.Espece == "Basilic") return RecB;
        if (plante.Espece == "Oignon") return RecO;
        if (plante.Espece == "Olivier") return RecOl;
        if (plante.Espece == "Poivron") return RecP;
        if (plante.Espece == "Roquette") return RecR;
        if (plante.Espece == "Thym") return RecTh;
        else return RecTo;
    }

    public void Planter(Simulation simu)
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
            string reponse = Console.ReadLine()!;
            int numeroAPlanter;
            while (!int.TryParse(reponse, out numeroAPlanter) || (numeroAPlanter < 0) || (numeroAPlanter >= Pot.SacDeGraines.Count))
            {
                Console.WriteLine("Vous n'avez pas entré un nombre valide. Quel est le numéro de la graine que vous voulez planter ? ");
                reponse = Console.ReadLine()!;
            }
            Console.WriteLine("A quel numéro de ligne voulez-vous la planter ? ");
            string reponseX = Console.ReadLine()!;
            int x;
            while (!int.TryParse(reponseX, out x) || (x < 0) || (x >= Pot.Hauteur))
            {
                Console.WriteLine("Vous n'avez pas entré un numéro de ligne valide. Quel est le numéro de la ligne où vous voulez planter ? ");
                reponseX = Console.ReadLine()!;
            }
            Console.WriteLine("A quel numéro de colonne voulez-vous la planter ? ");
            string reponseY = Console.ReadLine()!;
            int y;
            while (!int.TryParse(reponseY, out y) || (y < 0) || (y >= Pot.Longueur))
            {
                Console.WriteLine("Vous n'avez pas entré un numéro de colonne valide. Quel est le numéro de la colonne où vous voulez planter ? ");
                reponseY = Console.ReadLine()!;
            }
            Pot.SacDeGraines[numeroAPlanter].Quantite--;
            CreerPlante(Pot.SacDeGraines[numeroAPlanter].Espece, x, y, simu);
        }

    }

    public void Arroser()
    {
        Console.WriteLine("---");
        Console.WriteLine("Voici l'état d'humidité de vos plantes : ");
        int numero = 0;
        foreach (Plante plante in Pot.ListePlantes)
        {
            Console.WriteLine($"- {numero}. {plante.Espece} : niveau actuel : {plante.NiveauHumidite} | niveau optimal : {plante.SeuilHumidite} | diminution de l'humidité par tour : {plante.BesoinEau} ");
            numero++;
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

    public void MajBesoinEau()
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

    public void VerifierEsperanceDeVie(Plante plante)
    {
        if (plante.EsperanceDeVie < NumeroTour - plante.TourPlantation)
            plante.EstMorte();
    }

    public void AjouterAchat(int numero, int quantite)
    {
        ListeAchats[numero] += quantite;
    }

    public void RetirerAchat(int numero, int quantite)
    {
        ListeAchats[numero] -= quantite;
    }

    public int DemanderNombreAchats()
    {
        Console.WriteLine("Combien d'unités voulez-vous acheter ?");
        int nombreUnites = -1;
        string reponse = Console.ReadLine()!;
        while ((!Int32.TryParse(reponse, out nombreUnites)) || (nombreUnites < 0))
        {
            Console.WriteLine("Réponse invalide ");
            Console.WriteLine("Entrez le nombre d'unités.");
        }
        return nombreUnites;
    }


    public bool PayerAchat(double prixTotal)
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
            while (!Enum.TryParse<ChoixOuiNon>(reponse, true, out choix))
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

    public void AcheterGraine()
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
            Acheter();
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

    public void EffectuerAchat(int numero)
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
                    Chien chienA = new Chien(Pot);
                    Pot.ListeAnimaux.Add(chienA);
                    Console.WriteLine(" Vous possédez maintenant un chien");
                }
            }
        }
        else
        {
            AcheterGraine();
        }
    }

    public void Acheter()
    {
        Console.WriteLine($" Vous possédez {Argent} ");
        Console.WriteLine(" Vous pouvez achetez : \n1. un arrosage automatique, \n2. une bache, \n3. des coccinelle, \n4. un chien, \n5. un epouvantail, \n6. du fertilisant, \n7. des graines, \n8. des lampes UV, \n9. une pompe, \n10. une serre,\n11. un tuyau d'arrosage, \n12. du remede anti fusariose, \n13. du remede anti mildiou, \n14. du remede anti Oidium ");
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
                }
                else
                {
                    EffectuerAchat(numeroAAcheter - 1);
                    break;
                }
                numeroAAcheter = -1;
            }
        }
        if (ListeAchats[4] != 0) { PresenceEpouvantail = true; }
    }

    public void PoserAchat()
    {
        int numero = 0;
        Console.WriteLine("Vous possédez les achats suivants :");
        foreach (int nombreAchat in ListeAchats)
        {
            if ((nombreAchat != 0) && (numero != 3) && (numero != 6) && (numero != 1) && (numero != 8) && (numero != 10)) //On ne peut pas poser : chien (3), graine (6), bache (1), pompe (8), tuyau d'arrosage (10)
            {
                Console.WriteLine($"- {numero}. {achatsPossibles[numero].Nom} : {nombreAchat} unités");
            }
            numero++;
        }
        Console.WriteLine("Vous ne pouvez utiliser les baches, pompes et tuyau d'arrosage qu'en cas d'intempéries ; ils n'apparaissent pas dans la liste ci-dessus.");
        Console.WriteLine("Quel est le numéro de l'achat que vous voulez utiliser ? ");
        string reponse = Console.ReadLine()!;
        int numeroAPoser;
        while (!Int32.TryParse(reponse, out numeroAPoser) || (numeroAPoser < 0) || (numeroAPoser >= ListeAchats.Count) || (numeroAPoser == 0) || (numeroAPoser == 3) || (numeroAPoser == 6) || (numeroAPoser == 1) || (numeroAPoser == 8) || (numeroAPoser == 10))
        {
            Console.WriteLine("Vous n'avez pas entré un nombre valide. Quel est le numéro de l'achat que vous voulez utiliser ? ");
        }
        if (numeroAPoser == 0)
        {
            Pot.EffetArrosageAutomatique();
            Console.WriteLine("Vous avez installé un arrosage automatique.");
        }
        else if (numeroAPoser == 2)
        {
            //ApparitionAnimal(Coccinelle);
            Console.WriteLine("Vous avez posé des coccinelles.");
        }
        else if (numeroAPoser == 4)
        {
            PresenceEpouvantail = true;
            Console.WriteLine("Vous avez posé un épouvantail.");
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
        else if (numeroAPoser == 11)
        {
            //
            Console.WriteLine("Vous avez posé un  remède.");
        }
        else if (numeroAPoser == 12)
        {
            //
            Console.WriteLine("Vous avez posé un  remède.");
        }
        else if (numeroAPoser == 13)
        {
            //
            Console.WriteLine("Vous avez posé un  remède.");
        }
        // En cours
    }

    public void ImpactAchatPose() //Effectue l'impact pour les achats déjà posé
    {
        if (PresenceLampeUV) Pot.EffetLampeUV();
        if (PresenceArrosageAutomatique) Pot.EffetArrosageAutomatique();
        if (PresenceSerre) Pot.EffetSerre();
    }


    public void InitialisationPotager(Potager Pot, string[,] grille)
    {
        for (int i = 0; i < Pot.Hauteur; i++)
        {
            for (int j = 0; j < Pot.Longueur; j++)
            {
                grille[i, j] = " 🔳 ";
            }
        }
    }

    public void MajConditionsPotager()
    {
        Console.WriteLine("---");
        Console.WriteLine("-- Statuts du potager --");
        Console.WriteLine($"Saison : {Pot.Saison.Nom}, Température : {Pot.Temperature}");
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
                if ((plante.Espece == "Artichaut") && (plante.Taille == 3)) grille[plante.CoorY, plante.CoorX] = "🌲";
                if ((plante.Espece == "Artichaut") && (plante.Taille == plante.TailleMax)) grille[plante.CoorY, plante.CoorX] = "🥦";

                if ((plante.Espece == "Aubergine") && (plante.Taille == 1)) grille[plante.CoorY, plante.CoorX] = " aub";
                if ((plante.Espece == "Aubergine") && (plante.Taille == 2)) grille[plante.CoorY, plante.CoorX] = " AUB";
                if ((plante.Espece == "Aubergine") && (plante.Taille == 3)) grille[plante.CoorY, plante.CoorX] = "🌾";
                if ((plante.Espece == "Aubergine") && (plante.Taille == plante.TailleMax)) grille[plante.CoorY, plante.CoorX] = "🍆";

                if ((plante.Espece == "Basilic") && (plante.Taille == 1)) grille[plante.CoorY, plante.CoorX] = " bsl";
                if ((plante.Espece == "Basilic") && (plante.Taille == plante.TailleMax)) grille[plante.CoorY, plante.CoorX] = "🪴";

                if ((plante.Espece == "Oignon") && (plante.Taille == 1)) grille[plante.CoorY, plante.CoorX] = " ogn";
                if ((plante.Espece == "Oignon") && (plante.Taille == plante.TailleMax)) grille[plante.CoorY, plante.CoorX] = "🧅";

                if ((plante.Espece == "Olivier") && (plante.Taille == 1)) grille[plante.CoorY, plante.CoorX] = " olv";
                if ((plante.Espece == "Olivier") && (plante.Taille == 2)) grille[plante.CoorY, plante.CoorX] = " OLV";
                if ((plante.Espece == "Olivier") && (plante.Taille == 3)) grille[plante.CoorY, plante.CoorX] = "🌿";
                if ((plante.Espece == "Olivier") && (plante.Taille == 4)) grille[plante.CoorY, plante.CoorX] = "🌳";
                if ((plante.Espece == "Olivier") && (plante.Taille == plante.TailleMax)) grille[plante.CoorY, plante.CoorX] = "🫒";

                if ((plante.Espece == "Poivron") && (plante.Taille == 1)) grille[plante.CoorY, plante.CoorX] = " pvr";
                if ((plante.Espece == "Poivron") && (plante.Taille == 2)) grille[plante.CoorY, plante.CoorX] = " PVR";
                if ((plante.Espece == "Poivron") && (plante.Taille == plante.TailleMax)) grille[plante.CoorY, plante.CoorX] = "🫑";

                if ((plante.Espece == "Roquette") && (plante.Taille == 1)) grille[plante.CoorY, plante.CoorX] = " rqt";
                if ((plante.Espece == "Roquette") && (plante.Taille == 2)) grille[plante.CoorY, plante.CoorX] = " RQT";
                if ((plante.Espece == "Roquette") && (plante.Taille == plante.TailleMax)) grille[plante.CoorY, plante.CoorX] = "🥬";

                if ((plante.Espece == "Thym") && (plante.Taille == 1)) grille[plante.CoorY, plante.CoorX] = " thy";
                if ((plante.Espece == "Thym") && (plante.Taille == plante.TailleMax)) grille[plante.CoorY, plante.CoorX] = "🌱";

                if ((plante.Espece == "Tomate") && (plante.Taille == 1)) grille[plante.CoorY, plante.CoorX] = " tmt";
                if ((plante.Espece == "Tomate") && (plante.Taille == 2)) grille[plante.CoorY, plante.CoorX] = " TMT";
                if ((plante.Espece == "Tomate") && (plante.Taille == plante.TailleMax)) grille[plante.CoorY, plante.CoorX] = "🍅";
            }
        }
    }

    public void ChoisirActionsTour(ref bool jeuEnCours, ref string[,] grille, Simulation simu)
    {
        int reponse;
        do
        {
            MajAffichagePlantes(grille);
            AffichageComplet(grille);
            Console.WriteLine("Choisissez une action du menu principal :");
            string rep = Console.ReadLine()!;
            while (!int.TryParse(rep, out reponse))
            {
                Console.WriteLine("Vous n'avez pas entré un nombre valide. Que voulez-vous faire ? ");
                rep = Console.ReadLine()!;
            }
            if (reponse == 1) Planter(simu);
            if (reponse == 2) Acheter();
            if (reponse == 3) Arroser();
            if (reponse == 4) Console.WriteLine("ça arrive bientôt tkt");
        }
        while (reponse != 5 && reponse != 6);
        if (reponse == 5) NumeroTour += 1;
        if (reponse == 6) jeuEnCours = false;
    }

    public void AffichageComplet(string[,] grille)
    {
        Console.Clear();
        Console.WriteLine("--- Statuts du potager ---");
        Console.WriteLine($"Saison : {Pot.Saison.Nom}, Température : {Pot.Temperature}");
        Console.WriteLine();

        // Construire les lignes du potager (gauche)
        List<string> lignesPotager = new List<string>();
        for (int i = 0; i < Pot.Hauteur; i++)
        {
            string ligne = "";
            for (int j = 0; j < Pot.Longueur; j++)
            {
                ligne += grille[i, j];
            }
            lignesPotager.Add(ligne);
            lignesPotager.Add("");
        }

        // Construire les lignes de droite (récoltes, statuts, menu)
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
            lignesDroite.Add($"Statuts {plante.Espece} : Taille :{plante.Taille}, Santé {plante.Sante} ");
            lignesDroite.Add($"  | Humidité {plante.NiveauHumidite}, Luminosité {plante.NiveauLuminosite}, Température : {plante.NiveauTemperature}");
        }
        lignesDroite.Add("");
        lignesDroite.Add("-- Menu Principal --");
        lignesDroite.Add("(1) Planter une graine");
        lignesDroite.Add("(2) Faire un Achat");
        lignesDroite.Add("(3) Arroser");
        lignesDroite.Add("(4) Poser un item de votre inventaire");
        lignesDroite.Add("(5) Avancer dans le temps");
        lignesDroite.Add("(6) Quitter le jeu");

        int largeurAffichage = Pot.Longueur * 4;
        int maxLignes = Math.Max(lignesPotager.Count, lignesDroite.Count);

        // 1. Afficher potager + droite, ligne par ligne, tant qu'il y a des lignes de potager
        for (int i = 0; i < lignesPotager.Count; i++)
        {
            string gauche = lignesPotager[i];
            string droite = i < lignesDroite.Count ? lignesDroite[i] : "";
            Console.WriteLine(string.Format("{0,-" + largeurAffichage + "}   {1}", gauche, droite));
        }
        // 2. Si la partie droite est plus longue, continuer à l'afficher seule
        for (int i = lignesPotager.Count; i < lignesDroite.Count; i++)
        {
            Console.WriteLine($"{new string(' ', largeurAffichage)}   {lignesDroite[i]}");
        }
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

        string[,] GrillePotager = new string[pot.Hauteur, pot.Longueur];
        InitialisationPotager(pot, GrillePotager);

        while (jeuEnCours)
        {
            MajAffichagePlantes(GrillePotager);

            foreach (Plante plante in pot.ListePlantes)
            {
                MajBesoinEau();
                plante.MettreAJourPlantesAutour();
                VerifierEsperanceDeVie(plante);
                ImpactAchatPose();
                plante.ImpactConditions();
                plante.Contamination();
                if (NumeroTour % plante.TempsCroissance == 0) { plante.Grandir(); }
                plante.DonnerRecolte(pot, AssocierRecoltePlante(plante, RecArtichaut, RecAubergine, RecBasilic, RecOignon, RecOlivier, RecPoivron, RecRoquette, RecThym, RecTomate));
                Console.WriteLine(plante);

            }
            if (NumeroTour == 1)
            {
                pot.Saison.Nom = Saison.Printemps;
                Pot.Saison.ChangerBesoinEau();
                Pot.Saison.ChangerTemperature();
            }
            if (NumeroTour == 4)
            {
                pot.Saison.Nom = Saison.Ete;
                Pot.Saison.ChangerBesoinEau();
                Pot.Saison.ChangerTemperature();
            }
            if (NumeroTour == 7)
            {
                pot.Saison.Nom = Saison.Automne;
                Pot.Saison.ChangerBesoinEau();
                Pot.Saison.ChangerTemperature();
            }
            if (NumeroTour == 10)
            {
                pot.Saison.Nom = Saison.Hiver;
                Pot.Saison.ChangerBesoinEau();
                Pot.Saison.ChangerTemperature();
            }

            ChoisirActionsTour(ref jeuEnCours, ref GrillePotager, simu);
        }
        Console.WriteLine("FIN DE LA PARTIE.");
    }


}