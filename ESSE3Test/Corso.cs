using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace ESSE3Test
{
    public class Corso
    {
        [XmlAttribute("Num")]
        public int IdCorso { get; set; }

        [XmlElement("p06_cds_des")]
        public string Nome { get; set; }

        [XmlElement("fac_id")]
        public int IdFacoltà { get; set; }

    }
}
