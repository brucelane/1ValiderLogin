using System;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Text;
using System.Globalization;
using Microsoft.SharePoint;
using Microsoft.SharePoint.Client;
using Microsoft.SharePoint.WebPartPages.Communication;
using Microsoft.SharePoint.Security;
using Microsoft.SharePoint.Utilities;

namespace _1ValiderLogin.VisualWebPart1
{
    public partial class VisualWebPart1UserControl : UserControl
    {
        SPContext spContext = SPContext.Current;
        SharePointList listSP;
        //info de l'utilisateur demandeur
        string userNameD = "Inconnu";
        string userFonctD = "Inconnu";
        string userServiceD = "Inconnu";
        DateTime dateDemD = DateTime.Now;
        //titre liste pour dropDown
        string list1 = "Lieu de travail";
        string list2 = "Statut";
        string list3 = "Jour de la semaine";
        string list4 = "Portee du telephone";
        string list5 = "Poste de travail";
        //Liste a verifier l'existance
        string listAVerif = "Lieu de travail";
        //champ dans lequel se trouve l'info à recuperer
        string champ1 = "Lieu";
        string champ2 = "Statut";
        string champ3 = "Jour";
        string champ4 = "Portee";
        string champ5 = "Poste";
        //Log log;

        string url;

        protected void Page_Load(object sender, EventArgs e)
        {
            // recuperation de l'url du serveur local
            url = SPContext.Current.Web.Url;

            // log
            //log = new Log();
            //log.EcrireLog("ValiderLogin.txt", "creationDemande", false);

            // recuperation des informations de l'utilisateur actuel
            RecupInfoContextDemande();

            // creation des liste lors du deploiement
            deploiementWebPart(listAVerif);

            // demande validation des bouton
            //Button1.Attributes.Add("onclick", "javascript: return confirm('Etes-vous sûr de vouloir effectuer cette demande ?');");
        }

        //====================================================== GeneLogin ================================================================
        public void GeneLogin() // genere le login a partir du nom et du prenom renseigner
        {
            string Prenom;
            string[] PrenomComp;
            char[] charSplit = new char[] { '-' };

            if (txtPrenom.Text.Contains("-") == true) // gere les prenom composés
            {
                PrenomComp = txtPrenom.Text.Split(charSplit);
                Prenom = PrenomComp[0].ToLower().Substring(0, 1) + PrenomComp[1].ToLower().Substring(0, 1);
            }
            else
            {
                Prenom = txtPrenom.Text.ToLower().Substring(0,1);
            }

            txtLogin.Text = RemoveDiacritics(Prenom + "." + txtNom.Text.ToLower());
        }

        //=========================================================== RemoveDiacritics() ===============================================================
        static string RemoveDiacritics(string stIn)
        {
        // permet de modifier une chaine de caractere afin de remplacer tout les caractere avec accents par les meme caractere sans les accents
            string stFormD = stIn.Normalize(NormalizationForm.FormD);
            StringBuilder sb = new StringBuilder();

            for (int ich = 0; ich < stFormD.Length; ich++)
            {
                UnicodeCategory uc = CharUnicodeInfo.GetUnicodeCategory(stFormD[ich]);
                if (uc != UnicodeCategory.NonSpacingMark)
                {
                    sb.Append(stFormD[ich]);
                }
            }

            return (sb.ToString().Normalize(NormalizationForm.FormC));
        }

        //=========================================================== RecupInfoContextDemande() =========================================================
        public void RecupInfoContextDemande()
        {
            // recupere le login de la personne connecter, et en deduit son nom, son prenom, son service, et sa fonction
            string[] stringSep = new string[] { "\\" };
            string[] result;
            
            try
            {
                SPWeb currentWeb = SPContext.Current.Web;

                result = currentWeb.CurrentUser.LoginName.Split(stringSep, StringSplitOptions.None);
                userNameD = result[1];
                SPList list = spContext.Web.Lists["Contacts C.A.S.A."];
                SPQuery myquery = new SPQuery();
                myquery.Query = "";
                SPListItemCollection items = list.GetItems(myquery);

                foreach (SPListItem item in items)
                {
                    if (item["Login"].ToString() == result[1])
                    {
                        userNameD = item["Nom complet"].ToString();
                        userFonctD = item["Fonction"].ToString();
                        userServiceD = item["Service"].ToString();
                    }
                }
            }
            catch (Exception e)
            {
                userServiceD = e.Message;
            }

            lblNomCompletD.Text = userNameD;
            lblDemFonctionD.Text = userFonctD;
            lblDemServiceD.Text = userServiceD;
            lblDateDemandeD.Text = dateDemD.Date.Day.ToString() + "/" + dateDemD.Date.Month.ToString() + "/" + dateDemD.Date.Year.ToString();
        }

