using System;
using System.Data;
using System.Data.SqlClient;
using System.Collections.Generic;

namespace BandTracker
{
  public class Venue
  {
    private int _id;
    private string _venueName;

    public Venue(string venueName, int id = 0)
    {
      _venueName = venueName;
      _id = id;
    }

    public override bool Equals(System.Object otherVenue)
    {
      if(!(otherVenue is Venue)) return false;
      else
      {
        Venue newVenue = (Venue) otherVenue;
        bool nameEquality = this.GetVenueName() == newVenue.GetVenueName();
        bool idEquality = this.GetId() == newVenue.GetId();
        return(nameEquality && idEquality);
      }
    }

    public int GetId()
    {
        return _id;
    }
    public void SetId(int newId)
    {
      _id = newId;
    }

    public string GetVenueName()
    {
        return _venueName;
    }
    public void SetVenueName(string newVenueName)
    {
      _venueName = newVenueName;
    }



    public static List<Venue> GetAll()
    {
      List<Venue> allVenues = new List<Venue>{};
      SqlConnection conn = DB.Connection();
      SqlDataReader rdr = null;
      conn.Open();

      SqlCommand cmd = new SqlCommand("SELECT * FROM venues;", conn);
      rdr = cmd.ExecuteReader();
      while(rdr.Read())
      {
        int venueId = rdr.GetInt32(0);
        string venueName = rdr.GetString(1);
        Venue newVenue = new Venue(venueName, venueId);
        allVenues.Add(newVenue);
      }
      if (rdr != null)
      {
        rdr.Close();
      }
      if (conn != null)
      {
        conn.Close();
      }
      return allVenues;
    }


  }
}
