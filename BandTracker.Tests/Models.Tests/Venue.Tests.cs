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
    public void GetAll_VenuesEmptyAtFirst_0()
    {
      int result = Category.GetAll().Count;

      Assert.AreEqual(0, result);
    }

    }
  }
}
