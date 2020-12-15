using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ClassLibraryBookStore
{
    public class Penulis
    {
        private string idPenulis;
        private string namaPenulis;

        #region Constructor
        public Penulis(string idPenulis, string namaPenulis)
        {
            this.IdPenulis = idPenulis;
            this.NamaPenulis = namaPenulis;
        }
        #endregion

        #region Properties
        public string IdPenulis { get => idPenulis; set => idPenulis = value; }
        public string NamaPenulis { get => namaPenulis; set => namaPenulis = value; }
        #endregion
    }
}