using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using BandTracker.Models;

namespace BandTracker.Controllers
{
  public class HomeController : Controller
  {
    [HttpGet("/")]
    public ActionResult Index()
    {
      return View();
    }

    [HttpGet("/venues")]
    public ActionResult Venues()
    {
      List<Venue> allVenues = Venue.GetAll();
      return View(allVenues);
    }

    [HttpGet("/bands/{id}")]
    public ActionResult BandDetail(int id)
    {
      Dictionary<string, object> model = new Dictionary<string, object>();
      Band selectedBand = Band.Find(id);
      List<Venue> bandVenues = selectedBand.GetVenues();
      List<Venue> allVenues = Venue.GetAll();
      model.Add("band", selectedBand);
      model.Add("bandVenues", bandVenues);
      model.Add("allVenues", allVenues);
      return View(model);
    }

    [HttpGet("/venues/{id}")]
    public ActionResult VenueDetail(int id)
    {
      Dictionary<string, object> model = new Dictionary<string, object>();
      Venue selectedVenue = Venue.Find(id);
      List<Band> venueBands = selectedVenue.GetBands();
      List<Band> allBands = Band.GetAll();
      model.Add("venue", selectedVenue);
      model.Add("venueBands", venueBands);
      model.Add("allBands", allBands);
      return View(model);
    }

    [HttpGet("/venues/new")]
    public ActionResult VenueForm()
    {
      return View();
    }

    [HttpPost("/venues/new")]
    public ActionResult AddVenue()
    {
      Venue newVenue = new Venue(Request.Form["venue-name"], Request.Form["venue-address"]);
      newVenue.Save();
      return View("Success");
    }
  }
}
