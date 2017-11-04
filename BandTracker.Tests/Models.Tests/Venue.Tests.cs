using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using BandTracker.Models;

namespace BandTracker.Models.Tests
{
  [TestClass]
  public class VenueTests : IDisposable
  {
    public VenueTests()
    {
      DBConfiguration.ConnectionString = "server=localhost;user id=root;password=root;port=8889;database=band_tracker_test;";
    }
    public void Dispose()
    {
      Venue.ClearAll();
      Band.ClearAll();
    }

    [TestMethod]
    public void Update_UpdateNameAddressinVenue_Venue()
    {
      Venue testVenue = new Venue("MeowMeow", "Portland, OR");
      testVenue.Save();

      string newName = "Different thing";
      string newAddress = "Different otherthing";

      Venue newVenue = new Venue(newName, newAddress);
      testVenue.Update(newName, newAddress);

      Assert.AreEqual(newVenue.Name, testVenue.Name);
      Assert.AreEqual(newVenue.Address, testVenue.Address);
    }

    [TestMethod]
    public void Delete_DeletesVenueAssociationsFromDatabase_VenueList()
    {
      //Arrange
      Band testBand = new Band("Blink-182", "punk rock");
      testBand.Save();

      string testName = "CBGB";
      string testAddress = "315 Bowery, New York, NY 10003";
      Venue testVenue = new Venue(testName, testAddress);
      testVenue.Save();

      //Act
      testVenue.AddBand(testBand);
      testVenue.Delete();

      List<Venue> resultBandVenues = testBand.GetVenues();
      List<Venue> testBandVenues = new List<Venue> {};

      //Assert
      CollectionAssert.AreEqual(testBandVenues, resultBandVenues);
    }

    [TestMethod]
    public void Find_FindsVenueInDatabase_Venue()
    {
      Venue testVenue = new Venue("MeowMeow", "Portland, OR");
      testVenue.Save();

      Venue foundVenue = Venue.Find(testVenue.Id);

      Assert.AreEqual(testVenue, foundVenue);
    }

    [TestMethod]
    public void Save_SavesVenueToDatabase_VenueList()
    {
      Venue testVenue = new Venue("MeowMeow", "Portland, OR");
      testVenue.Save();

      List<Venue> result = Venue.GetAll();
      List<Venue> testList = new List<Venue>{testVenue};

      CollectionAssert.AreEqual(testList, result);
    }

    [TestMethod]
    public void Equals_ReturnsTrueForSameNameAddress_Venue()
    {
      Venue firstVenue = new Venue("MeowMeow", "Portland, OR");
      Venue secondVenue = new Venue("MeowMeow", "Portland, OR");

      Assert.AreEqual(firstVenue, secondVenue);
    }

    [TestMethod]
    public void GetAll_VenuesEmptyAtFirst_0()
    {
      int result = Venue.GetAll().Count;

      Assert.AreEqual(0, result);
    }

    [TestMethod]
    public void GetBands_ReturnsAllVenueBands_BandList()
    {
      //Arrange
      Venue testVenue = new Venue("MeowMeow", "Portland, OR");
      testVenue.Save();

      Band testBand = new Band("Blink-182", "punk rock");
      testBand.Save();

      Band testBand2 = new Band("Thursday", "screamo");
      testBand2.Save();

      //Act
      testVenue.AddBand(testBand);
      List<Band> savedBands = testVenue.GetBands();
      List<Band> testList = new List<Band> {testBand};

      //Assert
      CollectionAssert.AreEqual(testList, savedBands);
    }

    [TestMethod]
    public void AddBand_AddsBandToVenue_VenueList()
    {
      //Arrange
      Venue testVenue = new Venue("MeowMeow", "Portland, OR");
      testVenue.Save();

      Band testBand = new Band("Blink-182", "punk rock");
      testBand.Save();

      Band testBand2 = new Band("Thursday", "screamo");
      testBand2.Save();

      //Act
      testVenue.AddBand(testBand);
      testVenue.AddBand(testBand2);

      List<Band> result = testVenue.GetBands();
      List<Band> testList = new List<Band>{testBand, testBand2};

      //Assert
      CollectionAssert.AreEqual(testList, result);
    }

    [TestMethod]
    public void ClearBands_ClearBandsToVenue_VenueList()
    {
      Venue testVenue = new Venue("MeowMeow", "Portland, OR");
      testVenue.Save();

      Band testBand = new Band("Blink-182", "punk rock");
      testBand.Save();

      Band testBand2 = new Band("Thursday", "screamo");
      testBand2.Save();

      testVenue.AddBand(testBand);
      testVenue.AddBand(testBand2);

      List<Band> result = testVenue.GetBands();
      List<Band> testList = new List<Band>{testBand, testBand2};
      testVenue.ClearBands();

      CollectionAssert.AreEqual(testList, result);
    }
  }
}
