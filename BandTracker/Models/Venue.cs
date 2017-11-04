using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using MySql.Data.MySqlClient;

namespace BandTracker.Models
{
  public class Venue
  {
    public string Name {get; private set;}
    public string Address {get; private set;}
    public int Id {get; private set;}

    public Venue(string name, string address, int id = 0)
    {
      Name = name;
      Address = address;
      Id = id;
    }
    public void Update(string newName, string newAddress)
    {
      MySqlConnection conn = DB.Connection();
      conn.Open();
      var cmd = conn.CreateCommand() as MySqlCommand;
      cmd.CommandText = @"UPDATE venues SET name = @newName, address = @newaddress WHERE id = @searchId;";

      MySqlParameter searchId = new MySqlParameter();
      searchId.ParameterName = "@searchId";
      searchId.Value = Id;
      cmd.Parameters.Add(searchId);

      MySqlParameter name = new MySqlParameter();
      name.ParameterName = "@newName";
      name.Value = newName;
      cmd.Parameters.Add(name);

      MySqlParameter address = new MySqlParameter();
      address.ParameterName = "@newAddress";
      address.Value = newAddress;
      cmd.Parameters.Add(address);

      cmd.ExecuteNonQuery();
      Name = newName;
      Address = newAddress;

      conn.Close();
      if (conn != null)
      {
          conn.Dispose();
      }
    }

    public void Delete()
    {
      MySqlConnection conn = DB.Connection();
      conn.Open();

      MySqlCommand cmd = new MySqlCommand("DELETE FROM venues WHERE id = @VenueId; DELETE FROM bands_venues WHERE venue_id = @VenueId;", conn);
      MySqlParameter venueId = new MySqlParameter();
      venueId.ParameterName = "@VenueId";
      venueId.Value = this.Id;
      cmd.Parameters.Add(venueId);

      cmd.ExecuteNonQuery();

      conn.Close();
      if (conn != null)
      {
        conn.Dispose();
      }
    }

    public static Venue Find(int searchId)
    {
      MySqlConnection conn = DB.Connection();
      conn.Open();

      var cmd = conn.CreateCommand() as MySqlCommand;
      cmd.CommandText =  @"SELECT * FROM venues WHERE id = @thisId;";

      MySqlParameter thisId = new MySqlParameter();
      thisId.ParameterName = "@thisId";
      thisId.Value = searchId;
      cmd.Parameters.Add(thisId);

      int venueId = 0;
      string venueName = "";
      string venueAddress = "";

      var rdr = cmd.ExecuteReader() as MySqlDataReader;
      while(rdr.Read())
      {
        venueId = rdr.GetInt32(0);
        venueName = rdr.GetString(1);
        venueAddress = rdr.GetString(2);
      }

      Venue foundVenue = new Venue(venueName, venueAddress, venueId);

      conn.Close();
      if (conn != null)
      {
          conn.Dispose();
      }
      return foundVenue;
    }

    public void Save()
    {
      MySqlConnection conn = DB.Connection();
      conn.Open();

      var cmd = conn.CreateCommand() as MySqlCommand;
      cmd.CommandText = @"INSERT INTO venues (name, address) VALUES (@name, @address);";

      MySqlParameter name = new MySqlParameter();
      name.ParameterName = "@name";
      name.Value = Name;
      cmd.Parameters.Add(name);

      MySqlParameter address = new MySqlParameter();
      address.ParameterName = "@address";
      address.Value = Address;
      cmd.Parameters.Add(address);

      cmd.ExecuteNonQuery();
      Id = (int) cmd.LastInsertedId;

      conn.Close();
      if (conn != null)
      {
          conn.Dispose();
      }
    }

    public static void ClearAll()
    {
      MySqlConnection conn = DB.Connection();
      conn.Open();

      var cmd = conn.CreateCommand() as MySqlCommand;
      cmd.CommandText = @"DELETE FROM venues;";
      cmd.ExecuteNonQuery();

      conn.Close();
      if (conn != null)
      {
        conn.Dispose();
      }
    }