        //============================================================= deploiementWebPart() ============================================================
        public void deploiementWebPart(string listAVerif)
        {
            // verification de l'existence d'une liste dans SharePoint et creation s'il ne pas exister
            SPListCollection lists = spContext.Web.Lists;
            Boolean exist = false;

            foreach (SPList list in lists)
            {
                if (list.Title.ToString() == listAVerif)
                {
                    exist = true;
                }
            }

            if (exist == false)
            {
                CreationDesListDuWebPart(lists);
            }

        }

        //========================================================= CreationDesListDuWebPart() ===========================================================
        public void CreationDesListDuWebPart(SPListCollection lists)
        {
            champSPlist champ;
            //===============creation de la liste 1======================
            try
            {
                champ = new champSPlist(champ1);
                listSP = new SharePointList(list1, "", SPListTemplateType.GenericList); // creation de l'objet

                champ.addCollInfo("Antenne Judiciaire Antibes");
                champ.addCollInfo("Centre Technique Vallauris");
                champ.addCollInfo("Envibus (St-Bernard)");
                champ.addCollInfo("Gare routiere Antibes");
                champ.addCollInfo("Gare routiere Vallauris");
                champ.addCollInfo("Gare routiere Valbonne");
                champ.addCollInfo("Les Genets");
                champ.addCollInfo("Logements (Liberation)");
                champ.addCollInfo("Mediatheque Antibes");
                champ.addCollInfo("Mediatheque Semboules");
                champ.addCollInfo("Mediatheque Valbonne");
                champ.addCollInfo("Mediatheque Roquefort");
                champ.addCollInfo("Parenthese");
                champ.addCollInfo("Prevention Antibes");
                champ.addCollInfo("Prevention Vallauris");
                champ.addCollInfo("Prevention Valbonne");
                champ.addCollInfo("Prevention Haut Pays");
                champ.addCollInfo("Trait d union");
                champ.addCollInfo("indeterminée");

                listSP.addCollChamps(champ); // remplissage de la collection de champs a creer

                listSP.creerListInSharePoint(lists);
            }
            catch { }
            //===============creation de la liste 2======================
            try
            {
                champ = new champSPlist(champ2);
                listSP = new SharePointList(list2, "", SPListTemplateType.GenericList);
                champ.addCollInfo("Stagiaire");
                champ.addCollInfo("Contrat");
                champ.addCollInfo("Titulaire");
                champ.addCollInfo("indeterminée");
                listSP.addCollChamps(champ);
                listSP.creerListInSharePoint(lists);
            }
            catch { }
            //===============creation de la liste 3======================
            try
            {
                champ = new champSPlist(champ3);
                listSP = new SharePointList(list3, "", SPListTemplateType.GenericList);
                champ.addCollInfo("Lundi");
                champ.addCollInfo("Mardi");
                champ.addCollInfo("Mercredi");
                champ.addCollInfo("Jeudi");
                champ.addCollInfo("Vendredi");
                champ.addCollInfo("Samedi");
                champ.addCollInfo("Dimanche");
                listSP.addCollChamps(champ);
                listSP.creerListInSharePoint(lists);
            }
            catch { }
            //===============creation de la liste 4======================
            try
            {
                champ = new champSPlist(champ4);
                listSP = new SharePointList(list4, "", SPListTemplateType.GenericList);
                champ.addCollInfo("Fixes CASA uniquement");
                champ.addCollInfo("Fixes et portables CASA uniquement");
                champ.addCollInfo("Libre");
                listSP.addCollChamps(champ);
                listSP.creerListInSharePoint(lists);
            }
            catch { }
            //===============creation de la liste 5======================
            try
            {
                champ = new champSPlist(champ5);
                listSP = new SharePointList(list5, "", SPListTemplateType.GenericList);
                champ.addCollInfo("(aucun)");
                champ.addCollInfo("Poste fixe");
                champ.addCollInfo("Ordinateur Portable");
                champ.addCollInfo("Station Graphique");
                listSP.addCollChamps(champ);
                listSP.creerListInSharePoint(lists);
            }
            catch { }
            //===============creation de la liste 6======================
            try
            {
                listSP = new SharePointList("Demande Nouvel Arrivant", "", SPListTemplateType.GenericList);

                champ = new champSPlist("Nom bénéficiaire");
                listSP.addCollChamps(champ);
                champ = new champSPlist("Prénom bénéficiaire");
                listSP.addCollChamps(champ);
                champ = new champSPlist("Login");
                listSP.addCollChamps(champ);
                champ = new champSPlist("Matricule");
                listSP.addCollChamps(champ);
                champ = new champSPlist(SPEncode.HtmlEncode("date d'entrée"));
                listSP.addCollChamps(champ);
                champ = new champSPlist("date de sortie");
                listSP.addCollChamps(champ);
                champ = new champSPlist("Statut");
                listSP.addCollChamps(champ);
                champ = new champSPlist("Service");
                listSP.addCollChamps(champ);
                champ = new champSPlist("Fonction");
                listSP.addCollChamps(champ);
                champ = new champSPlist("Lieu de travail");
                listSP.addCollChamps(champ);
                champ = new champSPlist("N° bureau");
                listSP.addCollChamps(champ);
                champ = new champSPlist("Remplacement");
                listSP.addCollChamps(champ);
                champ = new champSPlist("Agent Remplacé");
                listSP.addCollChamps(champ);
                champ = new champSPlist("Téléphone fixe");
                listSP.addCollChamps(champ);
                champ = new champSPlist("Téléphone portable");
                listSP.addCollChamps(champ);
                champ = new champSPlist("Justification téléphone portable");
                listSP.addCollChamps(champ);
                champ = new champSPlist(SPEncode.HtmlEncode("Jour d'utilisation"));
                listSP.addCollChamps(champ);
                champ = new champSPlist(SPEncode.HtmlEncode("Portée d'utilisation"));
                listSP.addCollChamps(champ);
                champ = new champSPlist("Poste de travail");
                listSP.addCollChamps(champ);
                champ = new champSPlist("Justification du poste de travail");
                listSP.addCollChamps(champ);
                champ = new champSPlist("Accés Internet");
                listSP.addCollChamps(champ);
                champ = new champSPlist("Accés Mail");
                listSP.addCollChamps(champ);
                champ = new champSPlist("Accés à Post-Office");
                listSP.addCollChamps(champ);
                champ = new champSPlist("Accés à Actes-Office");
                listSP.addCollChamps(champ);
                champ = new champSPlist("Accés au logiciel Finance");
                listSP.addCollChamps(champ);
                champ = new champSPlist("Accés à la saisie information DRH");
                listSP.addCollChamps(champ);
                champ = new champSPlist("Besoins spécifiques");
                listSP.addCollChamps(champ);
                champ = new champSPlist("Demandeur");
                listSP.addCollChamps(champ);
                champ = new champSPlist("Fonction du demandeur");
                listSP.addCollChamps(champ);
                champ = new champSPlist("Service du demandeur");
                listSP.addCollChamps(champ);
                champ = new champSPlist("Date_demande");
                listSP.addCollChamps(champ);
                
                listSP.creerListInSharePoint(lists);
            }
            catch { }
        }

