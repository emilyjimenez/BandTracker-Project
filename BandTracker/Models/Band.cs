using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using MySql.Data.MySqlClient;

namespace BandTracker.Models
{
  public class Band
  {
    public string Name {get; private set;}
    public string Genre {get; private set;}
    public int Id {get; private set;}

    public Band(string name, string genre, int id = 0)
    {
      Name = name;
      Genre = genre;
      Id = id;
    }

    public static void ClearAll()
    {
      MySqlConnection conn = DB.Connection();
      conn.Open();

      var cmd = conn.CreateCommand() as MySqlCommand;
      cmd.CommandText = @"DELETE FROM bands;";
      cmd.ExecuteNonQuery();

      conn.Close();
      if (conn != null)
      {
        conn.Dispose();
      }
    }

    public void Save()
    {
      MySqlConnection conn = DB.Connection();
      conn.Open();

      var cmd = conn.CreateCommand() as MySqlCommand;
      cmd.CommandText = @"INSERT INTO bands (name, genre) VALUES (@BandName, @BandGenre);";

      MySqlParameter name = new MySqlParameter();
      name.ParameterName = "@BandName";
      name.Value = this.Name;
      cmd.Parameters.Add(name);

      MySqlParameter genre = new MySqlParameter();
      genre.ParameterName = "@BandGenre";
      genre.Value = this.Genre;
      cmd.Parameters.Add(genre);

      cmd.ExecuteNonQuery();
      Id = (int)cmd.LastInsertedId;

      conn.Close();
      if (conn != null)
      {
          conn.Dispose();
      }
    }

    public override bool Equals(System.Object otherBand)
    {
      if (!(otherBand is Band))
      {
          return false;
      }
      else
      {
        Band newBand = (Band) otherBand;
        bool idEquality = (this.Id == newBand.Id);
        bool nameEquality = (this.Name == newBand.Name);
        bool genreEquality = (this.Genre == newBand.Genre);
        return (idEquality && nameEquality && genreEquality);
      }
    }

    public override int GetHashCode()
    {
      return this.Id.GetHashCode();
    }

    public static List<Band> GetAll()
    {
      List<Band> allBands = new List<Band> {};
      MySqlConnection conn = DB.Connection();
      conn.Open();
      MySqlCommand cmd = conn.CreateCommand() as MySqlCommand;
      cmd.CommandText = @"SELECT * FROM bands;";
      MySqlDataReader rdr = cmd.ExecuteReader() as MySqlDataReader;
      while (rdr.Read())
      {
        int bandId = rdr.GetInt32(0);
        string bandName = rdr.GetString(1);
        string bandGenre = rdr.GetString(2);
        Band newBand = new Band(bandName, bandGenre, bandId);
        allBands.Add(newBand);
      }
      conn.Close();
      if (conn != null)
      {
        conn.Dispose();
      }
      return allBands;
    }

    public List<Venue> GetVenues()
    {
      MySqlConnection conn = DB.Connection();
      conn.Open();
      var cmd = conn.CreateCommand() as MySqlCommand;
      cmd.CommandText = @"SELECT venues.* FROM bands JOIN bands_venues ON (bands.id = bands_venues.band_id) JOIN venues ON (bands_venues.venue_id = venues.id) WHERE bands.id = @BandId;";

      MySqlParameter bandId = new MySqlParameter();
      bandId.ParameterName = "@BandId";
      bandId.Value = Id;
      cmd.Parameters.Add(bandId);

      var rdr = cmd.ExecuteReader() as MySqlDataReader;
      List<Venue> venues = new List<Venue>{};

      while(rdr.Read())
      {
        int venueId = rdr.GetInt32(0);
        string venueName = rdr.GetString(1);
        string venueAddress = rdr.GetString(2);
        Venue newVenue = new Venue(venueName, venueAddress, venueId);
        venues.Add(newVenue);
      }
      conn.Close();
      if (conn != null)
      {
          conn.Dispose();
      }
      return venues;
    }
  }
}
