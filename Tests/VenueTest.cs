using Xunit;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Collections.Generic;

namespace BandTracker
{
  public class VenueTest : IDisposable
  {
    public VenueTest()
    {
      DBConfiguration.ConnectionString = "Data Source=(localdb)\\mssqllocaldb;Initial Catalog=band_tracker_test;Integrated Security=SSPI;";
    }
    [Fact]
    public void Test_DatabaseEmptyAtFirst()
    {
      //Arrange, Act
      int result = Venue.GetAll().Count;

      //Assert
      Assert.Equal(0, result);
    }
    [Fact]
    public void Test_Equal_ReturnTrueIfNamesAreTheSame()
    {
      //Arrange
      Venue firstVenue = new Venue("Cool Haps");
      Venue otherFirstVenue = new Venue("Cool Haps");

      //Act Assert
      Assert.Equal(firstVenue, otherFirstVenue);
    }
    [Fact]
    public void Test_Save_SavesToDatabase()
    {
      //Arrange
      Venue newVenue = new Venue("Cool Haps");
      List<Venue> expectedResult = new List<Venue> {newVenue};

      //Act
      newVenue.Save();
      List<Venue> savedVenues = Venue.GetAll();

      //Assert
      Assert.Equal(expectedResult, savedVenues);
    }
    [Fact]
    public void Test_Find_FindsVenueInDatabase()
    {
      //Arrange
      Venue testVenue = new Venue("Cool Haps");
      testVenue.Save();

      //Act
      Venue foundVenue = Venue.Find(testVenue.GetId());

      //Assert
      Assert.Equal(testVenue, foundVenue);
    }
    [Fact]
    public void Test_Delete_DeletesASingleVenueById()
    {
      //Arrange
      Venue firstVenue = new Venue("Cool Haps");
      Venue secondVenue = new Venue("Sweet Spots");
      firstVenue.Save();
      secondVenue.Save();
      List<Venue> expectedResult = new List<Venue> {firstVenue};

      //Act
      secondVenue.Delete();
      List<Venue> result = Venue.GetAll();

      //Assert
      Assert.Equal(expectedResult, result);
    }
    [Fact]
    public void Test_Update_UpdatesNameInDatabase()
    {
      //Arrange
      Venue firstVenue = new Venue("Cool Haps");
      firstVenue.Save();

      //Act
      firstVenue.Update("Sweet Spots");
      Venue resultVenue = Venue.Find(firstVenue.GetId());

      //Assert
      Assert.Equal("Sweet Spots", firstVenue.GetVenueName());
      Assert.Equal("Sweet Spots", resultVenue.GetVenueName());
    }
    [Fact]
    public void Test_AddBandGetBand_AddsAndRetrievesBandsWithVenuesFromDatabase()
    {
      //Arrange
      Venue firstVenue = new Venue("Cool Haps");
      firstVenue.Save();
      Band firstBand = new Band("Super Serpentine");
      firstBand.Save();
      List<Band> expectedResult = new List<Band> {firstBand};

      //Act
      firstVenue.AddBand(firstBand.GetId());
      List<Band> result = firstVenue.GetBands();

      //Assert
      Assert.Equal(expectedResult, result);
    }


    public void Dispose()
    {
      Band.DeleteAll();
      Venue.DeleteAll();
    }
  }
}