        //=================================================== verifExistIdentite(firstName, name) =======================================================
        public int verifExistIdentite(string firstName, string name)
        {
            //log.EcrireLog("ValiderLogin.txt", "verifExistIdentite", true);
            int exist;
            SPWeb currentWeb = SPContext.Current.Web;
            // recupere les contacts
            SPList list = spContext.Web.Lists["Contacts C.A.S.A."];
            SPQuery myquery = new SPQuery();
            myquery.Query = "";
            SPListItemCollection items = list.GetItems(myquery);
            // recupere demande en cours
            SPList list2 = spContext.Web.Lists["Demande Nouvel Arrivant"];
            SPListItemCollection items2 = list2.GetItems(myquery);
            // valeur d'existance par default a 0
            exist = 0;
            // boucle dans contact
            //log.EcrireLog("ValiderLogin.txt", "1e boucle", true);
            foreach (SPListItem item in items)
            {
                try
                {
                    if (RemoveDiacritics(item["Nom"].ToString().ToLower()) == RemoveDiacritics(name.ToLower()) && RemoveDiacritics(item["Prénom"].ToString().ToLower()) == RemoveDiacritics(firstName.ToLower()))
                    {
                        exist = 1;
                    }
                }
                catch 
                { 
                    //log.EcrireLog("ValiderLogin.txt", "erreur 1ere boucle ", true); 
                }
            }
            //log.EcrireLog("ValiderLogin.txt", "1e boucle passée", true);
            // boucle dans demande
            foreach (SPListItem item in items2)
            {
                try
                {
                    if (RemoveDiacritics(item["Nom bénéficiaire"].ToString().ToLower()) == RemoveDiacritics(name.ToLower()) && RemoveDiacritics(item["Prénom bénéficiaire"].ToString().ToLower()) == RemoveDiacritics(firstName.ToLower()))
                    {
                        exist = 2;
                    }
                }
                catch 
                { 
                    //log.EcrireLog("ValiderLogin.txt", "erreur 1ere boucle ", true); 
                }
            }
            //log.EcrireLog("ValiderLogin.txt", "2e boucle passée", true);
            return exist;
        }

