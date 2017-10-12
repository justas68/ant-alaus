using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Alus
{ 
    class Baras
    {
        private String _pavadinimas;
        private String _cords;
        public Baras(String _pavadinimas, String _cords)
        {
            this._pavadinimas = _pavadinimas;
            this._cords = _cords;
        }
        public String getPav()
        {
            return _pavadinimas;
        }
        public String getCords()
        {
            return _cords;
        }
    }
}
