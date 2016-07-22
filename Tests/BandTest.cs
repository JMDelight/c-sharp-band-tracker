using Xunit;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Collections.Generic;

namespace BandTracker
{
  public class BandTest : IDisposable
  {
    public BandTest()
    {
      DBConfiguration.ConnectionString = "Data Source=(localdb)\\mssqllocaldb;Initial Catalog=band_tracker_test;Integrated Security=SSPI;";
    }
    [Fact]
    public void Test_DatabaseEmptyAtFirst()
    {
      //Arrange, Act
      int result = Band.GetAll().Count;

      //Assert
      Assert.Equal(0, result);
    }
    [Fact]
    public void Test_Equal_ReturnTrueIfNamesAreTheSame()
    {
      //Arrange
      Band firstBand = new Band("Super Serpentine");
      Band otherFirstBand = new Band("Super Serpentine");

      //Act Assert
      Assert.Equal(firstBand, otherFirstBand);
    }
    [Fact]
    public void Test_Save_SavesToDatabase()
    {
      //Arrange
      Band newBand = new Band("Super Serpentine");
      List<Band> expectedResult = new List<Band> {newBand};

      //Act
      newBand.Save();
      List<Band> savedBands = Band.GetAll();

      //Assert
      Assert.Equal(expectedResult, savedBands);
    }
    [Fact]
    public void Test_Find_FindsBandInDatabase()
    {
      //Arrange
      Band testBand = new Band("Super Serpentine");
      testBand.Save();

      //Act
      Band foundBand = Band.Find(testBand.GetId());

      //Assert
      Assert.Equal(testBand, foundBand);
    }
    [Fact]
    public void Test_Delete_DeletesASingleBandById()
    {
      //Arrange
      Band firstBand = new Band("Super Serpentine");
      Band secondBand = new Band("Sweet Sounds");
      firstBand.Save();
      secondBand.Save();
      List<Band> expectedResult = new List<Band> {firstBand};

      //Act
      secondBand.Delete();
      List<Band> result = Band.GetAll();

      //Assert
      Assert.Equal(expectedResult, result);
    }
    [Fact]
    public void Test_Update_UpdatesNameInDatabase()
    {
      //Arrange
      Band firstBand = new Band("Super Serpentine");
      firstBand.Save();

      //Act
      firstBand.Update("Sweet Sounds");
      Band resultBand = Band.Find(firstBand.GetId());

      //Assert
      Assert.Equal("Sweet Sounds", firstBand.GetBandName());
      Assert.Equal("Sweet Sounds", resultBand.GetBandName());
    }
    [Fact]
    public void Test_GetVenues_RetrievesVenuesForBandFromDatabase()
    {
      //Arrange
      Venue firstVenue = new Venue("Cool Haps");
      firstVenue.Save();
      Band firstBand = new Band("Super Serpentine");
      firstBand.Save();
      List<Venue> expectedResult = new List<Venue> {firstVenue};

      //Act
      firstVenue.AddBand(firstBand.GetId());
      List<Venue> result = firstBand.GetVenues();

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
