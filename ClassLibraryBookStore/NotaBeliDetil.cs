using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ClassLibraryBookStore
{
    public class NotaBeliDetil
    {
        private int harga;
        private int jumlah;
        private Buku buku;

        #region Constructors
        public NotaBeliDetil(Buku buku, int harga, int jumlah)
        {
            this.Harga = harga;
            this.Jumlah = jumlah;
            this.Buku = buku;
        }
        #endregion

        #region Properties
        public int Harga { get => harga; set => harga = value; }
        public int Jumlah { get => jumlah; set => jumlah = value; }
        public Buku Buku { get => buku; set => buku = value; }
        #endregion
    }
}