<%@ Page Title="Solicitar Certificado" Language="C#" EnableSessionState="True" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Contact.aspx.cs" Inherits="WACertifivados.Contact" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <h2><%: Title %>.</h2>
    <h3>Obtener clave secreta.</h3>
    <p>Recuerde guardar su clave en un lugar seguro.</p>

    <br />

    <label for="titularName">Nombre Titular (NombreApellido):</label>
    <input type="text" id="titularName" runat="server" />

    <label for="contrasena">Contraseña:</label>
    <input type="text" id="contrasena" runat="server" />

    <!-- Agrega aquí otros inputs para departamento, país, etc. -->

    <button type="submit" runat="server" onserverclick="GenerarClavesYCSR">Generar</button>
    
</asp:Content>