    public override bool Equals(System.Object otherVenue)
    {
      if (!(otherVenue is Venue))
      {
          return false;
      }
      else
      {
        Venue newVenue = (Venue) otherVenue;
        bool idEquality = (this.Id == newVenue.Id);
        bool nameEquality = (this.Name == newVenue.Name);
        bool addressEquality = (this.Address == newVenue.Address);
        return (idEquality && nameEquality && addressEquality);
      }
    }

    public override int GetHashCode()
    {
      return this.Id.GetHashCode();
    }

    public static List<Venue> GetAll()
    {
      List<Venue> allVenues = new List<Venue> {};
      MySqlConnection conn = DB.Connection();
      conn.Open();
      MySqlCommand cmd = conn.CreateCommand() as MySqlCommand;
      cmd.CommandText = @"SELECT * FROM venues;";
      MySqlDataReader rdr = cmd.ExecuteReader() as MySqlDataReader;
      while (rdr.Read())
      {
        int venueId = rdr.GetInt32(0);
        string venueName = rdr.GetString(1);
        string venueAddress = rdr.GetString(2);
        Venue newVenue = new Venue(venueName, venueAddress, venueId);
        allVenues.Add(newVenue);
      }
      conn.Close();
      if (conn != null)
      {
        conn.Dispose();
      }
      return allVenues;
    }

    public List<Band> GetBands()
    {
      MySqlConnection conn = DB.Connection();
      conn.Open();
      var cmd = conn.CreateCommand() as MySqlCommand;
      cmd.CommandText = @"SELECT bands.* FROM venues JOIN bands_venues ON (venues.id = bands_venues.venue_id) JOIN bands ON (bands_venues.band_id = bands.id) WHERE venues.id = @VenueId;";

      MySqlParameter venueId = new MySqlParameter();
      venueId.ParameterName = "@VenueId";
      venueId.Value = Id;
      cmd.Parameters.Add(venueId);

      var rdr = cmd.ExecuteReader() as MySqlDataReader;
      List<Band> bands = new List<Band>{};

      while(rdr.Read())
      {
        int bandId = rdr.GetInt32(0);
        string bandName = rdr.GetString(1);
        string bandGenre = rdr.GetString(2);
        Band newBand = new Band(bandName, bandGenre, bandId);
        bands.Add(newBand);
      }
      conn.Close();
      if (conn != null)
      {
          conn.Dispose();
      }
      return bands;
    }

    public void AddBand(Band newBand)
    {
      MySqlConnection conn = DB.Connection();
      conn.Open();
      var cmd = conn.CreateCommand() as MySqlCommand;
      cmd.CommandText = @"INSERT INTO bands_venues (venue_id, band_id) VALUES (@VenueId, @BandId);";

      MySqlParameter venue_id = new MySqlParameter();
      venue_id.ParameterName = "@VenueId";
      venue_id.Value = Id;
      cmd.Parameters.Add(venue_id);

      MySqlParameter band_id = new MySqlParameter();
      band_id.ParameterName = "@BandId";
      band_id.Value = newBand.Id;
      cmd.Parameters.Add(band_id);

      cmd.ExecuteNonQuery();
      conn.Close();
      if (conn != null)
      {
          conn.Dispose();
      }
    }

    public void ClearBands()
    {
      MySqlConnection conn = DB.Connection();
      conn.Open();

      var cmd = conn.CreateCommand() as MySqlCommand;
      cmd.CommandText = @"DELETE FROM bands WHERE id = @VenueId;";

      MySqlParameter venueId = new MySqlParameter();
      venueId.ParameterName = "@VenueId";
      venueId.Value = Id;
      cmd.Parameters.Add(venueId);

      cmd.ExecuteNonQuery();

      conn.Close();
      if (conn != null)
      {
          conn.Dispose();
      }
    }
  }
}
