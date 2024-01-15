using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
/*
using WALoginEmpleados.SWInformacionUsuarioValidada;
using WALoginEmpleados.App.Comunicacion.Autenticacion.Usuarios;*/

namespace WALoginEmpleados.App.Controladoras.Autenticacion
{/*
    public class CGestorAutenticacion
    {
        #region Atributos
        private InformacionUsuarios _infoUsuario;
        private ManejadorTokens _manejadorTokens;
        #endregion

        #region Constructor
        public CGestorAutenticacion()
        {
            _infoUsuario = new InformacionUsuarios();
            _manejadorTokens = new ManejadorTokens();
        }
        #endregion

        #region Propiedades
        public UsuarioExtendido AutenticarUsuario(string alias, string pin, string tipoUsuario)
        {
            return _infoUsuario.Obtener_Informacion_Usuario(alias, pin, tipoUsuario);
        }

        public string GenerarTokenParaUsuario(UsuarioExtendido usuario, string tipoUsuario)
        {
            // Utiliza ManejadorTokens para generar el token
            return _manejadorTokens.GenerarTokenJWT(usuario, tipoUsuario);
        }

        public string GenrarTokenParaDocumento(EFirmaDocumento documento, string claveDocumento)
        {
            return _manejadorTokens.GenerarTokenDocumento(documento, claveDocumento);
        }
        #endregion
    }*/
}