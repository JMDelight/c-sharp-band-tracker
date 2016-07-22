using System.Collections.Generic;
using System;
using Nancy;
using Library;

namespace BandTracker
{
  public class HomeModule : NancyModule
  {
    public HomeModule()
    {
      Get["/"] = _ => View["index.cshtml"];
      Get["/venues"] = _ => {
        List<Venue> allVenues = Venue.GetAll();
        return View["venues.cshtml", allVenues];
      };
      Post["/venues"] = _ => {
        Venue newVenue = new Venue(Request.Form["venue-name"]);
        newVenue.Save();
        List<Venue> allVenues = Venue.GetAll();
        return View["venues.cshtml", allVenues];
      }
      Get["/venues/{id}"] = parameters => {
        Dictionary<string, object> model = new Dictionary<string, object>{};
        Venue foundVenue = Venue.Find(parameters.id);
        model.Add("venue", foundVenue);
        List<Band> venueBands = foundVenue.GetBands();
        model.Add("venueBands", venueBands);
        List<Band> allBands = Band.GetAll();
        model.Add("allBands", allBands);
        return View["venue.cshtml", model];
      };
      Post["/venues/{id}"] = parameters => {
        Dictionary<string, object> model = new Dictionary<string, object>{};
        Venue foundVenue = Venue.Find(parameters.id);
        foundVenue.AddBand(Request.Form[""])
        model.Add("venue", foundVenue);
        List<Band> venueBands = foundVenue.GetBands();
        model.Add("venueBands", venueBands);
        List<Band> allBands = Band.GetAll();
        model.Add("allBands", allBands);
        return View["venue.cshtml", model];
    };
  }
}
