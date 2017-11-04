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

    [HttpGet("/categories/{id}")]
    public ActionResult VenueDetail(int id)
    {
      Dictionary<string, object> model = new Dictionary<string, object>();
      Venue selectedVenue = Venue.Find(id);
      List<Band> VenueBands = SelectedVenue.GetBands();
      List<Band> AllBands = Band.GetAll();
      model.Add("venue", selectedVenue);
      model.Add("venueBands", VenueBands);
      model.Add("allBands", AllTasks);
      return View(model);
    }
  }
}
