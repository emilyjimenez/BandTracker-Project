using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using BandTracker.Models;

namespace BandTracker.Models.Tests
{
  [TestClass]
  public class BandTests : IDisposable
  {
    public BandTests()
    {
      DBConfiguration.ConnectionString = "server=localhost;user id=root;password=root;port=8889;database=band_tracker_test;";
    }

    public void Dispose()
    {
      Venue.ClearAll();
      Band.ClearAll();
    }

    [TestMethod]
    public void Save_SavesToDatabase_BandList()
    {
      // Arrange
      Band testBand = new Band("Blink-182", "punkrock");

      // Act
      testBand.Save();
      List<Band> result = Band.GetAll();
      List<Band> testList = new List<Band>{testBand};

      // Assert
      CollectionAssert.AreEqual(testList, result);
    }

    [TestMethod]
    public void GetAll_BEmptyAtFirst_0()
    {
      int result = Band.GetAll().Count;

      Assert.AreEqual(0, result);
    }
  }
}
