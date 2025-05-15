
public abstract class ActionUrgente
{
    public object Sujet { get; set; }
    public ActionUrgente() { }

    public void ProposerAction(object Sujet, Potager Pot)
    {
        if (Sujet is Animaux)
        {
            Console.WriteLine("Vous pouvez : ");
            Console.WriteLine("-Faire du bruit (1) \n- Poser un épouvantail (2) \n- Faire fuir le chat (3) \n- Reboucher un trou (4) \n- Adopter un chien (5)");
        }
        else if (Sujet is Intemperie)
        {
            Console.WriteLine("Vous pouvez : ");
            Console.WriteLine("-Mettre une bâche (6) \n- Installer une pompe (7) \n- Utiliser le tuyau d'arrosage (8) \n- installer l'arrosage automatique (9)");
        }

        int choix = Convert.ToInt32(Console.ReadLine());
        if (choix == 1)
        {
            FaireBruit(Sujet);
        }
        else if (choix == 2)
        {
            PoserEpouvantail(Sujet);
        }
        else if (choix == 3)
        {
            FaireFuirChat(Sujet);
        }
        else if (choix == 4)
        {
            ReboucherTrou(Sujet);
        }
        else if (choix == 5)
        {
            AdopterChien(Sujet);
        }
        else if (choix == 6)
        {
            PoserBache(Sujet);
        }
        else if (choix == 7)
        {
            InstallerPompe(Sujet);
        }
        else if (choix == 8)
        {
            Arroser(Sujet, Pot.ListePlantes);
        }
        else if (choix == 9)
        {
            InstallerArosageAuto(Sujet, Pot.ListePlantes);
        }
    }

    public void FaireBruit(object Sujet)
    {
        if (Sujet is Oiseau oiseau)
        {
            oiseau.Disparait();
        }
        else
        {
            Console.WriteLine("Aucun Effet");
        }
    }

    public void PoserEpouvantail(object Sujet)
    {
        if (Sujet is Oiseau oiseau)
        {
            oiseau.Disparait();
            bool epouvantail = true; //Mettre dans simulation
        }
        else
        {
            Console.WriteLine("Aucun Effet");
        }
    }
    public void FaireFuirChat(object Sujet)
    {
        if (Sujet is Chat chat)
        {
            chat.Disparait();
        }
        else
        {
            Console.WriteLine("Aucun Effet");
        }
    }
    public void ReboucherTrou(object Sujet)
    {
        if (Sujet is Taupe taupe)
        {
            taupe.Disparait();
            //Enlever les trous
        }
        else
        {
            Console.WriteLine("Aucun Effet");
        }
    }
    public void AdopterChien(object Sujet)
    {
        if (Sujet is Rongeur rongeur)
        {
            rongeur.Disparait();
            bool PresenceChien = true; //A mettree dans Simulation
        }
        else
        {
            Console.WriteLine("Aucun Effet");
        }
    }
    public void PoserBache(object Sujet)
    {
        if ((Sujet is Grele)) /* Et Vérifier qu'on a une bache */
        {
            bool grele = false;
            bool bache = true; //A mettree dans Simulation
        }
        else
        {
            Console.WriteLine("Aucun Effet");
        }
    }
    public void InstallerPompe(object Sujet)
    {
        if ((Sujet is Inondation) /*&& (Vérifier qu'on a une pompe )*/)
        {
            bool inondation = false;
            bool pompe = true; //A mettree dans Simulation
        }
        else
        {
            Console.WriteLine("Aucun Effet");
        }
    }
    public void Arroser(object Sujet, List<Plante> ListePlantes)
    {
        if ((Sujet is Secheresse)/* && (Vérifier qu'on a un tuyau d'arrosage )*/)
        {
            foreach (Plante plante in ListePlantes)
            {
                plante.NiveauHumidite += 5;
            }
        }
        else
        {
            Console.WriteLine("Aucun Effet");
        }
    }
    public void InstallerArosageAuto(object Sujet, List<Plante> ListePlantes)
    {
        if ((Sujet is Secheresse) /*&& (Vérifier qu'on a un arrosage auto )*/)
        {
            bool Secheresse = false;
            foreach (Plante plante in ListePlantes)
            {
                plante.NiveauHumidite = plante.SeuilHumidite;
            }
        }
        else
        {
            Console.WriteLine("Aucun Effet");
        }
    }
}