        //============================================================ verifExistIdentite(login) ======================================================
        public int verifExistIdentite(string login)
        {
            int exist;
            SPWeb currentWeb = SPContext.Current.Web;
            // recupere les contacts
            SPList list = spContext.Web.Lists["Contacts C.A.S.A."];
            SPQuery myquery = new SPQuery();
            myquery.Query = "";
            SPListItemCollection items = list.GetItems(myquery);
            // recupere demande en cours
            SPList list2 = spContext.Web.Lists["Demande Nouvel Arrivant"];
            SPListItemCollection items2 = list2.GetItems(myquery);
            // valeur d'existance par default a 0
            exist = 0;
            // boucle dans contact
            foreach (SPListItem item in items)
            {
                if (RemoveDiacritics(item["Login"].ToString().ToLower()) == RemoveDiacritics(login.ToLower()))
                {
                    exist = 1;
                }
            }
            // boucle dans demande
            foreach (SPListItem item in items2)
            {
                if (RemoveDiacritics(item["Login"].ToString().ToLower()) == RemoveDiacritics(login.ToLower()))
                {
                    exist = 2;
                }
            }

            return exist;
        }

        //========================================================== creationDemande() =============================================================
        public void creationDemande()
        {
            SPListItem ligneInfo;
            string Prenom;
            string[] PrenomComp;
            string prenom1;
            string prenom2;
            char[] charSplit = new char[] { '-' };

            if (txtPrenom.Text.Contains("-") == true) // gere les prenom composés
            {
                PrenomComp = txtPrenom.Text.Split(charSplit);
                prenom1 = PrenomComp[0].ToLower();
                prenom2 = PrenomComp[1].ToLower();
                Prenom = prenom1[0].ToString().ToUpper() + prenom1.Substring(1).ToLower() + "-" + prenom2[0].ToString().ToUpper() + prenom2.Substring(1).ToLower();
            }
            else
            {
                Prenom = txtPrenom.Text.ToLower();
            }
            //log.EcrireLog("ValiderLogin.txt", Prenom, true);
 
            SPList list = spContext.Web.Lists["Demande Nouvel Arrivant"];
            ligneInfo = list.AddItem();
            //log.EcrireLog("ValiderLogin.txt", "ligneInfo.AddItem", true);
            ligneInfo["Nom bénéficiaire"] = NeverNull(txtNom.Text).ToUpper();
            //log.EcrireLog("ValiderLogin.txt", "Nom bénéficiaire", true);
            ligneInfo["Prénom bénéficiaire"] = NeverNull(Prenom);
            //log.EcrireLog("ValiderLogin.txt", "Prénom bénéficiaire", true);
            ligneInfo["Login"] = NeverNull(txtLogin.Text);
            //log.EcrireLog("ValiderLogin.txt", "Login", true);
            ligneInfo["Demandeur"] = NeverNull(lblNomCompletD.Text);
            //log.EcrireLog("ValiderLogin.txt", "Demandeur", true);
            ligneInfo["Fonction du demandeur"] = NeverNull(lblDemFonctionD.Text);
            //log.EcrireLog("ValiderLogin.txt", "Fonction du demandeur", true);
            ligneInfo["Service du demandeur"] = NeverNull(lblDemServiceD.Text);
            //log.EcrireLog("ValiderLogin.txt", "Service du demandeur", true);
            ligneInfo["Date_demande"] = NeverNull(lblDateDemandeD.Text);
            //log.EcrireLog("ValiderLogin.txt", "Date_demande", true);
            ligneInfo["Titre"] = "Etape1";
            //log.EcrireLog("ValiderLogin.txt", "Titre", true);
            ligneInfo.Update();
            //log.EcrireLog("ValiderLogin.txt", "ligneInfo.Update", true);
        }

