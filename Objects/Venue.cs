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

    public void Save()
    {
      SqlConnection conn = DB.Connection();
      SqlDataReader rdr;
      conn.Open();

      SqlCommand cmd = new SqlCommand("INSERT INTO venues (venue_name) OUTPUT INSERTED.id VALUES (@venueName);", conn);

      SqlParameter venueNameParameter = new SqlParameter();
      venueNameParameter.ParameterName = "@venueName";
      venueNameParameter.Value = this.GetVenueName();

      cmd.Parameters.Add(venueNameParameter);
      rdr = cmd.ExecuteReader();

      while(rdr.Read())
      {
        this._id = rdr.GetInt32(0);
      }
      if (rdr != null)
      {
        rdr.Close();
      }
      if (conn != null)
      {
        conn.Close();
      }
    }

    public void Update(string newVenueName)
    {
      _venueName = newVenueName;
      SqlConnection conn = DB.Connection();
      SqlDataReader rdr = null;
      conn.Open();

      SqlCommand cmd = new SqlCommand("UPDATE venues SET venue_name = @venueName where id = @id;", conn);

      SqlParameter nameParameter = new SqlParameter();
      nameParameter.ParameterName = "@venueName";
      nameParameter.Value = newVenueName;

      SqlParameter idParameter = new SqlParameter();
      idParameter.ParameterName = "@id";
      idParameter.Value = this.GetId();

      cmd.Parameters.Add(nameParameter);
      cmd.Parameters.Add(idParameter);
      rdr = cmd.ExecuteReader();

      if (rdr != null)
      {
        rdr.Close();
      }
      if (conn != null)
      {
        conn.Close();
      }
    }

    public void AddBand(int bandId)
    {
      SqlConnection conn = DB.Connection();
      conn.Open();

      SqlCommand cmd = new SqlCommand("INSERT INTO bands_venues (band_id, venue_id) VALUES (@bandId, @venueId);", conn);

      SqlParameter venueIdParameter = new SqlParameter();
      venueIdParameter.ParameterName = "@venueId";
      venueIdParameter.Value = this.GetId();
      cmd.Parameters.Add(venueIdParameter);

      SqlParameter bandIdParameter = new SqlParameter();
      bandIdParameter.ParameterName = "@bandId";
      bandIdParameter.Value = bandId;
      cmd.Parameters.Add(bandIdParameter);

      cmd.ExecuteNonQuery();

      if (conn != null)
      {
        conn.Close();
      }
    }

    public static Venue Find(int searchId)
    {
      SqlConnection conn = DB.Connection();
      SqlDataReader rdr;
      conn.Open();

      SqlCommand cmd = new SqlCommand("SELECT * FROM venues WHERE id = @venueId;", conn);

      SqlParameter venueIdParameter = new SqlParameter();
      venueIdParameter.ParameterName = "@venueId";
      venueIdParameter.Value = searchId.ToString();
      cmd.Parameters.Add(venueIdParameter);

      rdr = cmd.ExecuteReader();

      int foundVenueId = 0;
      string foundVenueName = null;
      while(rdr.Read())
      {
        foundVenueId = rdr.GetInt32(0);
        foundVenueName = rdr.GetString(1);
      }
      Venue foundVenue = new Venue(foundVenueName, foundVenueId);
      if (rdr != null)
      {
        rdr.Close();
      }
      if (conn != null)
      {
        conn.Close();
      }
      return foundVenue;
    }

    public List<Band> GetBands()
    {
      List<Band> allBands = new List<Band>{};
      SqlConnection conn = DB.Connection();
      SqlDataReader rdr = null;
      conn.Open();

      SqlCommand cmd = new SqlCommand("SELECT bands.* FROM venues JOIN bands_venues ON (venues.id = bands_venues.venue_id) JOIN bands ON (bands_venues.band_id = bands.id) WHERE venues.id = @venueId;", conn);

      SqlParameter venueIdParameter = new SqlParameter();
      venueIdParameter.ParameterName = "@venueId";
      venueIdParameter.Value = this.GetId();
      cmd.Parameters.Add(venueIdParameter);

      rdr = cmd.ExecuteReader();

      while(rdr.Read())
      {
        int bandId = rdr.GetInt32(0);
        string bandName = rdr.GetString(1);
        Band newBand = new Band(bandName, bandId);
        allBands.Add(newBand);
      }
      if (rdr != null)
      {
        rdr.Close();
      }
      if (conn != null)
      {
        conn.Close();
      }
      return allBands;
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

    public void Delete()
    {
      SqlConnection conn = DB.Connection();
      conn.Open();

      SqlCommand cmd = new SqlCommand("DELETE FROM venues WHERE id = @venueId;", conn);

      SqlParameter venueIdParameter = new SqlParameter();
      venueIdParameter.ParameterName = "@venueId";
      venueIdParameter.Value = this.GetId();

      cmd.Parameters.Add(venueIdParameter);
      cmd.ExecuteNonQuery();

      if (conn != null)
      {
        conn.Close();
      }
    }

    public static void DeleteAll()
    {
      SqlConnection conn = DB.Connection();
      conn.Open();
      SqlCommand cmd = new SqlCommand("DELETE FROM venues;", conn);
      cmd.ExecuteNonQuery();
    }
  }
}
