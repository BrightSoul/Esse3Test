using System;
using System.Xml.Serialization;

public class Facoltà
{
    [XmlElement("des")]
    public String Nome {get; set;}

    [XmlElement("fac_id")]
    public int Id { get; set; }

	public Facoltà()
	{
        
	}
}