        //============================================================== NeverNull() ===============================================================
        public string NeverNull(string i)
        {
            if (i == null)
            {
                i = "";
            }
            return i;
        }

        //============================================================= event =====================================================================

        //=================================================================================================
        //====================== Clique sur bouton Accepter ===============================================
        //=================================================================================================

        protected void Button1_Click(object sender, EventArgs e)
        {
            //log.EcrireLog("ValiderLogin.txt", "Button1_Click", true);
            if (Page.IsValid)// la validation est bonne ...
            {
                //log.EcrireLog("ValiderLogin.txt", "(Page.IsValid)", true);
                // lors du click tout les label ont leur parametre visible qui passe a false
                lblExisteDeja.Visible = false;
                lblExistDem.Visible = false;
                lblChangeLogin.Visible = false;
                lblLoginExistDem.Visible = false;
                lblValide.Visible = false;
                // verifie si le nom et le prenom existent deja
                int exist = verifExistIdentite(txtPrenom.Text, txtNom.Text);
                //log.EcrireLog("ValiderLogin.txt", exist.ToString(), true);
                // verifie si le login existe deja
                int exist2 = verifExistIdentite(txtLogin.Text);
                //log.EcrireLog("ValiderLogin.txt", exist2.ToString(), true);

                // puis analyse de la réponse
                if (exist == 1) // si existe dans Contact C.A.S.A.
                {
                    lblExisteDeja.Visible = true;
                }
                else if (exist == 2) // si existe dans Demande C.A.S.A.
                {
                    lblExistDem.Visible = true;
                }
                else if (exist2 == 1) // si existe dans Contact C.A.S.A.
                {
                    lblChangeLogin.Visible = true;
                }
                else if (exist2 == 2) // si existe dans Demande C.A.S.A.
                {
                    lblLoginExistDem.Visible = true;
                }
                else if (exist2 == 0 && exist == 0) // si n'existe pas
                {
                    //log.EcrireLog("ValiderLogin.txt", "appel creationDemande", true);
                    lblValide.Visible = true;
                    creationDemande(); // creation de la demande

                    Response.Redirect(url + "/SitePages/Etape1.aspx?nigol=" + txtLogin.Text);
                }
            }
        }

        //=================================================================================================
        //====================== Modif text de txtNom =====================================================
        //=================================================================================================
        protected void txtNom_TextChanged(object sender, EventArgs e)
        {
            if (txtNom.Text != "")
            {
                GeneLogin();
            }
        }

        //=================================================================================================
        //====================== Modif text de txtPrenom ==================================================
        //=================================================================================================
        protected void txtPrenom_TextChanged(object sender, EventArgs e)
        {
            if (txtNom.Text != "")
            {
                GeneLogin();
            }
        }
    }
}
