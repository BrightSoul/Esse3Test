using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace ESSE3Test
{
    [XmlRoot("WS")]
    public class ElencoCorsi
    {
        [XmlArray("DataSet")]
        [XmlArrayItem("Row")]
        public Corso[] Corsi { get; set; }
    }
}
