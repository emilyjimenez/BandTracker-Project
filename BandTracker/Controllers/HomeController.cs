using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using BandTracker.Models;
using System;

namespace BandTracker.Controllers
{
  public class HomeController : Controller
  {
    [HttpGet("/")]
    public ActionResult Index()
    {
      List<Band> allBands = Band.GetAll();
      return View(allBands);
    }

    [HttpGet("/venues")]
    public ActionResult Venues()
    {
      List<Venue> allVenues = Venue.GetAll();
      return View(allVenues);
    }

    [HttpGet("/bands")]
    public ActionResult Bands()
    {
      List<Band> allbands = Band.GetAll();
      return View(allbands);
    }

    [HttpGet("/venues/{id}/bands/new")]
    public ActionResult BandForm()
    {
      return View();
    }

    [HttpPost("/bands/new")]
    public ActionResult AddBand(int id)
    {
      Venue Venue = Venue.Find(id);
      Band newBand = new Band(Request.Form["band-name"], Request.Form["band-genre"]);
      newBand.Save();
      return View("Success");
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
