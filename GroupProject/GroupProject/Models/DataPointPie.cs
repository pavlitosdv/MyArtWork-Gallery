using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;

namespace GroupProject.Models
{
	//DataContract for Serializing Data - required to serve in JSON format
	[DataContract]
	public class DataPointPie
	{
		public DataPointPie(string name, double y)
		{
			this.Name = name;
			this.Y = y;
		}

		//Explicitly setting the name to be used while serializing to JSON.
		[DataMember(Name = "name")]
		public string Name = "";

		//Explicitly setting the name to be used while serializing to JSON.
		[DataMember(Name = "y")]
		public Nullable<double> Y = null;
	}
}