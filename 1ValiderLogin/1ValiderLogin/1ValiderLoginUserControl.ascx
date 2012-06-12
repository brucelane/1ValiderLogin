<%@ Assembly Name="$SharePoint.Project.AssemblyFullName$" %>
<%@ Assembly Name="Microsoft.Web.CommandUI, Version=14.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %> 
<%@ Register Tagprefix="SharePoint" Namespace="Microsoft.SharePoint.WebControls" Assembly="Microsoft.SharePoint, Version=14.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %> 
<%@ Register Tagprefix="Utilities" Namespace="Microsoft.SharePoint.Utilities" Assembly="Microsoft.SharePoint, Version=14.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register Tagprefix="asp" Namespace="System.Web.UI" Assembly="System.Web.Extensions, Version=3.5.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35" %>
<%@ Import Namespace="Microsoft.SharePoint" %> 
<%@ Register Tagprefix="WebPartPages" Namespace="Microsoft.SharePoint.WebPartPages" Assembly="Microsoft.SharePoint, Version=14.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="1ValiderLoginUserControl.ascx.cs" Inherits="_1ValiderLogin.VisualWebPart1.VisualWebPart1UserControl" %>
        <style type="text/css">
            .style1
            {
                width: 271px;
            }
        </style>
        <div>
            <asp:Image ID="Image1" runat="server" AlternateText="Fiche Nouvel Arrivant" ImageUrl="~/_layouts/images/1ValiderLogin/_nouvelarrivantentete.jpg" />
            <table style="border-top-style: none; border-right-style: none; border-left-style: none;
                border-bottom-style: solid">
                <tr>
                    <td style="width: 150px">
                        <b>Demandeur</b>
                    </td>
                    <td style="width: 191px">
                    </td>
                </tr>
                <tr>
                    <td style="width: 150px">
                        Nom du demandeur :
                    </td>
                    <td style="width: 191px">
                        <asp:Label ID="lblNomCompletD" runat="server"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td style="width: 150px">
                        Fonction :
                    </td>
                    <td style="width: 191px">
                        <asp:Label ID="lblDemFonctionD" runat="server"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td style="width: 150px">
                        Service :
                    </td>
                    <td style="width: 191px">
                        <asp:Label ID="lblDemServiceD" runat="server"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td style="width: 150px">
                        Date de la demande :
                    </td>
                    <td style="width: 191px">
                        <asp:Label ID="lblDateDemandeD" runat="server"></asp:Label>
                    </td>
                </tr>
            </table>
            <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                <ContentTemplate>
                    <table style="border-top-style: none; border-right-style: none; border-left-style: none">
                        <caption>
                            <b>Veuillez entrer un Nom et un Prénom :</b>
                            <tr>
                                <td style="width: 70px">
                                    Nom :
                                </td>
                                <td class="style1">
                                    <asp:TextBox ID="txtNom" runat="server" OnTextChanged="txtNom_TextChanged" 
                                        Width="250px" AutoPostBack="True"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtNom">*</asp:RequiredFieldValidator>
                                </td>
                            </tr>
                            <tr>
                                <td style="width: 70px">
                                    Prénom :
                                </td>
                                <td class="style1">
                                    <asp:TextBox ID="txtPrenom" runat="server" OnTextChanged="txtPrenom_TextChanged"
                                        Width="250px" AutoPostBack="True"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="txtPrenom">*</asp:RequiredFieldValidator>
                                </td>
                            </tr>
                            <tr>
                                <td style="width: 70px">
                                    Login :
                                </td>
                                <td class="style1">
                                    <asp:TextBox ID="txtLogin" runat="server" AutoPostBack="True" Width="250px"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="txtLogin">*</asp:RequiredFieldValidator>
                                </td>
                            </tr>
                        </caption>
                    </table>
                </ContentTemplate>
            </asp:UpdatePanel>
            <table style="border-top-style: none; border-right-style: none; border-left-style: none;
                border-bottom-style: solid; width: 348px;">
                <tr>
                    <td>
                        <asp:Button ID="Button1" runat="server" OnClick="Button1_Click" Text="Valider le Nom"
                            Width="340px" />
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                            <ContentTemplate>
                                <asp:Label ID="lblExisteDeja" runat="server" Visible="False">Cette personne existe déja !</asp:Label>
                                <asp:Label ID="lblChangeLogin" runat="server" Visible="False">Ce login existe déja ! Veuillez le modifier !</asp:Label>
                                <asp:Label ID="lblValide" runat="server" Text="Demande effectuée !" Visible="False"></asp:Label>
                                &nbsp;
                                <asp:Label ID="lblExistDem" runat="server" Text="Demande deja existante !" Visible="False"></asp:Label>
                                <asp:Label ID="lblLoginExistDem" runat="server" Text="Login deja en cour de demande !"
                                    Visible="False"></asp:Label>
                            </ContentTemplate>
                            <Triggers>
                                <asp:AsyncPostBackTrigger ControlID="Button1" EventName="Click" />
                            </Triggers>
                        </asp:UpdatePanel>
                    </td>
                </tr>
            </table>
        </div>
<p>
    version 1.0.4 (12 juin 2012)</p>

