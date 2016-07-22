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
      Get["/bands"] = _ => {
        List<Band> allBands = Band.GetAll();
        return View["bands.cshtml", allBands];
      };
      Post["/bands"] = _ => {
        Band newBand = new Band(Request.Form["band-name"]);
        newBand.Save();
        List<Band> allBands = Band.GetAll();
        return View["bands.cshtml", allBands];
      };
      Get["/bands/{id}"] = parameters => {
        Dictionary<string, object> model = new Dictionary<string, object>{};
        Band foundBand = Band.Find(parameters.id);
        model.Add("band", foundBand);
        List<Venue> bandVenues = foundBand.GetVenues();
        model.Add("bandVenues", bandVenues);
        return View["band.cshtml", model];
      };
      Get["/venues"] = _ => {
        List<Venue> allVenues = Venue.GetAll();
        return View["venues.cshtml", allVenues];
      };
      Post["/venues"] = _ => {
        Venue newVenue = new Venue(Request.Form["venue-name"]);
        newVenue.Save();
        List<Venue> allVenues = Venue.GetAll();
        return View["venues.cshtml", allVenues];
      };
      Delete["/venues"] = _ => {
        Venue.DeleteAll();
        List<Venue> allVenues = Venue.GetAll();
        return View["venues.cshtml", allVenues];
      };
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
        foundVenue.AddBand(Request.Form["band-id"]);
        model.Add("venue", foundVenue);
        List<Band> venueBands = foundVenue.GetBands();
        model.Add("venueBands", venueBands);
        List<Band> allBands = Band.GetAll();
        model.Add("allBands", allBands);
        return View["venue.cshtml", model];
      };
      Patch["/venues/{id}"] = parameters => {
        Dictionary<string, object> model = new Dictionary<string, object>{};
        Venue foundVenue = Venue.Find(parameters.id);
        foundVenue.Update(Request.Form["venue-name"]);
        model.Add("venue", foundVenue);
        List<Band> venueBands = foundVenue.GetBands();
        model.Add("venueBands", venueBands);
        List<Band> allBands = Band.GetAll();
        model.Add("allBands", allBands);
        return View["venue.cshtml", model];
      };
      Delete["/venues/{id}"] = parameters => {
        Venue foundVenue = Venue.Find(parameters.id);
        foundVenue.Delete();
        List<Venue> allVenues = Venue.GetAll();
        return View["venues.cshtml", allVenues];
      };
    }
  }
}
