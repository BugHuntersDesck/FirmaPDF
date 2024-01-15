<%@ Page Title="Par de Claves" Language="C#" EnableSessionState="True" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="About.aspx.cs" Inherits="WACertifivados.About" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <h2><%: Title %>.</h2>
    <h3>Solicitar Certificado.</h3>
    <h2>Usuario: </h2>
    <!-- label para mostar el nombre del titular -->
    <asp:Label ID="lblNombreTitular" runat="server" Text="Label"></asp:Label>
    <br />
    <asp:Label ID="lblRuta" runat="server" Text="Label"></asp:Label>

    <p>Elija Autoridad Certificadora.</p>

    <!-- crear caja de opciones con dos "NetValle" "Otra"-->
    <asp:RadioButtonList ID="RadioButtonList1" runat="server" RepeatDirection="Horizontal" RepeatLayout="Flow">
        <asp:ListItem Selected="True">NetValle</asp:ListItem>
        <asp:ListItem>Otra</asp:ListItem>
    </asp:RadioButtonList>

    <!-- crear boton para el metodo enviarSolicitud-->
    <asp:Button ID="Button1" runat="server" Text="Enviar Solicitud" OnClick="btn_EnviarSolicitudCSR_Click" />

    <br />
    

</asp:Content>
